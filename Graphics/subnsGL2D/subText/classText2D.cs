using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics.GL2D {
	public class Text2D : IRedrawable, IDrawable, IText<GLFont>, IDisposable {
		private const string _DefaultText = "";
		private const TextOverflow _DefaultOverflow = TextOverflow.Hide;
		private static readonly Color4 _DefaultCol = Color4.Black;
		private static readonly ScreenRegion _DefaultRegion = ScreenRegion.Unbounded;
		private static readonly EventHandler _FireUpdate = delegate(object sender,EventArgs e) {
			((Text2D)sender).Update();
		};
		public static GLFont DefaultFont = new GLFont();
		private RenderingContext _context;
		private string _text;
		private Color4 _col;
		private TextOverflow _overflow;
		private GLFont _font;
		private ScreenRegion _region;
		private Bitmap _texture;
		private int _id;
		public Text2D(RenderingContext context,string text,Color4 color,GLFont font,TextOverflow overflow,ScreenRegion region) {
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
		public Text2D(RenderingContext2D context,Color4 color,GLFont font,TextOverflow overflow,ScreenRegion region) : this(context,_DefaultText,color,font,overflow,region) {
		}
		public Text2D(RenderingContext2D context,string text,GLFont font,TextOverflow overflow,ScreenRegion region) : this(context,text,_DefaultCol,font,overflow,region) {
		}
		public Text2D(RenderingContext2D context,string text,Color4 color,TextOverflow overflow,ScreenRegion region) : this(context,text,color,DefaultFont,overflow,region) {
		}
		public Text2D(RenderingContext2D context,string text,Color4 color,GLFont font,ScreenRegion region) : this(context,text,color,font,_DefaultOverflow,region) {
		}
		public Text2D(RenderingContext2D context,string text,Color4 color,GLFont font,TextOverflow overflow) : this(context,text,color,font,overflow,_DefaultRegion) {
		}
		public Text2D(RenderingContext2D context,GLFont font,TextOverflow overflow,ScreenRegion region) : this(context,_DefaultText,_DefaultCol,font,overflow,region) {
		}
		public Text2D(RenderingContext2D context,Color4 color,TextOverflow overflow,ScreenRegion region) : this(context,_DefaultText,color,DefaultFont,overflow,region) {
		}
		public Text2D(RenderingContext2D context,Color4 color,GLFont font,ScreenRegion region) : this(context,_DefaultText,color,font,_DefaultOverflow,region) {
		}
		public Text2D(RenderingContext2D context,Color4 color,GLFont font,TextOverflow overflow) : this(context,_DefaultText,color,font,overflow,_DefaultRegion) {
		}
		public Text2D(RenderingContext2D context,string text,TextOverflow overflow,ScreenRegion region) : this(context,text,_DefaultCol,DefaultFont,overflow,region) {
		}
		public Text2D(RenderingContext2D context,string text,GLFont font,ScreenRegion region) : this(context,text,_DefaultCol,font,_DefaultOverflow,region) {
		}
		public Text2D(RenderingContext2D context,string text,GLFont font,TextOverflow overflow) : this(context,text,_DefaultCol,font,overflow,_DefaultRegion) {
		}
		public Text2D(RenderingContext2D context,string text,Color4 color,ScreenRegion region) : this(context,text,color,DefaultFont,_DefaultOverflow,region) {
		}
		public Text2D(RenderingContext2D context,string text,Color4 color,TextOverflow overflow) : this(context,text,color,DefaultFont,overflow,_DefaultRegion) {
		}
		public Text2D(RenderingContext2D context,string text,Color4 color,GLFont font) : this(context,text,color,font,_DefaultOverflow,_DefaultRegion) {
		}
		public Text2D(RenderingContext2D context,TextOverflow overflow,ScreenRegion region) : this(context,_DefaultText,_DefaultCol,DefaultFont,overflow,region) {
		}
		public Text2D(RenderingContext2D context,GLFont font,ScreenRegion region) : this(context,_DefaultText,_DefaultCol,font,_DefaultOverflow,region) {
		}
		public Text2D(RenderingContext2D context,GLFont font,TextOverflow overflow) : this(context,_DefaultText,_DefaultCol,font,overflow,_DefaultRegion) {
		}
		public Text2D(RenderingContext2D context,Color4 color,ScreenRegion region) : this(context,_DefaultText,color,DefaultFont,_DefaultOverflow,region) {
		}
		public Text2D(RenderingContext2D context,Color4 color,TextOverflow overflow) : this(context,_DefaultText,color,DefaultFont,overflow,_DefaultRegion) {
		}
		public Text2D(RenderingContext2D context,Color4 color,GLFont font) : this(context,_DefaultText,color,font,_DefaultOverflow,_DefaultRegion) {
		}
		public Text2D(RenderingContext2D context,string text,ScreenRegion region) : this(context,text,_DefaultCol,DefaultFont,_DefaultOverflow,region) {
		}
		public Text2D(RenderingContext2D context,string text,TextOverflow overflow) : this(context,text,_DefaultCol,DefaultFont,overflow,_DefaultRegion) {
		}
		public Text2D(RenderingContext2D context,string text,GLFont font) : this(context,text,_DefaultCol,font,_DefaultOverflow,_DefaultRegion) {
		}
		public Text2D(RenderingContext2D context,string text,Color4 color) : this(context,text,color,DefaultFont,_DefaultOverflow,_DefaultRegion) {
		}
		public Text2D(RenderingContext2D context,ScreenRegion region) : this(context,_DefaultText,_DefaultCol,DefaultFont,_DefaultOverflow,region) {
		}
		public Text2D(RenderingContext2D context,TextOverflow overflow) : this(context,_DefaultText,_DefaultCol,DefaultFont,overflow,_DefaultRegion) {
		}
		public Text2D(RenderingContext2D context,GLFont font) : this(context,_DefaultText,_DefaultCol,font,_DefaultOverflow,_DefaultRegion) {
		}
		public Text2D(RenderingContext2D context,Color4 color) : this(context,_DefaultText,color,DefaultFont,_DefaultOverflow,_DefaultRegion) {
		}
		public Text2D(RenderingContext2D context,string text) : this(context,text,_DefaultCol,DefaultFont,_DefaultOverflow,_DefaultRegion) {
		}
		public Text2D(RenderingContext2D context) : this(context,_DefaultText,_DefaultCol,DefaultFont,_DefaultOverflow,_DefaultRegion) {
		}
		public RenderingContext Context {
			get {
				return _context;
			}
			set {
				if (_context != value) {
					Dispose();
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
				var graphics = System.Drawing.Graphics.FromImage(_texture);
				string text = TextContent;
				string write;
				if (Region.IsUnbounded) {
					write = text;
				} else {
					write = "";
					float lineWidth = 0;
					int totalHeight = 0;
					bool hideOverflow = Overflow == TextOverflow.Hide;
					float maxWidth = (float)Region.Size.X;
					int maxHeight = (int)Region.Size.Y;
					for (int i = 0; i < text.Length; i++) {
						if (lineWidth >= maxWidth) {
							write += "\n";
							lineWidth -= maxWidth;
							totalHeight += Font.Size;
							if (hideOverflow && totalHeight >= maxHeight) {
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
		private void Update() {
			Update(true);
		}
		private byte FromFloat(float val) {
			return (byte)Math.Round(val*255);
		}
		public void Dispose() {
			GL.DeleteTexture(_id);
			_texture.Dispose();
		}
		public void Draw(RenderingContext context) {
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

