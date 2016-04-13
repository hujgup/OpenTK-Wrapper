using System;
using OpenTK;

namespace Graphics {
	/// <summary>
	/// Represents an element that has boundaries that objects can be inside.
	/// </summary>
	public interface IWithin<TPoint,TShape> {
		/// <summary>
		/// Checks whether a given point is within this instance's BoundingBox.
		/// </summary>
		/// <returns><c>true</c> if the point was within bounds, <c>false</c> otherwise.</returns>
		/// <param name="point">The point to check.</param>
		bool PointWithinBounds(TPoint point);
		/// <summary>
		/// Checks whether a given point is within this instance's rendered content.
		/// </summary>
		/// <returns><c>true</c> if the point was within the rendered content, <c>false</c> otherwise.</returns>
		/// <param name="point">The point to check.</param>
		bool PointWithinContent(TPoint point);
		/// <summary>
		/// Checks whether this instance's bounds overlap with another instance's bounds.
		/// </summary>
		/// <returns><c>true</c> the two bounds collide, <c>false</c> otherwise.</returns>
		/// <param name="box">The bounding box of another element.</param>
		bool BoundsCollide(BoundingBox box);
		/// <summary>
		/// Checks whether this instance's bounds overlap with another instance's bounds.
		/// </summary>
		/// <returns><c>true</c> the two bounds collide, <c>false</c> otherwise.</returns>
		/// <param name="shape">A shape that has bounds.</param>
		bool BoundsCollide(IBounded shape);
		/// <summary>
		/// Checks whether this instance's content overlaps another instance's content.
		/// </summary>
		/// <returns><c>true</c> if the contents collids, <c>false</c> otherwise.</returns>
		/// <param name="shape">A shape that has content.</param>
		bool ContentCollides(TShape shape);
	}
}

