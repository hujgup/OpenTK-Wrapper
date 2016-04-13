using System;
using System.Drawing;

namespace Graphics {
	/// <summary>
	/// Represents a font that may be used to draw text.
	/// </summary>
	public class GLFont : IAttributeChangeListener {
		private const int _DefaultSize = 16;
		private const FontStyle _DefaultStyle = FontStyle.Regular;
		private static readonly FontFamily _DefaultFamily = FontFamily.GenericMonospace;
		private FontFamily _family;
		private int _size;
		private FontStyle _style;
		/// <summary>
		/// Initializes a new instance of the <see cref="Graphics.GLFont"/> class.
		/// </summary>
		/// <param name="family">The set of fonts to utilize, in priority order.</param>
		/// <param name="size">The size of 1em, in pixels.</param>
		/// <param name="style">Font styling parameters.</param>
		public GLFont(FontFamily family,int size,FontStyle style) {
			_family = family;
			_size = size;
			_style = style;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Graphics.GLFont"/> class.
		/// </summary>
		/// <param name="size">The size of 1em, in pixels.</param>
		/// <param name="style">Font styling parameters.</param>
		public GLFont(int size,FontStyle style) : this(_DefaultFamily,size,style) {
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Graphics.GLFont"/> class.
		/// </summary>
		/// <param name="family">The set of fonts to utilize, in priority order.</param>
		/// <param name="style">Font styling parameters.</param>
		public GLFont(FontFamily family,FontStyle style) : this(family,_DefaultSize,style) {
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Graphics.GLFont"/> class.
		/// </summary>
		/// <param name="family">The set of fonts to utilize, in priority order.</param>
		/// <param name="size">The size of 1em, in pixels.</param>
		public GLFont(FontFamily family,int size) : this(family,size,_DefaultStyle) {
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Graphics.GLFont"/> class.
		/// </summary>
		/// <param name="style">Font styling parameters.</param>
		public GLFont(FontStyle style) : this(_DefaultSize,style) {
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Graphics.GLFont"/> class.
		/// </summary>
		/// <param name="size">The size of 1em, in pixels.</param>
		public GLFont(int size) : this(_DefaultFamily,size) {
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Graphics.GLFont"/> class.
		/// </summary>
		/// <param name="family">The set of fonts to utilize, in priority order.</param>
		public GLFont(FontFamily family) : this(family,_DefaultSize) {
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Graphics.GLFont"/> class.
		/// </summary>
		public GLFont() : this(_DefaultFamily) {
		}
		/// <summary>
		/// Gets or sets the set of fonts to utilize, in priority order.
		/// </summary>
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
		/// <summary>
		/// Gets or sets the size of 1em, in pixels.
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
		/// <summary>
		/// Gets or sets this font's style parameters.
		/// </summary>
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
		/// <summary>
		/// Occurs when an attribute's value changes.
		/// </summary>
		public event EventHandler AttributeChange;
		public static implicit operator Font(GLFont font) {
			return new Font(font.Family,font.Size,font.Style,GraphicsUnit.Pixel);
		}
	}
}

