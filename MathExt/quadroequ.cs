//2020, Andrei Borziak
using System;
using MathPanel;

namespace MathPanelExt
{
	/// <summary>
	/// класс для решения квадратного уравнения и рисования примитивов (на основе подготовки данных в вывода через JSON в GRAPHIX
	/// </summary>

	public class QuadroEqu
	{
		public QuadroEqu()
		{
		}
		/// <summary>
		/// корни квадратного уравнения
		/// </summary>
		public static void Solve(double a, double b, double c, out double x1, out double x2)
		{
			double discr = Math.Sqrt(b * b - 4 * a * c);
			x1 = (-b - discr) / (2 * a);
			x2 = (-b + discr) / (2 * a);
		}

		/// <summary>
		/// подготовки данных для квадратного уравнения
		/// </summary>
		public static string DrawRange(double a, double b, double c, double x0, double x1, int n)
		{
			string s = "{{\"x\":{0},\"y\":{1}}}";

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			double step = (x1 - x0) / n;
			double x = x0;
			for (int i = 0; i <= n; i++, x += step)
			{
				var y = x * (a * x + b) + c;
				if (i != 0) sb.Append(",");
				sb.AppendFormat(s, Dynamo.D2S(x), Dynamo.D2S(y));
			}
			return sb.ToString();
		}
		/// <summary>
		/// подготовки данных для эллипса
		/// </summary>
		public static string DrawEllipse(double a, double b, double x0, double y0, double fi0, double fi1, int n)
		{
			string s = "{{\"x\":{0},\"y\":{1}}}";

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			double step = (fi1 - fi0) / n;
			double fi = fi0;
			for (int i = 0; i <= n; i++, fi += step)
			{
				var x = x0 + a * Math.Cos(fi);
				var y = y0 + b * Math.Sin(fi);
				if (i != 0) sb.Append(",");
				sb.AppendFormat(s, Dynamo.D2S(x), Dynamo.D2S(y));
			}
			return sb.ToString();
		}
		/// <summary>
		/// подготовки данных для линии
		/// </summary>
		public static string DrawLine(double x0, double y0, double x1, double y1)
		{
			string s = "{{\"x\":{0},\"y\":{1}}}";

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i <= 1; i++)
			{
				var x = i == 0 ? x0 : x1;
				var y = i == 0 ? y0 : y1;
				if (i != 0) sb.Append(",");
				sb.AppendFormat(s, Dynamo.D2S(x), Dynamo.D2S(y));
			}
			return sb.ToString();
		}

		/// <summary>
		/// вычилить точку Bezier 1-го уровня для заданного шага
		/// </summary>
		static double Bezier1(double x0, double x1, double step)
		{
			return x0 + (x1 - x0) * step;
		}
		/// <summary>
		/// вычилить точку Bezier 2-го уровня для заданного шага
		/// </summary>
		static double Bezier2(double x0, double x1, double x2, double step)
		{
			double p0 = Bezier1(x0, x1, step);
			double p1 = Bezier1(x1, x2, step);
			return Bezier1(p0, p1, step);
		}
		/// <summary>
		/// вычилить точку Bezier 3-го уровня для заданного шага
		/// </summary>
		static double Bezier3(double x0, double x1, double x2, double x3, double step)
		{
			double p0 = Bezier2(x0, x1, x1, step);
			double p1 = Bezier2(x1, x2, x3, step);
			return Bezier1(p0, p1, step);
		}

		/// <summary>
		/// подготовки данных Bezier 2-го уровня
		/// </summary>
		public static string DrawBezier2(double x0, double y0, double x1, double y1, double x2, double y2, int n)
		{
			string s = "{{\"x\":{0},\"y\":{1}}}";

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i <= n; i++)
			{
				double step = ((double)i) / n;
				var x = Bezier2(x0, x1, x2, step);
				var y = Bezier2(y0, y1, y2, step);
				if (i != 0) sb.Append(",");
				sb.AppendFormat(s, Dynamo.D2S(x), Dynamo.D2S(y));
			}
			return sb.ToString();
		}

		/// <summary>
		/// подготовки данных Bezier 3-го уровня
		/// </summary>
		public static string DrawBezier3(double x0, double y0, double x1, double y1, double x2, double y2, double x3, double y3, int n)
		{
			string s = "{{\"x\":{0},\"y\":{1}}}";

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i <= n; i++)
			{
				double step = ((double)i) / n;
				var x = Bezier3(x0, x1, x2, x3, step);
				var y = Bezier3(y0, y1, y2, y3, step);
				if (i != 0) sb.Append(",");
				sb.AppendFormat(s, Dynamo.D2S(x), Dynamo.D2S(y));
			}
			return sb.ToString();
		}
		/// <summary>
		/// заполняет цветами ячейки таблицы типа битмап
		/// </summary>
		public static string DrawBitmap(int rows, int cols, System.Drawing.Color[] clrs, 
			int xShift = 0, int yShift = 0, bool bFromBottom = true)
		{
			string s = "{{\"x\":{0},\"y\":{1},\"clr\":\"{2}\"}}";
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			for (int i = 0; i < clrs.Length && i < rows * cols; i++)
			{
				var x = i % cols + xShift;
				var y = bFromBottom ? (i / cols + yShift) : (rows - 1 - i / cols + yShift);
				if (i != 0) sb.Append(",");
				sb.AppendFormat(s, Dynamo.D2S(x), Dynamo.D2S(y), MathPanel.Facet3.ColorHtml(clrs[i]));
			}
			return sb.ToString();
		}

		/// <summary>
		/// подготовки данных рисования текста
		/// </summary>
		public static string DrawText(double x, double y, string text)
		{
			string s = "{{\"x\":{0},\"y\":{1}, \"txt\":\"{2}\", \"sty\":\"circle\"}}";
			return string.Format(s, Dynamo.D2S(x), Dynamo.D2S(y), text);
		}

		/// <summary>
		/// подготовки данных для точки
		/// </summary>
		public static string DrawPoint(double x, double y, string text, string style)
		{
			string s = "{{\"x\":{0},\"y\":{1}, \"txt\":\"{2}\", \"sty\":\"{3}\"}}";
			return string.Format(s, Dynamo.D2S(x), Dynamo.D2S(y), text, style);
		}
	}
}