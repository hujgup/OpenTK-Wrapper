using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics {
	public class Polygon : PrimitiveShape {
		public Polygon(Color4 color,float lineWidth,IEnumerable<Vector2d> points) : base(color,lineWidth,points) {
		}
		public Polygon(Color4 color,float lineWidth,params Vector2d[] points): base(color,lineWidth,points) {
		}
		public Polygon(Color4 color,IEnumerable<Vector2d> points) : base(color,points) {
		}
		public Polygon(Color4 color,params Vector2d[] points) : base(color,points) {
		}
		public Polygon(float lineWidth,IEnumerable<Vector2d> points) : base(lineWidth,points) {
		}
		public Polygon(float lineWidth,params Vector2d[] points) : base(lineWidth,points) {
		}
		public Polygon(IEnumerable<Vector2d> points) : base(points) {
		}
		public Polygon(params Vector2d[] points) : base(points) {
		}
		public Polygon(LineLoop loop) : base(loop.Color,loop.LineWidth,loop.Vertices) {
		}
		protected override PrimitiveType PrimType {
			get {
				return PrimitiveType.Polygon;
			}
		}
		public override ShapeType ShapeType {
			get {
				return ShapeType.Solid;
			}
		}
		public List<Vector2d> Vertices {
			get {
				return _points;
			}
		}
	}
}

