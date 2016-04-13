using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics {
	/// <summary>
	/// Represents a specific OpenGL rendering context.
	/// </summary>
	public abstract class RenderingContext : IRectangle, IColored, IDisposable {
		private double _depth;
		private List<MouseButton> _mouseButtons;
		/// <summary>
		/// Initializes a new instance of the <see cref="Graphics.RenderingContext"/> class.
		/// </summary>
		/// <param name="logicTickRate">The logic tick rate, i.e. the number of times the LogicTick event should fire per second.</param>
		/// <param name="renderTickRate">The frame rate, i.e. the number of times the RenderTick event should fire per second.</param>
		/// <param name="windowWidth">The width of the window, in pixels.</param>
		/// <param name="windowHeight">The height of the window, in pixels.</param>
		/// <param name="windowDepth">The depth of 3D space represented by the window.</param>
		/// <param name="mode">Describes how this context should draw objects.</param>
		/// <param name="title">The name of the window that appears above the content and in the taskbar.</param>
		/// <param name="fullscreen">If set to <c>true</c>, the window will start off in fullscreen mode.</param>
		public RenderingContext(double logicTickRate,double renderTickRate,int windowWidth,int windowHeight,double windowDepth,GraphicsMode mode,string title,bool fullscreen) {
			BaseWindow = new GameWindow(1,1,mode,title,fullscreen ? GameWindowFlags.Fullscreen : GameWindowFlags.Default);
			BaseWindow.ClientSize = new Size(windowWidth,windowHeight);
			BaseWindow.Location = new System.Drawing.Point(128,128);
			_depth = windowDepth;
			_mouseButtons = new List<MouseButton>();
			Color = Color4.White;
			Keyboard = new KeyboardState();
			MousePosition = new Vector2d();
			IsWindowOpen = false;
			DesiredLogicRate = logicTickRate;
			DesiredFrameRate = renderTickRate;
			BaseWindow.Load += (object sender,EventArgs e) => {
				BaseWindow.VSync = VSyncMode.On;
				IsWindowOpen = true;
				OnWindowOpen(e);
				OnUpdateRegions();
			};
			BaseWindow.Closed += (object sender,EventArgs e) => {
				IsWindowOpen = false;
				OnWindowClose(e);
			};
			BaseWindow.Resize += (object sender,EventArgs e) => {
				Focus();
				GL.Viewport(0,0,BaseWindow.Width,BaseWindow.Height);
				OnUpdateRegions();
			};
			BaseWindow.UpdateFrame += (object sender,FrameEventArgs e) => {
				if (IsWindowOpen && !BaseWindow.IsExiting) {
					OnLogicTick(e);
				}
			};
			BaseWindow.RenderFrame += (object sender,FrameEventArgs e) => {
				if (Focus() && !BaseWindow.IsExiting) {
					GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
					GL.ClearColor(Color);
					GL.MatrixMode(MatrixMode.Projection);
					GL.LoadIdentity();
					GL.Ortho(0,BaseWindow.Width,BaseWindow.Height,0,0,_depth);			
					OnRenderTick(e);
					BaseWindow.SwapBuffers();
				}
			};
			BaseWindow.IconChanged += (object sender,EventArgs e) => {
				OnIconChange(e);
			};
			BaseWindow.KeyDown += (object sender,OpenTK.Input.KeyboardKeyEventArgs e) => {
				Keyboard = e.Keyboard;
			};
			BaseWindow.KeyUp += (object sender,OpenTK.Input.KeyboardKeyEventArgs e) => {
				Keyboard = e.Keyboard;
			};
			BaseWindow.KeyPress += (object sender,KeyPressEventArgs e) => {
				OnKeyPress(e);
			};
			BaseWindow.MouseDown += (object sender,MouseButtonEventArgs e) => {
				if (e.IsPressed && !_mouseButtons.Contains(e.Button)) {
					_mouseButtons.Add(e.Button);
				}
			};
			BaseWindow.MouseUp += (object sender,MouseButtonEventArgs e) => {
				if (!e.IsPressed && _mouseButtons.Contains(e.Button)) {
					_mouseButtons.Remove(e.Button);
				}
			};
			BaseWindow.MouseWheel += (object sender,MouseWheelEventArgs e) => {
				OnMouseWheel(e);
			};
			BaseWindow.MouseMove += (object sender, MouseMoveEventArgs e) => {
				MousePosition = new Vector2d(e.X,e.Y);
				MousePositionChange = new Vector2d(DesiredFrameRate*e.XDelta,DesiredFrameRate*e.YDelta);
			};
		}
		protected GameWindow BaseWindow {
			get;
			private set;
		}
		/// <summary>
		/// Gets or sets the background color.
		/// </summary>
		public Color4 Color {
			get;
			set;
		}
		/// <summary>
		/// Gets a value indicating whether this instance's window has been opened (i.e. is ready to draw to).
		/// </summary>
		public bool IsWindowOpen {
			get;
			protected set;
		}
		/// <summary>
		/// Gets the current state of the keyboard.
		/// </summary>
		public KeyboardState Keyboard {
			get;
			protected set;
		}
		/// <summary>
		/// Gets the position of the mouse relative to the window described by this <see cref="Graphics.RenderingContext"/>.
		/// </summary>
		public Vector2d MousePosition {
			get;
			protected set;
		}
		/// <summary>
		/// Gets a value indicating, if the current rate of change continues, how far the mouse will move in one second.
		/// </summary>
		public Vector2d MousePositionChange {
			get;
			protected set;
		}
		/// <summary>
		/// Gets a value indicating whether the mouse is currently over the window described by this <see cref="Graphics.RenderingContext"/>.
		/// </summary>
		public bool MouseInScreen {
			get {
				Vector2d pos = MousePosition;
				return pos.X >= 0 && pos.X < BaseWindow.Width && pos.Y >= 0 && pos.Y < BaseWindow.Height;
			}
		}
		/// <summary>
		/// Gets the set of mouse buttons that are currently pressed.
		/// </summary>
		public ReadOnlyCollection<MouseButton> MouseButtonsDown {
			get {
				return _mouseButtons.AsReadOnly();
			}
		}
		/// <summary>
		/// Gets or sets the title of the window described by this <see cref="Graphics.RenderingContext"/> (i.e. its name as visible above its content and in the taskbar).
		/// </summary>
		public string Title {
			get {
				return BaseWindow.Title;
			}
			set {
				BaseWindow.Title = value;
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether the window described by this <see cref="Graphics.RenderingContext"/> is currently visible.
		/// </summary>
		public bool WindowVisible {
			get {
				return BaseWindow.Visible;
			}
			set {
				BaseWindow.Visible = value;
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the border type of the window described by this <see cref="Graphics.RenderingContext"/>.
		/// </summary>
		public WindowBorder WindowBorderType {
			get {
				return BaseWindow.WindowBorder;
			}
			set {
				BaseWindow.WindowBorder = value;
			}
		}
		/// <summary>
		/// Gets or sets the rate at which LogicTick events should be firing, in Hertz.
		/// </summary>
		public double DesiredLogicRate {
			get {
				return BaseWindow.TargetUpdateFrequency;
			}
			set {
				BaseWindow.TargetUpdateFrequency = value;
			}
		}
		/// <summary>
		/// Gets or sets the rate at which RenderTick events should be firing, in Hertz.
		/// </summary>
		public double DesiredFrameRate {
			get {
				return BaseWindow.TargetRenderFrequency;
			}
			set {
				BaseWindow.TargetRenderFrequency = value;
			}
		}
		/// <summary>
		/// Gets the rate at which LogicTick events are actually firing, in Hertz.
		/// </summary>
		public double ActualLogicRate {
			get {
				return BaseWindow.UpdateFrequency;
			}
		}
		/// <summary>
		/// Gets the rate at which RenderTick events are actually firing, in Hertz.
		/// </summary>
		public double ActualFrameRate {
			get {
				return BaseWindow.RenderFrequency;
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the state of the window described by this <see cref="Graphics.RenderingContext"/>.
		/// </summary>
		public WindowState WindowState {
			get {
				return BaseWindow.WindowState;
			}
			set {
				BaseWindow.WindowState = value;
			}
		}
		/// <summary>
		/// Gets or sets the top-left position of the window described by this <see cref="Graphics.RenderingContext"/>, in pixels.
		/// </summary>
		public Vector2d Position {
			get {
				return new Vector2d(BaseWindow.Location.X,BaseWindow.Location.Y);
			}
			set {
				BaseWindow.Location = new System.Drawing.Point((int)value.X,(int)value.Y);
			}
		}
		/// <summary>
		/// Gets or sets the size of the window described by this <see cref="Graphics.RenderingContext"/>, in pixels.
		/// </summary>
		public Vector2d Size {
			get {
				return new Vector2d(BaseWindow.ClientSize.Width,BaseWindow.ClientSize.Height);
			}
			set {
				BaseWindow.ClientSize = new System.Drawing.Size((int)value.X,(int)value.Y);
			}
		}
		/// <summary>
		/// Gets or sets the bottom-right position of the window described by this <see cref="Graphics.RenderingContext"/>.
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
		/// Gets or sets the cursor currently in use by the window described by this <see cref="Graphics.RenderingContext"/>.
		/// </summary>
		public MouseCursor Cursor {
			get {
				return BaseWindow.Cursor;
			}
			set {
				BaseWindow.Cursor = value;
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether the cursor is visible.
		/// </summary>
		public bool CursorVisible {
			get {
				return BaseWindow.CursorVisible;
			}
			set {
				BaseWindow.CursorVisible = value;
			}
		}
		/// <summary>
		/// Gets or sets the icon used by the window described by this <see cref="Graphics.RenderingContext"/>.
		/// </summary>
		public Icon Icon {
			get {
				return BaseWindow.Icon;
			}
			set {
				BaseWindow.Icon = value;
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
		/// <summary>
		/// Occurs when the window is opened for the first time.
		/// </summary>
		public event EventHandler WindowOpen;
		/// <summary>
		/// Occurs when the window is destroyed.
		/// </summary>
		public event EventHandler WindowClose;
		/// <summary>
		/// Occurs when the icon used by the window changes.
		/// </summary>
		public event EventHandler WindowIconChange;
		/// <summary>
		/// Occurs when a single key is pressed.
		/// </summary>
		public event EventHandler<KeyPressEventArgs> KeyPress;
		/// <summary>
		/// Occurs when the mouse wheel is moved.
		/// </summary>
		public event EventHandler<MouseWheelEventArgs> MouseWheel;
		/// <summary>
		/// Occurs when program logic should execute.
		/// </summary>
		public event EventHandler<FrameEventArgs> LogicTick;
		/// <summary>
		/// Occurs when objects should be drawn to this window.
		/// </summary>
		public event EventHandler<FrameEventArgs> RenderTick;

		/// <summary>
		/// Draws a specified object to this window. Implementors of IDrawable, please note that this method is an alias for obj.Draw(this), and calling it will cause infinite recursion.
		/// </summary>
		/// <param name="obj">The object to draw.</param>
		public void Draw(IDrawable obj) {
			obj.Draw(this);
		}
		/// <summary>
		/// Causes calls to <see cref="OpenTK.Graphics.OpenGL.GL" /> static methods to execute in the context of this window.
		/// </summary>
		public bool Focus() {
			if (IsWindowOpen) {
				BaseWindow.MakeCurrent();
			}
			return IsWindowOpen;
		}
		/// <summary>
		/// Opens the window described by this <see cref="Graphics.RenderingContext"/>.
		/// </summary>
		public void Open() {
			if (!IsWindowOpen) {
				BaseWindow.Run(DesiredLogicRate,DesiredFrameRate);
			}
		}
		/// <summary>
		/// Destroys the window described by this <see cref="Graphics.RenderingContext"/>.
		/// </summary>
		public void Close() {
			if (IsWindowOpen) {
				BaseWindow.Exit();
			}
		}
		/// <summary>
		/// Releases all resource used by the <see cref="Graphics.RenderingContext"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="Graphics.RenderingContext"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="Graphics.RenderingContext"/> in an unusable state. After
		/// calling <see cref="Dispose"/>, you must release all references to the <see cref="Graphics.RenderingContext"/> so
		/// the garbage collector can reclaim the memory that the <see cref="Graphics.RenderingContext"/> was occupying.</remarks>
		public void Dispose() {
			Close();
			BaseWindow.Dispose();
		}

	}
}

