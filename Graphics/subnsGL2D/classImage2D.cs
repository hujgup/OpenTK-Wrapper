using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics.GL2D {
	public class Image2D : IRectangle, IRedrawable, IDrawable, IWithin2D, IBounded, IEquatable<IRectangle>, IDisposable, ICloneable {
		public const byte DefaultAlphaThreshold = 0;
		private RenderingContext _context;
		private int _imageID;
		public Bitmap _bmp;
		public Image2D(RenderingContext context,string path,Vector2d position,Vector2d size) : this(context,path,position) {
			Size = size;
		}
		public Image2D(RenderingContext context,Bitmap bmp,Vector2d position,Vector2d size) : this(context,bmp,position) {
			Size = size;
		}
		public Image2D(RenderingContext context,string path,Vector2d position) : this(context,path) {
			Position = Position;
		}
		public Image2D(RenderingContext context,Bitmap bmp,Vector2d position) : this(context,bmp) {
			Position = Position;
		}
		public Image2D(RenderingContext context,string path) {
			_context = context;
			_context.Focus();
			_bmp = new Bitmap(path);
			Position = new Vector2d();
			Create(false);
		}
		public Image2D(RenderingContext context,Bitmap bmp) {
			_context = context;
			_context.Focus();
			_bmp = new Bitmap(bmp);
			Position = new Vector2d();
			Create(false);
		}
		public RenderingContext Context {
			get {
				return _context;
			}
			set {
				if (_context != value) {
					_context = value;
					Create();
				}
			}
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
		private void Create(bool destroyID) {
			if (_context.Focus()) {
				if (destroyID) {
					GL.DeleteTexture(_imageID);
				}
				_imageID = GL.GenTexture();
			}
		}
		private void Create() {
			Create(true);
		}
		private Color PixelColorActual(int x,int y,bool absolute) {
			if (absolute) {
				x -= (int)Position.X;
				y -= (int)Position.Y;
			}
			return _bmp.GetPixel(x,y);
		}
		private bool PixelVisibleActual(Color col,byte alphaThreshold) {
			return col.A > alphaThreshold;
		}
		private byte FromFloat(float val) {
			return (byte)Math.Floor(256*val);
		}
		public void Overlay(Color4 color,byte alphaThreshold) {
			_bmp = new Bitmap((int)Size.X,(int)Size.Y);
			Color replacement = new Color();
			replacement.R = FromFloat(color.R);
			replacement.G = FromFloat(color.G);
			replacement.B = FromFloat(color.B);
			replacement.A = FromFloat(color.A);
			int maxX = (int)Size.X;
			int maxY = (int)Size.Y;
			int x;
			Color col;
			for (int y = 0; y < maxY; y++) {
				for (x = 0; x < maxX; x++) {
					col = PixelColorActual(x,y,false);
					if (PixelVisibleActual(col,alphaThreshold)) {
						_bmp.SetPixel(x,y,replacement);
					}
				}
			}
		}
		public void Overlay(Color4 color) {
			return Overlay(color,DefaultAlphaThreshold);
		}
		public void Draw(RenderingContext context) {
			Context = context;
			Draw();
		}
		public void Draw() {
			if (_context.Focus()) {
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
			Image2D res = (Image2D)MemberwiseClone();
			res._imageID = GL.GenTexture();
			res._bmp = (Bitmap)_bmp.Clone();
			return res;
		}
		public Color4 PixelColor(int x,int y,bool absolute) {
			Color col = PixelColorActual(x,y,absolute);
			return new Color4(col.R,col.G,col.B,col.A);
		}
		public Color4 PixelColor(int x,int y) {
			return PixelColor(x,y,false);
		}
		public bool PixelIsVisible(int x,int y,byte alphaThreshold,bool absolute) {
			return PixelVisibleActual(PixelColorActual(x,y,absolute),alphaThreshold);
		}
		public bool PixelIsVisible(int x,int y,byte alphaThreshold) {
			return PixelIsVisible(x,y,alphaThreshold,false);
		}
		public bool PixelIsVisible(int x,int y,bool absolute) {
			return PixelIsVisible(x,y,DefaultAlphaThreshold,absolute);
		}
		public bool PixelIsVisible(int x,int y) {
			return PixelIsVisible(x,y,DefaultAlphaThreshold);
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
		public bool ContentCollides(IPrimitiveShape2D shape,byte alphaThreshold) {
			return shape.ContentCollides(this,alphaThreshold);
		}
		public bool ContentCollides(IPrimitiveShape2D shape) {
			return ContentCollides(shape,DefaultAlphaThreshold);
		}
		public bool ContentCollides(Image2D image,byte alphaThreshold) {
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
		public bool ContentCollides(Image2D image) {
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

