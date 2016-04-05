using System;
using OpenTK;
using OpenTK.Graphics;

namespace Graphics {
	public class SolidRectangle : IRectangle, IPrimitiveShape, IDrawable {
		private Quadrilateral _quad;
		public SolidRectangle(Vector2d position,Vector2d size) {
			_quad = new Quadrilateral();
			Update(position,Vector2d.Add(position,size));
		}
		public Vector2d Position {
			get {
				return _quad.FirstPoint;
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
				return _quad.ThirdPoint;
			}
			set {
				Update(Position,value);
			}
		}
		public Color4 Color {
			get {
				return _quad.Color;
			}
			set {
				_quad.Color = value;
			}
		}
		public float LineWidth {
			get {
				return _quad.LineWidth;
			}
			set {
				_quad.LineWidth = value;
			}
		}
		private void Update(Vector2d position,Vector2d extent) {
			_quad.FirstPoint = position;
			_quad.SecondPoint = new Vector2d(extent.X,position.Y);
			_quad.ThirdPoint = extent;
			_quad.FourthPoint = new Vector2d(position.X,extent.Y);
		}
		public void Draw(RenderingContext2D context) {
			_quad.Draw(context);
		}
	}
}

