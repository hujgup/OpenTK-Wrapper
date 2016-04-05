using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics {
	public class RenderingContext2D : IDisposable {
		private static readonly GraphicsMode _DefaultMode = new GraphicsMode(32,24,0,4);
		private GameWindow _window;
		private List<MouseButton> _mouseButtons;
		public RenderingContext2D(double logicTickRate,double renderTickRate,int windowWidth,int windowHeight,GraphicsMode mode,string title,bool fullscreen) {
			_window = new GameWindow(windowWidth,windowHeight,mode,title,fullscreen ? GameWindowFlags.Fullscreen : GameWindowFlags.Default);
			_mouseButtons = new List<MouseButton>();
			BackgroundColor = Color4.White;
			Keyboard = new KeyboardState();
			MousePosition = new Vector2d();
			_window.Load += (object sender,EventArgs e) => {
				_window.VSync = VSyncMode.On;
				OnWindowLoad(e);
			};
			_window.Resize += (object sender,EventArgs e) => {
				GL.Viewport(0,0,_window.Width,_window.Height);
			};
			_window.UpdateFrame += (object sender,FrameEventArgs e) => {
				OnLogicTick(e);
			};
			_window.RenderFrame += (object sender,FrameEventArgs e) => {
				GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
				GL.ClearColor(BackgroundColor);
				GL.MatrixMode(MatrixMode.Projection);
				GL.LoadIdentity();
				GL.Ortho(0,_window.Width,_window.Height,0,0,1);			
				OnRenderTick(e);
				_window.SwapBuffers();
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
			_window.Run(logicTickRate,renderTickRate);
		}
		public RenderingContext2D(double logicTickRate,double renderTickRate,int windowWidth,int windowHeight,GraphicsMode mode,string title) : this(logicTickRate,renderTickRate,windowWidth,windowHeight,mode,title,false) {
		}
		public RenderingContext2D(double logicTickRate,double renderTickRate,int windowWidth,int windowHeight,string title) : this(logicTickRate,renderTickRate,windowWidth,windowHeight,_DefaultMode,title) {
		}
		public RenderingContext2D(double logicTickRate,double renderTickRate,int windowWidth,int windowHeight,GraphicsMode mode) : this(logicTickRate,renderTickRate,windowWidth,windowHeight,mode,"Graphics Window") {
		}
		public RenderingContext2D(double logicTickRate,double renderTickRate,int windowWidth,int windowHeight) : this(logicTickRate,renderTickRate,windowWidth,windowHeight,_DefaultMode) {
		}
		private RenderingContext2D(double logicTickRate,double renderTickRate,GameWindow window) {
		}
		public Color4 BackgroundColor {
			get;
			set;
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
		public double UpdateRate {
			get {
				return _window.UpdateFrequency;
			}
			set {
				_window.TargetUpdateFrequency = value;
			}
		}
		public double FrameRate {
			get {
				return _window.RenderFrequency;
			}
			set {
				_window.TargetRenderFrequency = value;
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
		public Vector2d WindowPosition {
			get {
				return new Vector2d(_window.X,_window.Y);
			}
			set {
				_window.X = (int)Math.Floor(value.X);
				_window.Y = (int)Math.Floor(value.Y);
			}
		}
		public Vector2d WindowSize {
			get {
				return new Vector2d(_window.Width,_window.Height);
			}
			set {
				_window.Width = (int)Math.Floor(value.X);
				_window.Height = (int)Math.Floor(value.Y);
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
		private void OnWindowLoad(EventArgs e) {
			if (WindowLoad != null) {
				WindowLoad(this,e);
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
		public event EventHandler WindowLoad;
		public event EventHandler WindowIconChange;
		public event EventHandler<KeyPressEventArgs> KeyPress;
		public event EventHandler<MouseWheelEventArgs> MouseWheel;
		public event EventHandler<FrameEventArgs> LogicTick;
		public event EventHandler<FrameEventArgs> RenderTick;

		public void Draw(IDrawable obj) {
			obj.Draw(this);
		}
		public void Focus() {
			_window.MakeCurrent();
		}
		public void Close() {
			_window.Close();
		}
		public void Dispose() {
			Close();
			_window.Dispose();
		}
	}
}

