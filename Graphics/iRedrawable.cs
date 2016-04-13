using System;

namespace Graphics {
	public interface IRedrawable {
		RenderingContext Context {
			get;
			set;
		}
		void Draw();
	}
}

