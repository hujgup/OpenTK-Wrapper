using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Graphics.Algebra;

namespace Graphics.GL2D {
	public class Line2D : PrimitiveShape2D {
		public Line2D(Color4 color,float lineWidth,Vector2d origin,Vector2d destination) : base(color,lineWidth,new Vector2d[2] {origin, destination}) {
		}
		public Line2D(Color4 color,Vector2d origin,Vector2d destination) : base(color,Resolve(origin,destination)) {
		}
		public Line2D(float lineWidth,Vector2d origin,Vector2d destination) : base(lineWidth,Resolve(origin,destination)) {
		}
		public Line2D(Vector2d origin,Vector2d destination) : base(Resolve(origin,destination)) {
		}
		protected override PrimitiveType PrimType {
			get {
				return PrimitiveType.Lines;
			}
		}
		public override ShapeType ShapeType {
			get {
				return ShapeType.LineSet;
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
		protected override bool AdvancedContentCollides(IPrimitiveShape2D shape) {
			List<Line2D> shapeLines = shape.GetSides();
			LinearEquasion currentEqn = GetLinearEquasion();
			bool res = false;
			Vector2d intersect;
			foreach (Line2D shapeLine in shapeLines) {
				intersect = currentEqn.Intersect(shapeLine.GetLinearEquasion(),Origin,Destination);
				if (!double.IsNaN(intersect.X)) {
					res = true;
					break;
				}
			}
			return res;
		}
		public LinearEquasion GetLinearEquasion() {
			return LinearEquasion.Regression(Origin,Destination);
		}
	}
}

