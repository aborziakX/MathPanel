//2020, Andrei Borziak
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPanel
{
    /// <summary>
    /// Sphere derived from GeOb
    /// </summary>
    public class Sphere : GeOb
    {
        public Sphere(double size = 1, string color = null, int divide = 20, double fi0 = 0, double fi1 = 2 * Math.PI, 
            double fi_h0 = -Math.PI * 0.51, double fi_h1 = Math.PI * 0.51) : base()
        {
            name = "Sphere" + id_counter;
            radius = size / 2.0;
            ColorSet(color);
            divide = ( divide / 4 ) *4;

            double x0, y0, z0, x1, y1, z1;
            Vec3 v0 = new Vec3();
            Vec3 v1 = new Vec3();
            Vec3 v2 = new Vec3();
            Vec3 v3 = new Vec3();
            double angle_step = ((2 * Math.PI) / divide);
            double angle_h = 0, rad1, rad2;
            for (int j = 0; j < divide / 4; j++)
            {
                if ( (angle_h < fi_h0 || angle_h > fi_h1) && (-angle_h < fi_h0 || -angle_h > fi_h1) )
                {
                    angle_h += angle_step;
                    continue;
                }
                rad1 = Math.Cos(angle_h) * radius;
                z0 = Math.Sin(angle_h) * radius;
                angle_h += angle_step;
                rad2 = Math.Cos(angle_h) * radius;
                z1 = Math.Sin(angle_h) * radius;
                double angle = 0;

                for (int i = 0; i < divide; i++)
                {
                    if (angle < fi0 || angle > fi1)
                    {
                        angle += angle_step;
                        continue;
                    }
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

                    if (angle_h >= fi_h0 && angle_h <= fi_h1)
                    {
                        //грани: 0 fore
                        Facet3 fac0_a = (j < divide / 4 - 1) ? new Facet3(v0, v1, v2, v3) : new Facet3(v0, v1, v3);
                        if (!string.IsNullOrEmpty(color)) fac0_a.clr.Copy(clr);
                        fac0_a.name = name + "tfac" + id_fac++;
                        lstFac.Add(fac0_a);
                    }
                    if (-angle_h >= fi_h0 && -angle_h <= fi_h1)
                    {
                        //symmetric down
                        v0.z = -v0.z;
                        v1.z = -v1.z;
                        v2.z = -v2.z;
                        v3.z = -v3.z;
                        Facet3 fac0_b = (j < divide / 4 - 1) ? new Facet3(v0, v3, v2, v1) : new Facet3(v0, v3, v1);
                        if (!string.IsNullOrEmpty(color)) fac0_b.clr.Copy(clr);
                        fac0_b.name = name + "dfac" + id_fac++;
                        lstFac.Add(fac0_b);
                    }
                }
            }
        }
    }
}
