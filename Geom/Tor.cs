//2020, Andrei Borziak
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPanel
{
    /// <summary>
    /// Tor derived from GeOb
    /// </summary>
    public class Tor : GeOb
    {
        public Tor(double size = 1, double radSml = 0.25, string color = null, int divide = 20) : base()
        {
            name = "Tor" + id_counter;
            radius = size / 2.0;
            ColorSet(color);

            double x0, y0, z0, x1, y1, z1, x2, y2, x3, y3, x0_base, y0_base, x1_base, y1_base, r0, r1;
            Vec3 v0 = new Vec3();
            Vec3 v1 = new Vec3();
            Vec3 v2 = new Vec3();
            Vec3 v3 = new Vec3();

            double angle_step = ((2 * Math.PI) / divide);
            double angle, angle_2;

            
            angle = 0;
            for (int i = 0; i < divide; i++)
            {   //большой радиус
                //вершины
                x0_base = Math.Cos(angle) * radius;
                y0_base = Math.Sin(angle) * radius;
                angle += angle_step;
                x1_base = Math.Cos(angle) * radius;
                y1_base = Math.Sin(angle) * radius;

                angle_2 = 0;
                for (int j = 0; j < divide; j++)
                {   //малый радиус
                    z0 = Math.Sin(angle_2) * radSml;
                    r0 = Math.Cos(angle_2) * radSml;
                    angle_2 += angle_step;
                    z1 = Math.Sin(angle_2) * radSml;
                    r1 = Math.Cos(angle_2) * radSml;

                    x0 = x0_base * (1 - r0 / radius);
                    y0 = y0_base * (1 - r0 / radius);
                    v0.Copy(x0, y0, z0);

                    x1 = x0_base * (1 - r1 / radius);
                    y1 = y0_base * (1 - r1 / radius);
                    v1.Copy(x1, y1, z1);

                    x2 = x1_base * (1 - r1 / radius);
                    y2 = y1_base * (1 - r1 / radius);
                    v2.Copy(x2, y2, z1);

                    x3 = x1_base * (1 - r0 / radius);
                    y3 = y1_base * (1 - r0 / radius);
                    v3.Copy(x3, y3, z0);

                    //грани
                    Facet3 fac0_a = new Facet3(v0, v1, v2, v3);
                    if (!string.IsNullOrEmpty(color)) fac0_a.clr.Copy(clr);
                    fac0_a.name = name + "fac" + id_fac++;
                    lstFac.Add(fac0_a);
                }
            }
        }
    }
}
