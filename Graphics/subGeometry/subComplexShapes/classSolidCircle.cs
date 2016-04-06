﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenTK;
using OpenTK.Graphics;

namespace Graphics {
	public class SolidCircle : Circle {
		private Polygon _poly;
		public SolidCircle(Vector2d center,double radius) : base(center,radius) {
			_poly = new Polygon();
			Update();
		}
		public override Color4 Color {
			get {
				return _poly.Color;
			}
			set {
				_poly.Color = value;
			}
		}
		public override float LineWidth {
			get {
				return _poly.LineWidth;
			}
			set {
				_poly.LineWidth = value;
			}
		}
		public override ShapeType ShapeType {
			get {
				return ShapeType.LineSet;
			}
		}
		public override ReadOnlyCollection<Vector2d> Outline {
			get {
				return _poly.Outline;
			}
		}
		protected override void Update() {
			_poly.Vertices.Clear();
			List<Vector2d> circumference = Geometry.GetPointsOnCircle(Center,Radius);
			_poly.Vertices.AddRange(circumference);
		}
		public override List<Line> GetSides() {
			return _poly.GetSides();
		}
		public override void Draw(RenderingContext2D context) {
			_poly.Draw(context);
		}
		public override bool PointWithinBounds(Vector2d point) {
			return _poly.PointWithinBounds(point);
		}
		public override bool PointWithinContent(Vector2d point) {
			return _poly.PointWithinContent(point);
		}
		public override bool BoundsCollide(BoundingBox box) {
			return _poly.BoundsCollide(box);
		}
		public override bool BoundsCollide(IBounded shape) {
			return _poly.BoundsCollide(shape);
		}
		public override bool ContentCollides(IPrimitiveShape shape) {
			return _poly.ContentCollides(shape);
		}
		public override bool ContentCollides(GLImage image,byte alphaThreshold) {
			return image.ContentCollides(_poly,alphaThreshold);
		}
		public override bool ContentCollides(GLImage image) {
			return image.ContentCollides(_poly);
		}
		public override bool ContentCollides(BoundingBox box) {
			return _poly.ContentCollides(box);
		}
	}
}

