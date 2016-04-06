using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenTK;
using OpenTK.Graphics;

namespace Graphics {
	public interface IPrimitiveShape : IWithin, IBounded {
		Color4 Color {
			get;
			set;
		}
		float LineWidth {
			get;
			set;
		}
		ShapeType ShapeType {
			get;
		}
		ReadOnlyCollection<Vector2d> Outline {
			get;
		}
		List<Line> GetSides();
	}
}

