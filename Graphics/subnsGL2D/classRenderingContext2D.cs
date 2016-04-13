using System;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics.GL2D {
	public class RenderingContext2D : RenderingContext {
		private static readonly GraphicsMode _DefaultMode = new GraphicsMode(32,24,0,4);
		public RenderingContext2D(double logicTickRate,double renderTickRate,int windowWidth,int windowHeight,GraphicsMode mode,string title,bool fullscreen) : base(logicTickRate,renderTickRate,windowWidth,windowHeight,1d,mode,title,fullscreen) {
		}
		public RenderingContext2D(double logicTickRate,double renderTickRate,int windowWidth,int windowHeight,GraphicsMode mode,string title) : this(logicTickRate,renderTickRate,windowWidth,windowHeight,mode,title,false) {
		}
		public RenderingContext2D(double logicTickRate,double renderTickRate,int windowWidth,int windowHeight,string title) : this(logicTickRate,renderTickRate,windowWidth,windowHeight,_DefaultMode,title) {
		}
		public RenderingContext2D(double logicTickRate,double renderTickRate,int windowWidth,int windowHeight,GraphicsMode mode) : this(logicTickRate,renderTickRate,windowWidth,windowHeight,mode,"Graphics Window") {
		}
		public RenderingContext2D(double logicTickRate,double renderTickRate,int windowWidth,int windowHeight) : this(logicTickRate,renderTickRate,windowWidth,windowHeight,_DefaultMode) {
		}
	}
}

