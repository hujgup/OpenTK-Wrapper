using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenTK;
using OpenTK.Graphics;

namespace Graphics {
	public class SolidRectangle : IRectangle, IPrimitiveShape, IDrawable, IEquatable<IRectangle> {
		private Quadrilateral _quad;
		public SolidRectangle(Vector2d position,Vector2d size) {
			_quad = new Quadrilateral();
			Update(position,Vector2d.Add(position,size));
		}
		public Vector2d Position {
			get {
				return _quad.FirstPoint;
			}
			set {
				Update(value,Extent);
			}
		}
		public Vector2d Size {
			get {
				return Vector2d.Subtract(Extent,Position);
			}
			set {
				Extent = Vector2d.Add(Position,value);
			}
		}
		public Vector2d Extent {
			get {
				return _quad.ThirdPoint;
			}
			set {
				Update(Position,value);
			}
		}
		public BoundingBox Bounds {
			get {
				return new BoundingBox(Position,Extent);
			}
		}
		public Color4 Color {
			get {
				return _quad.Color;
			}
			set {
				_quad.Color = value;
			}
		}
		public float LineWidth {
			get {
				return _quad.LineWidth;
			}
			set {
				_quad.LineWidth = value;
			}
		}
		public ShapeType ShapeType {
			get {
				return ShapeType.LineSet;
			}
		}
		public ReadOnlyCollection<Vector2d> Outline {
			get {
				return _quad.Outline;
			}
		}
		private void Update(Vector2d position,Vector2d extent) {
			_quad.FirstPoint = position;
			_quad.SecondPoint = new Vector2d(extent.X,position.Y);
			_quad.ThirdPoint = extent;
			_quad.FourthPoint = new Vector2d(position.X,extent.Y);
		}
		public List<Line> GetSides() {
			return _quad.GetSides();
		}
		public void Draw(RenderingContext2D context) {
			_quad.Draw(context);
		}
		public bool PointWithinBounds(Vector2d point) {
			return _quad.PointWithinBounds(point);
		}
		public bool PointWithinContent(Vector2d point) {
			return _quad.PointWithinContent(point);
		}
		public bool BoundsCollide(BoundingBox box) {
			return _quad.BoundsCollide(box);
		}
		public bool BoundsCollide(IBounded shape) {
			return _quad.BoundsCollide(shape);
		}
		public bool ContentCollides(IPrimitiveShape shape) {
			return _quad.ContentCollides(shape);
		}
		public bool ContentCollides(GLImage image,byte alphaThreshold) {
			return image.ContentCollides(_quad,alphaThreshold);
		}
		public bool ContentCollides(GLImage image) {
			return ContentCollides(image,GLImage.DefaultAlphaThreshold);
		}
		public bool ContentCollides(BoundingBox box) {
			return _quad.ContentCollides(box);
		}
		public bool Equals(IRectangle other) {
			return Position == other.Position && Size == other.Size;
		}
		public override bool Equals(object other) {
			return ((IRectangle)this).GetType().IsInstanceOfType(other) ? Equals((IRectangle)other) : false;
		}
		public override int GetHashCode() {
			unchecked {
				return Position.GetHashCode() + Size.GetHashCode();
			}
		}
		public static bool operator ==(SolidRectangle a,SolidRectangle b) {
			return a.Equals(b);
		}
		public static bool operator !=(SolidRectangle a,SolidRectangle b) {
			return !(a == b);
		}
	}
}

