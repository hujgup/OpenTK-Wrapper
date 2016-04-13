using System;

namespace Graphics {
	/// <summary>
	/// Represents an element that has defined, finite bounds.
	/// </summary>
	public interface IBounded {
		/// <summary>
		/// Gets the boundary of this instance.
		/// </summary>
		BoundingBox Bounds {
			get;
		}
	}
}

