using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics.GL2D {
	public class LineLoop2D : PrimitiveShape2D {
		public LineLoop2D(Color4 color,float lineWidth,IEnumerable<Vector2d> points) : base(color,lineWidth,points) {
		}
		public LineLoop2D(Color4 color,float lineWidth,params Vector2d[] points): base(color,lineWidth,points) {
		}
		public LineLoop2D(Color4 color,IEnumerable<Vector2d> points) : base(color,points) {
		}
		public LineLoop2D(Color4 color,params Vector2d[] points) : base(color,points) {
		}
		public LineLoop2D(float lineWidth,IEnumerable<Vector2d> points) : base(lineWidth,points) {
		}
		public LineLoop2D(float lineWidth,params Vector2d[] points) : base(lineWidth,points) {
		}
		public LineLoop2D(IEnumerable<Vector2d> points) : base(points) {
		}
		public LineLoop2D(params Vector2d[] points) : base(points) {
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

