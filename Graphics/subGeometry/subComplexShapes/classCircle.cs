using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenTK;
using OpenTK.Graphics;

namespace Graphics {
	public abstract class Circle : ICircle, IPrimitiveShape, IDrawable {
		private Vector2d _center;
		private double _radius;
		public Circle(Vector2d center,double radius) {
			_center = center;
			_radius = radius;
		}
		public Vector2d Center {
			get {
				return _center;
			}
			set {
				_center = value;
				Update();
			}
		}
		public double Radius {
			get {
				return _radius;
			}
			set {
				_radius = value;
				Update();
			}
		}
		public BoundingBox Bounds {
			get {
				Vector2d position = new Vector2d(Center.X - Radius,Center.Y - Radius);
				Vector2d extent = new Vector2d(Center.X + Radius,Center.Y + Radius);
				return new BoundingBox(position,extent,this);
			}
		}
		public abstract Color4 Color {
			get;
			set;
		}
		public abstract float LineWidth {
			get;
			set;
		}
		public abstract ShapeType ShapeType {
			get;
		}
		public abstract ReadOnlyCollection<Vector2d> Outline {
			get;
		}
		protected abstract void Update();
		public abstract List<Line> GetSides();
		public abstract void Draw(RenderingContext2D context);
		public abstract bool PointWithinBounds(Vector2d point);
		public abstract bool PointWithinContent(Vector2d point);
		public abstract bool BoundsCollide(BoundingBox box);
		public abstract bool BoundsCollide(IBounded shape);
		public abstract bool ContentCollides(IPrimitiveShape shape);
		public abstract bool ContentCollides(GLImage image,byte alphaThreshold);
		public abstract bool ContentCollides(GLImage image);
		public abstract bool ContentCollides(BoundingBox box);
	}
}

