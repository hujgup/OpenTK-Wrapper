using System;
using System.Collections.Generic;
using OpenTK;

namespace Graphics {
	/// <summary>
	/// Defines a set of constants and methods for dealing with Eucidian geometry.
	/// </summary>
	public static class Geometry {
		/// <summary>
		/// The mathematical constant Tau, defined as 2*pi.
		/// </summary>
		public const double TAU = 2*Math.PI;
		/// <summary>
		/// Half of the value of pi.
		/// </summary>
		public const double HalfPI = Math.PI/2;
		/// <summary>
		/// Gets a given number of equally-spaced points along the circumference of a circle.
		/// </summary>
		/// <returns>The points on a circle.</returns>
		/// <param name="center">The center of the circle.</param>
		/// <param name="radius">The radius of the circle.</param>
		/// <param name="numberOfPoints">The number of points to get.</param>
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
		/// <summary>
		/// Gets a given number of equally-spaced points along the circumference of a circle.
		/// </summary>
		/// <returns>The points on a circle.</returns>
		/// <param name="circle">The circle to get the points along.</param> 
		/// <param name="numberOfPoints">The number of points to get.</param>
		public static List<Vector2d> GetPointsOnCircle(ICircle<Vector2d> circle,int numberOfPoints) {
			return GetPointsOnCircle(circle.Center,circle.Radius,numberOfPoints);
		}
		/// <summary>
		/// Gets a set of equally-spaced along the circumference of a circle., with the distance between each point being 1.
		/// </summary>
		/// <returns>The points on a circle.</returns>
		/// <param name="center">The center of the circle.</param>
		/// <param name="radius">The radius of the circle.</param>
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
		/// <summary>
		/// Gets a set of equally-spaced along the circumference of a circle., with the distance between each point being 1.
		/// </summary>
		/// <returns>The points on a circle.</returns>
		/// <param name="circle">The circle to get the points along.</param> 
		public static List<Vector2d> GetPointsOnCircle(ICircle<Vector2d> circle) {
			return GetPointsOnCircle(circle.Center,circle.Radius);
		}
		/// <summary>
		/// Gets the length of the hypoteneuse of an n-dimensional triangle.
		/// </summary>
		/// <param name="sideLengths">The length of each specific side.</param>
		public static double Pythagoras(params double[] sideLengths) {
			double res = 0;
			for (int i = 0; i < sideLengths.Length; i++) {
				res += sideLengths[i]*sideLengths[i];
			}
			return Math.Sqrt(res);
		}
		/// <summary>
		/// Gets the length of the hypoteneuse of an n-dimensional triangle.
		/// </summary>
		/// <param name="sideLengths">The length of each specific side.</param>
		public static double Pythagoras(params float[] sideLengths) {
			return Pythagoras(ArrCnvDouble<float>(sideLengths));
		}
		private static double[] ArrCnvDouble<T>(T[] arr) {
			double[] res = new double[arr.Length];
			for (int i = 0; i < arr.Length; i++) {
				res[i] = Convert.ToDouble(arr[i]);
			}
			return res;
		}
		/// <summary>
		/// Gets the length of the hypoteneuse of an n-dimensional triangle.
		/// </summary>
		/// <param name="sideLengths">The length of each specific side.</param>
		public static double Pythagoras(params decimal[] sideLengths) {
			return Pythagoras(ArrCnvDouble<decimal>(sideLengths));
		}
		/// <summary>
		/// Gets the length of the hypoteneuse of an n-dimensional triangle.
		/// </summary>
		/// <param name="sideLengths">The length of each specific side.</param>
		public static double Pythagoras(params long[] sideLengths) {
			return Pythagoras(ArrCnvDouble<long>(sideLengths));
		}
		/// <summary>
		/// Gets the length of the hypoteneuse of an n-dimensional triangle.
		/// </summary>
		/// <param name="sideLengths">The length of each specific side.</param>
		public static double Pythagoras(params int[] sideLengths) {
			return Pythagoras(ArrCnvDouble<int>(sideLengths));
		}
		/// <summary>
		/// Gets the length of the hypoteneuse of an n-dimensional triangle.
		/// </summary>
		/// <param name="sideLengths">The length of each specific side.</param>
		public static double Pythagoras(params short[] sideLengths) {
			return Pythagoras(ArrCnvDouble<short>(sideLengths));
		}
		/// <summary>
		/// Gets the length of the hypoteneuse of an n-dimensional triangle.
		/// </summary>
		/// <param name="sideLengths">The length of each specific side.</param>
		public static double Pythagoras(params byte[] sideLengths) {
			return Pythagoras(ArrCnvDouble<byte>(sideLengths));
		}
	}
}

