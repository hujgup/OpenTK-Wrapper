using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics {
	public class LineLoop : PrimitiveShape {
		public LineLoop(Color4 color,float lineWidth,IEnumerable<Vector2d> points) : base(color,lineWidth,points) {
		}
		public LineLoop(Color4 color,float lineWidth,params Vector2d[] points): base(color,lineWidth,points) {
		}
		public LineLoop(Color4 color,IEnumerable<Vector2d> points) : base(color,points) {
		}
		public LineLoop(Color4 color,params Vector2d[] points) : base(color,points) {
		}
		public LineLoop(float lineWidth,IEnumerable<Vector2d> points) : base(lineWidth,points) {
		}
		public LineLoop(float lineWidth,params Vector2d[] points) : base(lineWidth,points) {
		}
		public LineLoop(IEnumerable<Vector2d> points) : base(points) {
		}
		public LineLoop(params Vector2d[] points) : base(points) {
		}
		protected override PrimitiveType PrimType {
			get {
				return PrimitiveType.LineLoop;
			}
		}
		public override ShapeType ShapeType {
			get {
				return ShapeType.Hollow;
			}
		}
		public List<Vector2d> Vertices {
			get {
				return _points;
			}
		}
	}
}

