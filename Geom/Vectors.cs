using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPanel
{

    /// <summary>
    /// 3-х мерный вектор
    /// </summary>
    public class Vec3
    {
        public double x, y, z;
        public Vec3()
        {
            x = 0;
            y = 0;
            z = 0;
        }
        public Vec3(double x0 = 0, double y0 = 0, double z0 = 0)
        {
            x = x0;
            y = y0;
            z = z0;
        }
        public void Copy(double x0, double y0, double z0)
        {
            x = x0;
            y = y0;
            z = z0;
        }
        public void Copy(Vec3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }
        public void Add(double x0, double y0, double z0)
        {
            x += x0;
            y += y0;
            z += z0;
        }
        public void Add(Vec3 v)
        {
            x += v.x;
            y += v.y;
            z += v.z;
        }
        public void Sum(Vec3 v, ref Vec3 res)
        {
            res.x = x + v.x;
            res.y = y + v.y;
            res.z = z + v.z;
        }
        public static Vec3 Product(Vec3 v1, Vec3 v2)
        {
            var v3 = new Vec3();
            Product(v1, v2, ref v3);
            return v3;
        }
        public static void Product(Vec3 v1, Vec3 v2, ref Vec3 v3)
        {
            v3.z = v1.x * v2.y - v1.y * v2.x;
            v3.x = v1.y * v2.z - v1.z * v2.y;
            v3.y = v1.z * v2.x - v1.x * v2.z;
        }
        public double Length()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        public double Distance(double x1, double y1, double z1)
        {
            return Math.Sqrt((x - x1) * (x - x1) + (y - y1) * (y - y1) + (z - z1) * (z - z1));
        }

        public double Distance(Vec3 v)
        {
            return Math.Sqrt((x - v.x) * (x - v.x) + (y - v.y) * (y - v.y) + (z - v.z) * (z - v.z));
        }
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
        public new string ToString()
        {
            return string.Format("x={0}, y={1}, z={2}", x, y, z);
        }

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
    }

    /// <summary>
    /// 3-х мерная матрица
    /// </summary>
    public class Mat3
    {
        public Vec3 a, b, c;    //строки матрицы
        public Mat3()
        {
            a = new Vec3(1, 0, 0);
            b = new Vec3(0, 1, 0);
            c = new Vec3(0, 0, 1);
        }
        public void Copy(Mat3 mat)
        {
            a.Copy(mat.a);
            b.Copy(mat.b);
            c.Copy(mat.c);
        }
        public void Add(Mat3 mat)
        {
            a.Add(mat.a);
            b.Add(mat.b);
            c.Add(mat.c);
        }
        public void Sum(Mat3 mat, ref Mat3 res)
        {
            a.Sum(mat.a, ref res.a);
            b.Sum(mat.b, ref res.b);
            c.Sum(mat.c, ref res.c);
        }

        public void Mult(Vec3 v, ref Vec3 res)
        {
            res.x = a.x * v.x + a.y * v.y + a.z * v.z;
            res.y = b.x * v.x + b.y * v.y + b.z * v.z;
            res.z = c.x * v.x + c.y * v.y + c.z * v.z;
        }

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
        //build from angles
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
            mx.Mult(mz, ref mtemp);
            my.Mult(mtemp, ref mz);
            Copy(mz);
        }

        //build from vectors - each one is a new column
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

        //вычислить определитель
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
            {   //bad!
                m.a.Copy(0, 0, 0);
                m.b.Copy(0, 0, 0);
                m.c.Copy(0, 0, 0);
                return;
            }

            m.Copy(this);
            m.Transp();

            //присоединенная матрица
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

            m.Copy(mAdj);
        }

        public new string ToString()
        {
            return string.Format("{0}, {1}, {2}\n{3}, {4}, {5}\n{6}, {7}, {8}", a.x, a.y, a.z, b.x, b.y, b.z, c.x, c.y, c.z);
        }
    }
}
