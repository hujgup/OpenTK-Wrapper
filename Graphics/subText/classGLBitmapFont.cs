using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace Graphics {
	public class GLBitmapFont : IAttributeChangeListener {
		private const int _DefaultSize = 16;
		private readonly EventHandler _OnUpdate = delegate(object sender,EventArgs e) {
			OnAttributeChange();
		};
		public const char PlaceholderCharacter = '\uFFFD';
		private GlyphMap _glyphs;
		private int _size;
		public GLBitmapFont(IDictionary<char,Bitmap> glyphs) {
			_glyphs = new GlyphMap(glyphs);
			_glyphs.AttributeChange += _OnUpdate;
			Size = _DefaultSize;
		}
		public GLBitmapFont() {
			_glyphs = new GlyphMap();
			_glyphs.AttributeChange += _OnUpdate;
			Size = _DefaultSize;
		}
		public GlyphMap Glyphs {
			get {
				return _glyphs;
			}
			private set {
				_glyphs.AttributeChange -= _OnUpdate;
				_glyphs = value;
				_glyphs.AttributeChange += _OnUpdate;
			}
		}
		/// <summary>
		/// Gets or sets the height of each glyph, in pixels.
		/// </summary>
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
		public bool PrintableASCIIIsDefined {
			get {
				bool res = true;
				for (byte i = 32; res && i < 127; i++) {
					res &= Glyphs.ContainsKey(Convert.ToChar(i));
				}
				return res;
			}
		}
		public bool PlaceholderIsDefined {
			get {
				return Glyphs.ContainsKey(PlaceholderCharacter);
			}
		}
		public Bitmap PlaceholderBitmap {
			get {
				return PlaceholderIsDefined ? Glyphs[PlaceholderCharacter] : null;
			}
		}
		private void OnAttributeChange() {
			if (AttributeChange != null) {
				AttributeChange(this,EventArgs.Empty);
			}
		}
		/// <summary>
		/// Occurs when an attribute's value changes.
		/// </summary>
		public event EventHandler AttributeChange;
		public List<Bitmap> ResizeImages(List<Bitmap> list) {
			for (int i = 0; i < list.Count; i++) {
				list[i] = ResizeImage(list[i]);
			}
		}
		public Bitmap ResizeImage(Bitmap bmp) {
			double size = (double)Size;
			Rectangle rect = new Rectangle(0,0,(int)Math.Round(bmp.Width*size/(double)bmp.Height,MidpointRounding.AwayFromZero),Size);
			Bitmap res = new Bitmap(rect.Width,rect.Height);
			res.SetResolution(bmp.HorizontalResolution,bmp.VerticalResolution);
			var graphics = System.Drawing.Graphics.FromImage(res);
			graphics.CompositingMode = CompositingMode.SourceCopy;
			graphics.CompositingQuality = CompositingQuality.HighQuality;
			graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			graphics.SmoothingMode = SmoothingMode.None;
			graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			ImageAttributes wrap = new ImageAttributes();
			wrap.SetWrapMode(WrapMode.TileFlipXY);
			graphics.DrawImage(bmp,rect,0,0,bmp.Width,bmp.Height,GraphicsUnit.Pixel,wrap);
			return res;
		}
		public Bitmap CharToImage(char c,bool ignoreSizeParam) {
			Bitmap res = null;
			if (Glyphs.ContainsKey(c)) {
				res = Glyphs[c];
			} else {
				res = PlaceholderBitmap;
			}
			if (!ignoreSizeParam) {
				res = ResizeImage(res);
			}
			return res;
		}
		public Bitmap CharToImage(char c) {
			return CharToImage(c,false);
		}
		public List<Bitmap> TextToImages(IEnumerable<char> text) {
			List<Bitmap> res = new List<Bitmap>();
			IEnumerator<char> enumerator = text.GetEnumerator();
			Bitmap glyph;
			while (enumerator.MoveNext()) {
				glyph = CharToImage(enumerator.Current);
				if (glyph != null) {
					res.Add(glyph);
				}
			}
			return res;			
		}
		public List<Bitmap> TextToImages(char[] text) {
			List<Bitmap> res = new List<Bitmap>(text.Length);
			Bitmap glyph;
			for (int i = 0; i < text.Length; i++) {
				glyph = CharToImage(text[i]);
				if (glyph != null) {
					res.Add(glyph);
				}
			}
			return res;
		}
		public List<Bitmap> TextToImages(string text) {
			return TextToImages(text.ToCharArray());
		}
	}
}

