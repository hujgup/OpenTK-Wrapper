using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics {
	public class TriangleFan : PrimitiveShape {
		public TriangleFan(Color4 color,float lineWidth,Vector2d center,IEnumerable<Vector2d> circumference) : base(color,lineWidth,Resolve(center,circumference)) {
		}
		public TriangleFan(Color4 color,float lineWidth,Vector2d center,params Vector2d[] circumference) : base(color,lineWidth,Resolve(center,circumference)) {
		}
		public TriangleFan(Color4 color,Vector2d center,IEnumerable<Vector2d> circumference) : base(color,Resolve(center,circumference)) {
		}
		public TriangleFan(Color4 color,Vector2d center,params Vector2d[] circumference) : base(color,Resolve(center,circumference)) {
		}
		public TriangleFan(float lineWidth,Vector2d center,IEnumerable<Vector2d> circumference) : base(lineWidth,Resolve(center,circumference)) {
		}
		public TriangleFan(float lineWidth,Vector2d center,params Vector2d[] circumference) : base(lineWidth,Resolve(center,circumference)) {
		}
		public TriangleFan(Vector2d center,IEnumerable<Vector2d> circumference) : base(Resolve(center,circumference)) {
		}
		public TriangleFan(Vector2d center,params Vector2d[] circumference) : base(Resolve(center,circumference)) {
		}
		protected override PrimitiveType PrimType {
			get {
				return PrimitiveType.TriangleFan;
			}
		}
		protected override ShapeType ShapeType {
			get {
				return ShapeType.Solid;
			}
		}
		public Vector2d Center {
			get {
				return _points[0];
			}
			set {
				_points[0] = value;
			}
		}
		public ReadOnlyCollection<Vector2d> Circumference {
			get {
				return _points.AsReadOnly();
			}
		}
		public List<Vector2d> AllPoints {
			get {
				return _points;
			}
		}
		private static IEnumerable<Vector2d> Resolve(Vector2d center,IEnumerable<Vector2d> circumference) {
			List<Vector2d> res = new List<Vector2d>();
			IEnumerator<Vector2d> enumerator = circumference.GetEnumerator();
			res.Add(center);
			while (enumerator.MoveNext()) {
				res.Add(enumerator.Current);
			}
			if (res.Count < 2) {
				throw new ArgumentException("Not enough points defined to create a TriangleFan","circumference");
			}
			return res;
		}
		private static IEnumerable<Vector2d> Resolve(Vector2d center,Vector2d[] circumference) {
			return Resolve(center,(IEnumerable<Vector2d>)circumference);
		}
		public override List<Line> GetSides() {
			ReadOnlyCollection<Vector2d> circumference = Circumference;
			List<Line> res = new List<Line>(circumference.Count);
			for (int i = 0, iPlusOne = 1; i < circumference.Count; i++, iPlusOne++) {
				res.Add(new Line(circumference[i],circumference[iPlusOne]));
			}
			if (circumference.Count > 1) {
				res.Add(new Line(circumference[circumference.Count - 1],circumference[0]));
			}
			return res;
		}
	}
}

