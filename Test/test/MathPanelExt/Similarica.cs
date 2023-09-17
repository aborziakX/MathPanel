using System;
using System.Collections.Generic;
using System.Text;

namespace MathPanelExt
{
	//простой класс для выравнивания строк и объектов, вычисления похожести
	public class Similarica
	{
		//шаблон делегата для вычисления разности между объектами
		public delegate double CalcDifference(object x, object y);
		//члены класса
		int m_width, m_height;	//ширина и высота таблицы
		double[] m_weight;		//веса или промежуточные результаты ячеек
		int[] m_from;	//наилучшие родители
		int m_iType;    //режим: 0-сравнить строки, 1-массивы строк, 2-массивы объектов
		string m_sWid, m_sHei; //строки для сравнения
		string[] m_sliWid;  //массивы для сравнения
		string[] m_sliHei;
		object[] m_objWid;  //объекты для сравнения
		object[] m_objHei;
		double m_dMax;	//итоговый результат
		//параметры для изменения на ходу
		public double m_dSkipFee = -1.0;    //штраф за пропуск
		public double m_dMisFee = -1.0;     //штраф за несопадение
		public double m_dMatchFee = 1.0;    //штраф за сопадение
		public bool bNoSpace = false;	//удалять пробелы из строк
		public CalcDifference pc_hand = null;	//обработчик для сравнения объектов
		//простой конструктор
		public Similarica()
		{
			m_dMax = 0;
			m_iType = -1;
		}

		//сравнить 2 строки
		public double Calc(string sWid, string sHei)
		{
			//копировать данные
			m_sWid = " " + sWid;    //' ' для начальной строки
			m_sHei = " " + sHei;
			m_iType = 0;

			//выделить место для весов и путей
			m_height = m_sHei.Length;
			m_width = m_sWid.Length;
			m_weight = new double[m_height * m_width];
			m_from = new int[m_height * m_width];

			//для красивого вывода
			m_sliWid = new string[m_sWid.Length];
			for (int i = 0; i < m_sWid.Length; i++)
				m_sliWid[i] = m_sWid.Substring(i, 1);

			m_sliHei = new string[m_sHei.Length];
			for (int i = 0; i < m_sHei.Length; i++)
				m_sliHei[i] = m_sHei.Substring(i, 1);

			//найти веса и наилучший путь
			return DoCalc();
		}

		//сравнить 2 массива строк
		public double Calc(string[] sliWid, string[] sliHei)
		{
			//копировать данные
			m_sliWid = new string[sliWid.Length + 1];
			m_sliWid[0] = "";
			for (int i = 0; i < sliWid.Length; i++)
				m_sliWid[i + 1] = sliWid[i];

			m_sliHei = new string[sliHei.Length + 1];
			m_sliHei[0] = "";
			for (int i = 0; i < sliHei.Length; i++)
				m_sliHei[i + 1] = sliHei[i];

			m_iType = 1;

			//выделить место для весов и путей
			m_height = m_sliHei.Length;
			m_width = m_sliWid.Length;
			m_weight = new double[m_height * m_width];
			m_from = new int[m_height * m_width];

			//найти веса и наилучший путь
			return DoCalc();
		}

		//сравнить 2 массива объектов
		public double Calc(object[] objWid, object[] objHei, CalcDifference hand)
		{
			//копировать данные
			m_objWid = new object[objWid.Length + 1];
			m_objWid[0] = "";
			for (int i = 0; i < objWid.Length; i++)
				m_objWid[i + 1] = objWid[i];

			m_objHei = new object[objHei.Length + 1];
			m_objHei[0] = "";
			for (int i = 0; i < objHei.Length; i++)
				m_objHei[i + 1] = objHei[i];

			m_iType = 2;
			//установить обработчик
			pc_hand = hand;

			//выделить место для весов и путей
			m_height = m_objHei.Length;
			m_width = m_objWid.Length;
			m_weight = new double[m_height * m_width];
			m_from = new int[m_height * m_width];

			//для красивого вывода
			m_sliWid = new string[m_objWid.Length];
			m_sliWid[0] = "";
			for (int i = 1; i < m_objWid.Length; i++)
				m_sliWid[i] = string.Format("{0}", m_objWid[i].ToString());

			m_sliHei = new string[m_objHei.Length];
			m_sliHei[0] = "";
			for (int i = 1; i < m_objHei.Length; i++)
				m_sliHei[i] = string.Format("{0}", m_objHei[i].ToString());

			//найти веса и наилучший путь
			return DoCalc();
		}

		//возвращает максимальный вес ячейки
		double GetWeight(int row0, int col0)
		{
			double dMax, weight;
			int i0 = row0 - 1;  //предыдущая строка
			int j0 = col0 - 1;  //предыдущая колонка
			int ia = row0 * m_width + col0;

			//переход из предыдущей колонки, эта строка
			weight = m_weight[row0 * m_width + j0] + m_dSkipFee;    //добавляем штраф за пропуск
			dMax = weight;
			m_from[ia] = -1;

			//переход из предыдущей строки, эта колонка
			weight = m_weight[i0 * m_width + col0] + m_dSkipFee;    //добавляем штраф за пропуск
			if (weight > dMax)
			{	//если лучше, запоминаем
				dMax = weight;
				m_from[ia] = 1;
			}

			//переход по диагонали, из предыдущих строки и колонки
			weight = m_weight[i0 * m_width + j0] + GetMatch(row0, col0);
			if (weight > dMax)
			{	//если лучше, запоминаем
				dMax = weight;
				m_from[ia] = 0;
			}
			return dMax;//возвращаем лучший результат
		}

		//штраф за несопадение или сопадение в строке i и колонке j
		double GetMatch(int i, int j)
		{
			if (m_iType == 0)
			{	//сравнить строки
				string ch1 = m_sHei.Substring(i, 1);
				string ch2 = m_sWid.Substring(j, 1);
				//возвращаем результат
				return (ch1 == ch2) ? m_dMatchFee : m_dMisFee;
			}
			double d = 0;
			if (m_iType == 1)
			{   //сравнить массивы строк
				string sl1 = m_sliHei[i];
				string sl2 = m_sliWid[j];
				if (bNoSpace)
				{	//удалить пробелы и табуляторы
					sl1 = sl1.Replace(" ", "").Replace("\t", "");
					sl2 = sl2.Replace(" ", "").Replace("\t", "");
				}
				//возвращаем результат
				d = (sl1 == sl2) ? m_dMatchFee : m_dMisFee;
			}
			else if(pc_hand != null)
			{   //сравнить массивы объектов используя обработчик
				var diff = pc_hand((m_objHei[i]), (m_objWid[j]));
				d = (diff == 0) ? m_dMatchFee : m_dMisFee;
			}
			return d;
		}

		//найти веса и наилучший путь
		double DoCalc()
		{
			int i, j;
			//инициализирем первую строку весов
			for (j = 0; j < m_width; j++)
			{
				m_weight[0 * m_width + j] = j * m_dSkipFee;
			}
			//внутри	
			for (i = 1; i < m_height; i++)
			{   //строка за строкой
				m_weight[i * m_width + 0] = i * m_dSkipFee;
				for (j = 1; j < m_width; j++)
				{
					m_weight[i * m_width + j] = GetWeight(i, j);
				}
			}
			//результат в последней строке, в последней колонке
			m_dMax = m_weight[((m_height - 1) * m_width) + m_width - 1];
			return m_dMax;
		}

		//отладочная функция
		//генерировать html-таблицу с весами и наилучшим путем
		public string Printweights(string style = "font-size:9pt;")
		{
			int i, j;
			//найти наилучшие колонки в обратной последовательности
			int[] iBestCol = new int[m_height];
			iBestCol[m_height - 1] = m_width - 1;
			for (i = m_height - 1; i > 0; i--)
			{
				j = iBestCol[i];
				while (j >= 0)
				{	//как сюда попали
					int from = m_from[i * m_width + j];
					if (from == 1)
					{   //вверх
						iBestCol[i - 1] = j;
						break;
					}
					else if (from == 0)
					{   //по диагонали
						iBestCol[i - 1] = j > 0 ? j - 1 : 0;//2020-07-15
						break;
					}
					else
					{	//по горизонтали
						if (j == 0)
						{   //2020-07-15
							iBestCol[i - 1] = 0;
							break;
						}
						j--;
					}
				}
			}

			//генерировать html-таблицу
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("<table id='scoretable' border='1' cellpadding='0' cellspacing='0' style='{0}'>\n", style);
			//первая строка
			sb.Append("<tr><td></td>\n");
			for (j = 0; j < m_sliWid.Length; j++)
			{
				var q = m_sliWid[j].Trim();
				sb.Append(string.Format("<td><b>{0}</b></td>\n", q.Length > 0 ? q.Substring(0, 1) : ""));
			}
			sb.Append("</tr>\n");
			//следущие строки
			for (i = 0; i < m_height; i++)
			{
				sb.Append("<tr>\n");
				var q2 = m_sliHei[i].Trim();
				sb.Append(string.Format("<td><b>{0}</b></td>\n", q2.Length > 0 ? q2.Substring(0, 1) : ""));
				//выделяем оптимальный путь
				for (j = 0; j < m_width; j++)
				{
					double ma = 0.0;
					if (i > 0 && j > 0) ma = GetMatch(i, j);
					sb.Append(string.Format("<td{0}>{1}<br>{2}</td>\n",
						(j == iBestCol[i]) ? " style='background:#cccccc'" : "",
						ma, m_weight[i * m_width + j]));
				}
				sb.Append("</tr>\n");
			}
			sb.Append("</table>\n");
			return sb.ToString();
		}

		//отладочная функция
		//генерировать цветное выравнивание в html
		//белый - одинаковые, красный (class='red') - данные из первого массива, зеленый (class='green') - данные из второго массива
		public string PrintStrings(string style = "font-size:9pt;")
		{
			int i, j;
			//найти наилучшие колонки в обратной последовательности
			int[] iBestCol = new int[m_height];
			iBestCol[m_height - 1] = m_width - 1;
			for (i = m_height - 1; i > 0; i--)
			{
				j = iBestCol[i];
				while (j >= 0)
				{   //как сюда попали
					int from = m_from[i * m_width + j];
					if (from == 1)
					{   //вверх
						iBestCol[i - 1] = j;
						break;
					}
					else if (from == 0)
					{   //по диагонали
						iBestCol[i - 1] = j > 0 ? j - 1 : 0;//2020-07-15
						break;
					}
					else
					{   //по горизонтали
						if (j == 0)
						{   //2020-07-15
							iBestCol[i - 1] = 0;
							break;
						}
						j--;
					}
				}
			}

			//генерировать div'ы
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("<div style='{0}'>\n", style);
			int prevCol = 0;
			for (i = 1; i < m_height; i++)
			{
				j = iBestCol[i];
				if (prevCol + 1 == j)
				{   //движение по диагонали
					double ma = GetMatch(i, j);
					if (ma > 0)
					{ //совпали
						sb.Append(string.Format("<div class='{0}'>{1}</div>\n", "",
							m_sliHei[i].Replace("<", "&lt;").Replace(">", "&gt;")));
					}
					else
					{   //не совпали, показываем строки из двух массивов
						sb.Append(string.Format("<div class='{0}'>{1}</div>\n", "red",
							m_sliWid[j].Replace("<", "&lt;").Replace(">", "&gt;")));
						sb.Append(string.Format("<div class='{0}'>{1}</div>\n", "green",
							m_sliHei[i].Replace("<", "&lt;").Replace(">", "&gt;")));
					}
				}
				else if (prevCol == j)
				{   //движение вниз, показываем строку из второго массива
					sb.Append(string.Format("<div class='{0}'>{1}</div>\n", "green",
						m_sliHei[i].Replace("<", "&lt;").Replace(">", "&gt;")));
				}
				else
				{   //движение вправо, проверяем диагональ
					prevCol++;
					double ma = GetMatch(i, prevCol);
					if (ma > 0)
					{   //совпали, одна строка
						sb.Append(string.Format("<div class='{0}'>{1}</div>\n", "",
							m_sliHei[i].Replace("<", "&lt;").Replace(">", "&gt;")));
					}
					else
					{   //не совпали, показать 2 строки
						sb.Append(string.Format("<div class='{0}'>{1}</div>\n", "red",
							m_sliWid[prevCol].Replace("<", "&lt;").Replace(">", "&gt;")));
						sb.Append(string.Format("<div class='{0}'>{1}</div>\n", "green",
							m_sliHei[i].Replace("<", "&lt;").Replace(">", "&gt;")));
					}
					prevCol++;
					while (prevCol <= j)
					{   //пропуск, показываем строку из первого массива
						sb.Append(string.Format("<div class='{0}'>{1}</div>\n", "red",
							m_sliWid[prevCol].Replace("<", "&lt;").Replace(">", "&gt;")));
						prevCol++;
					}
				}

				prevCol = j;
			}

			sb.Append("</div>");
			return sb.ToString();
		}
	}
}
