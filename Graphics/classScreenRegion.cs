using System;
using System.Drawing;
using OpenTK;

namespace Graphics {
	/// <summary>
	/// Represents an area on the screen.
	/// </summary>
	public class ScreenRegion : IEquatable<IRectangle>, IAttributeChangeListener, IRectangle, IBounded, IDisposable {
		private static readonly Vector2d _Inf = new Vector2d(double.PositiveInfinity,double.PositiveInfinity);
		private static readonly EventHandler _FireUpdate = delegate(object sender,EventArgs e) {
			((ScreenRegion)sender).OnAttributeChange();
		};
		private Vector2d _pos;
		private Vector2d _size;
		private RenderingContext _context;
		/// <summary>
		/// Initializes a new instance of the <see cref="Graphics.ScreenRegion"/> class.
		/// </summary>
		/// <param name="position">The position on the screen, in pixels.</param>
		/// <param name="size">The size of the region, in pixels.</param>
		public ScreenRegion(Vector2d position,Vector2d size) {
			_pos = position;
			_size = size;
			_context = null;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Graphics.ScreenRegion"/> class.
		/// </summary>
		/// <param name="position">The position on the screen, in pixels.</param>
		public ScreenRegion(Vector2d position) : this(position,_Inf) {
		}
		public ScreenRegion(IRectangle rect) {
			_pos = rect.Position;
			_size = rect.Size;			
			_context = null;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Graphics.ScreenRegion"/> class.
		/// </summary>
		/// <param name="context">The RenderingContext to get the size of.</param>
		public ScreenRegion(RenderingContext context) : this(context,Vector2d.Zero) {
			_pos = Vector2d.Zero;
			_size = context.Size;
			_context = context;
			_context.UpdateRegions += _FireUpdate;
		}
		/// <summary>
		/// Gets a value representing a region that is unbounded in the positive direction.
		/// </summary>
		public static ScreenRegion Unbounded {
			get {
				return new ScreenRegion(Vector2d.Zero,_Inf);
			}
		}
		/// <summary>
		/// Gets or sets the top-left position.
		/// </summary>
		public Vector2d Position {
			get {
				return _pos;
			}
			set {
				if (_pos != value) {
					_pos = value;
					OnAttributeChange();
				}
			}
		}
		/// <summary>
		/// Gets or sets the size of this region.
		/// </summary>
		public Vector2d Size {
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
		/// Gets or sets the bottom-right position.
		/// </summary>
		public Vector2d Extent {
			get {
				return Vector2d.Add(Position,Size);
			}
			set {
				Size = Vector2d.Subtract(value,Position);
			}
		}
		/// <summary>
		/// Gets the boundary of this instance.
		/// </summary>
		public BoundingBox Bounds {
			get {
				return new BoundingBox(Position,Extent);
			}
		}
		/// <summary>
		/// Gets a value indicating whether this instance is unbounded.
		/// </summary>
		public bool IsUnbounded {
			get {
				return _size.X == double.PositiveInfinity && _size.Y == double.PositiveInfinity;
			}
		}
		/// <summary>
		/// Gets a value indicating whether this instance represents a RenderingContext.
		/// </summary>
		public bool IsContextLinked {
			get {
				return _context != null;
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
		/// <summary>
		/// Releases all resource used by the <see cref="Graphics.ScreenRegion"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="Graphics.ScreenRegion"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="Graphics.ScreenRegion"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the <see cref="Graphics.ScreenRegion"/> so the garbage
		/// collector can reclaim the memory that the <see cref="Graphics.ScreenRegion"/> was occupying.</remarks>
		public void Dispose() {
			if (_context != null) {
				_context.UpdateRegions -= _FireUpdate;
			}
		}
		/// <summary>
		/// Determines whether the specified <see cref="Graphics.IRectangle"/> is equal to the current <see cref="Graphics.ScreenRegion"/>.
		/// </summary>
		/// <param name="other">The <see cref="Graphics.IRectangle"/> to compare with the current <see cref="Graphics.ScreenRegion"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="Graphics.IRectangle"/> is equal to the current
		/// <see cref="Graphics.ScreenRegion"/>; otherwise, <c>false</c>.</returns>
		public bool Equals(IRectangle other) {
			return Position == other.Position && Size == other.Size;
		}
	}
}

