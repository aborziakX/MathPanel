//2020, Andrei Borziak
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPanel
{
    /// <summary>
    /// Фигура, получающаяся при вращении (токарный станок)
    /// </summary>
    public class RadiusVar : GeOb
    {
        public RadiusVar(double height = 1, double[] radv = null, string color = null, int divide = 12, int iTop = 3) : base()
        {
            radius = height / 2.0;
            if (radv == null || radv.Length < 2)
            {
                radv = new double[2];
                radv[0] = radius;
                radv[1] = radius;
            };
            name = "RadiusVar" + id_counter;
            ColorSet(color);

            double x0, y0, z0, x1, y1, z1 = -radius, rad1, rad2;
            Vec3 v0 = new Vec3();
            Vec3 v1 = new Vec3();
            Vec3 v2 = new Vec3();
            Vec3 v3 = new Vec3();
            double angle_step = ((2 * Math.PI) / divide);
            double angle, dh = height / (radv.Length - 1);

            //боковая
            for (int j = 0; j < radv.Length - 1; j++)
            {
                rad1 = radv[j];
                z0 = z1;
                rad2 = radv[j + 1];
                z1 = z0 + dh;
                angle = 0;

                for (int i = 0; i < divide; i++)
                {
                    //вершины
                    x0 = Math.Cos(angle) * rad1;
                    y0 = Math.Sin(angle) * rad1;
                    angle += angle_step;
                    x1 = Math.Cos(angle) * rad1;
                    y1 = Math.Sin(angle) * rad1;

                    v0.Copy(x0, y0, z0);
                    v1.Copy(x1, y1, z0);

                    angle -= angle_step;
                    x0 = Math.Cos(angle) * rad2;
                    y0 = Math.Sin(angle) * rad2;
                    angle += angle_step;
                    x1 = Math.Cos(angle) * rad2;
                    y1 = Math.Sin(angle) * rad2;

                    v2.Copy(x1, y1, z1);
                    v3.Copy(x0, y0, z1);

                    //грани: 0 fore
                    Facet3 fac0_a = new Facet3(v0, v1, v2, v3);
                    if (!string.IsNullOrEmpty(color)) fac0_a.clr.Copy(clr);
                    fac0_a.name = name + "fac" + id_fac++;
                    lstFac.Add(fac0_a);
                }
            }

            //top
            angle = 0;
            rad1 = radv[radv.Length - 1];
            z1 = radius;
            for (int i = 0; i < divide && ((iTop & 1) > 0); i++)
            {
                //вершины
                x0 = Math.Cos(angle) * rad1;
                y0 = Math.Sin(angle) * rad1;
                angle += angle_step;
                x1 = Math.Cos(angle) * rad1;
                y1 = Math.Sin(angle) * rad1;

                v0.Copy(x0, y0, z1);
                v1.Copy(x1, y1, z1);
                v2.Copy(0, 0, z1);

                //грани
                Facet3 fac0_a = new Facet3(v0, v1, v2);
                if (!string.IsNullOrEmpty(color)) fac0_a.clr.Copy(clr);
                fac0_a.name = name + "fac" + id_fac++;
                lstFac.Add(fac0_a);
            }


            //bottom
            angle = 0;
            rad1 = radv[0];
            z0 = -radius;
            for (int i = 0; i < divide && ((iTop & 2) > 0); i++)
            {
                //вершины
                x0 = Math.Cos(angle) * rad1;
                y0 = Math.Sin(angle) * rad1;
                angle += angle_step;
                x1 = Math.Cos(angle) * rad1;
                y1 = Math.Sin(angle) * rad1;

                v0.Copy(x0, y0, z0);
                v1.Copy(x1, y1, z0);
                v2.Copy(0, 0, z0);

                //грани
                Facet3 fac0_a = new Facet3(v2, v1, v0);
                if (!string.IsNullOrEmpty(color)) fac0_a.clr.Copy(clr);
                fac0_a.name = name + "fac" + id_fac++;
                lstFac.Add(fac0_a);
            }
        }
    }
}
