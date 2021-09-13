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
		//форматы вывода
		static readonly string sXY = "{{\"x\":{0},\"y\":{1}}}";
		static readonly string sClr = "{{\"x\":{0},\"y\":{1},\"clr\":\"{2}\"}}";
		static readonly string sTxt = "{{\"x\":{0},\"y\":{1}, \"txt\":\"{2}\", \"sty\":\"{3}\"}}";
		static readonly string sFull =
			"{{\"x\":{0},\"y\":{1}, \"txt\":\"{2}\", \"sty\":\"{3}\", \"clr\":\"{4}\", \"rad\":\"{5}\", \"fontsize\":\"{6}\"}}";

		/// <summary>
		/// подготовка данных для квадратного уравнения
		/// </summary>
		public static string DrawRange(double a, double b, double c, double x0, double x1, int n)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			double step = (x1 - x0) / n;
			double x = x0;
			for (int i = 0; i <= n; i++, x += step)
			{
				var y = x * (a * x + b) + c;
				if (i != 0) sb.Append(",");
				sb.AppendFormat(sXY, Dynamo.D2S(x), Dynamo.D2S(y));
			}
			return sb.ToString();
		}
		/// <summary>
		/// подготовка данных для эллипса
		/// </summary>
		public static string DrawEllipse(double a, double b, double x0, double y0, double fi0, double fi1, int n)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			double step = (fi1 - fi0) / n;
			double fi = fi0;
			for (int i = 0; i <= n; i++, fi += step)
			{
				var x = x0 + a * Math.Cos(fi);
				var y = y0 + b * Math.Sin(fi);
				if (i != 0) sb.Append(",");
				sb.AppendFormat(sXY, Dynamo.D2S(x), Dynamo.D2S(y));
			}
			return sb.ToString();
		}
		/// <summary>
		/// подготовка данных для линии
		/// </summary>
		public static string DrawLine(double x0, double y0, double x1, double y1)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i <= 1; i++)
			{
				var x = i == 0 ? x0 : x1;
				var y = i == 0 ? y0 : y1;
				if (i != 0) sb.Append(",");
				sb.AppendFormat(sXY, Dynamo.D2S(x), Dynamo.D2S(y));
			}
			return sb.ToString();
		}

		/// <summary>
		/// подготовка данных для линии с наконечником
		/// </summary>
		public static string DrawArrow(double x0, double y0, double x1, double y1, int szArr)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			//линия
			for (int i = 0; i <= 1; i++)
			{
				var x = i == 0 ? x0 : x1;
				var y = i == 0 ? y0 : y1;
				if (i != 0) sb.Append(",");
				sb.AppendFormat(sXY, Dynamo.D2S(x), Dynamo.D2S(y));
			}
			
			if( x0 != x1 || y0 != y1 )
			{   //рисуем наконечник
				//вектор длиной szArr
				double dx = (x0 - x1);
				double dy = (y0 - y1);
				double dlen = Math.Sqrt(dx * dx + dy * dy);
				dx = (dx * szArr) / dlen;
				dy = (dy * szArr) / dlen;

				//отступить от конечной точки
				double xBeg = x1 + dx;
				double yBeg = y1 + dy;

				//нормаль 1
				double xNorm1 = -dy;
				double yNorm1 = dx;
				//точка 1 вдоль нормали 1
				double xPoint1 = xBeg + xNorm1 * 0.4;
				double yPoint1 = yBeg + yNorm1 * 0.4;
				//к точке 1
				sb.Append(",");
				sb.AppendFormat(sXY, Dynamo.D2S(xPoint1), Dynamo.D2S(yPoint1));
				//назад
				//sb.Append(",");
				//sb.AppendFormat(sXY, Dynamo.D2S(x1), Dynamo.D2S(y1));

				//нормаль 2
				double xNorm2 = dy;
				double yNorm2 = -dx;
				//точка 2 вдоль нормали 2
				double xPoint2 = xBeg + xNorm2 * 0.4;
				double yPoint2 = yBeg + yNorm2 * 0.4;
				//к точке 2
				sb.Append(",");
				sb.AppendFormat(sXY, Dynamo.D2S(xPoint2), Dynamo.D2S(yPoint2));
				//назад
				sb.Append(",");
				sb.AppendFormat(sXY, Dynamo.D2S(x1), Dynamo.D2S(y1));
			}
			return sb.ToString();
		}

		public static string DrawArrow(double x0, double y0, double x1, double y1)
		{
			return DrawArrow(x0, y0, x1, y1, 5);
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
		/// подготовка данных Bezier 2-го уровня
		/// </summary>
		public static string DrawBezier2(double x0, double y0, double x1, double y1, double x2, double y2, int n)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i <= n; i++)
			{
				double step = ((double)i) / n;
				var x = Bezier2(x0, x1, x2, step);
				var y = Bezier2(y0, y1, y2, step);
				if (i != 0) sb.Append(",");
				sb.AppendFormat(sXY, Dynamo.D2S(x), Dynamo.D2S(y));
			}
			return sb.ToString();
		}

		/// <summary>
		/// подготовка данных Bezier 3-го уровня
		/// </summary>
		public static string DrawBezier3(double x0, double y0, double x1, double y1, double x2, double y2, double x3, double y3, int n)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i <= n; i++)
			{
				double step = ((double)i) / n;
				var x = Bezier3(x0, x1, x2, x3, step);
				var y = Bezier3(y0, y1, y2, y3, step);
				if (i != 0) sb.Append(",");
				sb.AppendFormat(sXY, Dynamo.D2S(x), Dynamo.D2S(y));
			}
			return sb.ToString();
		}
		/// <summary>
		/// заполняет цветами ячейки таблицы типа битмап
		/// </summary>
		public static string DrawBitmap(int rows, int cols, System.Drawing.Color[] clrs, 
			int xShift = 0, int yShift = 0, bool bFromBottom = true)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			for (int i = 0; i < clrs.Length && i < rows * cols; i++)
			{
				var x = i % cols + xShift;
				var y = bFromBottom ? (i / cols + yShift) : (rows - 1 - i / cols + yShift);
				if (i != 0) sb.Append(",");
				sb.AppendFormat(sClr, Dynamo.D2S(x), Dynamo.D2S(y), MathPanel.Facet3.ColorHtml(clrs[i]));
			}
			return sb.ToString();
		}

		/// <summary>
		/// подготовка данных рисования текста
		/// </summary>
		public static string DrawText(double x, double y, string text)
		{
			return string.Format(sTxt, Dynamo.D2S(x), Dynamo.D2S(y), text, "circle");
		}

		/// <summary>
		/// подготовка данных для точки
		/// </summary>
		public static string DrawPoint(double x, double y, string text, string style)
		{
			return string.Format(sTxt, Dynamo.D2S(x), Dynamo.D2S(y), text, style);
		}
		/// <summary>
		/// подготовка данных для точки, все параметры
		/// </summary>
		public static string DrawPoint(double x, double y, string text, string style, string clr, string pointsize, string fontsize)
		{
			return string.Format(sFull, Dynamo.D2S(x), Dynamo.D2S(y), text, style, clr, pointsize, fontsize); 
		}

		/// <summary>
		/// подготовка данных для графика
		/// </summary>
		public static string DrawGraphic(double x0, double x1, double[] yArr, int n)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			double step = (x1 - x0) / (n > 1 ? n - 1 : 1);
			double x = x0;
			for (int i = 0; i < n; i++, x += step)
			{
				var y = yArr[i];
				if (i != 0) sb.Append(",");
				sb.AppendFormat(sXY, Dynamo.D2S(x), Dynamo.D2S(y));
			}
			return sb.ToString();
		}

		/// <summary>
		/// подготовка данных для прямоугольника
		/// </summary>
		public static string DrawRect(double x0, double y0, double x1, double y1, bool bFill)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.AppendFormat(sXY, Dynamo.D2S(x0), Dynamo.D2S(y0));

			sb.Append(",");
			sb.AppendFormat(sXY, Dynamo.D2S(x1), Dynamo.D2S(y0));

			sb.Append(",");
			sb.AppendFormat(sXY, Dynamo.D2S(x1), Dynamo.D2S(y1));

			sb.Append(",");
			if (bFill) sb.AppendFormat(sTxt, Dynamo.D2S(x0), Dynamo.D2S(y1), "", "line_endf");
			else
			{
				sb.AppendFormat(sXY, Dynamo.D2S(x0), Dynamo.D2S(y1));
				sb.Append(",");
				sb.AppendFormat(sTxt, Dynamo.D2S(x0), Dynamo.D2S(y0), "", "line_end");
			}

			return sb.ToString();
		}
	}
}