using System;
using System.Drawing;
using OpenTK;

namespace Graphics {
	public class ScreenRegion : IAttributeChangeListener, IRectangle, IBounded, IEquatable<IRectangle>, IDisposable {
		private static readonly Vector2d _Inf = new Vector2d(double.PositiveInfinity,double.PositiveInfinity);
		private static readonly EventHandler _FireUpdate = delegate(object sender,EventArgs e) {
			((ScreenRegion)sender).OnAttributeChange();
		};
		private Vector2d _pos;
		private Vector2d _size;
		private RenderingContext2D _context;
		public ScreenRegion(Vector2d position,Vector2d size) {
			_pos = position;
			_size = size;
			_context = null;
		}
		public ScreenRegion(RenderingContext2D context,Vector2d position) {
			_pos = position;
			_size = Vector2d.Subtract(context.Size,position);
			_context = context;
			_context.UpdateRegions += _FireUpdate;
		}
		public ScreenRegion(Vector2d position) : this(position,_Inf) {
		}
		public ScreenRegion(IRectangle rect) {
			_pos = rect.Position;
			_size = rect.Size;			
		}
		public ScreenRegion(RenderingContext2D context) : this(context,Vector2d.Zero) {
		}
		public static ScreenRegion Unbounded {
			get {
				return new ScreenRegion(Vector2d.Zero,_Inf);
			}
		}
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
		public bool IsUnbounded {
			get {
				return _size.X == double.PositiveInfinity && _size.Y == double.PositiveInfinity;
			}
		}
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
		public event EventHandler AttributeChange;
		public void Dispose() {
			if (_context != null) {
				_context.UpdateRegions -= _FireUpdate;
			}
		}
		public bool Equals(IRectangle other) {
			return Position == other.Position && Size == other.Size;
		}
	}
}

