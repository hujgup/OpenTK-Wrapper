using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics {
	public class Point : PrimitiveShape {
		public Point(Color4 color,float lineWidth,Vector2d point) : base(color,lineWidth,point) {
		}
		public Point(Color4 color,Vector2d point) : base(color,point) {
		}
		public Point(float lineWidth,Vector2d point) : base(lineWidth,point) {
		}
		public Point(Vector2d point) : base(point) {
		}
		public static Vector2d[] Resolve(Vector2d point) {
			return new Vector2d[1] {
				point
			};
		}
		protected override PrimitiveType PrimType {
			get {
				return PrimitiveType.Points;
			}
		}
		public override ShapeType ShapeType {
			get {
				return ShapeType.Point;
			}
		}
		public Vector2d Vertex {
			get {
				return _points[0];
			}
			set {
				_points[0] = value;
			}
		}
	}
}

