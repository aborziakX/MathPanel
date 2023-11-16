using System;

namespace MathPanel
{
    /// <summary>
    /// 3-х мерный вектор
    /// </summary>
    public class Vec3
    {
        public double x, y, z;  //координаты вектора
        //конструктор без параметров
        public Vec3()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        //конструктор с параметрами для инициализации вектора
        public Vec3(double x0 = 0, double y0 = 0, double z0 = 0)
        {
            x = x0;
            y = y0;
            z = z0;
        }

        //конструктор - копирование данных из другого вектора
        public Vec3(Vec3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        //копирование данных
        public void Copy(double x0, double y0, double z0)
        {
            x = x0;
            y = y0;
            z = z0;
        }
        
        //копирование данных из другого вектора
        public void Copy(Vec3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }
        
        //прибавление вектора, параметры - его компоненты
        public void Add(double x0, double y0, double z0)
        {
            x += x0;
            y += y0;
            z += z0;
        }
        
        //прибавление вектора
        public void Add(Vec3 v)
        {
            x += v.x;
            y += v.y;
            z += v.z;
        }

        //сложение 2-х векторов, результат записывается в res
        public void Sum(Vec3 v, ref Vec3 res)
        {
            res.x = x + v.x;
            res.y = y + v.y;
            res.z = z + v.z;
        }

        //сложение 2-х векторов, результат записывается в this
        public void SumTwo(Vec3 v1, Vec3 v2)
        {
            x = v1.x + v2.x;
            y = v1.y + v2.y;
            z = v1.z + v2.z;
        }

        //векторное произведение, результат в новом векторе
        public static Vec3 Product(Vec3 v1, Vec3 v2)
        {
            var v3 = new Vec3();
            Product(v1, v2, ref v3);
            return v3;
        }
        
        //векторное произведение, результат в v3
        public static void Product(Vec3 v1, Vec3 v2, ref Vec3 v3)
        {
            v3.z = v1.x * v2.y - v1.y * v2.x;
            v3.x = v1.y * v2.z - v1.z * v2.y;
            v3.y = v1.z * v2.x - v1.x * v2.z;
        }
        
        //длина вектора
        public double Length()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }
        
        //расстояние между 2-мя векторами
        public double Distance(double x1, double y1, double z1)
        {
            return Math.Sqrt((x - x1) * (x - x1) + (y - y1) * (y - y1) + (z - z1) * (z - z1));
        }
        
        //расстояние между 2-мя векторами
        public double Distance(Vec3 v)
        {
            return Math.Sqrt((x - v.x) * (x - v.x) + (y - v.y) * (y - v.y) + (z - v.z) * (z - v.z));
        }
        
        //нормализация вектора, длина = 1
        public double Normalize()
        {
            double len = Length();
            if (len != 0)
            {
                x /= len;
                y /= len;
                z /= len;
            }
            return len;
        }
        
        //строковое представление
        public new string ToString()
        {
            return string.Format("[x={0}, y={1}, z={2}]", x, y, z);
        }
        
        //скалярное произведение
        public double ScalarProduct(Vec3 v1)
        {
            //len(a) * len(b) * cos(fi)
            return (x * v1.x + y * v1.y + z * v1.z);
        }

        /// <summary>
        /// точка пересечения линии, проходящей через 2 точки, с плоскостью
        /// </summary>
        public static bool HitPlane(Vec3 p1, Vec3 p2, ref Vec3 res, double a, double b, double c, double d)
        {
            var v1 = new Vec3(p2.x - p1.x, p2.y - p1.y, p2.z - p1.z);
            var v2 = new Vec3(a, b, c );    //normal
            if (v2.ScalarProduct(v1) == 0)
                return false;   //нет пересечения
            //p1 + t * v1 -> a.x + b * y + c * z + d = 0
            //a * (p1.x + t * v1.x) + b * (p1.y + t * v1.y) + c * (p1.z + t * v1.z) + d = 0;
            double t = -(a * p1.x + b * p1.y + c * p1.z + d) / (a * v1.x + a * v1.y + c * v1.z);
            res.Copy(p1.x + t * v1.x, p1.y + t * v1.y, p1.z + t * v1.z);
            return true;
        }
        
        //масштабирование
        public void Scale(double p)
        {
            x *= p;
            y *= p;
            z *= p;
        }

        //вращение вокруг оси axe, находим точки vecs на расстоянинии radius
        public static void RotationPoints(Vec3 axe, double radius, Vec3[] vecs)
        {
            if (vecs == null || vecs.Length == 0 ||
                (axe.x == 0 && axe.y == 0 && axe.z == 0)) return;
            double fi = 2.0 * Math.PI / vecs.Length; //шаг

            //найти непараллельный вектор
            Vec3 v = new Vec3(axe.x, axe.y * 2, axe.z * 3);
            if (axe.x == 0 && axe.y == 0) v.x = 1;
            else if (axe.x == 0 && axe.z == 0) v.x = 1;
            else if (axe.y == 0 && axe.z == 0) v.y = 1;

            //перемножим - найдем перпендикуляр
            Vec3 vNorm = Vec3.Product(axe, v);
            vNorm.Normalize();
            vNorm.Scale(radius);

            //axe - new Z, vNorm - new X, найдем Y
            Vec3 vY = Vec3.Product(axe, vNorm);
            vY.Normalize();
            vY.Scale(radius);

            //rotate            
            Vec3 b = new Vec3();
            for (int m = 0; m < vecs.Length; m++)
            {
                var zRotor = (fi * m);
                Vec3 a = vecs[m];
                a.Copy(vNorm);
                b.Copy(vY);
                a.Scale(Math.Cos(zRotor));
                b.Scale(Math.Sin(zRotor));
                a.Add(b);
            }
        }
    }

    /// <summary>
    /// 3-х мерная матрица
    /// </summary>
    public class Mat3
    {
        public Vec3 a, b, c;    //строки матрицы
        //конструктор без параметров создает единичную матрицу
        public Mat3()
        {
            a = new Vec3(1, 0, 0);
            b = new Vec3(0, 1, 0);
            c = new Vec3(0, 0, 1);
        }
        
        //копирование данных
        public void Copy(Mat3 mat)
        {
            a.Copy(mat.a);
            b.Copy(mat.b);
            c.Copy(mat.c);
        }
        
        //масштабирование, для каждой строки свой коэффициент
        public void Scale(double p1, double p2, double p3 )
        {
            a.Scale(p1);
            b.Scale(p2);
            c.Scale(p3);
        }
        
        //прибавление матрицы
        public void Add(Mat3 mat)
        {
            a.Add(mat.a);
            b.Add(mat.b);
            c.Add(mat.c);
        }
        
        //сложение 2-х матриц, результат в res
        public void Sum(Mat3 mat, ref Mat3 res)
        {
            a.Sum(mat.a, ref res.a);
            b.Sum(mat.b, ref res.b);
            c.Sum(mat.c, ref res.c);
        }
        
        //умножение матрицы на вектор, результат в res
        public void Mult(Vec3 v, ref Vec3 res)
        {
            res.x = a.x * v.x + a.y * v.y + a.z * v.z;
            res.y = b.x * v.x + b.y * v.y + b.z * v.z;
            res.z = c.x * v.x + c.y * v.y + c.z * v.z;
        }
        
        //умножение матрицы на матрицу, результат в res
        public void Mult(Mat3 mat, ref Mat3 res)
        {
            res.a.x = a.x * mat.a.x + a.y * mat.b.x + a.z * mat.c.x;//1,1
            res.a.y = a.x * mat.a.y + a.y * mat.b.y + a.z * mat.c.y;//1,2
            res.a.z = a.x * mat.a.z + a.y * mat.b.z + a.z * mat.c.z;//1,3

            res.b.x = b.x * mat.a.x + b.y * mat.b.x + b.z * mat.c.x;//2,1
            res.b.y = b.x * mat.a.y + b.y * mat.b.y + b.z * mat.c.y;//2,2
            res.b.z = b.x * mat.a.z + b.y * mat.b.z + b.z * mat.c.z;//2,3

            res.c.x = c.x * mat.a.x + c.y * mat.b.x + c.z * mat.c.x;//3,1
            res.c.y = c.x * mat.a.y + c.y * mat.b.y + c.z * mat.c.y;//3,2
            res.c.z = c.x * mat.a.z + c.y * mat.b.z + c.z * mat.c.z;//3,3
        }
        
        //построить матрицу из углов поворота
        //Чтобы найти координаты в СКЯ мы последовательно(Z, X, Y)
        //применяем соотношения(6.2) с учетом смены углов вращения.
        //Чтобы найти координаты в СКК мы в обратной последовательности(Y, X, Z)
        //применяем соотношения(6.3) с учетом смены углов вращения.
        public void Build(double xRotor, double yRotor, double zRotor)
        {
            //rotate Z, X, Y
            Mat3 mz = new Mat3();
            mz.a = new Vec3(Math.Cos(zRotor), -Math.Sin(zRotor), 0);
            mz.b = new Vec3(Math.Sin(zRotor), Math.Cos(zRotor), 0);
            mz.c = new Vec3(0, 0, 1);

            Mat3 mx = new Mat3();
            mx.a = new Vec3(1, 0, 0);
            mx.b = new Vec3(0, Math.Cos(xRotor), -Math.Sin(xRotor));
            mx.c = new Vec3(0, Math.Sin(xRotor), Math.Cos(xRotor));

            Mat3 my = new Mat3();
            my.a = new Vec3(Math.Cos(yRotor), 0, Math.Sin(yRotor));
            my.b = new Vec3(0, 1, 0);
            my.c = new Vec3(-Math.Sin(yRotor), 0, Math.Cos(yRotor));

            Mat3 mtemp = new Mat3();
            //mx.Mult(mz, ref mtemp);
            //my.Mult(mtemp, ref mz);
            //Copy(mz);

            //порядок Y, X, Z
            mx.Mult(my, ref mtemp);
            mz.Mult(mtemp, ref my);
            Copy(my);

            //test length=1
            //double lenA = a.Length();
            //double lenB = b.Length();
            //double lenC = c.Length();
        }

        //построить матрицу из векторов - каждый вектор заполняет колонку
        public void BuildVec(Vec3 a1, Vec3 b1, Vec3 c1)
        {
            a.x = a1.x;
            b.x = a1.y;
            c.x = a1.z;

            a.y = b1.x;
            b.y = b1.y;
            c.y = b1.z;

            a.z = c1.x;
            b.z = c1.y;
            c.z = c1.z;
        }

        //вычислить определитель матрицы
        public double Determinant()
        {
            double d = 0;
            d += a.x * (b.y * c.z - b.z * c.y);
            d -= a.y * (b.x * c.z - b.z * c.x);
            d += a.z * (b.x * c.y - b.y * c.x);

            return d;
        }

        //транспонировать матрицу
        public void Transp()
        {
            var m = new Mat3();
            m.a.x = a.x;
            m.a.y = b.x;
            m.a.z = c.x;

            m.b.x = a.y;
            m.b.y = b.y;
            m.b.z = c.y;

            m.c.x = a.z;
            m.c.y = b.z;
            m.c.z = c.z;

            Copy(m);
        }

        //найти обратную матрицу
        public void Inverse(ref Mat3 m)
        {
            double d = Determinant();
            if (d == 0)
            {   //плохой случай, возвращаем нулевую матрицу!
                m.a.Copy(0, 0, 0);
                m.b.Copy(0, 0, 0);
                m.c.Copy(0, 0, 0);
                return;
            }
            //копируем матрицу
            m.Copy(this);
            //транспонируем
            m.Transp();

            //вычислить присоединенную матрицу
            var mAdj = new Mat3();
            mAdj.a.x = (m.b.y * m.c.z - m.b.z * m.c.y) / d;
            mAdj.a.y = -(m.b.x * m.c.z - m.b.z * m.c.x) / d;
            mAdj.a.z = (m.b.x * m.c.y - m.b.y * m.c.x) / d;

            mAdj.b.x = -(m.a.y * m.c.z - m.a.z * m.c.y) / d;
            mAdj.b.y = (m.a.x * m.c.z - m.a.z * m.c.x) / d;
            mAdj.b.z = -(m.a.x * m.c.y - m.a.y * m.c.x) / d;

            mAdj.c.x = (m.a.y * m.b.z - m.a.z * m.b.y) / d;
            mAdj.c.y = -(m.a.x * m.b.z - m.a.z * m.b.x) / d;
            mAdj.c.z = (m.a.x * m.b.y - m.a.y * m.b.x) / d;
            //вернуть результат
            m.Copy(mAdj);
        }
        
        //строковое представление матрицы
        public new string ToString()
        {
            return string.Format("[[{0}, {1}, {2}],\n[{3}, {4}, {5}],\n[{6}, {7}, {8}]]", a.x, a.y, a.z, b.x, b.y, b.z, c.x, c.y, c.z);
        }
    }
}
