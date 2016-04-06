using System;
using OpenTK;

namespace Graphics {
	public class LinearEquasion {
		public LinearEquasion(double gradient,double yOffset) {
			Gradient = gradient;
			YIntercept = yOffset;
		}
		public LinearEquasion(double gradient) : this(gradient,0) {
		}
		public double Gradient {
			get;
			set;
		}
		public double YIntercept {
			get;
			set;
		}
		public bool IsHorizontalLine {
			get {
				return Gradient == 0;
			}
		}
		public bool IsVerticalLine {
			get {
				return double.IsInfinity(Gradient);
			}
		}
		public double XIntercept {
			get {
				return IsHorizontalLine ? double.NaN : (IsVerticalLine ? YIntercept : -YIntercept/Gradient);
			}
		}
		public static LinearEquasion VerticalLine(double x) {
			return new LinearEquasion(double.PositiveInfinity,x);
		}
		public static LinearEquasion Regression(Vector2d p1,Vector2d p2) {
			double xDiff = p2.X - p1.X;
			LinearEquasion res;
			if (xDiff == 0) {
				res = VerticalLine(p1.X);
			} else {
				res = new LinearEquasion((p2.Y - p1.Y)/(p2.X - p1.X));
				res.YIntercept = p1.Y - res.GetY(p1.X);
			}
			return res;
		}
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
		public Vector2d Intersect(LinearEquasion eqn) {
			return Intersect(eqn,new Vector2d(double.NegativeInfinity,double.NegativeInfinity),new Vector2d(double.PositiveInfinity,double.PositiveInfinity));
		}
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
		public bool PointOnLine(Vector2d point) {
			return PointOnLine(point,0);
		}
	}
}

