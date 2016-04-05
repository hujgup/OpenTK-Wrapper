using System;
using OpenTK;
using OpenTK.Graphics;

namespace Graphics {
	public class HollowRectangle : IRectangle, IPrimitiveShape, IDrawable {
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
		private void Update(Vector2d position,Vector2d extent) {
			_loop.Vertices.Clear();
			_loop.Vertices.Add(position);
			_loop.Vertices.Add(new Vector2d(extent.X,position.Y));
			_loop.Vertices.Add(extent);
			_loop.Vertices.Add(new Vector2d(position.X,extent.Y));
		}
		public void Draw(RenderingContext2D context) {
			_loop.Draw(context);
		}
	}
}

