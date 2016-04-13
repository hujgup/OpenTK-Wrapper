using System;
using System.Drawing;
using OpenTK;
using Graphics.GL2D;

namespace Graphics {
	/// <summary>
	/// Represents a box that surrounds a graphics element.
	/// </summary>
	public struct BoundingBox : IRectangle, IWithin2D, IEquatable<IRectangle> {
		/// <summary>
		/// Initializes a new instance of the <see cref="Graphics.BoundingBox"/> struct.
		/// </summary>
		/// <param name="position">The top-left position of the graphics element on the screen.</param>
		/// <param name="extent">The bottom-right position of the graphics element on the screen.</param>
		public BoundingBox(Vector2d position,Vector2d extent) : this() {
			Position = position;
			Extent = extent;
		}
		/// <summary>
		/// Gets or sets the top-left position.
		/// </summary>
		public Vector2d Position {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the size of the rectangle.
		/// </summary>
		public Vector2d Size {
			get {
				return Vector2d.Subtract(Extent,Position);
			}
			set {
				Extent = Vector2d.Add(Position,value);
			}
		}
		/// <summary>
		/// Gets or sets the bottom-right position.
		/// </summary>
		public Vector2d Extent {
			get;
			set;
		}
		/// <summary>
		/// Checks whether a given point is within this instance's BoundingBox.
		/// </summary>
		/// <returns><c>true</c> if the point was within bounds, <c>false</c> otherwise.</returns>
		/// <param name="point">The point to check.</param>
		public bool PointWithinBounds(Vector2d point) {
			return point.X >= Position.X && point.X <= Extent.X && point.Y >= Position.Y && point.Y <= Extent.Y;
		}
		/// <summary>
		/// Checks whether a given point is within this instance's rendered content.
		/// </summary>
		/// <returns><c>true</c> if the point was within the rendered content, <c>false</c> otherwise.</returns>
		/// <param name="point">The point to check.</param>
		public bool PointWithinContent(Vector2d point) {
			return PointWithinBounds(point);
		}
		/// <summary>
		/// Checks whether this instance's bounds overlap with another instance's bounds.
		/// </summary>
		/// <returns><c>true</c> the two bounds collide, <c>false</c> otherwise.</returns>
		/// <param name="box">The bounding box of another element.</param>
		public bool BoundsCollide(BoundingBox box) {
			return (Position.X <= box.Extent.X && Position.Y <= box.Extent.Y) || (box.Position.X <= Extent.X && box.Position.Y <= Extent.Y);
		}
		/// <summary>
		/// Checks whether this instance's bounds overlap with another instance's bounds.
		/// </summary>
		/// <returns><c>true</c> the two bounds collide, <c>false</c> otherwise.</returns>
		/// <param name="shape">A shape that has bounds.</param>
		public bool BoundsCollide(IBounded shape) {
			return BoundsCollide(shape.Bounds);
		}
		/// <summary>
		/// Checks whether this instance's content overlaps another instance's content.
		/// </summary>
		/// <returns><c>true</c> if the contents collids, <c>false</c> otherwise.</returns>
		/// <param name="shape">A shape that has content.</param>
		public bool ContentCollides(IPrimitiveShape2D shape) {
			return shape.ContentCollides(this);
		}
		/// <summary>
		/// Checks whether this instance's content overlaps the content of an image.
		/// </summary>
		/// <returns><c>true</c>, if this instance's content overlaps the image's content, <c>false</c> otherwise.</returns>
		/// <param name="image">The image to check.</param>
		/// <param name="alphaThreshold">Pixels that have an alpha value below this threshold will not be considered when the overlap is calculated.</param>
		public bool ContentCollides(Image2D image,byte alphaThreshold) {
			return ContentCollides(image);
		}
		/// <summary>
		/// Checks whether this instance's content overlaps the content of an image.
		/// </summary>
		/// <returns><c>true</c>, if this instance's content overlaps the image's content, <c>false</c> otherwise.</returns>
		/// <param name="image">The image to check.</param>
		public bool ContentCollides(Image2D image) {
			return BoundsCollide(image.Bounds);
		}
		/// <summary>
		/// Checks whether this instance's content overlaps another instance's bounding boc.
		/// </summary>
		/// <returns><c>true</c> if this instance's content overlaps with the bounds, <c>false</c> otherwise</returns>
		/// <param name="box">The bounding box of another element.</param>
		public bool ContentCollides(BoundingBox box) {
			return BoundsCollide(box);
		}
		/// <summary>
		/// Determines whether the specified <see cref="Graphics.IRectangle"/> is equal to the current <see cref="Graphics.BoundingBox"/>.
		/// </summary>
		/// <param name="rect">The <see cref="Graphics.IRectangle"/> to compare with the current <see cref="Graphics.BoundingBox"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="Graphics.IRectangle"/> is equal to the current
		/// <see cref="Graphics.BoundingBox"/>; otherwise, <c>false</c>.</returns>
		public bool Equals(IRectangle rect) {
			return Position == rect.Position && Extent == rect.Extent;
		}
		/// <summary>
		/// Serves as a hash function for a <see cref="Graphics.BoundingBox"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode() {
			unchecked {
				return Position.GetHashCode() + Extent.GetHashCode();
			}
		}
		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Graphics.BoundingBox"/>.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Graphics.BoundingBox"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
		/// <see cref="Graphics.BoundingBox"/>; otherwise, <c>false</c>.</returns>
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

