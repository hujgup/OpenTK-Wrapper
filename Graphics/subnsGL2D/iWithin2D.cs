using System;
using OpenTK;

namespace Graphics.GL2D {
	/// <summary>
	/// Represents a two-dimensional element that has boundaries that objects can be inside.
	/// </summary>
	public interface IWithin2D : IWithin<Vector2d,IPrimitiveShape2D> {
		/// <summary>
		/// Checks whether this instance's content overlaps the content of an image.
		/// </summary>
		/// <returns><c>true</c>, if this instance's content overlaps the image's content, <c>false</c> otherwise.</returns>
		/// <param name="image">The image to check.</param>
		/// <param name="alphaThreshold">Pixels that have an alpha value below this threshold will not be considered when the overlap is calculated.</param>
		bool ContentCollides(Image2D image,byte alphaThreshold);
		/// <summary>
		/// Checks whether this instance's content overlaps the content of an image.
		/// </summary>
		/// <returns><c>true</c>, if this instance's content overlaps the image's content, <c>false</c> otherwise.</returns>
		/// <param name="image">The image to check.</param>
		bool ContentCollides(Image2D image);
		/// <summary>
		/// Checks whether this instance's content overlaps another instance's bounding boc.
		/// </summary>
		/// <returns><c>true</c> if this instance's content overlaps with the bounds, <c>false</c> otherwise</returns>
		/// <param name="box">The bounding box of another element.</param>
		bool ContentCollides(BoundingBox box);
	}
}

