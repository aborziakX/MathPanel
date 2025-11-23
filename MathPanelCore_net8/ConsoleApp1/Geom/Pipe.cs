//2020, Andrei Borziak
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPanel
{
    /// <summary>
    /// Pipe derived from GeOb
    /// </summary>
    public class Pipe : GeOb
    {
        public Pipe(double [] size, Vec3 [] center, string [] color = null, int divide = 20, int iTop = 3) : base()
        {
            name = "Pipe" + id_counter;
            if (size == null || center == null || size.Length < 2 || size.Length != center.Length) return;
            radius = size[0] / 2.0;
            ColorSet((color != null && color.Length > 0) ? color[0] : null);

            double x0, y0, z0, x1, y1, z1, dx, dy, dz, A, B, C, D;
            Vec3 v0 = new Vec3();
            Vec3 v1 = new Vec3();
            Vec3 v2 = new Vec3();
            Vec3 v3 = new Vec3();
            Vec3 xAxe = new Vec3();
            Vec3 yAxe = new Vec3();
            Vec3 zAxe = new Vec3();
            Vec3 secAxe = new Vec3();
            Mat3 mat = new Mat3();

            double angle_step = ((2 * Math.PI) / divide);
            double angle, rad, rad_2, ratio, xShift = 0, yShift = 0, zShift = 0;
            //TODO rad is variable
            for (int i = 0; i < size.Length - 1; i++)
            {
                string ccc = (color != null && color.Length > 0) ? color[i % color.Length] : null;
                string ccc2 = (color != null && color.Length > 0) ? color[(i + 1) % color.Length] : null;
                rad = size[i];
                rad_2 = size[i+1];
                ratio = rad_2 / rad;
                //dx, dy, dz - normal, new Z-axe
                x0 = center[i].x + xShift;
                y0 = center[i].y + yShift;
                z0 = center[i].z + zShift;
                x1 = center[i + 1].x;
                y1 = center[i + 1].y;
                z1 = center[i + 1].z;
                //найти точку на плоскости, которая проходит x0, y0, z0
                //A * x + B * y + C * z + D = 0
                A = x1 - x0;
                B = y1 - y0;
                C = z1 - z0;
                D = -(A * x0 + B * y0 + C * z0);
                double len = Math.Sqrt(A * A + B * B + C * C);
                zAxe.Copy(A, B, C);
                zAxe.Normalize();

                if (i == size.Length - 2)
                {   //last segment
                    //B != 0, меняем Y, Z
                    //A * x0 + B * (dy + y0) + C * (dz + z0) + D = 0
                    //B * dy + C * dz = 0
                    //dz = 1, dy = - C * dz / B
                    if (B != 0)
                    {
                        dz = 1;
                        dy = -(C * dz) / B;
                        dx = 0;
                    }
                    else
                    {
                        if (C != 0)
                        {
                            dy = 1;
                            dz = -(B * dy) / C;
                            dx = 0;
                        }
                        else
                        {   //A != 0, A * dx + B * dy = 0
                            dz = 0;
                            dy = 1;
                            dx = -(B * dy) / A;
                        }
                    }
                    yAxe.Copy(dx, dy, dz);
                }
                else
                {   //in the middle
                    v1.Copy(A, B, C);
                    v2.Copy(center[i + 2].x - x0, center[i + 2].y - y0, center[i + 2].z - z0);
                    Vec3.Product(v1, v2, ref yAxe);
                }
                yAxe.Normalize();
                Vec3.Product(yAxe, zAxe, ref xAxe);

                /*Dynamo.Console("yz=" + yAxe.ScalarProduct(zAxe));
                Dynamo.Console("zx=" + zAxe.ScalarProduct(xAxe));
                Dynamo.Console("xy=" + xAxe.ScalarProduct(yAxe));*/

                mat.BuildVec(xAxe, yAxe, zAxe);

                //в обычной системе V = M * V~ + P0;
                double xpr0, ypr0, zpr0 = 0, xpr1, ypr1, zpr1 = len;   //в новой системе координат
                Vec3 vpr = new Vec3();
                Vec3 vpr_0 = new Vec3(x0, y0, z0);

                //бока
                angle = 0;
                if (i == size.Length - 2)
                {   //last
                    for (int j = 0; j < divide; j++)
                    {   //вершины
                        xpr0 = Math.Cos(angle) * rad;
                        ypr0 = Math.Sin(angle) * rad;
                        angle += angle_step;
                        xpr1 = Math.Cos(angle) * rad;
                        ypr1 = Math.Sin(angle) * rad;

                        vpr.Copy(xpr0, ypr0, zpr0);
                        mat.Mult(vpr, ref v0);
                        v0.Add(vpr_0);

                        vpr.Copy(xpr1, ypr1, zpr0);
                        mat.Mult(vpr, ref v1);
                        v1.Add(vpr_0);

                        vpr.Copy(xpr1 * ratio, ypr1 * ratio, zpr1);
                        mat.Mult(vpr, ref v2);
                        v2.Add(vpr_0);

                        vpr.Copy(xpr0 * ratio, ypr0 * ratio, zpr1);
                        mat.Mult(vpr, ref v3);
                        v3.Add(vpr_0);

                        //грани
                        Facet3 fac0_a = new Facet3(v0, v1, v2, v3);
                        if (!string.IsNullOrEmpty(ccc)) fac0_a.clr.SetColor(ccc);
                        fac0_a.name = name + "fac" + id_fac++;
                        lstFac.Add(fac0_a);
                    }
                }
                else
                {   //in the middle
                    //2-я направляющая
                    dx = center[i + 2].x - x1;
                    dy = center[i + 2].y - y1;
                    dz = center[i + 2].z - z1;
                    double len2 = Math.Sqrt(dx * dx + dy * dy + dz * dz);

                    //найти угол между направляющими
                    v0.Copy(zAxe);
                    v1.Copy(dx, dy, dz);
                    v1.Normalize();
                    v2.Copy(v1.x - v0.x, v1.y - v0.y, v1.z - v0.z);
                    double cc = v2.Length();
                    double fi = Math.Asin(cc / 2) * 2;
                    //Dynamo.Console("ang=" + (fi * 180) / Math.PI);

                    double teta = Math.PI / 2 - fi;
                    double q = rad_2 * (1 - Math.Cos(fi)) / Math.Sin(fi);
                    double zPrMin = len - q;
                    double zPrMax = len + q;
                    //Dynamo.Console("zPrMin=" + zPrMin);
                    //Dynamo.Console("zPrMax=" + zPrMax);

                    //выразить 2-ю направляющую в координатах
                    //secAxe.Copy(len2 * Math.Sin(fi), 0, len2 * Math.Cos(fi));
                    secAxe.Copy(dx, dy, dz);
                    secAxe.Normalize();

                    for (int j = 0; j < divide; j++)
                    {   //вершины
                        xpr0 = Math.Cos(angle) * rad;
                        ypr0 = Math.Sin(angle) * rad;
                        angle += angle_step;
                        xpr1 = Math.Cos(angle) * rad;
                        ypr1 = Math.Sin(angle) * rad;

                        vpr.Copy(xpr0, ypr0, zpr0);
                        mat.Mult(vpr, ref v0);
                        v0.Add(vpr_0);

                        vpr.Copy(xpr1, ypr1, zpr0);
                        mat.Mult(vpr, ref v1);
                        v1.Add(vpr_0);

                        double delta = ((rad_2 - xpr1 * ratio) / (2 * rad_2)) * (zPrMax - zPrMin) + zPrMin;
                        vpr.Copy(xpr1 * ratio, ypr1 * ratio, delta);
                        mat.Mult(vpr, ref v2);
                        v2.Add(vpr_0);

                        delta = ((rad_2 - xpr0 * ratio) / (2 * rad_2)) * (zPrMax - zPrMin) + zPrMin;
                        vpr.Copy(xpr0 * ratio, ypr0 * ratio, delta);
                        mat.Mult(vpr, ref v3);
                        v3.Add(vpr_0);

                        //грань первого цилиндра
                        Facet3 fac0_a = new Facet3(v0, v1, v2, v3);
                        if (!string.IsNullOrEmpty(ccc)) fac0_a.clr.SetColor(ccc);
                        fac0_a.name = name + "fac" + id_fac++;
                        lstFac.Add(fac0_a);

                        //грань второго цилиндра
                        v1.Copy(v2);
                        delta = ((rad_2 - xpr1 * ratio) / (2 * rad_2)) * (zPrMax - zPrMin);
                        v1.Add(secAxe.x * delta, secAxe.y * delta, secAxe.z * delta);
                        
                        v0.Copy(v3);
                        delta = ((rad_2 - xpr0 * ratio) / (2 * rad_2)) * (zPrMax - zPrMin);
                        v0.Add(secAxe.x * delta, secAxe.y * delta, secAxe.z * delta);

                        fac0_a = new Facet3(v3, v2, v1, v0);
                        if (!string.IsNullOrEmpty(ccc2)) fac0_a.clr.SetColor(ccc2);
                        fac0_a.name = name + "fac" + id_fac++;
                        lstFac.Add(fac0_a);
                    }
                    //залезли во 2-й цилиндр
                    xShift = secAxe.x * (zPrMax - zPrMin) * 0.5;
                    yShift = secAxe.y * (zPrMax - zPrMin) * 0.5;
                    zShift = secAxe.z * (zPrMax - zPrMin) * 0.5;
                }

                //закрыть вход
                angle = 0;
                v0.Copy(vpr_0);
                for (int j = 0; (i == 0) && j < divide && ((iTop & 1) > 0); j++)
                {    //вершины
                    xpr0 = Math.Cos(angle) * rad;
                    ypr0 = Math.Sin(angle) * rad;
                    angle += angle_step;
                    xpr1 = Math.Cos(angle) * rad;
                    ypr1 = Math.Sin(angle) * rad;

                    vpr.Copy(xpr0, ypr0, zpr0);
                    mat.Mult(vpr, ref v1);
                    v1.Add(vpr_0);

                    vpr.Copy(xpr1, ypr1, zpr0);
                    mat.Mult(vpr, ref v2);
                    v2.Add(vpr_0);

                    //грани
                    Facet3 fac0_a = new Facet3(v2, v1, v0);
                    if (!string.IsNullOrEmpty(ccc)) fac0_a.clr.SetColor(ccc);
                    fac0_a.name = name + "fac" + id_fac++;
                    lstFac.Add(fac0_a);
                }

                //закрыть выход
                angle = 0;
                v0.Copy(x1, y1, z1);
                for (int j = 0; (i == size.Length - 2) && j < divide && ((iTop & 2) > 0); j++)
                {    //вершины
                    xpr0 = Math.Cos(angle) * rad_2;
                    ypr0 = Math.Sin(angle) * rad_2;
                    angle += angle_step;
                    xpr1 = Math.Cos(angle) * rad_2;
                    ypr1 = Math.Sin(angle) * rad_2;

                    vpr.Copy(xpr0, ypr0, zpr1);
                    mat.Mult(vpr, ref v1);
                    v1.Add(vpr_0);

                    vpr.Copy(xpr1, ypr1, zpr1);
                    mat.Mult(vpr, ref v2);
                    v2.Add(vpr_0);

                    //грани
                    Facet3 fac0_a = new Facet3(v0, v1, v2);
                    if (!string.IsNullOrEmpty(ccc)) fac0_a.clr.SetColor(ccc);
                    fac0_a.name = name + "fac" + id_fac++;
                    lstFac.Add(fac0_a);
                }
            }
        }
    }
}
