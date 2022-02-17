//2020, Andrei Borziak
using System;

namespace MathPanel
{
    /// <summary>
    /// Цилиндр на базе GeOb
    /// </summary>
    public class Cylinder : GeOb
    {
        /// <summary>
        /// конструктор цилиндра
        /// </summary>
        /// <param name="size">размер</param>
        /// <param name="color">цвет</param>
        /// <param name="divide">на сколько частей делить боковую поверхность по окружности</param>
        /// <param name="iTop">побитовое ИЛИ: какие торцы рисовать</param>
        /// <param name="hDivide">на сколько частей делить боковую поверхность по вертикали</param>
        public Cylinder(double size = 1, string color = null, int divide = 20, int iTop = 3, int hDivide = 1) : base()
        {
            name = "Cylinder" + id_counter;
            radius = size / 2.0;
            ColorSet(color);

            double x0, y0, z0 = -radius, x1, y1, z1 = radius;
            Vec3 v0 = new Vec3();
            Vec3 v1 = new Vec3();
            Vec3 v2 = new Vec3();
            Vec3 v3 = new Vec3();
            double angle_step = ((2 * Math.PI) / divide);
            double angle;
            double dH = hDivide > 1 ? size / hDivide : size;

            //боковая поверхность
            double z = z0;
            for (int j = 0; j < hDivide; j++)
            {
                angle = 0;
                for (int i = 0; i < divide; i++)
                {
                    //вершины
                    x0 = Math.Cos(angle) * radius;
                    y0 = Math.Sin(angle) * radius;
                    angle += angle_step;
                    x1 = Math.Cos(angle) * radius;
                    y1 = Math.Sin(angle) * radius;

                    v0.Copy(x0, y0, z);
                    v1.Copy(x1, y1, z);
                    v2.Copy(x1, y1, z + dH);
                    v3.Copy(x0, y0, z + dH);

                    //грани
                    Facet3 fac0_a = new Facet3(v0, v1, v2, v3);
                    if (!string.IsNullOrEmpty(color)) fac0_a.clr.Copy(clr);
                    fac0_a.name = name + "fac" + id_fac++;
                    lstFac.Add(fac0_a);
                }
                z += dH;
            }

            //верхний торец
            angle = 0;
            for (int i = 0; i < divide && ((iTop & 1) > 0); i++)
            {
                //вершины
                x0 = Math.Cos(angle) * radius;
                y0 = Math.Sin(angle) * radius;
                angle += angle_step;
                x1 = Math.Cos(angle) * radius;
                y1 = Math.Sin(angle) * radius;

                v0.Copy(x0, y0, z1);
                v1.Copy(x1, y1, z1);
                v2.Copy(0, 0, z1);

                //грани
                Facet3 fac0_a = new Facet3(v0, v1, v2);
                if (!string.IsNullOrEmpty(color)) fac0_a.clr.Copy(clr);
                fac0_a.name = name + "fac" + id_fac++;
                lstFac.Add(fac0_a);
            }

            //нижний торец
            angle = 0;
            for (int i = 0; i < divide && ((iTop & 2) > 0); i++)
            {
                //вершины
                x0 = Math.Cos(angle) * radius;
                y0 = Math.Sin(angle) * radius;
                angle += angle_step;
                x1 = Math.Cos(angle) * radius;
                y1 = Math.Sin(angle) * radius;

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
