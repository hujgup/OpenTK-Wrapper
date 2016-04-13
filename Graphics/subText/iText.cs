using System;
using OpenTK.Graphics;

namespace Graphics {
	public interface IText<T> : IDrawable, IBounded, IColored {
		RenderingContext Context {
			get;
			set;
		}
		string TextContent {
			get;
			set;
		}
		TextOverflow Overflow {
			get;
			set;
		}
		T Font {
			get;
			set;
		}
		void Draw();
	}
}