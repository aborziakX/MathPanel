using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPanelExt
{
	//шаблон делегата функционала
    public delegate double CalcFunc(double [] dParams);

    /// <summary>
    /// класс для оптимизации методом градиентного спуска
    /// </summary>

    public class GradientSearch
    {
		public CalcFunc m_funcExternal = null;//делегат функционала
		public CalcFunc m_funcOverlimits = null;//делегат лимитов, меньше 0 - нарушение области

		public double TRESHOLD = 1e-3; //если разница меньше, то прервать
		public double DT = 1e-3; //начальный шаг
		public double DIAMETER = 1.0;   //диаметр для случайного разброса параметров
		public int m_numParam = 0;     //число параметров
		public double[] m_dParams = new double[10]; //массив параметров
		Random rnd = new Random((int)DateTime.Now.Ticks);

        public GradientSearch()
        {

        }

		//method of fast decline
		//calculate functional
		//get small variation, find derivative DV
		//go in the direction against DV
		//repeat while the functional is decresing
		//минимизация!
		public double doGradient(int NRANDOM, int NTIMES)
		{
			if (m_numParam < 1 || m_numParam > 10 || m_funcExternal == null)
				throw new Exception("GradientSearch не инициализирован");
			if (NRANDOM < 1 || NRANDOM > 100 || NTIMES < 1 || NTIMES > 100)
				throw new Exception("GradientSearch неверные аргументы");

			int NSPACE = m_numParam;
			int i, j, k, ivar, jSuc = 0;
			double step, func, funcOld, funcStep;
			double funcBest = 1e+12;
			double[] dBest = new double[m_numParam];
			double[] df = new double[m_numParam];

			for (i = 0; i < NRANDOM; i++)
			{   //to work with local minimums
				for (k = 0; k < NSPACE; k++)
				{
					m_dParams[k] = (rnd.NextDouble() / 2 ) * DIAMETER;
					//System.Console.WriteLine( "par {0}", m_dParams[ k ] );
				}
				jSuc = 0;

				if (m_funcOverlimits != null && m_funcOverlimits(m_dParams) < 0)
					continue;

				if (i % 10 == 0) System.Console.WriteLine("{0} {1}", i, m_dParams[0]);
				funcOld = m_funcExternal(m_dParams);
				//System.Console.WriteLine( "start distance={0}", funcOld );

				for (k = 0; k < NTIMES; k++)
				{
					//calculate gradient
					double gradlen = 0.0;
					for (ivar = 0; ivar < NSPACE; ivar++)
					{
						m_dParams[ivar] += DT;          //slight variation
						funcStep = m_funcExternal(m_dParams);
						m_dParams[ivar] -= (DT + DT);   //2 steps back
						func = m_funcExternal(m_dParams);
						m_dParams[ivar] += DT;      //return to start pos
						df[ivar] = -(funcStep - func);
						gradlen += df[ivar] * df[ivar];
					}
					//normalize
					gradlen = Math.Sqrt(gradlen);
					if (gradlen == 0.0) break;
					for (ivar = 0; ivar < NSPACE; ivar++)
					{
						df[ivar] = df[ivar] / gradlen;
					}

					step = 3.0 * DT;
					funcStep = funcOld;
					jSuc = 0;
					//find optimal step in gradient direction
					for (j = 0; j < 20 && step >= DT; j++)
					{
						for (ivar = 0; ivar < NSPACE; ivar++)
						{
							m_dParams[ivar] += step * df[ivar];
						}
						func = m_funcExternal(m_dParams);
						if (funcStep - func < 0.0 ||
							(m_funcOverlimits != null && m_funcOverlimits(m_dParams) < 0))
						{   //back
							for (ivar = 0; ivar < NSPACE; ivar++)
							{
								m_dParams[ivar] -= step * df[ivar];
							}
							step *= 0.7;
						}
						else
						{   //accept
							funcStep = func;
							step *= 2.0;
							jSuc = j;
						}
					}

					//System.Console.WriteLine( "distance={0}", funcStep );
					if (funcOld - funcStep < TRESHOLD) break;
					//keep the new values
					funcOld = funcStep;
				}

				//System.Console.WriteLine( "opt distance={0}", funcOld );
				if (funcOld < funcBest)
				{   //save
					string buf = string.Format("###i={0}, k={1}, jSuc={2}, better distance={3}, param0={4}", 
						i, k, jSuc, funcOld, m_dParams[0]);
					System.Console.WriteLine("{0}", buf);
					funcBest = funcOld;
					for (ivar = 0; ivar < NSPACE; ivar++)
					{
						dBest[ivar] = m_dParams[ivar];
					}
				}
			}

			for (ivar = 0; ivar < NSPACE; ivar++)
			{
				m_dParams[ivar] = dBest[ivar];
			}

			return funcBest;
		}
    }
}
