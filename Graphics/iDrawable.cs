using System;

namespace Graphics {
	/// <summary>
	/// Represents an element that can be rendered to the screen.
	/// </summary>
	public interface IDrawable {
		/// <summary>
		/// Draw this instance to the screen.
		/// </summary>
		/// <param name="context">The rendering context to render under.</param>
		void Draw(RenderingContext context);
	}
}

