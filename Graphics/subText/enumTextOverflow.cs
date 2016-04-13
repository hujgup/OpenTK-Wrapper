using System;

namespace Graphics {
	/// <summary>
	/// Describes what do do in the event that text exceeds the bounds of a box.
	/// </summary>
	public enum TextOverflow {
		/// <summary>
		/// Specifies that text should continue to be drawn on overflow.
		/// </summary>
		Overflow,
		/// <summary>
		/// Specified that text should not be drawn on overflow.
		/// </summary>
		Hide
	}
}

