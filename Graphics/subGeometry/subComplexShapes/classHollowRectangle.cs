using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenTK;
using OpenTK.Graphics;

namespace Graphics {
	public class HollowRectangle : IRectangle, IPrimitiveShape, IDrawable, IEquatable<IRectangle> {
		private LineLoop _loop;
		public HollowRectangle(Vector2d position,Vector2d size) {
			_loop = new LineLoop();
			Update(position,Vector2d.Add(position,size));
		}
		public Vector2d Position {
			get {
				return _loop.Vertices[0];
			}
			set {
				Update(value,Extent);
			}
		}
		public Vector2d Size {
			get {
				return Vector2d.Subtract(Extent,Position);
			}
			set {
				Extent = Vector2d.Add(Position,value);
			}
		}
		public Vector2d Extent {
			get {
				return _loop.Vertices[2];
			}
			set {
				Update(Position,value);
			}
		}
		public BoundingBox Bounds {
			get {
				return new BoundingBox(Position,Extent);
			}
		}
		public ShapeType ShapeType {
			get {
				return ShapeType.LineSet;
			}
		}
		public Color4 Color {
			get {
				return _loop.Color;
			}
			set {
				_loop.Color = value;
			}
		}
		public float LineWidth {
			get {
				return _loop.LineWidth;
			}
			set {
				_loop.LineWidth = value;
			}
		}
		public ReadOnlyCollection<Vector2d> Outline {
			get {
				return _loop.Outline;
			}
		}
		private void Update(Vector2d position,Vector2d extent) {
			_loop.Vertices.Clear();
			_loop.Vertices.Add(position);
			_loop.Vertices.Add(new Vector2d(extent.X,position.Y));
			_loop.Vertices.Add(extent);
			_loop.Vertices.Add(new Vector2d(position.X,extent.Y));
		}
		public List<Line> GetSides() {
			return _loop.GetSides();
		}
		public void Draw(RenderingContext2D context) {
			_loop.Draw(context);
		}
		public bool PointWithinBounds(Vector2d point) {
			return _loop.PointWithinBounds(point);
		}
		public bool PointWithinContent(Vector2d point) {
			return _loop.PointWithinContent(point);
		}
		public bool BoundsCollide(BoundingBox box) {
			return _loop.BoundsCollide(box);
		}
		public bool BoundsCollide(IBounded shape) {
			return _loop.BoundsCollide(shape);
		}
		public bool ContentCollides(IPrimitiveShape shape) {
			return _loop.ContentCollides(shape);
		}
		public bool ContentCollides(GLImage image,byte alphaThreshold) {
			return image.ContentCollides(_loop,alphaThreshold);
		}
		public bool ContentCollides(GLImage image) {
			return ContentCollides(image,GLImage.DefaultAlphaThreshold);
		}
		public bool ContentCollides(BoundingBox box) {
			return _loop.ContentCollides(box);
		}
		public bool Equals(IRectangle other) {
			return Position == other.Position && Size == other.Size;
		}
		public override bool Equals(object other) {
			return ((IRectangle)this).GetType().IsInstanceOfType(other) ? Equals((IRectangle)other) : false;
		}
		public override int GetHashCode() {
			unchecked {
				return Position.GetHashCode() + Size.GetHashCode();
			}
		}
		public static bool operator ==(HollowRectangle a,HollowRectangle b) {
			return a.Equals(b);
		}
		public static bool operator !=(HollowRectangle a,HollowRectangle b) {
			return !(a == b);
		}
	}
}

