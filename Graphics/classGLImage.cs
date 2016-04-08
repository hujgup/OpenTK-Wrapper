using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Graphics {
	public class GLImage : IRectangle, IDrawable, IWithin, IBounded, IEquatable<IRectangle>, IDisposable, ICloneable {
		public const int DefaultAlphaThreshold = 0;
		private int _imageID;
		public Bitmap _bmp;
		public GLImage(string path,Vector2d position,Vector2d size) : this(path,position) {
			Size = size;
		}
		public GLImage(string path,Vector2d position) : this(path) {
			Position = Position;
		}
		public GLImage(string path) {
			_imageID = GL.GenTexture();
			_bmp = new Bitmap(path);
			Position = new Vector2d();
		}
		public Vector2d Position {
			get;
			set;
		}
		public Vector2d Size {
			get {
				return new Vector2d((double)_bmp.Width,(double)_bmp.Height);
			}
			set {
				_bmp = new Bitmap(_bmp,(int)Math.Round(value.X),(int)Math.Round(value.Y));
			}
		}
		public Vector2d Extent {
			get {
				return Vector2d.Add(Position,Size);
			}
			set {
				Size = Vector2d.Subtract(value,Position);
			}
		}
		public BoundingBox Bounds {
			get {
				return new BoundingBox(Position,Extent);
			}
		}
		public void Draw(RenderingContext2D context) {
			if (context.Focus()) {
				GL.BindTexture(TextureTarget.Texture2D,_imageID);
				GL.TexParameter(TextureTarget.Texture2D,TextureParameterName.TextureMinFilter,(int)TextureMinFilter.Linear);
				GL.TexParameter(TextureTarget.Texture2D,TextureParameterName.TextureMagFilter,(int)TextureMagFilter.Linear);
				BitmapData data = _bmp.LockBits(new Rectangle(0,0,_bmp.Width,_bmp.Height),ImageLockMode.ReadOnly,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				GL.TexImage2D(TextureTarget.Texture2D,0,PixelInternalFormat.Rgba,data.Width,data.Height,0,OpenTK.Graphics.OpenGL.PixelFormat.Bgra,PixelType.UnsignedByte,data.Scan0);
				_bmp.UnlockBits(data);
			}
		}
		public void Dispose() {
			GL.DeleteTexture(_imageID);
			_bmp.Dispose();
		}
		public object Clone() {
			GLImage res = (GLImage)MemberwiseClone();
			res._imageID = GL.GenTexture();
			res._bmp = (Bitmap)_bmp.Clone();
			return res;
		}
		public bool PixelIsVisible(int x,int y,byte alphaThreshold,bool absolute) {
			if (absolute) {
				x -= (int)Position.X;
				y -= (int)Position.Y;
			}
			Color col = _bmp.GetPixel(x,y);
			return col.A > alphaThreshold;
		}
		public bool PixelIsVisible(int x,int y,byte alphaThreshold) {
			return PixelIsVisible(x,y,alphaThreshold,false);
		}
		public bool PixelIsVisible(int x,int y,bool absolute) {
			return PixelIsVisible(x,y,DefaultAlphaThreshold,false);
		}
		public bool PixelIsVisible(int x,int y) {
			return PixelIsVisible(x,y,false);
		}
		public bool PointWithinBounds(Vector2d point) {
			return Bounds.PointWithinBounds(point);
		}
		public bool PointWithinContent(Vector2d point,byte alphaThreshold) {
			bool res = PointWithinBounds(point);
			if (res) {
				res = PixelIsVisible((int)point.X,(int)point.Y,alphaThreshold,true);
			}
			return res;
		}
		public bool PointWithinContent(Vector2d point) {
			return PointWithinContent(point,DefaultAlphaThreshold);
		}
		public bool BoundsCollide(BoundingBox box) {
			return Bounds.BoundsCollide(box);
		}
		public bool BoundsCollide(IBounded shape) {
			return Bounds.BoundsCollide(shape.Bounds);
		}
		public bool ContentCollides(IPrimitiveShape shape,byte alphaThreshold) {
			return shape.ContentCollides(this,alphaThreshold);
		}
		public bool ContentCollides(IPrimitiveShape shape) {
			return ContentCollides(shape,DefaultAlphaThreshold);
		}
		public bool ContentCollides(GLImage image,byte alphaThreshold) {
			bool res = BoundsCollide(image.Bounds);
			if (res) {
				Vector2d point;
				int x;
				for (int y = 0; y < Size.Y; y++) {
					for (x = 0; x < Size.X; x++) {
						if (PixelIsVisible(x,y,alphaThreshold)) {
							point = Vector2d.Add(Position,new Vector2d((double)x,(double)y));
							res = image.PointWithinBounds(point) && image.PixelIsVisible(x,y,alphaThreshold,true);
							if (res) {
								x = (int)Size.X;
								y = (int)Size.Y;
							}
						}
					}
				}
			}
			return res;
		}
		public bool ContentCollides(GLImage image) {
			return ContentCollides(image,DefaultAlphaThreshold);
		}
		public bool ContentCollides(BoundingBox box) {
			return Bounds.ContentCollides(box);
		}
		public bool Equals(IRectangle other) {
			return Position == other.Position && Size == other.Size;
		}
	}
}

