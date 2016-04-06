using System;
using System.Collections.Generic;
using OpenTK;

namespace Graphics {
	public static class Geometry {
		public const double TAU = 2*Math.PI;
		public const double HalfPI = Math.PI/2;
		public static List<Vector2d> GetPointsOnCircle(Vector2d center,double radius,int numberOfPoints) {
			List<Vector2d> res = new List<Vector2d>(numberOfPoints);
			double angle = 0;
			double precession = TAU/numberOfPoints;
			int pointsAdded = 0;
			while (pointsAdded < numberOfPoints) {
				res.Add(new Vector2d(center.X + Math.Cos(angle),center.Y + Math.Sin(angle)));
				angle += precession;
				pointsAdded++;
			}
			return res;
		}
		public static List<Vector2d> GetPointsOnCircle(Vector2d center,double radius) {
			List<Vector2d> res = new List<Vector2d>((int)Math.Ceiling(TAU/radius));
			// c = 2*pi*r
			// 1 = x*r
			// x = r
			double angle = 0;
			while (angle < TAU) {
				res.Add(new Vector2d(center.X + Math.Cos(angle),center.Y + Math.Sin(angle)));
				angle += radius;
			}
			return res;
		}
	}
}

