using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics {
	public class PointSet : PrimitiveShape {
		public PointSet(Color4 color,float lineWidth,IEnumerable<Point> points) : base(color,lineWidth,Resolve(points)) {
		}
		public PointSet(Color4 color,float lineWidth,params Point[] points) : base(color,lineWidth,Resolve(points)) {
		}
		public PointSet(Color4 color,float lineWidth,IEnumerable<Vector2d> points) : base(color,lineWidth,points) {
		}
		public PointSet(Color4 color,float lineWidth,params Vector2d[] points): base(color,lineWidth,points) {
		}
		public PointSet(Color4 color,IEnumerable<Vector2d> points) : base(color,points) {
		}
		public PointSet(Color4 color,params Vector2d[] points) : base(color,points) {
		}
		public PointSet(Color4 color,IEnumerable<Point> points) : base(color,Resolve(points)) {
		}
		public PointSet(Color4 color,params Point[] points) : base(color,Resolve(points)) {
		}
		public PointSet(float lineWidth,IEnumerable<Vector2d> points) : base(lineWidth,points) {
		}
		public PointSet(float lineWidth,params Vector2d[] points) : base(lineWidth,points) {
		}
		public PointSet(float lineWidth,IEnumerable<Point> points) : base(lineWidth,Resolve(points)) {
		}
		public PointSet(float lineWidth,params Point[] points) : base(lineWidth,Resolve(points)) {
		}
		public PointSet(IEnumerable<Vector2d> points) : base(points) {
		}
		public PointSet(params Vector2d[] points) : base(points) {
		}
		public PointSet(IEnumerable<Point> points) : base(Resolve(points)) {
		}
		public PointSet(params Point[] points) : base(Resolve(points)) {
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
		protected static IEnumerable<Vector2d> Resolve(IEnumerable<Point> points) {
			List<Vector2d> res = new List<Vector2d>();
			IEnumerator<Point> enumerator = points.GetEnumerator();
			while (enumerator.MoveNext()) {
				res.Add(enumerator.Current.Vertex);
			}
			return res;
		}
		protected static IEnumerable<Vector2d> Resolve(Point[] points) {
			return Resolve((IEnumerable<Point>)points);
		}
	}
}

