using System;
using System.Drawing;
using OpenTK;

namespace Graphics {
	public struct BoundingBox : IRectangle, IWithin, IEquatable<IRectangle> {
		public BoundingBox(Vector2d position,Vector2d extent) : this() {
			Position = position;
			Extent = extent;
		}
		public Vector2d Position {
			get;
			set;
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
			get;
			set;
		}
		public bool PointWithinBounds(Vector2d point) {
			return point.X >= Position.X && point.X <= Extent.X && point.Y >= Position.Y && point.Y <= Extent.Y;
		}
		public bool PointWithinContent(Vector2d point) {
			return PointWithinBounds(point);
		}
		public bool BoundsCollide(BoundingBox box) {
			return (Position.X <= box.Extent.X && Position.Y <= box.Extent.Y) || (box.Position.X <= Extent.X && box.Position.Y <= Extent.Y);
		}
		public bool BoundsCollide(IBounded shape) {
			return BoundsCollide(shape.Bounds);
		}
		public bool ContentCollides(IPrimitiveShape shape) {
			return shape.ContentCollides(this);
		}
		public bool ContentCollides(GLImage image,byte alphaThreshold) {
			return ContentCollides(image);
		}
		public bool ContentCollides(GLImage image) {
			return BoundsCollide(image.Bounds);
		}
		public bool ContentCollides(BoundingBox box) {
			return BoundsCollide(box);
		}
		public bool Equals(IRectangle rect) {
			return Position == rect.Position && Extent == rect.Extent;
		}
		public override int GetHashCode() {
			unchecked {
				return Position.GetHashCode() + Extent.GetHashCode();
			}
		}
		public override bool Equals(object obj) {
			return ((IRectangle)this).GetType().IsInstanceOfType(obj) ? Equals((IRectangle)obj) : false;
		}
		public static bool operator ==(BoundingBox a,BoundingBox b) {
			return a.Equals(b);
		}
		public static bool operator !=(BoundingBox a,BoundingBox b) {
			return !(a == b);
		}
		public static explicit operator Rectangle(BoundingBox box) {
			return new Rectangle((int)box.Position.X,(int)box.Position.Y,(int)box.Size.X,(int)box.Size.Y);
		}
	}
}

