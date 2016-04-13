using System;
using OpenTK;

namespace Graphics.Algebra {
	/// <summary>
	/// Represents a linear equasion.
	/// </summary>
	public class LinearEquasion {
		/// <summary>
		/// Initializes a new instance of the <see cref="Graphics.Algebra.LinearEquasion"/> class.
		/// </summary>
		/// <param name="gradient">The gradient.</param>
		/// <param name="yOffset">The value of y at x = 0.</param>
		public LinearEquasion(double gradient,double yOffset) {
			Gradient = gradient;
			YIntercept = yOffset;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Graphics.Algebra.LinearEquasion"/> class.
		/// </summary>
		/// <param name="gradient">The gradient.</param>
		public LinearEquasion(double gradient) : this(gradient,0) {
		}
		/// <summary>
		/// Gets or sets the gradient.
		/// </summary>
		public double Gradient {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the Y intercept. This value is an alias for the X intercept when the gradient is infinite.
		/// </summary>
		public double YIntercept {
			get;
			set;
		}
		/// <summary>
		/// Gets a value indicating whether this instance is a horizontal line.
		/// </summary>
		public bool IsHorizontalLine {
			get {
				return Gradient == 0;
			}
		}
		/// <summary>
		/// Gets a value indicating whether this instance is a vertical line.
		/// </summary>
		public bool IsVerticalLine {
			get {
				return double.IsInfinity(Gradient);
			}
		}
		/// <summary>
		/// Gets the X intercept.
		/// </summary>
		public double XIntercept {
			get {
				return IsHorizontalLine ? double.NaN : (IsVerticalLine ? YIntercept : -YIntercept/Gradient);
			}
		}
		/// <summary>
		/// Creates a vertical line.
		/// </summary>
		/// <returns>The new line.</returns>
		/// <param name="x">The X intercept of the vertical line.</param>
		public static LinearEquasion VerticalLine(double x) {
			return new LinearEquasion(double.PositiveInfinity,x);
		}
		/// <summary>
		/// Performs exact linear regression.
		/// </summary>
		/// <param name="p1">The first point.</param>
		/// <param name="p2">The second point.</param>
		public static LinearEquasion Regression(Vector2d p1,Vector2d p2) {
			LinearEquasion res;
			double xDiff = p2.X - p1.X;
			if (xDiff == 0) {
				res = VerticalLine(p1.X);
			} else {
				res = new LinearEquasion((p2.Y - p1.Y)/(p2.X - p1.X));
				res.YIntercept = p1.Y - res.GetY(p1.X);
			}
			return res;
		}
		/// <summary>
		/// Gets a line perpendicular to this line that passes through a given point.
		/// </summary>
		/// <returns>A perpendicular line passing throigh a given point.</returns>
		/// <param name="throughPoint">The point that the perpendicular line should pass through.</param>
		public LinearEquasion GetPerpendicularLine(Vector2d throughPoint) {
			LinearEquasion res;
			if (IsVerticalLine) {
				res = new LinearEquasion(0,throughPoint.Y);
			} else if (IsHorizontalLine) {
				res = LinearEquasion.VerticalLine(throughPoint.X);
			} else {
				res = new LinearEquasion(-1/Gradient);
				res.YIntercept = throughPoint.Y - res.GetY(throughPoint.X);
			}
			return res;
		}
		/// <summary>
		/// Gets the intersection point of this line and another line.
		/// </summary>
		/// <param name="eqn">The line to check the intersection point for.</param>
		/// <param name="bound1">The first bound on the location of the intersection point.</param>
		/// <param name="bound2">The second bound on the location of the intersection point.</param>
		public Vector2d Intersect(LinearEquasion eqn,Vector2d bound1,Vector2d bound2) {
			Vector2d res;
			Vector2d inf = new Vector2d(double.PositiveInfinity,double.PositiveInfinity);
			if (IsVerticalLine) {
				if (eqn.IsVerticalLine) {
					res = inf;
				} else {
					res = new Vector2d(XIntercept,eqn.GetY(XIntercept));
				}
			} else if (eqn.IsVerticalLine) {
				if (IsVerticalLine) {
					res = inf;
				} else {
					res = new Vector2d(eqn.XIntercept,GetY(eqn.XIntercept));
				}
			} else {
				double x = (eqn.YIntercept - YIntercept)/(Gradient - eqn.Gradient);
				res = new Vector2d(x,GetY(x));
			}
			if (!res.Equals(inf)) {
				Vector2d lowerBound = new Vector2d(Math.Min(bound1.X,bound2.X),Math.Min(bound1.Y,bound1.Y));
				Vector2d upperBound = new Vector2d(Math.Max(bound1.X,bound2.X),Math.Max(bound1.Y,bound1.Y));
				if (!(res.X >= lowerBound.X && res.X <= upperBound.X && res.Y >= lowerBound.Y && res.Y <= upperBound.Y)) {
					res = new Vector2d(double.NaN,double.NaN);
				}
			}
			return res;
		}
		/// <summary>
		/// Gets the intersection point of this line and another line.
		/// </summary>
		/// <param name="eqn">The line to check the intersection point for.</param>
		public Vector2d Intersect(LinearEquasion eqn) {
			return Intersect(eqn,new Vector2d(double.NegativeInfinity,double.NegativeInfinity),new Vector2d(double.PositiveInfinity,double.PositiveInfinity));
		}
		/// <summary>
		/// Gets the Y value of this equasion at the given value for X.
		/// </summary>
		/// <returns>The Y value.</returns>
		/// <param name="x">The X value.</param>
		public double GetY(double x) {
			double res;
			if (IsVerticalLine) {
				if (x == XIntercept) {
					res = double.PositiveInfinity;
				} else {
					res = double.NaN;
				}
			} else {
				res = Gradient*x + YIntercept;
			}
			return res;
		}
		/// <summary>
		/// Checks whether a given point is on this line.
		/// </summary>
		/// <returns><c>true</c> the point was on this line, <c>false</c> otherwise.</returns>
		/// <param name="point">The point to check.</param>
		/// <param name="thickness">The thickness of this line.</param>
		public bool PointOnLine(Vector2d point,float thickness) {
			double y = GetY(point.X);
			bool res = y == point.Y;
			if (y != point.Y && thickness > 0) {
				LinearEquasion perpen = GetPerpendicularLine(point);
				Vector2d intersect = Intersect(perpen);
				res = Vector2d.Subtract(intersect,point).Length <= thickness;
			}
			return res;
		}
		/// <summary>
		/// Checks whether a given point is on this line.
		/// </summary>
		/// <returns><c>true</c> the point was on this line, <c>false</c> otherwise.</returns>
		/// <param name="point">The point to check.</param>
		public bool PointOnLine(Vector2d point) {
			return PointOnLine(point,0);
		}
	}
}

