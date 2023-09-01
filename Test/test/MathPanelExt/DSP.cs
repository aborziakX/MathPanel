using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//digital signals processing

namespace MathPanelExt
{
    public class DSP
    {
        //discrete fast Fourier transform
        public static int FaFT(int n, double[] xRe, double[] xIm, double[] foRe, double[] foIm)
        {
			int m = (int)Math.Floor(Math.Log((double)n) / Math.Log((double)2) + 0.001);
			//assert(n >= 1 && n <= 65536 && n == pow((double)2, (double)m));
			if (!(n >= 1 && n <= 65536 && n == Math.Pow((double)2, (double)m))) return -1;

			double re, im, pi = 3.1415926535;
			int i, ip, j, jm1, k, ke, ked2, nd2 = n / 2, nm1 = n - 1;

			//prepare output
			for (i = 0; i < n; i++)
			{
				foRe[i] = xRe[i];
				foIm[i] = (xIm == null ? 0.0 : xIm[i]);
			}

			//interlace decomposition - bit reversal sorting
			//0001 -> 1000, 0010 -> 0100 etc.
			j = nd2;    //reverse of 1
			for (i = 1; i < nm1; i++)
			{
				//		TRACE( "%d ", j );
				if (i < j)
				{   //rotate
					re = foRe[i];
					im = foIm[i];
					foRe[i] = foRe[j];
					foIm[i] = foIm[j];
					foRe[j] = re;
					foIm[j] = im;
				}
				//make reverse of ( i + 1 )
				k = nd2;    //one non-zero bit
				while (k <= j)
				{
					j = j - k;
					k = k >> 1;
				}
				j = j + k;
			}

			double ur, ui, sr, si, tr, ti;
			for (k = 1; k <= m; k++)
			{
				ke = (int)Math.Pow((double)2, (double)k);
				ked2 = ke / 2;
				ur = 1.0;
				ui = 0.0;
				sr = Math.Cos(pi / ked2);
				si = -Math.Sin(pi / ked2);
				for (j = 1; j <= ked2; j++)
				{
					jm1 = j - 1;
					for (i = jm1; i < nm1; i += ke)
					{
						ip = i + ked2;
						tr = foRe[ip] * ur - foIm[ip] * ui;
						ti = foRe[ip] * ui + foIm[ip] * ur;

						foRe[ip] = foRe[i] - tr;
						foIm[ip] = foIm[i] - ti;

						foRe[i] = foRe[i] + tr;
						foIm[i] = foIm[i] + ti;
					}

					tr = ur;
					ur = tr * sr - ui * si;
					ui = tr * si + ui * sr;
				}
			}
			return 0;
		}
        //inverse
        public static int InvFaFT(int n, double[] xRe, double[] xIm, double[] foRe, double[] foIm)
        {
			int m = (int)Math.Floor(Math.Log((double)n) / Math.Log((double)2) + 0.001);
			//assert(n >= 1 && n <= 65536 && n == pow((double)2, (double)m));
			if (!(n >= 1 && n <= 65536 && n == Math.Pow((double)2, (double)m))) return -1;

			double[] foRe2 = new double[n];
			double[] foIm2 = new double[n];
			int i;
			for (i = 0; i < n; i++)
			{
				xRe[i] = foRe[i];
				xIm[i] = -foIm[i];
			}

			FaFT(n, xRe, xIm, foRe2, foIm2);

			for (i = 0; i < n; i++)
			{
				xRe[i] = foRe2[i] / n;
				xIm[i] = -foIm2[i] / n;
			}

			//delete[] foRe2;
			//delete[] foIm2;

			return 0;
		}
    }
}
