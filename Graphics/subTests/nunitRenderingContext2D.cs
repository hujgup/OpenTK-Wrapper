using System;
using OpenTK.Graphics;
using OpenTK;
using NUnit.Framework;

namespace Graphics.GL2D {
	[TestFixture()]
	public class NUnitRenderingContext2D {
		[Test()]
		public void FourArgPassed() {
			RenderingContext2D ctx = new RenderingContext2D(30,60,800,600);
			ctx.WindowOpen += (object sender,EventArgs e) => {
				Assert.AreEqual(30,Math.Round(ctx.DesiredUpdateRate),"Update rate must match the first constructor parameter");
				Assert.AreEqual(60,Math.Round(ctx.DesiredFrameRate),"Frame rate must match the second constructor parameter");
				Assert.AreEqual(new Vector2d(800,600),ctx.Size,"Window size must match the third and fourth constructer parameters");
				ctx.Close();
			};
			ctx.Open();
		}
		[Test()]
		public void FourArgDefaults() {
			RenderingContext2D ctx = new RenderingContext2D(30,60,800,600);
			ctx.WindowOpen += (object sender,EventArgs e) => {
				Assert.AreEqual(Color4.White,ctx.BackgroundColor,"Default background color mast be white");
				Assert.AreEqual("Graphics Window",ctx.Title,"Default title must be 'Graphics Window'");
				Assert.IsTrue(ctx.WindowVisible,"The window must start out as visible");
				Assert.AreEqual(WindowBorder.Resizable,ctx.WindowBorderType,"Default window border type must be 'Resizable'");
				Assert.AreEqual(WindowState.Normal,ctx.WindowState,"Default window state must be 'Normal'");
				Assert.IsTrue(ctx.CursorVisible,"The cursor must start off as visible");
				ctx.Close();
			};
			ctx.Open();
		}
		[Test()]
		public void FiveArgStringPassed() {
			RenderingContext2D ctx = new RenderingContext2D(5,42,1280,720,"Some string");
			ctx.WindowOpen += (object sender,EventArgs e) => {
				Assert.AreEqual(5,Math.Round(ctx.DesiredUpdateRate),"Update rate must match the first constructor parameter");
				Assert.AreEqual(42,Math.Round(ctx.DesiredFrameRate),"Frame rate must match the second constructor parameter");
				Assert.AreEqual(new Vector2d(1280,720),ctx.Size,"Window size must match the third and fourth constructer parameters");
				Assert.AreEqual("Some string",ctx.Title,"Title must match the 5th constructor parameter");
				ctx.Close();
			};
			ctx.Open();
		}
		[Test()]
		public void FiveArgStringDefaults() {
			RenderingContext2D ctx = new RenderingContext2D(5,42,1280,720,"Some string");
			ctx.WindowOpen += (object sender,EventArgs e) => {
				Assert.AreEqual(Color4.White,ctx.BackgroundColor,"Default background color mast be white");
				Assert.IsTrue(ctx.WindowVisible,"The window must start out as visible");
				Assert.AreEqual(WindowBorder.Resizable,ctx.WindowBorderType,"Default window border type must be 'Resizable'");
				Assert.AreEqual(WindowState.Normal,ctx.WindowState,"Default window state must be 'Normal'");
				Assert.IsTrue(ctx.CursorVisible,"The cursor must start off as visible");
				ctx.Close();
			};
			ctx.Open();
		}
		[Test()]
		public void SevenArgPassed() {
			RenderingContext2D ctx = new RenderingContext2D(1,15,50,50,GraphicsMode.Default,"title",true);
			ctx.WindowOpen += (object sender,EventArgs e) => {
				Assert.AreEqual(1,Math.Round(ctx.DesiredUpdateRate),"Update rate must match the first constructor parameter");
				Assert.AreEqual(15,Math.Round(ctx.DesiredFrameRate),"Frame rate must match the second constructor parameter");
				Assert.AreEqual(new Vector2d(50,50),ctx.Size,"Window size must match the third and fourth constructer parameters");
				Assert.AreEqual("title",ctx.Title,"Title must match the 6th constructor parameter");
				Assert.AreEqual(WindowState.Fullscreen,ctx.WindowState,"Initial fullscreen state must correspond to the 7th constructor parameter");
				Assert.AreEqual(WindowBorder.Hidden,ctx.WindowBorderType,"Default window border type must be 'Hidden' when fullscreen is true");
				ctx.Close();
			};
			ctx.Open();
		}
		[Test()]
		public void SevenArgDefaults() {
			RenderingContext2D ctx = new RenderingContext2D(1,15,50,50,GraphicsMode.Default,"title",true);
			ctx.WindowOpen += (object sender,EventArgs e) => {
				Assert.AreEqual(Color4.White,ctx.BackgroundColor,"Default background color mast be white");
				Assert.IsTrue(ctx.WindowVisible,"The window must start out as visible");
				Assert.IsTrue(ctx.CursorVisible,"The cursor must start off as visible");
				ctx.Close();
			};
			ctx.Open();
		}
		[Test()]
		public void PropertiesExist() {
			RenderingContext2D ctx = new RenderingContext2D(1,1,1,1);
			ctx.WindowOpen += (object sender,EventArgs e) => {
				Assert.DoesNotThrow(delegate() {
					Console.WriteLine(ctx.ActualFrameRate);
				},"Property ActualFrameRate must exist on a RenderingContext2D object");
				Assert.AreEqual(0d.GetType(),ctx.ActualFrameRate.GetType(),"Property x must be of type y");
				Assert.DoesNotThrow(delegate() {
					Console.WriteLine(ctx.ActualUpdateRate);
				},"Property ActualUpdateRate must exist on a RenderingContext2D object");
				Assert.AreEqual(0d.GetType(),ctx.ActualUpdateRate.GetType(),"Property x must be of type y");
				Assert.DoesNotThrow(delegate() {
					Console.WriteLine(ctx.BackgroundColor);
					ctx.BackgroundColor = Color4.Black;
				},"Property BackgroundColor must exist on a RenderingContext2D object");
				Assert.AreEqual(Color4.White.GetType(),ctx.BackgroundColor.GetType(),"Property x must be of type y");
				Assert.DoesNotThrow(delegate() {
					Console.WriteLine(ctx.Cursor);
					ctx.Cursor = MouseCursor.Default;
				},"Property Cursor must exist on a RenderingContext2D object");
				Assert.AreEqual(MouseCursor.Default.GetType(),ctx.Cursor.GetType(),"Property x must be of type y");
				Assert.DoesNotThrow(delegate() {
					Console.WriteLine(ctx.CursorVisible);
					ctx.CursorVisible = false;
				},"Property CursorVisible must exist on a RenderingContext2D object");
				Assert.AreEqual(true.GetType(),ctx.CursorVisible.GetType(),"Property x must be of type y");
				Assert.DoesNotThrow(delegate() {
					Console.WriteLine(ctx.DesiredFrameRate);
					ctx.DesiredFrameRate = 4;
				},"Property DesiredFrameRate must exist on a RenderingContext2D object");
				Assert.AreEqual(0d.GetType(),ctx.DesiredFrameRate.GetType(),"Property x must be of type y");
				Assert.DoesNotThrow(delegate() {
					Console.WriteLine(ctx.DesiredUpdateRate);
					ctx.DesiredUpdateRate = 4;
				},"Property DesiredUpdateRate must exist on a RenderingContext2D object");
				Assert.AreEqual(0d.GetType(),ctx.DesiredUpdateRate.GetType(),"Property x must be of type y");
				Assert.DoesNotThrow(delegate() {
					Console.WriteLine(ctx.Extent);
					ctx.Extent = new Vector2d(500,500);
				},"Property Extent must exist on a RenderingContext2D object");
				Assert.AreEqual(Vector2d.One.GetType(),ctx.Extent.GetType(),"Property x must be of type y");
				Assert.DoesNotThrow(delegate() {
					Console.WriteLine(ctx.Icon);
				},"Property Icon must exist on a RenderingContext2D object");
				// todo
				//Assert.AreEqual(System.Drawing.Icon.GetType(),ctx.Icon.GetType(),"Property x must be of type y");
				Assert.DoesNotThrow(delegate() {
					Console.WriteLine(ctx.IsWindowOpen);
				},"Property IsWindowOpen must exist on a RenderingContext2D object");
				Assert.AreEqual(true.GetType(),ctx.IsWindowOpen.GetType(),"Property x must be of type y");
				Assert.DoesNotThrow(delegate() {
					Console.WriteLine(ctx.Keyboard);
				},"Property Keyboard must exist on a RenderingContext2D object");
				Assert.AreEqual(new OpenTK.Input.KeyboardState().GetType(),ctx.Keyboard.GetType(),"Property x must be of type y");
				Assert.DoesNotThrow(delegate() {
					Console.WriteLine(ctx.MouseButtonsDown);
				},"Property MouseButtonsDown must exist on a RenderingContext2D object");
				Assert.AreEqual(new System.Collections.Generic.List<OpenTK.Input.MouseButton>().AsReadOnly().GetType(),ctx.MouseButtonsDown.GetType(),"Property x must be of type y");
				Assert.DoesNotThrow(delegate() {
					Console.WriteLine(ctx.MouseInScreen);
				},"Property MouseInScreen must exist on a RenderingContext2D object");
				Assert.AreEqual(true.GetType(),ctx.MouseInScreen.GetType(),"Property x must be of type y");
				Assert.DoesNotThrow(delegate() {
					Console.WriteLine(ctx.MousePosition);
				},"Property MousePosition must exist on a RenderingContext2D object");
				Assert.AreEqual(Vector2d.One.GetType(),ctx.MousePosition.GetType(),"Property x must be of type y");
				Assert.DoesNotThrow(delegate() {
					Console.WriteLine(ctx.MousePositionChange);
				},"Property MousePositionChange must exist on a RenderingContext2D object");
				Assert.AreEqual(Vector2d.One.GetType(),ctx.MousePositionChange.GetType(),"Property x must be of type y");
				Assert.DoesNotThrow(delegate() {
					Console.WriteLine(ctx.Position);
					ctx.Position = new Vector2d(64,64);
				},"Property Position must exist on a RenderingContext2D object");
				Assert.AreEqual(Vector2d.One.GetType(),ctx.Position.GetType(),"Property x must be of type y");
				Assert.DoesNotThrow(delegate() {
					Console.WriteLine(ctx.Size);
					ctx.Size = new Vector2d(32,32);
				},"Property Size must exist on a RenderingContext2D object");
				Assert.AreEqual(Vector2d.One.GetType(),ctx.Size.GetType(),"Property x must be of type y");
				Assert.DoesNotThrow(delegate() {
					Console.WriteLine(ctx.Title);
					ctx.Title = "Hello world";
				},"Property Title must exist on a RenderingContext2D object");
				Assert.AreEqual("".GetType(),ctx.Title.GetType(),"Property x must be of type y");
				Assert.DoesNotThrow(delegate() {
					Console.WriteLine(ctx.WindowBorderType);
					ctx.WindowBorderType = WindowBorder.Fixed;
				},"Property WindowBorderType must exist on a RenderingContext2D object");
				Assert.AreEqual(WindowBorder.Fixed.GetType(),ctx.WindowBorderType.GetType(),"Property x must be of type y");
				Assert.DoesNotThrow(delegate() {
					Console.WriteLine(ctx.WindowState);
					ctx.WindowState = WindowState.Maximized;
				},"Property WindowState must exist on a RenderingContext2D object");
				Assert.AreEqual(WindowState.Normal.GetType(),ctx.WindowState.GetType(),"Property x must be of type y");
				Assert.DoesNotThrow(delegate() {
					Console.WriteLine(ctx.WindowVisible);
					ctx.WindowVisible = false;
				},"Property WindowVisible must exist on a RenderingContext2D object");
				Assert.AreEqual(true.GetType(),ctx.WindowVisible.GetType(),"Property x must be of type y");
				ctx.Close();
			};
			ctx.Open();
		}
		[Test()]
		public void MethodsExist() {
			// todo
		}
	}
}

