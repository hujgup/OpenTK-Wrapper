using System;
using OpenTK.Graphics;

namespace Graphics {
	/// <summary>
	/// Represents an element that has a color.
	/// </summary>
	public interface IColored {
		/// <summary>
		/// Gets or sets the color of this instance.
		/// </summary>
		Color4 Color {
			get;
			set;
		}
	}
}

