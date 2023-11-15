using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPanel
{
    /// <summary>
    /// Tree derived from GeOb
    /// </summary>
    public class Tree : GeOb
    {
        public Tree(double size = 1, string color = null, int divide = 20, int iTop = 3) : base()
        {
            name = "Tree" + id_counter;
            radius = size / 2.0;
            ColorSet(color);

            double z1 = size;
            Vec3 v0 = new Vec3(0, 0, z1);
            Vec3 [] vecs = new Vec3[divide];
            Vec3 v1, v2;

            for (int i = 0; i < divide; i++)
            {
                vecs[i] = new Vec3();
            }
            Vec3.RotationPoints(v0, radius, vecs);

            //боковая
            for (int i = 0; i < divide; i++)
            {
                //вершины
                v1 = vecs[i];
                v2 = vecs[i < divide - 1 ? i :0];

                //грани
                //Facet3 fac0_a = new Facet3(v0, v1, v2);
                Facet3 fac0_a = new Facet3(v0, v2, v1);
                if (!string.IsNullOrEmpty(color)) fac0_a.clr.Copy(clr);
                lstFac.Add(fac0_a);
            }

            //bottom
            /*angle = 0;
            for (int i = 0; i < divide & ((iTop & 2) > 0); i++)
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
                fac0_a.name = "fac" + id++;
                lstFac.Add(fac0_a);
            } */
        }
    }
}

