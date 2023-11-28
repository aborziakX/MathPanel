using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPanel
{
    /// <summary>
    /// Дерево природное: derived from GeOb
    /// </summary>
    public class Tree : GeOb
    {
        static Random rand = new Random(); //генератор случайных чисел
        //зона готово
        public List<Vec3> lstBegFix = new List<Vec3>();
        public List<Vec3> lstDirFix = new List<Vec3>();

        /// <summary>
        /// конструктор Дерева
        /// </summary>
        /// <param name="size">размер</param>
        /// <param name="color">цвет</param>
        /// <param name="side">сколько граней в стволе</param>
        /// <param name="iter">сколько итераций фрактализации</param>
        /// <param name="divide">на сколько дополнительно разбить грани</param>
        public Tree(double size = 1, string color = null, int side = 8, int iter = 3, int divide = 1) : base()
        {
            name = "Tree" + id_counter;
            radius = size / 2.0;
            ColorSet(color);
            double z1 = size, radBase = size * 0.1;

            //сформировать фрактал: точка старта-направление
            Vec3 v0, v1, v2, v3, vBeg, vDir;
            v0 = new Vec3();
            v3 = new Vec3();
            
            //зона роста
            List<Vec3> lstBeg = new List<Vec3>();
            List<Vec3> lstDir = new List<Vec3>();
            //зона временно
            List<Vec3> lstBegTmp = new List<Vec3>();
            List<Vec3> lstDirTmp = new List<Vec3>();
            //первая итерация
            v1 = new Vec3();
            v2 = new Vec3(0, 0, z1);
            lstBeg.Add(v1);
            lstDir.Add(v2);

            //итерации фрактализации
            for (int j = 1; j < iter; j++)
            {
                lstBegTmp.Clear();
                lstDirTmp.Clear();
                for (int i = 0; i < lstBeg.Count; i++)
                {
                    vBeg = lstBeg[i];
                    vDir = lstDir[i];
                    double ln = vDir.Length();

                    //ветка 1
                    v1 = new Vec3(vBeg);
                    v2 = new Vec3(vDir);
                    v2.Scale(0.25);
                    v1.SumTwo(vBeg, v2); //начало ветки
                    //направление
                    RandBranch(v2, ln);
                    v2.Normalize();
                    v2.Scale(ln * 0.4);
                    lstBegTmp.Add(v1);
                    lstDirTmp.Add(v2);

                    //ветка 2
                    v1 = new Vec3(vBeg);
                    v2 = new Vec3(vDir);
                    v2.Scale(0.5);
                    v1.SumTwo(vBeg, v2); //начало ветки
                    //направление
                    RandBranch(v2, ln);
                    v2.Normalize();
                    v2.Scale(ln * 0.3);
                    lstBegTmp.Add(v1);
                    lstDirTmp.Add(v2);

                    //ветка 3
                    v1 = new Vec3(vBeg);
                    v2 = new Vec3(vDir);
                    v2.Scale(0.75);
                    v1.SumTwo(vBeg, v2); //начало ветки
                    //направление
                    RandBranch(v2, ln);
                    v2.Normalize();
                    v2.Scale(ln * 0.20);
                    lstBegTmp.Add(v1);
                    lstDirTmp.Add(v2);

                    //добавить 1/4 в новый
                    v1 = new Vec3();
                    v2 = new Vec3(vDir);
                    v2.Scale(0.75);
                    v1.SumTwo(vBeg, v2);
                    v2.Scale(0.25);
                    lstBegTmp.Add(v1);
                    lstDirTmp.Add(v2);

                    //добавить 3/4 в готово
                    v1 = new Vec3(vBeg);
                    v2 = new Vec3(vDir);
                    v2.Scale(0.75);
                    lstBegFix.Add(v1);
                    lstDirFix.Add(v2);
                }
                lstBeg.Clear();
                lstBeg.AddRange(lstBegTmp);
                lstDir.Clear();
                lstDir.AddRange(lstDirTmp);
            }

            //добавляем остаток
            lstBegFix.AddRange(lstBeg);
            lstDirFix.AddRange(lstDir);

            //генерация рабочих векторов
            Vec3[] vecs = new Vec3[side];
            Vec3[] vecs_2 = new Vec3[side];
            for (int i = 0; i < side; i++)
            {
                vecs[i] = new Vec3();
                vecs_2[i] = new Vec3();
            }
            //отвязать от списка
            v1 = new Vec3();
            v2 = new Vec3();

            //генерация стеблей
            for (int j = 0; j < lstBegFix.Count; j++)
            {
                vBeg = lstBegFix[j];
                vDir = lstDirFix[j];
                v0.SumTwo(vBeg, vDir);
                double radNew = radBase * vDir.Length() / size;
                Vec3.RotationPoints(vDir, radNew, vecs);
                for (int i = 0; i < side; i++)
                {
                    vecs_2[i].Copy(vecs[i]);
                    vecs_2[i].Scale(0.25);
                }

                //генерация боковых граней
                for (int i = 0; i < side; i++)
                {
                    //вершины
                    v1.SumTwo(vBeg, vecs[i]);
                    int k = (i < side - 1 ? i + 1 : 0);
                    v2.SumTwo(vBeg, vecs[k]);

                    v0.SumTwo(vBeg, vDir);
                    v0.Add(vecs_2[i]);
                    v3.SumTwo(vBeg, vDir);
                    v3.Add(vecs_2[k]);

                    //грани
                    Facet3 fac0_a = new Facet3(v0, v1, v2, v3);
                    //Facet3 fac0_a = new Facet3(v0, v2, v1);
                    if (!string.IsNullOrEmpty(color)) fac0_a.clr.Copy(clr);
                    lstFac.Add(fac0_a);
                }
            }

            //поменять цвет
            this.Divide(divide);
            int r = clr.GetColor().R;
            int g = clr.GetColor().G;
            int b = clr.GetColor().B;
            foreach(var fac in lstFac)
            {
                int red = BitmapSimple.SafeColor((int)(r + (rand.NextDouble() * 200 - 100)));
                int green = BitmapSimple.SafeColor((int)(g + (rand.NextDouble() * 200 - 100)));
                int blue = BitmapSimple.SafeColor((int)(b + (rand.NextDouble() * 200 - 100)));
                var c = System.Drawing.Color.FromArgb(255, red, green, blue);
                fac.clr.SetColor(c);
            }
        }

        static void RandBranch(Vec3 v2, double ln)
        {
            double dx = 1.0, dy = 1.0, dz = 1.0;
            double _x = Math.Abs(v2.x);
            double _y = Math.Abs(v2.y);
            double _z = Math.Abs(v2.z);
            if (_x > _y && _x > _z) dx = 0.5;
            else if (_y > _x && _y > _z) dy = 0.5;
            else if (_z > _x && _z > _y) dz = 0.5;
            v2.Add((rand.NextDouble() - 0.5) * ln * dx,
                (rand.NextDouble() - 0.5) * ln * dy,
                (rand.NextDouble() - 0.5) * ln * dz);
        }
    }
}

