//2020, Andrei Borziak
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPanel
{
    /// <summary>
    /// Tetraedr derived from GeOb
    /// </summary>
    public class Tetra : GeOb
    {
        public Tetra(double size = 1, string color = null) : base()
        {
            name = "tetra" + id_counter;
            radius = size / 2.0;
            ColorSet(color);

            double x0 = -radius, y0 = 0, z0 = 0;
            double x1 = radius, y1 = radius * Math.Sqrt(3.0);
            double ym = radius / Math.Sqrt(5.0);
            double z1 = (4 * radius) / Math.Sqrt(5.0);
            //вершины
            Vec3 v000 = new Vec3(x0, y0, z0);
            Vec3 v100 = new Vec3(x1, y0, z0);
            Vec3 v010 = new Vec3(0, y1, z0);
            Vec3 v001 = new Vec3(0, ym, z1);

            //грани: 0 fore
            Facet3 fac0_a = new Facet3(v000, v100, v001);
            if (!string.IsNullOrEmpty(color)) fac0_a.clr.Copy(clr);
            fac0_a.name = "fo" + id_counter;
            lstFac.Add(fac0_a);

            //1 right
            Facet3 fac1_a = new Facet3(v100, v010, v001);
            if (!string.IsNullOrEmpty(color)) fac1_a.clr.Copy(clr);
            fac1_a.name = "rt" + id_counter;
            lstFac.Add(fac1_a);

            //3 left
            Facet3 fac3_a = new Facet3(v010, v000, v001);
            if (!string.IsNullOrEmpty(color)) fac3_a.clr.Copy(clr);
            fac3_a.name = "lf" + id_counter;
            lstFac.Add(fac3_a);

            //5 bottom
            Facet3 fac5 = new Facet3(v000, v010, v100);
            if (!string.IsNullOrEmpty(color)) fac5.clr.Copy(clr);
            fac5.name = "bm" + id_counter;
            lstFac.Add(fac5);
        }
    }
}
