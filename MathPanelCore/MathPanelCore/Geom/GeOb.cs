//2020, Andrei Borziak
using System;
using System.Collections.Generic;
using System.Linq;

namespace MathPanel
{
    /// <summary>
    /// оболочка вокруг Color - нужна для сериализации цвета
    /// </summary>
    [Serializable]
    public class ColorWrapper
    {
        //цвет по умолчанию - зеленый
        private System.Drawing.Color color = System.Drawing.Color.Green;
        //конструктор задает цвет
        public ColorWrapper(System.Drawing.Color color)
        {
            this.color = color;
        }

        //конструктор без параметров - нужен для сериализации
        public ColorWrapper()
        {
        }

        //получить цвет
        public System.Drawing.Color GetColor() { return color; }

        //задать цвет
        public void SetColor(System.Drawing.Color clr)
        {
            color = clr;
        }
        //задать цвет из строки
        public void SetColor(string clr)
        {
            color = System.Drawing.Color.FromName(clr);
        }

        //получить/задать ARGB
        public int Argb
        {
            get
            {
                return color.ToArgb();
            }
            set
            {
                color = System.Drawing.Color.FromArgb(value);
            }
        }

        //скопировать цвет
        public void Copy(ColorWrapper cw)
        {
            color = System.Drawing.Color.FromArgb(cw.Argb);
        }
    }

    /// <summary>
    /// грань из 3-6 точек(каждая 3-х мерный вектор)
    /// </summary>
    public class Facet3
    {
        static int id_counter = 0; //счетчик
        public string name = ""; //название
        public ColorWrapper clr = new ColorWrapper(); //оболочка для сериализации цвета, хранит цвет грани
        //координаты в системе объекта
        public Vec3 v1, v2, v3, v4, v5, v6;
        public Vec3 normal = new Vec3(); //нормаль
        //координаты в системе камеры
        public Vec3 v1_cam, v2_cam, v3_cam, v4_cam, v5_cam, v6_cam;
        public Vec3 normal_cam = new Vec3(); //нормаль
        public double dark = 1; //яркость
        public double area = 0; //площадь
        public int Count { get; } //число точек в грани
        static Random rand = new Random(); //генератор случайных чисел
        public Phob phob = null;
        public double cosfi = 0;
        public GeOb shape = null;

        //конструктор по умолчанию создает 4-х точечную грань
        public Facet3()
        {
            v1 = new Vec3();
            v2 = new Vec3();
            v3 = new Vec3();
            v4 = new Vec3();

            v1_cam = new Vec3();
            v2_cam = new Vec3();
            v3_cam = new Vec3();
            v4_cam = new Vec3();
            Count = 4;
            id_counter++;
            name = "fac" + id_counter;
        }

        //конструктор с 3-мя векторными парметрами - грань из 3-х точек
        public Facet3(Vec3 v1_0, Vec3 v2_0, Vec3 v3_0)
        {
            v1 = new Vec3(v1_0.x, v1_0.y, v1_0.z);
            v2 = new Vec3(v2_0.x, v2_0.y, v2_0.z);
            v3 = new Vec3(v3_0.x, v3_0.y, v3_0.z);

            v1_cam = new Vec3(v1_0.x, v1_0.y, v1_0.z);
            v2_cam = new Vec3(v2_0.x, v2_0.y, v2_0.z);
            v3_cam = new Vec3(v3_0.x, v3_0.y, v3_0.z);
            Count = 3;
            id_counter++;
            name = "fac" + id_counter;
        }

        //конструктор с 4-мя векторными парметрами - грань из 4-х точек
        public Facet3(Vec3 v1_0, Vec3 v2_0, Vec3 v3_0, Vec3 v4_0)
        {
            v1 = new Vec3(v1_0.x, v1_0.y, v1_0.z);
            v2 = new Vec3(v2_0.x, v2_0.y, v2_0.z);
            v3 = new Vec3(v3_0.x, v3_0.y, v3_0.z);
            v4 = new Vec3(v4_0.x, v4_0.y, v4_0.z);

            v1_cam = new Vec3(v1_0.x, v1_0.y, v1_0.z);
            v2_cam = new Vec3(v2_0.x, v2_0.y, v2_0.z);
            v3_cam = new Vec3(v3_0.x, v3_0.y, v3_0.z);
            v4_cam = new Vec3(v4_0.x, v4_0.y, v4_0.z);
            Count = 4;
            id_counter++;
            name = "fac" + id_counter;
        }

        static readonly char[] cc = new char[2];
        //конвертирует байт в строку для представления 16-тиричными числами
        static string Byte2Hex(byte b)
        {
            //старший разряд
            byte b1 = ((byte)(b >> 4));
            cc[0] = (char)(b1 > 9 ? b1 + 0x37 + 0x20 : b1 + 0x30);
            //младший разряд
            byte b2 = ((byte)(b & 0x0F));
            cc[1] = (char)(b2 > 9 ? b2 + 0x37 + 0x20 : b2 + 0x30);

            return new string(cc);
        }

        //преобразование из System.Drawing.Color в Html-представление цвета, используется другими классами
        public static string ColorHtml(System.Drawing.Color color)
        {
            return "#" + string.Format("{0}{1}{2}", Byte2Hex(color.R), Byte2Hex(color.G), Byte2Hex(color.B));
        }

        //преобразование цвета грани из System.Drawing.Color в Html-представление цвета
        public string ColorHtml()
        {
            System.Drawing.Color color = clr.GetColor();
            return "#" + string.Format("{0}{1}{2}", Byte2Hex(color.R), Byte2Hex(color.G), Byte2Hex(color.B));
        }

        //преобразование из System.Drawing.Color в Html-представление цвета, dark (яркость) от 0 до 1
        public static string ColorDarkHtml(System.Drawing.Color color, double dark)
        {
            byte r = (byte)(color.R * dark);
            byte g = (byte)(color.G * dark);
            byte b = (byte)(color.B * dark);
            return "#" + string.Format("{0}{1}{2}", Byte2Hex(r), Byte2Hex(g), Byte2Hex(b));
        }

        //преобразование цвета грани из System.Drawing.Color в Html-представление цвета, dark (яркость) от 0 до 1
        public string ColorDarkHtml(double dark)
        {
            System.Drawing.Color color = clr.GetColor();
            byte r = (byte)(color.R * dark);
            byte g = (byte)(color.G * dark);
            byte b = (byte)(color.B * dark);
            return "#" + string.Format("{0}{1}{2}", Byte2Hex(r), Byte2Hex(g), Byte2Hex(b));
        }

        //найти нормаль для грани
        public void CalcNormal(bool bCamera = false)
        {
            double aa;
            if (bCamera)
            {   //в системе координат камеры
                Vec3 tvec1 = new Vec3(v2_cam.x - v1_cam.x, v2_cam.y - v1_cam.y, v2_cam.z - v1_cam.z);
                Vec3 tvec2 = new Vec3(v3_cam.x - v1_cam.x, v3_cam.y - v1_cam.y, v3_cam.z - v1_cam.z);
                Vec3.Product(tvec1, tvec2, ref normal_cam);
                aa = normal_cam.Normalize();
            }
            else
            {   //в собственной системе координат
                Vec3 tvec1 = new Vec3(v2.x - v1.x, v2.y - v1.y, v2.z - v1.z);
                Vec3 tvec2 = new Vec3(v3.x - v1.x, v3.y - v1.y, v3.z - v1.z);
                Vec3.Product(tvec1, tvec2, ref normal);
                aa = normal.Normalize();
            }
            //площадь
            area = (Count == 3 ? aa * 0.5 : aa);
        }

        //найти центр для грани
        public void Center(out double x, out double y, out double z, bool bCamera = false)
        {
            if (bCamera)
            {   //в системе координат камеры
                x = (v1_cam.x + v2_cam.x + v3_cam.x + (Count == 4 ? v4_cam.x : 0)) / Count;
                y = (v1_cam.y + v2_cam.y + v3_cam.y + (Count == 4 ? v4_cam.y : 0)) / Count;
                z = (v1_cam.z + v2_cam.z + v3_cam.z + (Count == 4 ? v4_cam.z : 0)) / Count;
            }
            else
            {   //в собственной системе координат
                x = (v1.x + v2.x + v3.x + (Count == 4 ? v4.x : 0)) / Count;
                y = (v1.y + v2.y + v3.y + (Count == 4 ? v4.y : 0)) / Count;
                z = (v1.z + v2.z + v3.z + (Count == 4 ? v4.z : 0)) / Count;
            }
        }

        //найти случайную точку в грани
        public void RandomPoint(out double x, out double y, out double z, bool bCamera = false)
        {
            double f1 = rand.NextDouble();
            double f2 = rand.NextDouble();
            double f3 = rand.NextDouble();
            double f4 = (Count > 3 ? rand.NextDouble() : 0);
            double f = f1 + f2 + f3 + f4;
            if (bCamera)
            {   //в системе координат камеры
                x = (f1 * v1_cam.x + f2 * v2_cam.x + f3 * v3_cam.x + (Count > 3 ? f4 * v4_cam.x : 0)) / f;
                y = (f1 * v1_cam.y + f2 * v2_cam.y + f3 * v3_cam.y + (Count > 3 ? f4 * v4_cam.y : 0)) / f;
                z = (f1 * v1_cam.z + f2 * v2_cam.z + f3 * v3_cam.z + (Count > 3 ? f4 * v4_cam.z : 0)) / f;
            }
            else
            {   //в собственной системе координат
                x = (f1 * v1.x + f2 * v2.x + f3 * v3.x + (Count > 3 ? f4 * v4.x : 0)) / f;
                y = (f1 * v1.y + f2 * v2.y + f3 * v3.y + (Count > 3 ? f4 * v4.y : 0)) / f;
                z = (f1 * v1.z + f2 * v2.z + f3 * v3.z + (Count > 3 ? f4 * v4.z : 0)) / f;
            }
        }

        //строковое представление
        public new string ToString()
        {
            string s = name;
            s += (";v1 " + v1_cam.ToString());
            s += (";v2 " + v2_cam.ToString());
            s += (";v3 " + v3_cam.ToString());
            if (Count >= 4)
                s += (";v4 " + v4_cam.ToString());
            s += (";norm " + normal_cam.ToString());
            s += (";dark " + Math.Round(dark, 2).ToString());
            s += (";clr " + ColorHtml());

            return s;
        }
        public string ToStringOrig()
        {
            string s = name;
            s += (";v1 " + v1.ToString());
            s += (";v2 " + v2.ToString());
            s += (";v3 " + v3.ToString());
            if (Count >= 4)
                s += (";v4 " + v4.ToString());
            s += (";norm " + normal.ToString());
            s += (";dark " + Math.Round(dark, 2).ToString());
            s += (";clr " + ColorHtml());

            return s;
        }
    }

    /// <summary>
    /// Геометрический объект (Geometrical object - GeOb) - список граней
    /// </summary>
    public class GeOb
    {
        protected static int id_counter = 0; //счетчик
        protected static int id_fac = 0; //счетчик для граней
        public string name = ""; //название
        public double radius = 1; //размер
        public ColorWrapper clr = new ColorWrapper(); //основной цвет объекта
        public bool bDrawNorm = false;  //true - рисовать нормали
        public bool bDrawBack = false;  //true - рисовать задние грани
        public int iFill = 1;  //1-рисовать грани, 2-ребра, 3-грани и ребра
        public List<Facet3> lstFac = new List<Facet3>();   //список граней

        //центр вначале совпадает с координатами базового объекта
        //смещение
        Vec3 _vShift = new Vec3(0, 0, 0);
        //углы поворота
        double xRotor = 0; //вращение ящика вокруг оси X
        double yRotor = 0; //вращение ящика вокруг оси Y
        double zRotor = 0; //вращение ящика вокруг оси Z
        Mat3 matRotor = new Mat3(); //матрица поворота
        //коэффициенты растяжения
        double _scaleX = 1, _scaleY = 1, _scaleZ = 1;
        bool bNeedTranf = false;

        public GeOb()
        {
            id_counter++;
            name = "geo" + id_counter;
        }

        public void ColorSet(string color)
        {
            if (!string.IsNullOrEmpty(color))
            {
                try
                {
                    string[] arr = color.Split(',');
                    if (arr.Length == 1)
                    {
                        var x = System.Drawing.Color.FromName(color);
                        clr.SetColor(x);
                    }
                    else if (arr.Length == 3)
                    {
                        var x = System.Drawing.Color.FromArgb(int.Parse(arr[0]), int.Parse(arr[1]), int.Parse(arr[2]));
                        clr.SetColor(x);
                    }
                    else if (arr.Length == 4)
                    {
                        var x = System.Drawing.Color.FromArgb(int.Parse(arr[0]), int.Parse(arr[1]), int.Parse(arr[2]), int.Parse(arr[3]));
                        clr.SetColor(x);
                    }
                }
                catch (Exception) { };
            }
        }

        public new string ToString()
        {
            string s = name;
            foreach (var fac in lstFac)
            {
                s += (";" + fac.ToString());
            }

            return s;
        }
        public string ToStringOrig()
        {
            string s = name;
            foreach (var fac in lstFac)
            {
                s += (";" + fac.ToStringOrig());
            }
            return s;
        }
        public string Info()
        {
            string s = string.Format("GeOb {0}, размер {1}, vShift {2}, xRotor {3}, yRotor {4}, zRotor {5}, scaleX {6}, scaleY {7}, scaleZ {8}, граней {9}",
                name, radius, vShift.ToString(),
                xRotor, yRotor, zRotor, scaleX, scaleY, scaleZ, lstFac.Count);

            return s;
        }

        public double XRotor
        {
            get
            {
                return xRotor;
            }
            set
            {
                xRotor = value;
                //matRotor.Build(xRotor, yRotor, zRotor);
                bNeedTranf = true;
            }
        }

        public double YRotor
        {
            get
            {
                return yRotor;
            }
            set
            {
                yRotor = value;
                //matRotor.Build(xRotor, yRotor, zRotor);
                bNeedTranf = true;
            }
        }

        public double ZRotor
        {
            get
            {
                return zRotor;
            }
            set
            {
                zRotor = value;
                //matRotor.Build(xRotor, yRotor, zRotor);
                bNeedTranf = true;
            }
        }

        public void Reshape(double x0, double x1, double y0, double y1, double z0, double z1)
        {
            vShift.x = (x1 + x0) * 0.5;
            vShift.y = (y1 + y0) * 0.5;
            vShift.z = (z1 + z0) * 0.5;

            scaleX = Math.Abs(x1 - x0);
            scaleY = Math.Abs(y1 - y0);
            scaleZ = Math.Abs(z1 - z0);
        }

        public void Rotate(Vec3 v, ref Vec3 v_new)
        {
            matRotor.Mult(v, ref v_new);
        }

        public void RotateVec(Vec3 a1, Vec3 b1, Vec3 c1)
        {
            matRotor.BuildVec(a1, b1, c1);
            //TODO need real calc
            xRotor = 0.1;
            yRotor = 0.1;
            zRotor = 0.1;
        }

        public void Fractal(int n = 1, double weight = 0.33)
        {
            int i;
            double x, y, z, step;
            Vec3 v3 = new Vec3(0, 0, 0);
            Random rand = new Random();
            //test
            /*i = 0;
            Facet3 prev = null;
            foreach (var fac in lstFac)
            {
                if (i == 0) fac.clr.SetColor(System.Drawing.Color.FromName("Red"));
                if (i == 1) fac.clr.SetColor(System.Drawing.Color.FromName("Orange"));
                if (i == 2) fac.clr.SetColor(System.Drawing.Color.FromName("Yellow"));
                if (i == 3) fac.clr.SetColor(System.Drawing.Color.FromName("Blue"));
                i++;
                prev = fac;
            }*/
            for (i = 0; i < n; i++)
            {
                List<Facet3> lstNew = new List<Facet3>();
                foreach (var fac in lstFac)
                {
                    //fac.RandomPoint(out x, out y, out z);
                    fac.Center(out x, out y, out z);
                    fac.CalcNormal();
                    step = Math.Sqrt(fac.area) * weight * (rand.NextDouble() - 0.25);
                    v3.Copy(x + fac.normal.x * step, y + fac.normal.y * step, z + fac.normal.z * step);

                    //create new facets, add to list
                    Facet3 fac0_a = new Facet3(fac.v1, fac.v2, v3);
                    fac0_a.clr.Copy(fac.clr);
                    fac0_a.name = name + "-fo" + id_fac++;
                    lstNew.Add(fac0_a);

                    Facet3 fac0_b = new Facet3(fac.v2, fac.v3, v3);
                    fac0_b.clr.Copy(fac.clr);
                    fac0_b.name = name + "-rt" + id_fac++;
                    lstNew.Add(fac0_b);

                    if (fac.Count == 3)
                    {
                        Facet3 fac0_c = new Facet3(fac.v3, fac.v1, v3);
                        fac0_c.clr.Copy(fac.clr);
                        fac0_c.name = name + "-lf" + id_fac++;
                        lstNew.Add(fac0_c);
                    }
                    else
                    {
                        Facet3 fac0_c = new Facet3(fac.v3, fac.v4, v3);
                        fac0_c.clr.Copy(fac.clr);
                        fac0_c.name = name + "-lf" + id_fac++;
                        lstNew.Add(fac0_c);

                        Facet3 fac0_d = new Facet3(fac.v4, fac.v1, v3);
                        fac0_d.clr.Copy(fac.clr);
                        fac0_d.name = name + "-bt" + id_fac++;
                        lstNew.Add(fac0_d);
                    }
                }
                this.lstFac = lstNew;
            }
        }

        public void SetColor(System.Drawing.Color cc)
        {
            clr.SetColor(cc);
            foreach (var fac in lstFac)
            {
                fac.clr.SetColor(cc);
            }
        }

        public void CutByPlane(double a, double b, double c, double d, bool bClose = true)
        {
            List<Facet3> lstNew = new List<Facet3>();
            foreach (var fac in lstFac)
            {
                if (fac.Count == 3) lstNew.Add(fac);
                else
                {
                    Facet3 fac0_c = new Facet3(fac.v1, fac.v2, fac.v3);
                    fac0_c.clr.Copy(fac.clr);
                    fac0_c.name = name + "fac" + id_fac++;
                    lstNew.Add(fac0_c);

                    fac0_c = new Facet3(fac.v1, fac.v3, fac.v4);
                    fac0_c.clr.Copy(fac.clr);
                    fac0_c.name = name + "fac" + id_fac++;
                    lstNew.Add(fac0_c);
                }
            }
            //iFill = 3;

            List<Facet3> lstNew2 = new List<Facet3>();
            double[] dPositive = new double[3];
            Vec3[] vv = { new Vec3(), new Vec3(), new Vec3(), new Vec3() };
            List<Vec3> lstClose = new List<Vec3>();//для замыкания
            foreach (var fac in lstNew)
            {
                dPositive[0] = a * fac.v1.x + b * fac.v1.y + c * fac.v1.z + d;
                dPositive[1] = a * fac.v2.x + b * fac.v2.y + c * fac.v2.z + d;
                dPositive[2] = a * fac.v3.x + b * fac.v3.y + c * fac.v3.z + d;

                int nNeg = (dPositive[0] <= 0 ? 1 : 0) + (dPositive[1] <= 0 ? 1 : 0) + (dPositive[2] <= 0 ? 1 : 0);
                if (nNeg == 3)
                {
                    lstNew2.Add(fac);
                    continue;
                }
                if (nNeg == 0) continue;

                if (nNeg == 1)
                {   //2 точки за плоскостью, одна остается
                    if (dPositive[0] <= 0)
                    {
                        vv[0].Copy(fac.v1);
                        if (!Vec3.HitPlane(fac.v1, fac.v2, ref vv[1], a, b, c, d)) continue;
                        if (!Vec3.HitPlane(fac.v1, fac.v3, ref vv[2], a, b, c, d)) continue;
                        lstClose.Add(new Vec3(vv[1].x, vv[1].y, vv[1].z));
                        lstClose.Add(new Vec3(vv[2].x, vv[2].y, vv[2].z));
                    }
                    else if (dPositive[1] <= 0)
                    {
                        vv[1].Copy(fac.v2);
                        if (!Vec3.HitPlane(fac.v2, fac.v1, ref vv[0], a, b, c, d)) continue;
                        if (!Vec3.HitPlane(fac.v2, fac.v3, ref vv[2], a, b, c, d)) continue;
                        lstClose.Add(new Vec3(vv[0].x, vv[0].y, vv[0].z));
                        lstClose.Add(new Vec3(vv[2].x, vv[2].y, vv[2].z));
                    }
                    else
                    {
                        vv[2].Copy(fac.v3);
                        if (!Vec3.HitPlane(fac.v3, fac.v1, ref vv[0], a, b, c, d)) continue;
                        if (!Vec3.HitPlane(fac.v3, fac.v2, ref vv[1], a, b, c, d)) continue;
                        lstClose.Add(new Vec3(vv[0].x, vv[0].y, vv[0].z));
                        lstClose.Add(new Vec3(vv[1].x, vv[1].y, vv[1].z));
                    }

                    Facet3 fac0_c = new Facet3(vv[0], vv[1], vv[2]);
                    fac0_c.clr.Copy(fac.clr);
                    fac0_c.name = name + "fac" + id_fac++;
                    lstNew2.Add(fac0_c);
                }
                else
                {   //2 точки остаются, одна за плоскостью
                    if (dPositive[0] <= 0 && dPositive[1] <= 0)
                    {
                        vv[0].Copy(fac.v1);
                        vv[1].Copy(fac.v2);
                        if (!Vec3.HitPlane(fac.v2, fac.v3, ref vv[2], a, b, c, d)) continue;
                        if (!Vec3.HitPlane(fac.v1, fac.v3, ref vv[3], a, b, c, d)) continue;
                        lstClose.Add(new Vec3(vv[2].x, vv[2].y, vv[2].z));
                        lstClose.Add(new Vec3(vv[3].x, vv[3].y, vv[3].z));
                    }
                    else if (dPositive[1] <= 0 && dPositive[2] <= 0)
                    {
                        vv[0].Copy(fac.v2);
                        vv[1].Copy(fac.v3);
                        if (!Vec3.HitPlane(fac.v3, fac.v1, ref vv[2], a, b, c, d)) continue;
                        if (!Vec3.HitPlane(fac.v2, fac.v1, ref vv[3], a, b, c, d)) continue;
                        lstClose.Add(new Vec3(vv[2].x, vv[2].y, vv[2].z));
                        lstClose.Add(new Vec3(vv[3].x, vv[3].y, vv[3].z));
                    }
                    else
                    {
                        vv[0].Copy(fac.v3);
                        vv[1].Copy(fac.v1);
                        if (!Vec3.HitPlane(fac.v1, fac.v2, ref vv[2], a, b, c, d)) continue;
                        if (!Vec3.HitPlane(fac.v3, fac.v2, ref vv[3], a, b, c, d)) continue;
                        lstClose.Add(new Vec3(vv[2].x, vv[2].y, vv[2].z));
                        lstClose.Add(new Vec3(vv[3].x, vv[3].y, vv[3].z));
                    }

                    Facet3 fac0_c = new Facet3(vv[0], vv[1], vv[2], vv[3]);
                    fac0_c.clr.Copy(fac.clr);
                    fac0_c.name = name + "fac" + id_fac++;
                    lstNew2.Add(fac0_c);
                }
            }

            //надо замкнуть отрез
            if (lstClose.Count > 2 && bClose)
            {
                double xCe = 0, yCe = 0, zCe = 0;
                for (int i = 0; i < lstClose.Count; i++)
                {
                    Vec3 v = lstClose.ElementAt(i);
                    xCe += v.x;
                    yCe += v.y;
                    zCe += v.z;
                }
                vv[0].Copy(xCe / lstClose.Count, yCe / lstClose.Count, zCe / lstClose.Count);
                Vec3 vNormal = new Vec3(a, b, c);

                for (int i = 0; i < lstClose.Count / 2; i++)
                {
                    Vec3 v1 = lstClose.ElementAt(i * 2);
                    Vec3 v2 = lstClose.ElementAt(i * 2 + 1);


                    vv[1].Copy(v1.x - vv[0].x, v1.y - vv[0].y, v1.z - vv[0].z);
                    vv[2].Copy(v2.x - vv[0].x, v2.y - vv[0].y, v2.z - vv[0].z);
                    Vec3.Product(vv[1], vv[2], ref vv[3]);

                    double sc = vNormal.ScalarProduct(vv[3]);
                    Facet3 fac0_c = sc > 0 ? new Facet3(vv[0], v1, v2) : new Facet3(vv[0], v2, v1);
                    fac0_c.clr.Copy(clr);
                    fac0_c.name = name + "fac" + id_fac++;
                    lstNew2.Add(fac0_c);
                }
            }

            this.lstFac = lstNew2;
        }

        //2-х мерное изображение, ARGB в карте слева направо и сверху вниз
        //натягиваем на объем типа сферы
        public void SetBitmap(BitmapSimple bm, bool bLog = false)
        {
            if (lstFac.Count == 0) return;
            double xCe = 0, yCe = 0, zCe = 0;
            double x, y, z, len, fi, teta;
            /*foreach (var fac in lstFac)
            {
                fac.Center(out x, out y, out z);
                xCe += x;
                yCe += y;
                zCe += z;
            }
            xCe /= lstFac.Count;
            yCe /= lstFac.Count;
            zCe /= lstFac.Count;*/

            //todo squeeze image

            Vec3 v = new Vec3();
            int w = bm.width;
            int h = bm.height;
            int i, j, m = 0;
            double PI2 = 2 * Math.PI;
            double PI_half = 0.5 * Math.PI;

            double area = 0, imgArea = w * h;
            foreach (var fac in lstFac)
            {
                if (fac.area == 0) fac.CalcNormal();
                area += fac.area;
            }

            foreach (var fac in lstFac)
            {
                fac.Center(out x, out y, out z);
                v.Copy(x - xCe, y - yCe, z - zCe);
                len = v.Length();
                if (len == 0) continue;
                teta = Math.Asin(v.z / len); //-PI/2 - PI/2
                //fi 0 - 2PI
                fi = v.x != 0 ? Math.Atan2(Math.Abs(v.y), Math.Abs(v.x)) : PI_half;
                if (v.y >= 0 && v.x < 0) fi = Math.PI - fi;
                else if (v.y < 0 && v.x < 0) fi = Math.PI + fi;
                else if (v.y < 0 && v.x >= 0) fi = PI2 - fi;

                i = (int)Math.Floor((w * fi) / PI2);
                if (i < 0)
                {
                    int kk = 0;
                }
                i = i % w;

                j = (int)Math.Floor((h * (teta + PI_half) / Math.PI));
                if (j < 0)
                {
                    int kk = 0;
                }
                j = j % h;
                j = h - 1 - j;//reverse by vertical

                //how many pixels we need?
                double ar = (fac.area / area) * imgArea;
                int rad = (int)Math.Floor(Math.Sqrt(ar) * 0.5);
                int c = 0;
                if (rad < 1) c = bm.map[i + j * h];
                else
                {
                    long n = 0, al = 0, bl = 0, gr = 0, rd = 0;
                    for (int x1 = i - rad; x1 <= i + rad; x1++)
                    {
                        if (x1 >= 0 && x1 < w)
                        {
                            for (int y1 = j - rad; y1 <= j + rad; y1++)
                            {
                                if (y1 >= 0 && y1 < h)
                                {
                                    c = bm.map[x1 + y1 * w];
                                    bl += (c & 0xFF);
                                    gr += ((c >> 8) & 0xFF);
                                    rd += ((c >> 16) & 0xFF);
                                    al += ((c >> 24) & 0xFF);
                                    n++;
                                }
                            }
                        }
                    }
                    if (n > 0)
                    {
                        al /= n;
                        bl /= n;
                        gr /= n;
                        rd /= n;
                    }
                    c = System.Drawing.Color.FromArgb((int)al, (int)rd, (int)gr, (int)bl).ToArgb();
                }
                fac.clr.Argb = c;

                if (bLog)
                {
                    string s = string.Format("{0}, {1}, x={2}, y={3}, z={4}, fi={5}, te={6}, i={7}, j={8}",
                      m, fac.name, v.x, v.y, v.z, fi, teta, i, j);
                    Dynamo.Log(s);
                }

                m++;
            }
        }

        //натянуть картинку на поверхность близкую к плоскости
        public void SetBitmapPlane(BitmapSimple bm)
        {
            if (lstFac.Count == 0) return;
            double xCe = 0, yCe = 0, zCe = 0;
            double x, y, z, len, fi, teta;
            /*foreach (var fac in lstFac)
            {
                fac.Center(out x, out y, out z);
                xCe += x;
                yCe += y;
                zCe += z;
            }
            xCe /= lstFac.Count;
            yCe /= lstFac.Count;
            zCe /= lstFac.Count;*/


            Vec3 v = new Vec3();
            int w = bm.width;
            int h = bm.height;
            int i, j, m = 0, iMin = 0, iMax = 0, jMin = 0, jMax = 0;
            double PI2 = 2 * Math.PI;
            double PI_half = 0.5 * Math.PI;

            double area = 0, imgArea = w * h;
            foreach (var fac in lstFac)
            {
                if (fac.area == 0) fac.CalcNormal();
                area += fac.area;
            }

            foreach (var fac in lstFac)
            {
                fac.Center(out x, out y, out z);
                v.Copy(x - xCe, y - yCe, z - zCe);
                len = v.Length();
                if (len == 0) continue;
                teta = Math.Asin(v.z / len); //-PI/2 - PI/2
                //fi 0 - 2PI
                fi = v.x != 0 ? Math.Atan2(Math.Abs(v.y), Math.Abs(v.x)) : PI_half;
                if (v.y >= 0 && v.x < 0) fi = Math.PI - fi;
                else if (v.y < 0 && v.x < 0) fi = Math.PI + fi;
                else if (v.y < 0 && v.x >= 0) fi = PI2 - fi;

                i = (int)Math.Floor((w * fi) / PI2);
                if (i < 0)
                {   //debug
                    int kk = 0;
                }
                i = i % w;

                j = (int)Math.Floor((h * (teta + PI_half) / Math.PI));
                if (j < 0)
                {   //debug
                    int kk = 0;
                }
                j = j % h;
                j = h - 1 - j;//reverse by vertical
                if (m == 0)
                {
                    iMin = i;
                    iMax = i;
                    jMin = j;
                    jMax = j;
                }
                else
                {
                    if (iMin > i) iMin = i;
                    if (iMax < i) iMax = i;
                    if (jMin > j) jMin = j;
                    if (jMax < j) jMax = j;
                }
                m++;
            }
            //Dynamo.Log(string.Format("iMin = {0}, iMax = {1}, jMin = {2}, jMax={3}", iMin, iMax, jMin, jMax));

            //stretch
            int di = iMax - iMin;
            if (di == 0) di = 1;
            int dj = jMax - jMin;
            if (dj == 0) dj = 1;
            m = 0;
            foreach (var fac in lstFac)
            {
                fac.Center(out x, out y, out z);
                v.Copy(x - xCe, y - yCe, z - zCe);
                len = v.Length();
                if (len == 0) continue;
                teta = Math.Asin(v.z / len); //-PI/2 - PI/2
                //fi 0 - 2PI
                fi = v.x != 0 ? Math.Atan2(Math.Abs(v.y), Math.Abs(v.x)) : PI_half;
                if (v.y >= 0 && v.x < 0) fi = Math.PI - fi;
                else if (v.y < 0 && v.x < 0) fi = Math.PI + fi;
                else if (v.y < 0 && v.x >= 0) fi = PI2 - fi;

                i = (int)Math.Floor((w * fi) / PI2);
                i = i % w;

                j = (int)Math.Floor((h * (teta + PI_half) / Math.PI));
                j = j % h;
                j = h - 1 - j;//reverse by vertical

                //stretch
                i = ((i - iMin) * w) / di;
                j = ((j - jMin) * h) / dj;
                //Dynamo.Log(string.Format("i = {0}, j = {1}", i, j));

                //how many pixels we need?
                double ar = (fac.area / area) * imgArea;
                int rad = (int)Math.Floor(Math.Sqrt(ar) * 0.5);
                int c = 0;
                if (rad < 1) c = bm.map[i + j * h];
                else
                {
                    long n = 0, al = 0, bl = 0, gr = 0, rd = 0;
                    for (int x1 = i - rad; x1 <= i + rad; x1++)
                    {
                        if (x1 >= 0 && x1 < w)
                        {
                            for (int y1 = j - rad; y1 <= j + rad; y1++)
                            {
                                if (y1 >= 0 && y1 < h)
                                {
                                    c = bm.map[x1 + y1 * w];
                                    bl += (c & 0xFF);
                                    gr += ((c >> 8) & 0xFF);
                                    rd += ((c >> 16) & 0xFF);
                                    al += ((c >> 24) & 0xFF);
                                    n++;
                                }
                            }
                        }
                    }
                    if (n > 0)
                    {
                        al = al / n;
                        bl = bl / n;
                        gr = gr / n;
                        rd = rd / n;
                    }
                    c = System.Drawing.Color.FromArgb((int)al, (int)rd, (int)gr, (int)bl).ToArgb();
                }
                fac.clr.Argb = c;//m % 2 == 0 ? c : System.Drawing.Color.FromName("Green").ToArgb();
                m++;
            }
        }

        //разбить каждую грань на мелкие для натягивания изображения
        public void Divide(int n = 1)
        {
            bool bHas4 = false;
            foreach (var fac in lstFac)
            {
                if (fac.Count > 3)
                {
                    bHas4 = true;
                    break;
                }
            }
            if (bHas4)
            {
                List<Facet3> lstNew = new List<Facet3>();
                foreach (var fac in lstFac)
                {
                    if (fac.Count == 3) lstNew.Add(fac);
                    else
                    {
                        Facet3 fac0_c = new Facet3(fac.v1, fac.v2, fac.v3);
                        fac0_c.clr.Copy(fac.clr);
                        fac0_c.name = name + "fac" + id_fac++;
                        lstNew.Add(fac0_c);

                        fac0_c = new Facet3(fac.v1, fac.v3, fac.v4);
                        fac0_c.clr.Copy(fac.clr);
                        fac0_c.name = name + "fac" + id_fac++;
                        lstNew.Add(fac0_c);
                    }
                }
                lstFac = lstNew;
            }

            Vec3 v12 = new Vec3();
            Vec3 v13 = new Vec3();
            Vec3 v23 = new Vec3();

            for (int i = 0; i < n; i++)
            {
                List<Facet3> lstNew = new List<Facet3>();
                foreach (var fac in lstFac)
                {
                    //находим средние точки на каждом ребре трехугольной грани
                    v12.Copy(0.5 * (fac.v1.x + fac.v2.x), 0.5 * (fac.v1.y + fac.v2.y), 0.5 * (fac.v1.z + fac.v2.z));
                    v13.Copy(0.5 * (fac.v1.x + fac.v3.x), 0.5 * (fac.v1.y + fac.v3.y), 0.5 * (fac.v1.z + fac.v3.z));
                    v23.Copy(0.5 * (fac.v2.x + fac.v3.x), 0.5 * (fac.v2.y + fac.v3.y), 0.5 * (fac.v2.z + fac.v3.z));

                    //создаем 4 новые грани вместо большой
                    Facet3 fac0_c = new Facet3(fac.v1, v12, v13);
                    fac0_c.clr.Copy(fac.clr);
                    fac0_c.name = name + "fac" + id_fac++;
                    lstNew.Add(fac0_c);

                    fac0_c = new Facet3(v12, fac.v2, v23);
                    fac0_c.clr.Copy(fac.clr);
                    fac0_c.name = name + "fac" + id_fac++;
                    lstNew.Add(fac0_c);

                    fac0_c = new Facet3(v12, v23, v13);
                    fac0_c.clr.Copy(fac.clr);
                    fac0_c.name = name + "fac" + id_fac++;
                    lstNew.Add(fac0_c);

                    fac0_c = new Facet3(fac.v3, v13, v23);
                    fac0_c.clr.Copy(fac.clr);
                    fac0_c.name = name + "fac" + id_fac++;
                    lstNew.Add(fac0_c);
                }
                lstFac = lstNew;
            }
        }

        //разбить каждую грань на мелкие для натягивания изображения с искажением
        public void DivideFractal(int n = 1)
        {
            double dMinDistance = 0.000001;
            bool bHas4 = false;
            foreach (var fac in lstFac)
            {
                if (fac.Count > 3)
                {
                    bHas4 = true;
                    break;
                }
            }
            if (bHas4)
            {
                List<Facet3> lstNew = new List<Facet3>();
                foreach (var fac in lstFac)
                {
                    if (fac.Count == 3) lstNew.Add(fac);
                    else
                    {
                        Facet3 fac0_c = new Facet3(fac.v1, fac.v2, fac.v3);
                        fac0_c.clr.Copy(fac.clr);
                        fac0_c.name = name + "fac" + id_fac++;
                        lstNew.Add(fac0_c);

                        fac0_c = new Facet3(fac.v1, fac.v3, fac.v4);
                        fac0_c.clr.Copy(fac.clr);
                        fac0_c.name = name + "fac" + id_fac++;
                        lstNew.Add(fac0_c);
                    }
                }
                lstFac = lstNew;
            }

            Vec3 v12 = new Vec3();
            Vec3 v13 = new Vec3();
            Vec3 v23 = new Vec3();
            Vec3 vDiff = new Vec3();
            Random rand = new Random();
            //кортеж - информация о вершинах и их гранях, item1 - новая срединная точка
            List<Tuple<Vec3, Vec3, Vec3, List<Facet3>, List<Facet3>, List<int>, List<int>>> lstEdges =
                new List<Tuple<Vec3, Vec3, Vec3, List<Facet3>, List<Facet3>, List<int>, List<int>>>();
            Vec3 fac_v1, fac_v2;

            for (int i = 0; i < n; i++)
            {
                List<Facet3> lstNew = new List<Facet3>();
                lstEdges.Clear();
                //постоить ребра
                int ifa = 0;
                foreach (var fac in lstFac)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (k == 0)
                        { //edge v1-v2
                            fac_v1 = fac.v1;
                            fac_v2 = fac.v2;
                        }
                        else if (k == 1)
                        { //v2-v3                     
                            fac_v1 = fac.v2;
                            fac_v2 = fac.v3;
                        }
                        else
                        { //v1-v3
                            fac_v1 = fac.v1;
                            fac_v2 = fac.v3;
                        }
                        bool bFound = false;
                        foreach (var tup in lstEdges)
                        {
                            double len1 = tup.Item1.Distance(fac_v1);
                            double len2 = tup.Item2.Distance(fac_v2);
                            if (len1 < dMinDistance && len2 < dMinDistance)
                            {
                                bFound = true;
                                break;
                            }
                            //rotate
                            len1 = tup.Item1.Distance(fac_v2);
                            len2 = tup.Item2.Distance(fac_v1);
                            if (len1 < dMinDistance && len2 < dMinDistance)
                            {
                                bFound = true;
                                break;
                            }
                        }

                        if (bFound == false)
                        {
                            var tup = new Tuple<Vec3, Vec3, Vec3, List<Facet3>, List<Facet3>, List<int>, List<int>>
                                (new Vec3(), new Vec3(), new Vec3(),
                                new List<Facet3>(), new List<Facet3>(), new List<int>(), new List<int>());

                            tup.Item1.Copy(fac_v1);
                            tup.Item2.Copy(fac_v2);
                            vDiff.Copy(fac_v2.x - fac_v1.x, fac_v2.y - fac_v1.y, fac_v2.z - fac_v1.z);
                            var len = vDiff.Length();

                            if (vDiff.x == 0) vDiff.x = len * (rand.NextDouble() - 0.5) * 0.33;
                            if (vDiff.y == 0) vDiff.y = len * (rand.NextDouble() - 0.5) * 0.33;
                            if (vDiff.z == 0) vDiff.z = len * (rand.NextDouble() - 0.5) * 0.33;

                            double f1 = 0.5 + (rand.NextDouble() - 0.5) * 0.33;
                            double f2 = 0.5 + (rand.NextDouble() - 0.5) * 0.33;
                            double f3 = 0.5 + (rand.NextDouble() - 0.5) * 0.33;
                            //точка примерно посередине
                            tup.Item3.Copy(fac_v1.x + vDiff.x * f1, fac_v1.y + vDiff.y * f2, fac_v1.z + vDiff.z * f3);

                            lstEdges.Add(tup);
                        }
                    }
                    ifa++;
                }

                foreach (var fac in lstFac)
                {
                    //находим средние точки на каждом ребре трехугольной грани
                    bool bGood = true;
                    for (int k = 0; k < 3; k++)
                    {
                        if (k == 0)
                        { //edge v1-v2
                            fac_v1 = fac.v1;
                            fac_v2 = fac.v2;
                        }
                        else if (k == 1)
                        { //v2-v3                     
                            fac_v1 = fac.v2;
                            fac_v2 = fac.v3;
                        }
                        else
                        { //v1-v3
                            fac_v1 = fac.v1;
                            fac_v2 = fac.v3;
                        }
                        bool bFound = false;
                        foreach (var tup in lstEdges)
                        {
                            double len1 = tup.Item1.Distance(fac_v1);
                            double len2 = tup.Item2.Distance(fac_v2);
                            if (len1 < dMinDistance && len2 < dMinDistance)
                            {
                                bFound = true;
                            }
                            else
                            {
                                //rotate
                                len1 = tup.Item1.Distance(fac_v2);
                                len2 = tup.Item2.Distance(fac_v1);
                                if (len1 < dMinDistance && len2 < dMinDistance)
                                {
                                    bFound = true;
                                }
                            }
                            if (bFound)
                            {
                                if (k == 0) v12.Copy(tup.Item3);
                                else if (k == 1) v23.Copy(tup.Item3);
                                else v13.Copy(tup.Item3);
                                break;
                            }
                        }
                        if (!bFound)
                        {
                            bGood = false;
                            break;
                        }
                    }

                    if (!bGood)
                    {
                        int kk = 0;
                        continue;
                    }
                    //создаем 4 новые грани вместо большой
                    Facet3 fac0_c = new Facet3(fac.v1, v12, v13);
                    fac0_c.clr.Copy(fac.clr);
                    fac0_c.name = name + "fac" + id_fac++;
                    lstNew.Add(fac0_c);

                    fac0_c = new Facet3(v12, fac.v2, v23);
                    fac0_c.clr.Copy(fac.clr);
                    fac0_c.name = name + "fac" + id_fac++;
                    lstNew.Add(fac0_c);

                    fac0_c = new Facet3(v12, v23, v13);
                    fac0_c.clr.Copy(fac.clr);
                    fac0_c.name = name + "fac" + id_fac++;
                    lstNew.Add(fac0_c);

                    fac0_c = new Facet3(fac.v3, v13, v23);
                    fac0_c.clr.Copy(fac.clr);
                    fac0_c.name = name + "fac" + id_fac++;
                    lstNew.Add(fac0_c);
                }
                lstFac = lstNew;
            }
        }

        //разбить единственную грань из 4-х вершин на мелкие
        //предполагаем v1 - нижняя слева, далее против часовой
        //и натянуть BitmapSimple, если задана
        public void DivideIfOne4(int m, int n, BitmapSimple bm = null)
        {
            if (lstFac.Count != 1 || lstFac.ElementAt(0).Count != 4) return;

            Facet3 fac = lstFac.ElementAt(0);
            List<Facet3> lstNew = new List<Facet3>();
            //step from left to right
            double dx_wi = (fac.v2.x - fac.v1.x) / m;
            double dy_wi = (fac.v2.y - fac.v1.y) / m;
            double dz_wi = (fac.v2.z - fac.v1.z) / m;
            //step from top to bottom
            double dx_he = (fac.v4.x - fac.v1.x) / n;
            double dy_he = (fac.v4.y - fac.v1.y) / n;
            double dz_he = (fac.v4.z - fac.v1.z) / n;

            Vec3 v1 = new Vec3();
            Vec3 v2 = new Vec3();
            Vec3 v3 = new Vec3();
            Vec3 v4 = new Vec3();
            double x, y, z;

            for (int j = 0; j < n; j++)
            {
                x = fac.v1.x + dx_he * j;
                y = fac.v1.y + dy_he * j;
                z = fac.v1.z + dz_he * j;

                for (int i = 0; i < m; i++)
                {
                    v1.Copy(x, y, z);
                    v2.Copy(x + dx_wi, y + dy_wi, z + dz_wi);
                    v3.Copy(x + dx_wi + dx_he, y + dy_wi + dy_he, z + dz_wi + dz_he);
                    v4.Copy(x + dx_he, y + dy_he, z + dz_he);

                    x += dx_wi;
                    y += dy_wi;
                    z += dz_wi;

                    Facet3 fac_c = new Facet3(v1, v2, v3, v4);
                    if (bm == null)
                        fac_c.clr.Copy(fac.clr);
                    else
                    {   //stretch
                        int ww = ((i * bm.width) / m) % bm.width;
                        int hh = ((j * bm.height) / n) % bm.height;
                        hh = (bm.height - 1 - hh); //flip horizontally
                        int argb = bm.map[ww + hh * bm.width];
                        fac_c.clr.Argb = argb;
                    }
                    fac_c.name = name + "fac" + id_fac++;
                    lstNew.Add(fac_c);
                }
            }

            lstFac = lstNew;
        }

        //2022-02-01 
        /// <summary>
        /// найти ящик для формы
        /// </summary>
        public void FindBox(out double x0, out double x1, out double y0, out double y1, out double z0, out double z1)
        {
            x0 = double.MaxValue;
            y0 = double.MaxValue;
            z0 = double.MaxValue;
            x1 = double.MinValue;
            y1 = double.MinValue;
            z1 = double.MinValue;
            foreach (var fac in lstFac)
            {
                if (x0 > fac.v1.x) x0 = fac.v1.x;
                if (y0 > fac.v1.y) y0 = fac.v1.y;
                if (z0 > fac.v1.z) z0 = fac.v1.z;
                if (x1 < fac.v1.x) x1 = fac.v1.x;
                if (y1 < fac.v1.y) y1 = fac.v1.y;
                if (z1 < fac.v1.z) z1 = fac.v1.z;

                if (x0 > fac.v2.x) x0 = fac.v2.x;
                if (y0 > fac.v2.y) y0 = fac.v2.y;
                if (z0 > fac.v2.z) z0 = fac.v2.z;
                if (x1 < fac.v2.x) x1 = fac.v2.x;
                if (y1 < fac.v2.y) y1 = fac.v2.y;
                if (z1 < fac.v2.z) z1 = fac.v2.z;

                if (x0 > fac.v3.x) x0 = fac.v3.x;
                if (y0 > fac.v3.y) y0 = fac.v3.y;
                if (z0 > fac.v3.z) z0 = fac.v3.z;
                if (x1 < fac.v3.x) x1 = fac.v3.x;
                if (y1 < fac.v3.y) y1 = fac.v3.y;
                if (z1 < fac.v3.z) z1 = fac.v3.z;
            }
        }

        //2022-02-17
        public double scaleX
        {
            get
            {
                return _scaleX;
            }
            set
            {
                _scaleX = value;
                bNeedTranf = true;
            }
        }
        public double scaleY
        {
            get
            {
                return _scaleY;
            }
            set
            {
                _scaleY = value;
                bNeedTranf = true;
            }
        }
        public double scaleZ
        {
            get
            {
                return _scaleZ;
            }
            set
            {
                _scaleZ = value;
                bNeedTranf = true;
            }
        }
        public Vec3 vShift
        {
            get
            {
                return _vShift;
            }
            set
            {
                _vShift = value;
                bNeedTranf = true;
            }
        }

        public void Transform()
        {
            if (bNeedTranf == false) return;

            if (_vShift.x != 0 || _vShift.y != 0 || _vShift.z != 0 ||
                xRotor != 0 || yRotor != 0 || zRotor != 0 ||
                _scaleX != 1 || _scaleY != 1 || _scaleZ != 1)
            {
                Vec3 vref;
                Vec3 vTemp = new Vec3();
                Vec3 v_new = new Vec3();
                bool bRot = (xRotor != 0 || yRotor != 0 || zRotor != 0);
                if( bRot ) matRotor.Build(xRotor, yRotor, zRotor);

                //для каждой грани найти координаты в системе камеры
                foreach (var fac in lstFac)
                {
                    for (int j = 0; j < fac.Count && j < 4; j++)
                    {
                        if (j == 0)
                        {
                            vref = fac.v1;
                        }
                        else if (j == 1)
                        {
                            vref = fac.v2;
                        }
                        else if (j == 2)
                        {
                            vref = fac.v3;
                        }
                        else
                        {
                            vref = fac.v4;
                        }

                        //масштабировать
                        if (scaleX != 1) vref.x *= scaleX;
                        if (scaleY != 1) vref.y *= scaleY;
                        if (scaleZ != 1) vref.z *= scaleZ;
                        //вращать                    
                        if (bRot)
                        {
                            vTemp.Copy(vref.x, vref.y, vref.z);
                            Rotate(vTemp, ref v_new);

                            vref.x = v_new.x;
                            vref.y = v_new.y;
                            vref.z = v_new.z;
                        }
                        //транслировать
                        vref.Add(vShift);
                    }
                }
                //reset
                _vShift.x = 0;
                _vShift.y = 0;
                _vShift.z = 0;
                xRotor = 0;
                yRotor = 0;
                zRotor = 0;
                _scaleX = 1;
                _scaleY = 1;
                _scaleZ = 1;
            }
            bNeedTranf = false;
        }
    }
}

