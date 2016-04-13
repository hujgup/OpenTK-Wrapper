using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenTK;
using OpenTK.Graphics;
using Graphics.GL2D;

namespace Graphics.GL2D {
	/// <summary>
	/// Represents a two-dimensional OpenGL primitive shape.
	/// </summary>
	public interface IPrimitiveShape2D : IWithin2D, IBounded, IColored {
		/// <summary>
		/// Gets or sets the width of the drawn lines.
		/// </summary>
		float LineWidth {
			get;
			set;
		}
		/// <summary>
		/// Gets the type of this shape.
		/// </summary>
		ShapeType ShapeType {
			get;
		}
		/// <summary>
		/// Gets a set of points defining the outline of this shape.
		/// </summary>
		ReadOnlyCollection<Vector2d> Outline {
			get;
		}
		/// <summary>
		/// Gets a set of lines defining the sides of this shape.
		/// </summary>
		List<Line2D> GetSides();
	}
}

