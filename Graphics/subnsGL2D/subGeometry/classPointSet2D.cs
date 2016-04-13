using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics.GL2D {
	public class PointSet2D : PrimitiveShape2D {
		public PointSet2D(Color4 color,float lineWidth,IEnumerable<Point2D> points) : base(color,lineWidth,Resolve(points)) {
		}
		public PointSet2D(Color4 color,float lineWidth,params Point2D[] points) : base(color,lineWidth,Resolve(points)) {
		}
		public PointSet2D(Color4 color,float lineWidth,IEnumerable<Vector2d> points) : base(color,lineWidth,points) {
		}
		public PointSet2D(Color4 color,float lineWidth,params Vector2d[] points): base(color,lineWidth,points) {
		}
		public PointSet2D(Color4 color,IEnumerable<Vector2d> points) : base(color,points) {
		}
		public PointSet2D(Color4 color,params Vector2d[] points) : base(color,points) {
		}
		public PointSet2D(Color4 color,IEnumerable<Point2D> points) : base(color,Resolve(points)) {
		}
		public PointSet2D(Color4 color,params Point2D[] points) : base(color,Resolve(points)) {
		}
		public PointSet2D(float lineWidth,IEnumerable<Vector2d> points) : base(lineWidth,points) {
		}
		public PointSet2D(float lineWidth,params Vector2d[] points) : base(lineWidth,points) {
		}
		public PointSet2D(float lineWidth,IEnumerable<Point2D> points) : base(lineWidth,Resolve(points)) {
		}
		public PointSet2D(float lineWidth,params Point2D[] points) : base(lineWidth,Resolve(points)) {
		}
		public PointSet2D(IEnumerable<Vector2d> points) : base(points) {
		}
		public PointSet2D(params Vector2d[] points) : base(points) {
		}
		public PointSet2D(IEnumerable<Point2D> points) : base(Resolve(points)) {
		}
		public PointSet2D(params Point2D[] points) : base(Resolve(points)) {
		}
		protected override PrimitiveType PrimType {
			get {
				return PrimitiveType.Points;
			}
		}
		public override ShapeType ShapeType {
			get {
				return ShapeType.PointSet;
			}
		}
		public List<Vector2d> Points {
			get {
				return _points;
			}
		}
		protected static IEnumerable<Vector2d> Resolve(IEnumerable<Point2D> points) {
			List<Vector2d> res = new List<Vector2d>();
			IEnumerator<Point2D> enumerator = points.GetEnumerator();
			while (enumerator.MoveNext()) {
				res.Add(enumerator.Current.Vertex);
			}
			return res;
		}
		protected static IEnumerable<Vector2d> Resolve(Point2D[] points) {
			return Resolve((IEnumerable<Point2D>)points);
		}
	}
}

