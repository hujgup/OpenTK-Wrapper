using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics.GL2D {
	public class LineStrip2D : PrimitiveShape2D {
		public LineStrip2D(Color4 color,float lineWidth,IEnumerable<Vector2d> points) : base(color,lineWidth,points) {
		}
		public LineStrip2D(Color4 color,float lineWidth,params Vector2d[] points): base(color,lineWidth,points) {
		}
		public LineStrip2D(Color4 color,IEnumerable<Vector2d> points) : base(color,points) {
		}
		public LineStrip2D(Color4 color,params Vector2d[] points) : base(color,points) {
		}
		public LineStrip2D(float lineWidth,IEnumerable<Vector2d> points) : base(lineWidth,points) {
		}
		public LineStrip2D(float lineWidth,params Vector2d[] points) : base(lineWidth,points) {
		}
		public LineStrip2D(IEnumerable<Vector2d> points) : base(points) {
		}
		public LineStrip2D(params Vector2d[] points) : base(points) {
		}
		protected override PrimitiveType PrimType {
			get {
				return PrimitiveType.LineStrip;
			}
		}
		public override ShapeType ShapeType {
			get {
				return ShapeType.LineSet;
			}
		}
		public List<Vector2d> Points {
			get {
				return _points;
			}
		}
	}
}

