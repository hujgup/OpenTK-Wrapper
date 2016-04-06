using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics {
	public class Triangle : PrimitiveShape {
		public Triangle(Color4 color,float lineWidth,Vector2d point1,Vector2d point2,Vector2d point3) : base(color,lineWidth,Resolve(point1,point2,point3)) {
		}
		public Triangle(Color4 color,Vector2d point1,Vector2d point2,Vector2d point3) : base(color,Resolve(point1,point2,point3)) {
		}
		public Triangle(float lineWidth,Vector2d point1,Vector2d point2,Vector2d point3) : base(lineWidth,Resolve(point1,point2,point3)) {
		}
		public Triangle(Vector2d point1,Vector2d point2,Vector2d point3) : base(Resolve(point1,point2,point3)) {
		}
		protected override PrimitiveType PrimType {
			get {
				return PrimitiveType.Triangles;
			}
		}
		public override ShapeType ShapeType {
			get {
				return ShapeType.Solid;
			}
		}
		public Vector2d FirstPoint {
			get {
				return _points[0];
			}
			set {
				_points[0] = value;
			}
		}
		public Vector2d SecondPoint {
			get {
				return _points[1];
			}
			set {
				_points[1] = value;
			}
		}
		public Vector2d ThirdPoint {
			get {
				return _points[2];
			}
			set {
				_points[2] = value;
			}
		}
		private static Vector2d[] Resolve(Vector2d point1,Vector2d point2,Vector2d point3) {
			return new Vector2d[3] {
				point1,
				point2,
				point3
			};
		}
	}
}

