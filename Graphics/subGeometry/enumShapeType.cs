using System;

namespace Graphics {
	/// <summary>
	/// Specifies a set of basic shape types.
	/// </summary>
	public enum ShapeType {
		/// <summary>
		/// A shape whose side lengths are all 0.
		/// </summary>
		Point = 1,
		/// <summary>
		/// A shape that consists of a number of shapes whose side lengths are all 0.
		/// </summary>
		PointSet = 2,
		/// <summary>
		/// A shape that consists of a number of lines.
		/// </summary>
		LineSet = 3,
		/// <summary>
		/// A shape that consists of a number of lines that enclose a region.
		/// </summary>
		Hollow = 4,
		/// <summary>
		/// A shape that consists of a number of lines that enclose a region, and the region itself.
		/// </summary>
		Solid = 5
	}
}

