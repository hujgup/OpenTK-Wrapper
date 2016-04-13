using System;
using OpenTK;

namespace Graphics {
	/// <summary>
	/// Represents a rectangle.
	/// </summary>
	public interface IRectangle {
		/// <summary>
		/// Gets or sets the top-left position.
		/// </summary>
		Vector2d Position {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the size of the rectangle.
		/// </summary>
		Vector2d Size {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the bottom-right position.
		/// </summary>
		Vector2d Extent {
			get;
			set;
		}
	}
}

