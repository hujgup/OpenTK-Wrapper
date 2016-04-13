using System;
using System.Drawing;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;

namespace Graphics.GL2D {
	public class BitmapText2D : IRedrawable, IDrawable, IText<GLBitmapFont>, IDisposable {
		private const string _DefaultText = "";
		private const TextOverflow _DefaultOverflow = TextOverflow.Hide;
		private static readonly Color4 _DefaultCol = Color4.Black;
		private static readonly ScreenRegion _DefaultRegion = ScreenRegion.Unbounded;
		private static readonly EventHandler _FireUpdate = delegate(object sender,EventArgs e) {
			((BitmapText2D)sender).Update();
		};
		private RenderingContext _context;
		private string _text;
		private Color4 _col;
		private TextOverflow _overflow;
		private GLBitmapFont _font;
		private ScreenRegion _region;
		private List<Image2D> _allImages;
		private List<Image2D> _drawnImages;
		public BitmapText2D(RenderingContext context,GLBitmapFont font,string text,Color4 color,TextOverflow overflow,ScreenRegion region) {
			_context = context;
			_text = text;
			_col = color;
			_overflow = overflow;
			_font = font;
			_font.AttributeChange += _FireUpdate;
			_region = region;
			_region.AttributeChange += _FireUpdate;
			_allImages = new List<Image2D>(text.Length);
			Update(true);
		}
		public BitmapText2D(RenderingContext context,GLBitmapFont font,Color4 color,TextOverflow overflow,ScreenRegion region) : this(context,font,_DefaultText,color,overflow,region) {
		}
		public BitmapText2D(RenderingContext context,GLBitmapFont font,string text,TextOverflow overflow,ScreenRegion region) : this(context,font,text,_DefaultCol,overflow,region) {
		}
		public BitmapText2D(RenderingContext context,GLBitmapFont font,string text,Color4 color,ScreenRegion region) : this(context,font,text,color,_DefaultOverflow,region) {
		}
		public BitmapText2D(RenderingContext context,GLBitmapFont font,string text,Color4 color,TextOverflow overflow) : this(context,font,text,color,overflow,_DefaultRegion) {
		}
		public BitmapText2D(RenderingContext context,GLBitmapFont font,TextOverflow overflow,ScreenRegion region) : this(context,font,_DefaultText,_DefaultCol,overflow,region) {
		}
		public BitmapText2D(RenderingContext context,GLBitmapFont font,Color4 color,ScreenRegion region) : this(context,font,_DefaultText,color,_DefaultOverflow,region) {
		}
		public BitmapText2D(RenderingContext context,GLBitmapFont font,Color4 color,TextOverflow overflow) : this(context,font,_DefaultText,color,overflow,_DefaultRegion) {
		}
		public BitmapText2D(RenderingContext context,GLBitmapFont font,string text,ScreenRegion region) : this(context,font,text,_DefaultCol,_DefaultOverflow,region) {
		}
		public BitmapText2D(RenderingContext context,GLBitmapFont font,string text,TextOverflow overflow) : this(context,font,text,_DefaultCol,overflow,_DefaultRegion) {
		}
		public BitmapText2D(RenderingContext context,GLBitmapFont font,string text,Color4 color) : this(context,font,text,color,_DefaultOverflow,_DefaultRegion) {
		}
		public BitmapText2D(RenderingContext context,GLBitmapFont font,ScreenRegion region) : this(context,font,_DefaultText,_DefaultCol,_DefaultOverflow,region) {
		}
		public BitmapText2D(RenderingContext context,GLBitmapFont font,TextOverflow overflow) : this(context,font,_DefaultText,_DefaultCol,overflow,_DefaultRegion) {
		}
		public BitmapText2D(RenderingContext context,GLBitmapFont font,Color4 color) : this(context,font,_DefaultText,color,_DefaultOverflow,_DefaultRegion) {
		}
		public BitmapText2D(RenderingContext context,GLBitmapFont font,string text) : this(context,font,text,_DefaultCol,_DefaultOverflow,_DefaultRegion) {
		}
		public BitmapText2D(RenderingContext context,GLBitmapFont font) : this(context,font,_DefaultText,_DefaultCol,_DefaultOverflow,_DefaultRegion) {
		}
		public RenderingContext Context {
			get {
				return _context;
			}
			set {
				if (_context != value) {
					_context = value;
					Update(true);
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
					Update(true);
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
					Update(true);
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
		public GLBitmapFont Font {
			get {
				return _font;
			}
			set {
				if (_font != value) {
					_font.AttributeChange -= _FireUpdate;
					_font = value;
					_font.AttributeChange += _FireUpdate;
					Update(true);
				}
			}
		}
		public BoundingBox Bounds {
			get {
				return Region.Bounds;
			}
		}
		private void Update(bool generateImages) {
			if (generateImages) {
				Dispose();
				List<Bitmap> imgs = _font.TextToImages(TextContent);
				Vector2d position;
				Image2D img;
				foreach (Bitmap bmp in imgs) {
					img = new Image2D(_context,bmp);
					img.Overlay(Color);
					_allImages.Add(img);
				}
			}
			if (_allImages.Count != 0) {
				double maxWidth = Region.Size.X;
				double maxHeight = Region.Size.Y;
				double lineWidth = 0;
				double totalHeight = 0;
				Vector2d currentPosition = new Point(Region.Position.Y,Region.Position.Y);
				bool hideOverflow = Overflow == TextOverflow.Hide;
				_drawnImages = new List<Image2D>(_allImages.Count);
				foreach (Image2D img in _allImages) {
					if (lineWidth >= maxWidth) {
						currentPosition.X -= maxWidth;
						currentPosition.Y += Font.Size;
						if (hideOverflow && totalHeight >= maxHeight) {
							break;
						}
						lineWidth += img.Size.X;
					}
					_drawnImages.Add(img);
				}
			}
		}
		private void Update() {
			Update(false);
		}
		private byte FromFloat(float val) {
			return (byte)Math.Round(val*255);
		}
		public void Dispose() {
			foreach (Image2D img in _allImages) {
				img.Dispose();
			}
		}
		public void Draw(RenderingContext context) {
			Context = context;
			Draw();
		}
		public void Draw() {
			if (_context.Focus()) {
				foreach (Image2D img in _drawnImages) {
					img.Draw();
				}
			}
		}
	}
}

