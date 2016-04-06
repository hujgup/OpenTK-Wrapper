using System;
using OpenTK;

namespace Graphics {
	public interface ICircle {
		Vector2d Center {
			get;
			set;
		}
		double Radius {
			get;
			set;
		}
	}
}

