using System;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics {
	public class RenderingContext2D : IRectangle, IDisposable {
		private static readonly GraphicsMode _DefaultMode = new GraphicsMode(32,24,0,4);
		private GameWindow _window;
		private List<MouseButton> _mouseButtons;
		public RenderingContext2D(double logicTickRate,double renderTickRate,int windowWidth,int windowHeight,GraphicsMode mode,string title,bool fullscreen) {
			_window = new GameWindow(1,1,mode,title,fullscreen ? GameWindowFlags.Fullscreen : GameWindowFlags.Default);
			_window.ClientSize = new Size(windowWidth,windowHeight);
			_window.Location = new System.Drawing.Point(128,128);
			_mouseButtons = new List<MouseButton>();
			BackgroundColor = Color4.White;
			Keyboard = new KeyboardState();
			MousePosition = new Vector2d();
			IsWindowOpen = false;
			DesiredUpdateRate = logicTickRate;
			DesiredFrameRate = renderTickRate;
			_window.Load += (object sender,EventArgs e) => {
				_window.VSync = VSyncMode.On;
				IsWindowOpen = true;
				OnWindowOpen(e);
				OnUpdateRegions();
			};
			_window.Closed += (object sender,EventArgs e) => {
				IsWindowOpen = false;
				OnWindowClose(e);
			};
			_window.Resize += (object sender,EventArgs e) => {
				Focus();
				GL.Viewport(0,0,_window.Width,_window.Height);
				OnUpdateRegions();
			};
			_window.UpdateFrame += (object sender,FrameEventArgs e) => {
				if (IsWindowOpen && !_window.IsExiting) {
					OnLogicTick(e);
				}
			};
			_window.RenderFrame += (object sender,FrameEventArgs e) => {
				if (IsWindowOpen && !_window.IsExiting) {
					GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
					GL.ClearColor(BackgroundColor);
					GL.MatrixMode(MatrixMode.Projection);
					GL.LoadIdentity();
					GL.Ortho(0,_window.Width,_window.Height,0,0,1);			
					OnRenderTick(e);
					_window.SwapBuffers();
				}
			};
			_window.IconChanged += (object sender,EventArgs e) => {
				OnIconChange(e);
			};
			_window.KeyDown += (object sender,OpenTK.Input.KeyboardKeyEventArgs e) => {
				Keyboard = e.Keyboard;
			};
			_window.KeyUp += (object sender,OpenTK.Input.KeyboardKeyEventArgs e) => {
				Keyboard = e.Keyboard;
			};
			_window.KeyPress += (object sender,KeyPressEventArgs e) => {
				OnKeyPress(e);
			};
			_window.MouseDown += (object sender,MouseButtonEventArgs e) => {
				if (e.IsPressed && !_mouseButtons.Contains(e.Button)) {
					_mouseButtons.Add(e.Button);
				}
			};
			_window.MouseUp += (object sender,MouseButtonEventArgs e) => {
				if (!e.IsPressed && _mouseButtons.Contains(e.Button)) {
					_mouseButtons.Remove(e.Button);
				}
			};
			_window.MouseWheel += (object sender,MouseWheelEventArgs e) => {
				OnMouseWheel(e);
			};
			_window.MouseMove += (object sender, MouseMoveEventArgs e) => {
				MousePosition = new Vector2d(e.X,e.Y);
				MousePositionChange = new Vector2d(e.XDelta,e.YDelta);
			};
		}
		public RenderingContext2D(double logicTickRate,double renderTickRate,int windowWidth,int windowHeight,GraphicsMode mode,string title) : this(logicTickRate,renderTickRate,windowWidth,windowHeight,mode,title,false) {
		}
		public RenderingContext2D(double logicTickRate,double renderTickRate,int windowWidth,int windowHeight,string title) : this(logicTickRate,renderTickRate,windowWidth,windowHeight,_DefaultMode,title) {
		}
		public RenderingContext2D(double logicTickRate,double renderTickRate,int windowWidth,int windowHeight,GraphicsMode mode) : this(logicTickRate,renderTickRate,windowWidth,windowHeight,mode,"Graphics Window") {
		}
		public RenderingContext2D(double logicTickRate,double renderTickRate,int windowWidth,int windowHeight) : this(logicTickRate,renderTickRate,windowWidth,windowHeight,_DefaultMode) {
		}
		public Color4 BackgroundColor {
			get;
			set;
		}
		public bool IsWindowOpen {
			get;
			private set;
		}
		public KeyboardState Keyboard {
			get;
			private set;
		}
		public Vector2d MousePosition {
			get;
			private set;
		}
		public Vector2d MousePositionChange {
			get;
			private set;
		}
		public bool MouseInScreen {
			get {
				Vector2d pos = MousePosition;
				return pos.X >= 0 && pos.X < _window.Width && pos.Y >= 0 && pos.Y < _window.Height;
			}
		}
		public ReadOnlyCollection<MouseButton> MouseButtonsDown {
			get {
				return _mouseButtons.AsReadOnly();
			}
		}
		public string Title {
			get {
				return _window.Title;
			}
			set {
				_window.Title = value;
			}
		}
		public bool WindowVisible {
			get {
				return _window.Visible;
			}
			set {
				_window.Visible = value;
			}
		}
		public WindowBorder WindowBorderType {
			get {
				return _window.WindowBorder;
			}
			set {
				_window.WindowBorder = value;
			}
		}
		public double DesiredUpdateRate {
			get {
				return _window.TargetUpdateFrequency;
			}
			set {
				_window.TargetUpdateFrequency = value;
			}
		}
		public double ActualUpdateRate {
			get {
				return _window.UpdateFrequency;
			}
		}
		public double DesiredFrameRate {
			get {
				return _window.TargetRenderFrequency;
			}
			set {
				_window.TargetRenderFrequency = value;
			}
		}
		public double ActualFrameRate {
			get {
				return _window.RenderFrequency;
			}
		}
		public WindowState WindowState {
			get {
				return _window.WindowState;
			}
			set {
				_window.WindowState = value;
			}
		}
		public Vector2d Position {
			get {
				return new Vector2d(_window.Location.X,_window.Location.Y);
			}
			set {
				_window.Location = new System.Drawing.Point((int)value.X,(int)value.Y);
			}
		}
		public Vector2d Size {
			get {
				return new Vector2d(_window.ClientSize.Width,_window.ClientSize.Height);
			}
			set {
				_window.ClientSize = new System.Drawing.Size((int)value.X,(int)value.Y);
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
		public MouseCursor Cursor {
			get {
				return _window.Cursor;
			}
			set {
				_window.Cursor = value;
			}
		}
		public bool CursorVisible {
			get {
				return _window.CursorVisible;
			}
			set {
				_window.CursorVisible = value;
			}
		}
		public Icon Icon {
			get {
				return _window.Icon;
			}
			set {
				_window.Icon = value;
			}
		}

		private void OnUpdateRegions() {
			if (UpdateRegions != null) {
				UpdateRegions(this,EventArgs.Empty);
			}
		}
		private void OnWindowOpen(EventArgs e) {
			if (WindowOpen != null) {
				WindowOpen(this,e);
			}
		}
		private void OnWindowClose(EventArgs e) {
			if (WindowClose != null) {
				WindowClose(this,e);
			}
		}
		private void OnIconChange(EventArgs e) {
			if (WindowIconChange != null) {
				WindowIconChange(this,e);
			}
		}
		private void OnKeyPress(KeyPressEventArgs e) {
			if (KeyPress != null) {
				KeyPress(this,e);
			}
		}
		private void OnMouseWheel(MouseWheelEventArgs e) {
			if (MouseWheel != null) {
				MouseWheel(this,e);
			}
		}
		private void OnLogicTick(FrameEventArgs e) {
			if (LogicTick != null) {
				LogicTick(this,e);
			}
		}
		private void OnRenderTick(FrameEventArgs e) {
			if (RenderTick != null) {
				RenderTick(this,e);
			}
		}
		internal event EventHandler UpdateRegions;
		public event EventHandler WindowOpen;
		public event EventHandler WindowClose;
		public event EventHandler WindowIconChange;
		public event EventHandler<KeyPressEventArgs> KeyPress;
		public event EventHandler<MouseWheelEventArgs> MouseWheel;
		public event EventHandler<FrameEventArgs> LogicTick;
		public event EventHandler<FrameEventArgs> RenderTick;

		public void Draw(IDrawable obj) {
			obj.Draw(this);
		}
		public bool Focus() {
			if (IsWindowOpen) {
				_window.MakeCurrent();
			}
			return IsWindowOpen;
		}
		public void Open() {
			if (!IsWindowOpen) {
				_window.Run(DesiredUpdateRate,DesiredFrameRate);
			}
		}
		public void Close() {
			if (IsWindowOpen) {
				_window.Exit();
			}
		}
		public void Dispose() {
			Close();
			_window.Dispose();
		}
	}
}

