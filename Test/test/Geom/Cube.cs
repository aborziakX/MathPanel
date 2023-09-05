//2020, Andrei Borziak

namespace MathPanel
{
    /// <summary>
    /// Куб на базе GeOb
    /// </summary>
    public class Cube : GeOb
    {
        /// <summary>
        /// конструктор куба
        /// </summary>
        /// <param name="size">размер</param>
        /// <param name="color">цвет</param>
        /// <param name="iOpened">побитовое ИЛИ: какие грани отсутствуют</param>
        /// <param name="hDivide">на сколько частей делить</param>
        public Cube(double size = 1, string color = null, int iOpened = 0, int hDivide = 1) : base()
        {
            name = "cube" + id_counter;
            radius = size / 2.0;
            ColorSet(color);
            double dH = hDivide > 1 ? size / hDivide : size;
            double x, y, z;
            //наименьшие координаты
            double x0 = -radius, y0 = -radius, z0 = -radius;
            //наибольшие координаты
            double x1 = radius, y1 = radius, z1 = radius;
            //вершины для наглядности
            Vec3 v000 = new Vec3(x0, y0, z0);
            Vec3 v100 = new Vec3(x1, y0, z0);
            Vec3 v110 = new Vec3(x1, y1, z0);
            Vec3 v010 = new Vec3(x0, y1, z0);

            Vec3 v001 = new Vec3(x0, y0, z1);
            Vec3 v101 = new Vec3(x1, y0, z1);
            Vec3 v111 = new Vec3(x1, y1, z1);
            Vec3 v011 = new Vec3(x0, y1, z1);

            Vec3 v0 = new Vec3();
            Vec3 v1 = new Vec3();
            Vec3 v2 = new Vec3();
            Vec3 v3 = new Vec3();

            if ((iOpened & 0x1) == 0)
            {
                z = z1;
                for (int j = 0; j < hDivide; j++)
                {
                    for (int i = 0; i < hDivide; i++)
                    {
                        //передняя грань: v001, v101, v111, v011
                        x = x0 + dH * i;
                        y = y0 + dH * j;
                        //задать точки грани
                        v0.Copy(x, y, z);
                        v1.Copy(x + dH, y, z);
                        v2.Copy(x + dH, y + dH, z);
                        v3.Copy(x, y + dH, z);
                        //создать грань
                        Facet3 fac0_a = new Facet3(v0, v1, v2, v3);
                        if (!string.IsNullOrEmpty(color)) fac0_a.clr.Copy(clr);
                        fac0_a.name = name + "fo" + id_fac++;
                        //добавить в список
                        lstFac.Add(fac0_a);
                    }
                }
            }

            if ((iOpened & 0x2) == 0)
            {
                z = z0;
                for (int j = 0; j < hDivide; j++)
                {
                    for (int i = 0; i < hDivide; i++)
                    {
                        //задняя грань: v000, v010, v110, v100
                        x = x0 + dH * i;
                        y = y0 + dH * j;
                        v0.Copy(x, y, z);
                        v1.Copy(x + dH, y, z);
                        v2.Copy(x + dH, y + dH, z);
                        v3.Copy(x, y + dH, z);
                        Facet3 fac4 = new Facet3(v0, v3, v2, v1);
                        if (!string.IsNullOrEmpty(color)) fac4.clr.Copy(clr);
                        fac4.name = name + "bk" + id_fac++;
                        lstFac.Add(fac4);
                    }
                }
            }
            
            if ((iOpened & 0x4) == 0)
            {
                x = x0;
                for (int j = 0; j < hDivide; j++)
                {
                    for (int i = 0; i < hDivide; i++)
                    {
                        //грань слева: v011, v010, v000, v001
                        z = z0 + dH * i;
                        y = y0 + dH * j;
                        v0.Copy(x, y, z);
                        v1.Copy(x, y, z + dH);
                        v2.Copy(x, y + dH, z + dH);
                        v3.Copy(x, y + dH, z);
                        Facet3 fac3_a = new Facet3(v0, v1, v2, v3);
                        if (!string.IsNullOrEmpty(color)) fac3_a.clr.Copy(clr);
                        fac3_a.name = name + "lf" + id_fac++;
                        lstFac.Add(fac3_a);
                    }
                }
            }
            
            if ((iOpened & 0x8) == 0)
            {
                x = x1;
                for (int j = 0; j < hDivide; j++)
                {
                    for (int i = 0; i < hDivide; i++)
                    {
                        //грань справа: v101, v100, v110, v111
                        z = z0 + dH * i;
                        y = y0 + dH * j;
                        v0.Copy(x, y, z);
                        v1.Copy(x, y, z + dH);
                        v2.Copy(x, y + dH, z + dH);
                        v3.Copy(x, y + dH, z);
                        Facet3 fac1_a = new Facet3(v0, v3, v2, v1);
                        if (!string.IsNullOrEmpty(color)) fac1_a.clr.Copy(clr);
                        fac1_a.name = name + "rt" + id_fac++;
                        lstFac.Add(fac1_a);
                    }
                }
            }
            
            if ((iOpened & 0x10) == 0)
            {
                y = y0;
                for (int j = 0; j < hDivide; j++)
                {
                    for (int i = 0; i < hDivide; i++)
                    {
                        //грань сверху: v111, v110, v010, v011
                        z = z0 + dH * i;
                        x = x0 + dH * j;
                        v0.Copy(x, y, z);
                        v1.Copy(x + dH, y, z);
                        v2.Copy(x + dH, y, z + dH);
                        v3.Copy(x, y, z + dH);
                        Facet3 fac2_a = new Facet3(v0, v1, v2, v3);
                        if (!string.IsNullOrEmpty(color)) fac2_a.clr.Copy(clr);
                        fac2_a.name = name + "tp" + id_fac++;
                        lstFac.Add(fac2_a);
                    }
                }
            }
            
            if ((iOpened & 0x20) == 0)
            {
                y = y1;
                for (int j = 0; j < hDivide; j++)
                {
                    for (int i = 0; i < hDivide; i++)
                    {
                        //грань снизу: v001, v000, v100, v101
                        z = z0 + dH * i;
                        x = x0 + dH * j;
                        v0.Copy(x, y, z);
                        v1.Copy(x + dH, y, z);
                        v2.Copy(x + dH, y, z + dH);
                        v3.Copy(x, y, z + dH);
                        Facet3 fac5 = new Facet3(v0, v3, v2, v1);
                        if (!string.IsNullOrEmpty(color)) fac5.clr.Copy(clr);
                        fac5.name = name + "bm" + id_fac++;
                        lstFac.Add(fac5);
                    }
                }
            }

            bDrawBack = iOpened != 0;   //если есть открытые части, надо рисовать

            /*
            //6 test
            Vec3 v001_a = new Vec3(x0 - radius, y0, z1);
            Vec3 v001_b = new Vec3(x0 - radius, y0 - radius, z1);
            Vec3 v001_c = new Vec3(x0, y0 - radius, z1);
            Facet3 fac6 = new Facet3(v001, v001_a, v001_b, v001_c);
            if (!string.IsNullOrEmpty(color)) fac6.clr.Copy(clr);
            fac6.name = "t" + id_counter;
            lstFac.Add(fac6);*/

            //test string s = fac5.ColorHtml();
        }
    }
}
