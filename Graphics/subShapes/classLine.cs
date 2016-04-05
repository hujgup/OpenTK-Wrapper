using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics {
	public class Line : PrimitiveShape {
		public Line(Color4 color,float lineWidth,Vector2d origin,Vector2d destination) : base(color,lineWidth,new Vector2d[2] {origin, destination}) {
		}
		public Line(Color4 color,Vector2d origin,Vector2d destination) : base(color,Resolve(origin,destination)) {
		}
		public Line(float lineWidth,Vector2d origin,Vector2d destination) : base(lineWidth,Resolve(origin,destination)) {
		}
		public Line(Vector2d origin,Vector2d destination) : base(Resolve(origin,destination)) {
		}
		protected override PrimitiveType PrimType {
			get {
				return PrimitiveType.Lines;
			}
		}
		public Vector2d Origin {
			get {
				return _points[0];
			}
			set {
				_points[0] = value;
			}
		}
		public Vector2d Destination {
			get {
				return _points[1];
			}
			set {
				_points[1] = value;
			}
		}
		private static Vector2d[] Resolve(Vector2d origin,Vector2d destination) {
			return new Vector2d[2] {
				origin,
				destination
			};
		}
	}
}

