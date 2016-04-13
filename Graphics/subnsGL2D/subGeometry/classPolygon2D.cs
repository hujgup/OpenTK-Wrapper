using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics.GL2D {
	public class Polygon2D : PrimitiveShape2D {
		public Polygon2D(Color4 color,float lineWidth,IEnumerable<Vector2d> points) : base(color,lineWidth,points) {
		}
		public Polygon2D(Color4 color,float lineWidth,params Vector2d[] points): base(color,lineWidth,points) {
		}
		public Polygon2D(Color4 color,IEnumerable<Vector2d> points) : base(color,points) {
		}
		public Polygon2D(Color4 color,params Vector2d[] points) : base(color,points) {
		}
		public Polygon2D(float lineWidth,IEnumerable<Vector2d> points) : base(lineWidth,points) {
		}
		public Polygon2D(float lineWidth,params Vector2d[] points) : base(lineWidth,points) {
		}
		public Polygon2D(IEnumerable<Vector2d> points) : base(points) {
		}
		public Polygon2D(params Vector2d[] points) : base(points) {
		}
		public Polygon2D(LineLoop2D loop) : base(loop.Color,loop.LineWidth,loop.Vertices) {
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

