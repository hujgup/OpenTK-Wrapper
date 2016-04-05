using System;
using OpenTK;

namespace Graphics {
	public interface IRectangle {
		Vector2d Position {
			get;
			set;
		}
		Vector2d Size {
			get;
			set;
		}
		Vector2d Extent {
			get;
			set;
		}
	}
}

