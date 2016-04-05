using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics {
	public class LineStrip : PrimitiveShape {
		public LineStrip(Color4 color,float lineWidth,IEnumerable<Vector2d> points) : base(color,lineWidth,points) {
		}
		public LineStrip(Color4 color,float lineWidth,params Vector2d[] points): base(color,lineWidth,points) {
		}
		public LineStrip(Color4 color,IEnumerable<Vector2d> points) : base(color,points) {
		}
		public LineStrip(Color4 color,params Vector2d[] points) : base(color,points) {
		}
		public LineStrip(float lineWidth,IEnumerable<Vector2d> points) : base(lineWidth,points) {
		}
		public LineStrip(float lineWidth,params Vector2d[] points) : base(lineWidth,points) {
		}
		public LineStrip(IEnumerable<Vector2d> points) : base(points) {
		}
		public LineStrip(params Vector2d[] points) : base(points) {
		}
		protected override PrimitiveType PrimType {
			get {
				return PrimitiveType.LineStrip;
			}
		}
		public List<Vector2d> Points {
			get {
				return _points;
			}
		}
	}
}

