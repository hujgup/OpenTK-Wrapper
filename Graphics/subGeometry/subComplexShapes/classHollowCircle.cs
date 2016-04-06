using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenTK;
using OpenTK.Graphics;

namespace Graphics {
	public class HollowCircle : Circle {
		private LineLoop _loop;
		public HollowCircle(Vector2d center,double radius) : base(center,radius) {
			_loop = new LineLoop();
			Update();
		}
		public override Color4 Color {
			get {
				return _loop.Color;
			}
			set {
				_loop.Color = value;
			}
		}
		public override float LineWidth {
			get {
				return _loop.LineWidth;
			}
			set {
				_loop.LineWidth = value;
			}
		}
		public override ShapeType ShapeType {
			get {
				return ShapeType.LineSet;
			}
		}
		public override ReadOnlyCollection<Vector2d> Outline {
			get {
				return _loop.Outline;
			}
		}
		protected override void Update() {
			_loop.Vertices.Clear();
			List<Vector2d> circumference = Geometry.GetPointsOnCircle(Center,Radius);
			_loop.Vertices.AddRange(circumference);
		}
		public override List<Line> GetSides() {
			return _loop.GetSides();
		}
		public override void Draw(RenderingContext2D context) {
			_loop.Draw(context);
		}
		public override bool PointWithinBounds(Vector2d point) {
			return _loop.PointWithinBounds(point);
		}
		public override bool PointWithinContent(Vector2d point) {
			return _loop.PointWithinContent(point);
		}
		public override bool BoundsCollide(BoundingBox box) {
			return _loop.BoundsCollide(box);
		}
		public override bool BoundsCollide(IBounded shape) {
			return _loop.BoundsCollide(shape);
		}
		public override bool ContentCollides(IPrimitiveShape shape) {
			return _loop.ContentCollides(shape);
		}
		public override bool ContentCollides(GLImage image,byte alphaThreshold) {
			return image.ContentCollides(_loop,alphaThreshold);
		}
		public override bool ContentCollides(GLImage image) {
			return image.ContentCollides(_loop);
		}
		public override bool ContentCollides(BoundingBox box) {
			return _loop.ContentCollides(box);
		}
	}
}

