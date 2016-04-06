using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics {
	public abstract class PrimitiveShape : IPrimitiveShape, IDrawable, ICloneable {
		public const float DefaultLineWidth = 1f;
		public static readonly Color4 DefaultColor = Color4.Black;
		protected List<Vector2d> _points;
		public PrimitiveShape(Color4 color,float lineWidth,IEnumerable<Vector2d> points) {
			Color = color;
			LineWidth = lineWidth;
			_points = new List<Vector2d>(points);
		}
		public PrimitiveShape(Color4 color,float lineWidth,params Vector2d[] points) : this(color,lineWidth,(IEnumerable<Vector2d>)points) {
		}
		public PrimitiveShape(Color4 color,IEnumerable<Vector2d> points) : this(color,DefaultLineWidth,points) {
		}
		public PrimitiveShape(Color4 color,params Vector2d[] points) : this(color,DefaultLineWidth,points) {
		}
		public PrimitiveShape(float lineWidth,IEnumerable<Vector2d> points) : this(DefaultColor,lineWidth,points) {
		}
		public PrimitiveShape(float lineWidth,params Vector2d[] points) : this(DefaultColor,lineWidth,points) {
		}
		public PrimitiveShape(IEnumerable<Vector2d> points) : this(DefaultColor,points) {
		}
		public PrimitiveShape(params Vector2d[] points) : this(DefaultColor,points) {
		}
		protected abstract PrimitiveType PrimType {
			get;
		}
		public abstract ShapeType ShapeType {
			get;
		}
		public virtual ReadOnlyCollection<Vector2d> Outline {
			get {
				return _points.AsReadOnly();
			}
		}
		public BoundingBox Bounds {
			get {
				Vector2d position = new Vector2d(-1,-1);
				Vector2d extent = new Vector2d(-1,-1);
				bool unset = true;
				foreach (Vector2d point in _points) {
					if (unset) {
						position.X = point.X;
						position.Y = point.Y;
						extent.X = point.X;
						extent.Y = point.Y;
						unset = false;
					} else {
						if (position.X > point.X) {
							position.X = point.X;
						}
						if (position.Y > point.Y) {
							position.Y = point.Y;
						}
						if (extent.X < point.X) {
							extent.X = point.X;
						}
						if (extent.Y < point.Y) {
							extent.Y = point.Y;
						}
					}
				}
				position.X -= LineWidth;
				position.Y -= LineWidth;
				extent.X += LineWidth;
				extent.Y += LineWidth;
				return new BoundingBox(position,extent,this);
			}
		}
		public Color4 Color {
			get;
			set;
		}
		public float LineWidth {
			get;
			set;
		}
		protected virtual bool AdvancedContentCollides(IPrimitiveShape shape) {
			bool res = false;
			List<Line> shapeLines = shape.GetSides();
			if (ShapeType <= ShapeType.PointSet) {
				foreach (Vector2d point in _points) {
					foreach (Line shapeLine in shapeLines) {
						res = point.Equals(shapeLine.Origin);
						if (res) {
							break;
						}
					}
					if (res) {
						break;
					}
				}
			} else { // LineSet, Hollow, Solid
				List<Line> currentLines = GetSides();
				foreach (Line currentLine in currentLines) {
					foreach (Line shapeLine in shapeLines) {
						res = currentLine.ContentCollides(shapeLine);
						if (res) {
							break;
						}
					}
					if (res) {
						break;
					}
				}
			}
			return res;
		}
		protected virtual bool AdvancedPointWithinContent(Vector2d point) {
			bool res = false;
			if (ShapeType <= ShapeType.PointSet) {
				foreach (Vector2d p in _points) {
					res = p.Equals(point);
					if (res) {
						break;
					}
				}
			} else {
				List<Line> sides = GetSides();
				if (ShapeType >= ShapeType.Solid) {
					// Raycast
					LinearEquasion ray = new LinearEquasion(0,point.Y);
					int intersects = 0;
					foreach (Line side in sides) {
						if (!double.IsNaN(side.GetLinearEquasion().Intersect(ray).X)) {
							intersects++;
						}
					}
					res = intersects%2 == 1;
				} else if (ShapeType >= ShapeType.LineSet) {
					foreach (Line side in sides) {
						res = side.GetLinearEquasion().PointOnLine(point,LineWidth);
						if (res) {
							break;
						}
					}
				}
			}
			return res;
		}
		public virtual List<Line> GetSides() {
			List<Line> sides;
			if (ShapeType <= ShapeType.PointSet) {
				sides = new List<Line>(0);
			} else {
				int max = _points.Count - 1;
				sides = new List<Line>(ShapeType >= ShapeType.Hollow ? _points.Count : max);
				for (int i = 0, iPlusOne = 1; i < max; i++, iPlusOne++) {
					sides.Add(new Line(_points[i],_points[iPlusOne]));
				}
				if (ShapeType >= ShapeType.Hollow && _points.Count > 1) {
					sides.Add(new Line(_points[max],_points[0]));
				}
			}
			return sides;
		}
		public void Draw(RenderingContext2D context) {
			context.Focus();
			GL.LineWidth(LineWidth);
			GL.Begin(PrimType);
			GL.Color4(Color);
			foreach (Vector2d point in _points) {
				GL.Vertex2(point);
			}
			GL.End();
		}
		public object Clone() {
			PrimitiveShape res = (PrimitiveShape)MemberwiseClone();
			res._points = new List<Vector2d>(_points);
			return res;
		}
		public bool PointWithinBounds(Vector2d point) {
			return Bounds.PointWithinBounds(point);
		}
		public bool PointWithinContent(Vector2d point) {
			bool res = PointWithinBounds(point);
			if (res) {
				res = AdvancedPointWithinContent(point);
			}
			return res;
		}
		public bool BoundsCollide(BoundingBox box) {
			return Bounds.BoundsCollide(box);
		}
		public bool BoundsCollide(IBounded shape) {
			return BoundsCollide(shape.Bounds);
		}
		public bool ContentCollides(IPrimitiveShape shape) {
			bool res = this == shape;
			if (!res) {
				res = AdvancedContentCollides(shape);
			}
			return res;
		}
		public bool ContentCollides(GLImage image,byte alphaThreshold) {
			bool res = BoundsCollide(image.Bounds);
			if (res) {
				if (ShapeType <= ShapeType.PointSet) {
					ReadOnlyCollection<Vector2d> outline = Outline;
					foreach (Vector2d point in outline) {
						if (image.PointWithinBounds(point)) {
							res = image.PointWithinContent(point,alphaThreshold);
							if (res) {
								break;
							}
						}
					}
				} else {
					Vector2d point;
					int x;
					for (int y = 0; y < image.Size.Y; y++) {
						for (x = 0; x < image.Size.X; x++) {
							point = Vector2d.Add(image.Position,new Vector2d((double)x,(double)y));
							if (image.PointWithinBounds(point) && image.PixelIsVisible(x,y,alphaThreshold)) {
								res = PointWithinContent(point);
								if (res) {
									x = (int)image.Size.X;
									y = (int)image.Size.Y;
								}
							}
						}
					}
				}				
			}
			return res;
		}
		public bool ContentCollides(GLImage image) {
			return ContentCollides(image,GLImage.DefaultAlphaThreshold);
		}
		public bool ContentCollides(BoundingBox box) {
			bool res = Bounds == box;
			if (!res) {
				PrimitiveShape shape = (PrimitiveShape)MemberwiseClone();
				shape._points.Clear();
				shape._points.Add(box.Position);
				shape._points.Add(new Vector2d(box.Extent.X,box.Position.Y));
				shape._points.Add(box.Extent);
				shape._points.Add(new Vector2d(box.Position.X,box.Extent.Y));
				res = ContentCollides(shape);
			}
			return res;
		}
	}
}

