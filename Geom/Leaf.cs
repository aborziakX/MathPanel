using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPanel
{
    /// <summary>
    /// Лист природный: derived from GeOb
    /// </summary>
    public class Leaf : GeOb
    {
        static Random rand = new Random(); //генератор случайных чисел
        //дуб, x, y pairs
        static double[] oak =
        {
            -5, 0,
            3, 3,
            4, 1.8,
            5, 2,
            7, 0,
            5, -2.5,
            4, -2,
            2.5, -2.5
        };
        /// <summary>
        /// конструктор Листа
        /// </summary>
        /// <param name="size">размер</param>
        /// <param name="color">цвет</param>
        /// <param name="typ">тип листа</param>
        /// <param name="iter">сколько итераций фрактализации</param>
        /// <param name="divide">на сколько дополнительно разбить грани</param>
        public Leaf(double size = 1, string color = null, int typ = 0, int iter = 3, int divide = 1) : base()
        {
            name = "Leaf" + id_counter;
            radius = size / 2.0;
            ColorSet(color);
            double z1 = 0;
            Vec3 v0, v1, v2;
            v0 = new Vec3(0, 0, z1);
            v1 = new Vec3();
            v2 = new Vec3();

            //генерация боковых граней
            for (int i = 0; i < oak.Length / 2; i+=2)
            {
                //вершины
                v1.Copy(oak[i * 2 + 0], oak[i * 2 + 1], z1);
                if( i < oak.Length / 2 - 1)
                    v2.Copy(oak[i * 2 + 2], oak[i * 2 + 3], z1);
                else v2.Copy(oak[0], oak[1], z1);

                //грани
                Facet3 fac0_a = new Facet3(v0, v2, v1);
                if (!string.IsNullOrEmpty(color)) fac0_a.clr.Copy(clr);
                lstFac.Add(fac0_a);
            }
        }   
    }
}
