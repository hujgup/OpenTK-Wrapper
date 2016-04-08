using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics {
	public enum TextOverflow {
		Overflow,
		Hide
	}
	public class GLFont : IAttributeChangeListener {
		private const int _DefaultSize = 16;
		private const FontStyle _DefaultStyle = FontStyle.Regular;
		private static readonly FontFamily _DefaultFamily = FontFamily.GenericSerif;
		private FontFamily _family;
		private int _size;
		private FontStyle _style;
		public GLFont(FontFamily family,int size,FontStyle style) {
			_family = family;
			_size = size;
			_style = style;
		}
		public GLFont(int size,FontStyle style) : this(_DefaultFamily,size,style) {
		}
		public GLFont(FontFamily family,FontStyle style) : this(family,_DefaultSize,style) {
		}
		public GLFont(FontFamily family,int size) : this(family,size,_DefaultStyle) {
		}
		public GLFont(FontStyle style) : this(_DefaultSize,style) {
		}
		public GLFont(int size) : this(_DefaultFamily,size) {
		}
		public GLFont(FontFamily family) : this(family,_DefaultSize) {
		}
		public GLFont() : this(_DefaultFamily) {
		}
		public FontFamily Family {
			get {
				return _family;
			}
			set {
				if (_family != value) {
					_family = value;
					OnAttributeChange();
				}
			}
		}
		public int Size {
			get {
				return _size;
			}
			set {
				if (_size != value) {
					_size = value;
					OnAttributeChange();
				}
			}
		}
		public FontStyle Style {
			get {
				return _style;
			}
			set {
				if (_style != value) {
					_style = value;
					OnAttributeChange();
				}
			}
		}
		private void OnAttributeChange() {
			if (AttributeChange != null) {
				AttributeChange(this,EventArgs.Empty);
			}
		}
		public event EventHandler AttributeChange;
		public static implicit operator Font(GLFont font) {
			return new Font(font.Family,font.Size,font.Style,GraphicsUnit.Pixel);
		}
	}
	public class GLText : IDrawable, IBounded, IColored, IDisposable {
		private const string _DefaultText = "";
		private const TextOverflow _DefaultOverflow = TextOverflow.Hide;
		private static readonly Color4 _DefaultCol = Color4.Black;
		private static readonly Vector2d _DefaultPos = new Vector2d();
		private static readonly ScreenRegion _DefaultRegion = ScreenRegion.Unbounded;
		private static readonly EventHandler _FireUpdate = delegate(object sender,EventArgs e) {
			((GLText)sender).Update();
		};
		public static GLFont DefaultFont = new GLFont();
		private RenderingContext2D _context;
		private string _text;
		private Color4 _col;
		private TextOverflow _overflow;
		private GLFont _font;
		private ScreenRegion _region;
		private Bitmap _texture;
		private int _id;
		public GLText(RenderingContext2D context,string text,Color4 color,Vector2d position,GLFont font,TextOverflow overflow,ScreenRegion region) {
			_context = context;
			_text = text;
			_col = color;
			_overflow = overflow;
			_font = font;
			_font.AttributeChange += _FireUpdate;
			_region = region;
			_region.AttributeChange += _FireUpdate;
			Update(false);
		}
		public GLText(RenderingContext2D context,Color4 color,Vector2d position,GLFont font,TextOverflow overflow,ScreenRegion region) : this(context,_DefaultText,color,position,font,overflow,region) {
		}
		public GLText(RenderingContext2D context,string text,Vector2d position,GLFont font,TextOverflow overflow,ScreenRegion region) : this(context,text,_DefaultCol,position,font,overflow,region) {
		}
		public GLText(RenderingContext2D context,string text,Color4 color,GLFont font,TextOverflow overflow,ScreenRegion region) : this(context,text,color,_DefaultPos,font,overflow,region) {
		}
		public GLText(RenderingContext2D context,string text,Color4 color,Vector2d position,TextOverflow overflow,ScreenRegion region) : this(context,text,color,position,DefaultFont,overflow,region) {
		}
		public GLText(RenderingContext2D context,string text,Color4 color,Vector2d position,GLFont font,ScreenRegion region) : this(context,text,color,position,font,_DefaultOverflow,region) {
		}
		public GLText(RenderingContext2D context,string text,Color4 color,Vector2d position,GLFont font,TextOverflow overflow) : this(context,text,color,position,font,overflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,Vector2d position,GLFont font,TextOverflow overflow,ScreenRegion region) : this(context,_DefaultText,_DefaultCol,position,font,overflow,region) {
		}
		public GLText(RenderingContext2D context,Color4 color,GLFont font,TextOverflow overflow,ScreenRegion region) : this(context,_DefaultText,color,_DefaultPos,font,overflow,region) {
		}
		public GLText(RenderingContext2D context,Color4 color,Vector2d position,TextOverflow overflow,ScreenRegion region) : this(context,_DefaultText,color,position,DefaultFont,overflow,region) {
		}
		public GLText(RenderingContext2D context,Color4 color,Vector2d position,GLFont font,ScreenRegion region) : this(context,_DefaultText,color,position,font,_DefaultOverflow,region) {
		}
		public GLText(RenderingContext2D context,Color4 color,Vector2d position,GLFont font,TextOverflow overflow) : this(context,_DefaultText,color,position,font,overflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,string text,GLFont font,TextOverflow overflow,ScreenRegion region) : this(context,text,_DefaultCol,_DefaultPos,font,overflow,region) {
		}
		public GLText(RenderingContext2D context,string text,Vector2d position,TextOverflow overflow,ScreenRegion region) : this(context,text,_DefaultCol,position,DefaultFont,overflow,region) {
		}
		public GLText(RenderingContext2D context,string text,Vector2d position,GLFont font,ScreenRegion region) : this(context,text,_DefaultCol,position,font,_DefaultOverflow,region) {
		}
		public GLText(RenderingContext2D context,string text,Vector2d position,GLFont font,TextOverflow overflow) : this(context,text,_DefaultCol,position,font,overflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,string text,Color4 color,TextOverflow overflow,ScreenRegion region) : this(context,text,color,_DefaultPos,DefaultFont,overflow,region) {
		}
		public GLText(RenderingContext2D context,string text,Color4 color,GLFont font,ScreenRegion region) : this(context,text,color,_DefaultPos,font,_DefaultOverflow,region) {
		}
		public GLText(RenderingContext2D context,string text,Color4 color,GLFont font,TextOverflow overflow) : this(context,text,color,_DefaultPos,font,overflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,string text,Color4 color,Vector2d position,ScreenRegion region) : this(context,text,color,position,DefaultFont,_DefaultOverflow,region) {
		}
		public GLText(RenderingContext2D context,string text,Color4 color,Vector2d position,TextOverflow overflow) : this(context,text,color,position,DefaultFont,overflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,string text,Color4 color,Vector2d position,GLFont font) : this(context,text,color,position,font,_DefaultOverflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,GLFont font,TextOverflow overflow,ScreenRegion region) : this(context,_DefaultText,_DefaultCol,_DefaultPos,font,overflow,region) {
		}
		public GLText(RenderingContext2D context,Vector2d position,TextOverflow overflow,ScreenRegion region) : this(context,_DefaultText,_DefaultCol,position,DefaultFont,overflow,region) {
		}
		public GLText(RenderingContext2D context,Vector2d position,GLFont font,ScreenRegion region) : this(context,_DefaultText,_DefaultCol,position,font,_DefaultOverflow,region) {
		}
		public GLText(RenderingContext2D context,Vector2d position,GLFont font,TextOverflow overflow) : this(context,_DefaultText,_DefaultCol,position,font,overflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,Color4 color,TextOverflow overflow,ScreenRegion region) : this(context,_DefaultText,color,_DefaultPos,DefaultFont,overflow,region) {
		}
		public GLText(RenderingContext2D context,Color4 color,GLFont font,ScreenRegion region) : this(context,_DefaultText,color,_DefaultPos,font,_DefaultOverflow,region) {
		}
		public GLText(RenderingContext2D context,Color4 color,GLFont font,TextOverflow overflow) : this(context,_DefaultText,color,_DefaultPos,font,overflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,Color4 color,Vector2d position,ScreenRegion region) : this(context,_DefaultText,color,position,DefaultFont,_DefaultOverflow,region) {
		}
		public GLText(RenderingContext2D context,Color4 color,Vector2d position,TextOverflow overflow) : this(context,_DefaultText,color,position,DefaultFont,overflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,Color4 color,Vector2d position,GLFont font) : this(context,_DefaultText,color,position,font,_DefaultOverflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,string text,TextOverflow overflow,ScreenRegion region) : this(context,text,_DefaultCol,_DefaultPos,DefaultFont,overflow,region) {
		}
		public GLText(RenderingContext2D context,string text,GLFont font,ScreenRegion region) : this(context,text,_DefaultCol,_DefaultPos,font,_DefaultOverflow,region) {
		}
		public GLText(RenderingContext2D context,string text,GLFont font,TextOverflow overflow) : this(context,text,_DefaultCol,_DefaultPos,font,overflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,string text,Vector2d position,ScreenRegion region) : this(context,text,_DefaultCol,position,DefaultFont,_DefaultOverflow,region) {
		}
		public GLText(RenderingContext2D context,string text,Vector2d position,TextOverflow overflow) : this(context,text,_DefaultCol,position,DefaultFont,overflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,string text,Vector2d position,GLFont font) : this(context,text,_DefaultCol,position,font,_DefaultOverflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,string text,Color4 color,ScreenRegion region) : this(context,text,color,_DefaultPos,DefaultFont,_DefaultOverflow,region) {
		}
		public GLText(RenderingContext2D context,string text,Color4 color,TextOverflow overflow) : this(context,text,color,_DefaultPos,DefaultFont,overflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,string text,Color4 color,GLFont font) : this(context,text,color,_DefaultPos,font,_DefaultOverflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,string text,Color4 color,Vector2d position) : this(context,text,color,position,DefaultFont,_DefaultOverflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,TextOverflow overflow,ScreenRegion region) : this(context,_DefaultText,_DefaultCol,_DefaultPos,DefaultFont,overflow,region) {
		}
		public GLText(RenderingContext2D context,GLFont font,ScreenRegion region) : this(context,_DefaultText,_DefaultCol,_DefaultPos,font,_DefaultOverflow,region) {
		}
		public GLText(RenderingContext2D context,GLFont font,TextOverflow overflow) : this(context,_DefaultText,_DefaultCol,_DefaultPos,font,overflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,Vector2d position,ScreenRegion region) : this(context,_DefaultText,_DefaultCol,position,DefaultFont,_DefaultOverflow,region) {
		}
		public GLText(RenderingContext2D context,Vector2d position,TextOverflow overflow) : this(context,_DefaultText,_DefaultCol,position,DefaultFont,overflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,Vector2d position,GLFont font) : this(context,_DefaultText,_DefaultCol,position,font,_DefaultOverflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,Color4 color,ScreenRegion region) : this(context,_DefaultText,color,_DefaultPos,DefaultFont,_DefaultOverflow,region) {
		}
		public GLText(RenderingContext2D context,Color4 color,TextOverflow overflow) : this(context,_DefaultText,color,_DefaultPos,DefaultFont,overflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,Color4 color,GLFont font) : this(context,_DefaultText,color,_DefaultPos,font,_DefaultOverflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,Color4 color,Vector2d position) : this(context,_DefaultText,color,position,DefaultFont,_DefaultOverflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,string text,ScreenRegion region) : this(context,text,_DefaultCol,_DefaultPos,DefaultFont,_DefaultOverflow,region) {
		}
		public GLText(RenderingContext2D context,string text,TextOverflow overflow) : this(context,text,_DefaultCol,_DefaultPos,DefaultFont,overflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,string text,GLFont font) : this(context,text,_DefaultCol,_DefaultPos,font,_DefaultOverflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,string text,Vector2d position) : this(context,text,_DefaultCol,position,DefaultFont,_DefaultOverflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,string text,Color4 color) : this(context,text,color,_DefaultPos,DefaultFont,_DefaultOverflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,ScreenRegion region) : this(context,_DefaultText,_DefaultCol,_DefaultPos,DefaultFont,_DefaultOverflow,region) {
		}
		public GLText(RenderingContext2D context,TextOverflow overflow) : this(context,_DefaultText,_DefaultCol,_DefaultPos,DefaultFont,overflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,GLFont font) : this(context,_DefaultText,_DefaultCol,_DefaultPos,font,_DefaultOverflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,Vector2d position) : this(context,_DefaultText,_DefaultCol,position,DefaultFont,_DefaultOverflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,Color4 color) : this(context,_DefaultText,color,_DefaultPos,DefaultFont,_DefaultOverflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context,string text) : this(context,text,_DefaultCol,_DefaultPos,DefaultFont,_DefaultOverflow,_DefaultRegion) {
		}
		public GLText(RenderingContext2D context) : this(context,_DefaultText,_DefaultCol,_DefaultPos,DefaultFont,_DefaultOverflow,_DefaultRegion) {
		}
		public RenderingContext2D Context {
			get {
				return _context;
			}
			set {
				if (_context != value) {
					_context = value;
					Update();
				}
			}
		}
		public string TextContent {
			get {
				return _text;
			}
			set {
				if (_text != value) {
					_text = value;
					Update();
				}
			}
		}
		public Color4 Color {
			get {
				return _col;
			}
			set {
				if (_col != value) {
					_col = value;
					Update();
				}
			}
		}
		public TextOverflow Overflow {
			get {
				return _overflow;
			}
			set {
				if (_overflow != value) {
					_overflow = value;
					Update();
				}
			}
		}
		public TextOverflow VerticalOverflow {
			get {
				return _overflow;
			}
			set {
				if (_overflow != value) {
					_overflow = value;
					Update();
				}
			}
		}
		public ScreenRegion Region {
			get {
				return _region;
			}
			set {
				if (_region != value) {
					_region.AttributeChange -= _FireUpdate;
					_region = value;
					_region.AttributeChange += _FireUpdate;
					Update();
				}
			}
		}
		public GLFont Font {
			get {
				return _font;
			}
			set {
				if (_font != value) {
					_font.AttributeChange -= _FireUpdate;
					_font = value;
					_font.AttributeChange += _FireUpdate;
					Update();
				}
			}
		}
		public BoundingBox Bounds {
			get {
				return Region.Bounds;
			}
		}
		private void Update(bool wipeID) {
			if (_context.Focus()) {
				if (wipeID) {
					Dispose();
				}
				_texture = new Bitmap((int)_context.Size.X,(int)_context.Size.Y);
				_id = GL.GenTexture();
				GL.BindTexture(TextureTarget.Texture2D,_id);
				GL.TexParameter(TextureTarget.Texture2D,TextureParameterName.TextureMagFilter,(int)All.Linear);
				GL.TexParameter(TextureTarget.Texture2D,TextureParameterName.TextureMinFilter,(int)All.Linear);
				Font font = Font;
				System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(_texture);
				string text = TextContent;
				string write;
				if (Region.IsUnbounded) {
					write = text;
				} else {
					write = "";
					float lineWidth = 0;
					int totalHeight = 0;
					bool hideOverflow = VerticalOverflow == TextOverflow.Hide;
					float maxWidth = (float)Region.Size.X;
					for (int i = 0; i < text.Length; i++) {
						if (lineWidth >= maxWidth) {
							write += "\n";
							lineWidth -= maxWidth;
							totalHeight += Font.Size;
							if (hideOverflow) {
								break;
							}
						}
						write += text[i];
						lineWidth += graphics.MeasureString(text[i].ToString(),font).Width;
					}
				}
				Color col = System.Drawing.Color.FromArgb(FromFloat(Color.A),FromFloat(Color.R),FromFloat(Color.G),FromFloat(Color.B));
				graphics.DrawString(TextContent,font,new SolidBrush(col),new PointF((float)Region.Position.X,(float)Region.Position.Y));
				BitmapData data = _texture.LockBits(new Rectangle(0,0,_texture.Width,_texture.Height),ImageLockMode.ReadOnly,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				GL.TexImage2D(TextureTarget.Texture2D,0,PixelInternalFormat.Rgba,_texture.Width,_texture.Height,0,OpenTK.Graphics.OpenGL.PixelFormat.Bgra,PixelType.UnsignedByte,data.Scan0);
				graphics.Dispose();
				_texture.UnlockBits(data);
			}
		}
		private byte FromFloat(float val) {
			return (byte)Math.Round(val*255);
		}
		private void Update() {
			Update(true);
		}
		public void Dispose() {
			GL.DeleteTexture(_id);
			_texture.Dispose();
		}
		public void Draw(RenderingContext2D context) {
			Context = context;
			Draw();
		}
		public void Draw() {
			if (_context.Focus()) {
				GL.Enable(EnableCap.Texture2D);
				GL.Enable(EnableCap.Blend);
				GL.BlendFunc(BlendingFactorSrc.One,BlendingFactorDest.OneMinusSrcAlpha);
				GL.Begin(PrimitiveType.Quads);
				GL.TexCoord2(0,_context.Size.Y);
				GL.Vertex2(Vector2d.Zero);
				GL.TexCoord2(_context.Size);
				GL.Vertex2(_context.Size.X,0);
				GL.TexCoord2(_context.Size.X,0);
				GL.Vertex2(_context.Size);
				GL.TexCoord2(Vector2d.Zero);
				GL.Vertex2(0,_context.Size.Y);
				GL.End();
				GL.Disable(EnableCap.Texture2D);
				GL.Disable(EnableCap.Blend);
			}
		}
	}
}

