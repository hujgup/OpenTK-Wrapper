using System;
using OpenTK;

namespace Graphics {
	/// <summary>
	/// Represents a circle centered on a point.
	/// </summary>
	public interface ICircle<T> {
		/// <summary>
		/// Gets or sets the center of the circle.
		/// </summary>
		T Center {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the radius of the circle.
		/// </summary>
		double Radius {
			get;
			set;
		}
	}
}

