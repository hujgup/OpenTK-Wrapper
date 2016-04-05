using System;
using OpenTK.Graphics;

namespace Graphics {
	public interface IPrimitiveShape {
		Color4 Color {
			get;
			set;
		}
		float LineWidth {
			get;
			set;
		}
	}
}

