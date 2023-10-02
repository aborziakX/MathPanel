//2020, Andrei Borziak
using System;

namespace MathPanelExt
{
	/// <summary>
	/// class for solving the quadratic equation
	/// </summary>

	public class QuadroEquDemo
	{
		public QuadroEquDemo()
		{
		}
		/// <summary>
		/// calculate roots of quadratic equation
		/// </summary>
		public static void Solve(double a, double b, double c, out double x1, out double x2)
		{
			double discr = Math.Sqrt(b * b - 4 * a * c);
			x1 = (-b - discr) / (2 * a);
			x2 = (-b + discr) / (2 * a);
		}
	}
}