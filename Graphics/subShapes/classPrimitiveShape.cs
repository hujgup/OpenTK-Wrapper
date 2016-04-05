using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics {
	public abstract class PrimitiveShape : IPrimitiveShape, IDrawable {
		public const float DefaultLineWidth = 1f;
		public static readonly Color4 DefaultColor = Color4.Black;
		protected List<Vector2d> _points;
		public PrimitiveShape(Color4 color,float lineWidth,IEnumerable<Vector2d> points) {
			Color = color;
			LineWidth = lineWidth;
			_points = new List<Vector2d>(points);
		}
		public PrimitiveShape(Color4 color,float lineWidth,params Vector2d[] points) : this(color,lineWidth,(IEnumerable<Vector2d>)points) {
		}
		public PrimitiveShape(Color4 color,IEnumerable<Vector2d> points) : this(color,DefaultLineWidth,points) {
		}
		public PrimitiveShape(Color4 color,params Vector2d[] points) : this(color,DefaultLineWidth,points) {
		}
		public PrimitiveShape(float lineWidth,IEnumerable<Vector2d> points) : this(DefaultColor,lineWidth,points) {
		}
		public PrimitiveShape(float lineWidth,params Vector2d[] points) : this(DefaultColor,lineWidth,points) {
		}
		public PrimitiveShape(IEnumerable<Vector2d> points) : this(DefaultColor,points) {
		}
		public PrimitiveShape(params Vector2d[] points) : this(DefaultColor,points) {
		}
		protected abstract PrimitiveType PrimType {
			get;
		}
		public Color4 Color {
			get;
			set;
		}
		public float LineWidth {
			get;
			set;
		}
		public void Draw(RenderingContext2D context) {
			context.Focus();
			GL.LineWidth(LineWidth);
			GL.Begin(PrimType);
			GL.Color4(Color);
			foreach (Vector2d point in _points) {
				GL.Vertex2(point);
			}
			GL.End();
		}
	}
}

