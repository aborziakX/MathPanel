//2020, Andrei Borziak
using System;
using MathPanel;

namespace MathPanelExt
{
	//опции рисования в методах
	public class DrawOpt
    {
		//стиль, текст, цвет, размер, размер шрифта, высота гисто, ширина линии, цвет линии
		public string sty = "", txt = "", clr = "", rad = "", fontsize = "", hei = "", lnw = "", csk = "";
		public bool bFill = false;//заливать
		//матрица трансформации и сдвиг
		public double a11 = 1, a12 = 0, a21 = 0, a22 = 1, xTrans = 0, yTrans = 0;
		//преобразовать координаты в соответствии с матрицей трансформации
		public void Transform(ref double x, ref double y)
        {
			if (a11 == 1 && a12 == 0 && a21 == 0 && a22 == 1) return;
			double x_n = a11 * (x - xTrans) + a12 * (y - yTrans);
			double y_n = a21 * (x - xTrans) + a22 * (y - yTrans);
			x = x_n + xTrans;
			y = y_n + yTrans;
		}
		//трансформация - поворот на угол fi против часовой стрелки
		public void Rotor(double fi)
        {
			a11 = Math.Cos(fi);
			a12 = -Math.Sin(fi);
			a21 = Math.Sin(fi);
			a22 = Math.Cos(fi);
		}
	}

	/// <summary>
	/// класс для решения квадратного уравнения и рисования примитивов (на основе подготовки данных для вывода через JSON в GRAPHIX)
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
		static readonly string sFull_2 =
			"{{\"x\":{0},\"y\":{1}, \"txt\":\"{2}\", \"sty\":\"{3}\", \"clr\":\"{4}\", \"rad\":\"{5}\", \"fontsize\":\"{6}\", \"hei\":\"{7}\", \"lnw\":\"{8}\", \"csk\":\"{9}\"}}";

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
		public static string DrawEllipse(double a, double b, double x0, double y0, double fi0, double fi1, int n, DrawOpt opt = null)
		{
			string txt = "", sty = "line", clr = "undefined", pointsize = "undefined", fontsize = "undefined",
				hei = "undefined", lnw = "undefined", csk = "undefined";
			bool bFill = false;
			if (opt != null)
			{
				bFill = opt.bFill;
				sty = opt.sty;
				if (opt.clr != "") clr = opt.clr;
				if (opt.csk != "") csk = opt.csk;
				if (opt.rad != "") pointsize = opt.rad;
				if (opt.lnw != "") lnw = opt.lnw;
			}

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			double step = (fi1 - fi0) / n;
			double fi = fi0;
			for (int i = 0; i <= n; i++, fi += step)
			{
				var x = x0 + a * Math.Cos(fi);
				var y = y0 + b * Math.Sin(fi);
				if (opt != null) opt.Transform(ref x, ref y);
				if (i == n && sty == "line")
					sty = bFill ? "line_endf" : "line_end";
				if (i != 0) sb.Append(",");
				sb.AppendFormat(sFull_2, Dynamo.D2S(x), Dynamo.D2S(y), txt, sty, clr, pointsize, fontsize,
					hei, lnw, csk);
			}
			return sb.ToString();
		}

		/// <summary>
		/// подготовка данных для линии
		/// </summary>
		public static string DrawLine(double x0, double y0, double x1, double y1, DrawOpt opt = null)
		{
			string txt = "", sty = "line", clr = "undefined", pointsize = "undefined", fontsize = "undefined",
				hei = "undefined", lnw = "undefined", csk = "undefined";
			bool bFill = false;
			if (opt != null)
			{
				bFill = opt.bFill;
				sty = opt.sty;
				if (opt.clr != "") clr = opt.clr;
				if (opt.csk != "") csk = opt.csk;
				if (opt.lnw != "") lnw = opt.lnw;
				if (opt.rad != "") pointsize = opt.rad;
			}

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i <= 1; i++)
			{
				var x = i == 0 ? x0 : x1;
				var y = i == 0 ? y0 : y1;
				if (opt != null) opt.Transform(ref x, ref y);
				if (i == 1 && sty == "line")
					sty = bFill ? "line_endf" : "line_end";
				if (i != 0) sb.Append(",");
				sb.AppendFormat(sFull_2, Dynamo.D2S(x), Dynamo.D2S(y), txt, sty, clr, pointsize, fontsize,
					hei, lnw, csk);
			}
			return sb.ToString();
		}

		/// <summary>
		/// подготовка данных для линии с наконечником
		/// </summary>
		public static string DrawArrow(double x0, double y0, double x1, double y1, int szArr, DrawOpt opt = null)
		{
			string txt = "", sty = "line", clr = "undefined", pointsize = "undefined", fontsize = "undefined",
				hei = "undefined", lnw = "undefined", csk = "undefined";
			bool bFill = false;
			if (opt != null)
			{
				bFill = opt.bFill;
				sty = opt.sty;
				if (opt.clr != "") clr = opt.clr;
				if (opt.csk != "") csk = opt.csk;
				if (opt.lnw != "") lnw = opt.lnw;
				if (opt.rad != "") pointsize = opt.rad;
			}

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			double x = 0, y = 0;
			//линия
			for (int i = 0; i <= 1; i++)
			{
				x = i == 0 ? x0 : x1;
				y = i == 0 ? y0 : y1;
				if (opt != null) opt.Transform(ref x, ref y);
				if (i != 0) sb.Append(",");
				sb.AppendFormat(sFull_2, Dynamo.D2S(x), Dynamo.D2S(y), txt, sty, clr, pointsize, fontsize,
					hei, lnw, csk);
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
				x = xPoint1;
				y = yPoint1;
				if (opt != null) opt.Transform(ref x, ref y);
				sb.Append(",");
				sb.AppendFormat(sFull_2, Dynamo.D2S(x), Dynamo.D2S(y), txt, sty, clr, pointsize, fontsize,
					hei, lnw, csk);
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
				x = xPoint2;
				y = yPoint2;
				if (opt != null) opt.Transform(ref x, ref y);
				sb.Append(",");
				sb.AppendFormat(sFull_2, Dynamo.D2S(x), Dynamo.D2S(y), txt, sty, clr, pointsize, fontsize,
					hei, lnw, csk);
				
				//назад
				if (sty == "line")
					sty = bFill ? "line_endf" : "line_end";
				x = x1;
				y = y1;
				if (opt != null) opt.Transform(ref x, ref y);
				sb.Append(",");
				sb.AppendFormat(sFull_2, Dynamo.D2S(x), Dynamo.D2S(y), txt, sty, clr, pointsize, fontsize,
					hei, lnw, csk);
			}
			return sb.ToString();
		}

		public static string DrawArrow(double x0, double y0, double x1, double y1, DrawOpt opt = null)
		{
			return DrawArrow(x0, y0, x1, y1, 5, opt);
		}

		/// <summary>
		/// вычиcлить точку Bezier 1-го уровня для заданного шага
		/// </summary>
		static double Bezier1(double x0, double x1, double step)
		{
			return x0 + (x1 - x0) * step;
		}

		/// <summary>
		/// вычиcлить точку Bezier 2-го уровня для заданного шага
		/// </summary>
		static double Bezier2(double x0, double x1, double x2, double step)
		{
			double p0 = Bezier1(x0, x1, step);
			double p1 = Bezier1(x1, x2, step);
			return Bezier1(p0, p1, step);
		}

		/// <summary>
		/// вычиcлить точку Bezier 3-го уровня для заданного шага
		/// </summary>
		static double Bezier3(double x0, double x1, double x2, double x3, double step)
		{
			double p0 = Bezier2(x0, x1, x2, step);
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

			for (int i = 0; i < rows * cols; i++)
			{
				var x = i % cols + xShift;
				var y = bFromBottom ? (i / cols + yShift) : (rows - 1 - i / cols + yShift);
				if (i != 0) sb.Append(",");
				sb.AppendFormat(sClr, Dynamo.D2S(x), Dynamo.D2S(y), MathPanel.Facet3.ColorHtml(clrs[i % clrs.Length]));
			}
			return sb.ToString();
		}

		/// <summary>
		/// подготовка данных рисования текста
		/// </summary>
		public static string DrawText(double x, double y, string text)
		{
			return string.Format(sTxt, Dynamo.D2S(x), Dynamo.D2S(y), text, "text");
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
		public static string DrawGraphic(double x0, double x1, double[] yArr, DrawOpt opt = null)
		{
			string txt = "", sty = "line", clr = "undefined", pointsize = "undefined", fontsize = "undefined",
				hei = "undefined", lnw = "undefined", csk = "undefined";
			bool bFill = false;
			if (opt != null)
			{
				bFill = opt.bFill;
				sty = opt.sty;
				if (opt.clr != "") clr = opt.clr;
				if (opt.csk != "") csk = opt.csk;
				if (opt.rad != "") pointsize = opt.rad;
				if (opt.lnw != "") lnw = opt.lnw;
			}

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			int n = yArr.Length;
			double step = (x1 - x0) / (n > 1 ? n - 1 : 1);
			for (int i = 0; i < n; i++)
			{
				double x = x0 + step * i;
				double y = yArr[i];
				if (opt != null) opt.Transform(ref x, ref y);
				if (i == n - 1 && sty == "line")
					sty = bFill ? "line_endf" : "line_end";
				if (i != 0) sb.Append(",");
				sb.AppendFormat(sFull_2, Dynamo.D2S(x), Dynamo.D2S(y), txt, sty, clr, pointsize, fontsize,
					hei, lnw, csk);
			}
			return sb.ToString();
		}

		/// <summary>
		/// подготовка данных для графика
		/// </summary>
		public static string DrawGraphic(double[] xArr, double[] yArr, DrawOpt opt = null)
		{
			string txt = "", sty = "line", clr = "undefined", pointsize = "undefined", fontsize = "undefined",
				hei = "undefined", lnw = "undefined", csk = "undefined";
			bool bFill = false;
			if (opt != null)
			{
				bFill = opt.bFill;
				sty = opt.sty;
				if (opt.clr != "") clr = opt.clr;
				if (opt.csk != "") csk = opt.csk;
				if (opt.rad != "") pointsize = opt.rad;
				if (opt.lnw != "") lnw = opt.lnw;
			}

			int n = yArr.Length;
			if (n != xArr.Length) return "";

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < n; i++)
			{
				double x = xArr[i];
				double y = yArr[i];
				if (opt != null) opt.Transform(ref x, ref y);
				if (i == n - 1 && sty == "line")
					sty = bFill ? "line_endf" : "line_end";
				if (i != 0) sb.Append(",");
				sb.AppendFormat(sFull_2, Dynamo.D2S(x), Dynamo.D2S(y), txt, sty, clr, pointsize, fontsize,
					hei, lnw, csk);
			}
			return sb.ToString();
		}

		/// <summary>
		/// подготовка данных для прямоугольника
		/// </summary>
		public static string DrawRect(double x0, double y0, double x1, double y1, DrawOpt opt = null)
		{
			string txt = "", sty = "line", clr = "undefined", rad = "undefined", fontsize = "undefined",
				hei = "undefined", lnw = "undefined", csk = "undefined";
			bool bFill = false;
			if (opt != null)
			{
				bFill = opt.bFill;
				sty = opt.sty;
				if (opt.clr != "") clr = opt.clr;
				if (opt.csk != "") csk = opt.csk;
				if (opt.rad != "") rad = opt.rad;
				if (opt.lnw != "") lnw = opt.lnw;
			}

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			double x = 0, y = 0;
			for (int i = 0; i <= 4; i++)
			{
				if (i == 0)
				{
					x = x0;
					y = y0;
				}
				else if (i == 1)
				{
					x = x1;
					y = y0;
				}
				else if (i == 2)
				{
					x = x1;
					y = y1;
				}
				else if (i == 3)
				{
					x = x0;
					y = y1;
				}
				else if (i == 4)
				{
					x = x0;
					y = y0;
					if (sty == "line")
						sty = bFill ? "line_endf" : "line_end";
				}
				if (opt != null) opt.Transform(ref x, ref y);
				if( i > 0 ) sb.Append(",");
				sb.AppendFormat(sFull_2, Dynamo.D2S(x), Dynamo.D2S(y), txt, sty, clr, rad, fontsize,
					hei, lnw, csk);
			}
			return sb.ToString();
		}

		/// <summary>
		/// подготовка данных для прямоугольника
		/// </summary>
		/// <param name="bFill">true=заливать</param>
		public static string DrawRect(double x0, double y0, double x1, double y1, bool bFill)
		{
			string txt = "", sty = "line", clr = "undefined", rad = "undefined", fontsize = "undefined",
				hei = "undefined", lnw = "undefined", csk = "undefined";

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			double x = 0, y = 0;
			for (int i = 0; i <= 4; i++)
			{
				if (i == 0)
				{
					x = x0;
					y = y0;
				}
				else if (i == 1)
				{
					x = x1;
					y = y0;
				}
				else if (i == 2)
				{
					x = x1;
					y = y1;
				}
				else if (i == 3)
				{
					x = x0;
					y = y1;
				}
				else if (i == 4)
				{
					x = x0;
					y = y0;
					if (sty == "line")
						sty = bFill ? "line_endf" : "line_end";
				}
				if (i > 0) sb.Append(",");
				sb.AppendFormat(sFull_2, Dynamo.D2S(x), Dynamo.D2S(y), txt, sty, clr, rad, fontsize,
					hei, lnw, csk);
			}
			return sb.ToString();
		}

		/// <summary>
		/// подготовка данных для звезды
		/// </summary>
		public static string DrawStar(double rBig, double r0, double x0, double y0, int n, DrawOpt opt = null)
		{
			string txt = "", sty = "line", clr = "undefined", pointsize = "undefined", fontsize = "undefined",
				hei = "undefined", lnw = "undefined", csk = "undefined";
			double fiBeg = Math.PI * 0.5;
			bool bFill = false;
			if (opt != null)
			{
				bFill = opt.bFill;
				sty = opt.sty;
				if (opt.clr != "") clr = opt.clr;
				if (opt.csk != "") csk = opt.csk;
				if (opt.rad != "") pointsize = opt.rad;
				if (opt.lnw != "") lnw = opt.lnw;
			}

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			double step = (Math.PI * 1) / n;
			double step_2 = (Math.PI * 1) / (n);
			double fi = fiBeg;
			double fi_2 = fiBeg + step_2;
			for (int i = 0; i <= n; i++)
			{
				if (i == n)
				{
					if(sty == "line")
						sty = bFill ? "line_endf" : "line_end";
					fi = fiBeg;
				}
				var x = x0 + rBig * Math.Cos(fi);
				var y = y0 + rBig * Math.Sin(fi);
				if (opt != null) opt.Transform(ref x, ref y);
				if (i != 0) sb.Append(",");
				sb.AppendFormat(sFull_2, Dynamo.D2S(x), Dynamo.D2S(y), txt, sty, clr, pointsize, fontsize,
					hei, lnw, csk);
				if (i == n) break;
				fi += 2 * step;

				x = x0 + r0 * Math.Cos(fi_2);
				y = y0 + r0 * Math.Sin(fi_2);
				if (opt != null) opt.Transform(ref x, ref y);
				sb.Append(",");
				sb.AppendFormat(sFull_2, Dynamo.D2S(x), Dynamo.D2S(y), txt, sty, clr, pointsize, fontsize,
					hei, lnw, csk);
				fi_2 += 2 * step;
			}
			return sb.ToString();
		}
	}
}