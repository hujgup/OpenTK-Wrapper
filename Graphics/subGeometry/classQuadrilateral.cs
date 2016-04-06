using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics {
	public class Quadrilateral : PrimitiveShape {
		private static readonly Vector2d _Empty = new Vector2d();
		public Quadrilateral(Color4 color,float lineWidth,Vector2d point1,Vector2d point2,Vector2d point3,Vector2d point4) : base(color,lineWidth,Resolve(point1,point2,point3,point4)) {
		}
		public Quadrilateral(Color4 color,Vector2d point1,Vector2d point2,Vector2d point3,Vector2d point4) : base(color,Resolve(point1,point2,point3,point4)) {
		}
		public Quadrilateral(float lineWidth,Vector2d point1,Vector2d point2,Vector2d point3,Vector2d point4) : base(lineWidth,Resolve(point1,point2,point3,point4)) {
		}
		public Quadrilateral(Vector2d point1,Vector2d point2,Vector2d point3,Vector2d point4) : base(Resolve(point1,point2,point3,point4)) {
		}
		internal Quadrilateral() : this(_Empty,_Empty,_Empty,_Empty) {
		}
		protected override PrimitiveType PrimType {
			get {
				return PrimitiveType.Quads;
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
		public Vector2d FourthPoint {
			get {
				return _points[3];
			}
			set {
				_points[3] = value;
			}
		}
		private static Vector2d[] Resolve(Vector2d point1,Vector2d point2,Vector2d point3,Vector2d point4) {
			return new Vector2d[4] {
				point1,
				point2,
				point3,
				point4
			};
		}
	}
}

