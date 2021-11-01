//2020, Andrei Borziak
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace MathPanel
{
    //сериализуются только public! Dictionary - трюк
    /// <summary>
    /// оболочка вокруг Dictionary - нужен для сериализации
    /// </summary>
    [Serializable]
    public class SerializableClassX
    {
        // since the Dictionary<> class is not serializable,
        // we convert it to the List<KeyValuePair<>>
        public Dictionary<string, string> DictionaryX
        {
            get
            {
                //return SerializableList == null ? null :
                //        SerializableList.ToDictionary(item => item.Key, item => item.Value);
                return SerializableArray == null ? null :
                        SerializableArray.ToDictionary(item => item.Key, item => item.Value);
            }
            set
            {
                //SerializableList = value == null ? null : value.ToList();
                SerializableArray = value == null ? null : value.ToArray();
            }
        }
        // public List<KeyValuePair<string, string>> SerializableList { get; set; }
        KeyValuePair<string, string> [] SerializableArray { get; set; }
    }

    /// <summary>
    /// Physical object (Phob) has predefined propertises and a dictionary for dynamic attributes
    /// </summary>
    public class Phob
    {   //TODO: add key types?
        static int id_counter = 0;
        public int Id { get; }
        public double x, y, z;  //decart coordinates
        public double v_x, v_y, v_z;  //velocity
        public double mass, radius;   //mass and radius
        public bool bDrawAsLine = false;
        public Vec3 p1 = new Vec3(), p2 = new Vec3();
        double x_temp, y_temp, z_temp, radius_temp; //saved
        GeOb shape = null;  //форма
        //custom attributes
        readonly Dictionary<string, string> dicAttr = new Dictionary<string, string>();
        public SerializableClassX serDic = new SerializableClassX();
        public Phob()
        {
            x = 0;
            y = 0;
            z = 0;
            v_x = 0;
            v_y = 0;
            v_z = 0;
            mass = 1;
            radius = 1;
            Id = id_counter++;
        }
        public Phob(double x1 = 0, double y1 = 0, double z1 = 0, double v_x1 = 0, double v_y1 = 0, double v_z1 = 0)
        {
            x = x1;
            y = y1;
            z = z1;
            v_x = v_x1;
            v_y = v_y1;
            v_z = v_z1;
            mass = 1;
            radius = 1;
            Id = id_counter++;
        }
        public void Dic2List()
        {
            serDic.DictionaryX = dicAttr;
        }
        public void DicFromList()
        {
            dicAttr.Clear();
            foreach (var pair in serDic.DictionaryX)
                dicAttr.Add(pair.Key, pair.Value);
        }
        public new string ToString()
        {
            var attr = new StringBuilder();
            foreach (var kv in dicAttr)
            {
                attr.AppendFormat(", \"{0}\"=\"{1}\"", kv.Key, kv.Value);
            }
            return string.Format("Id={0}, x={1}, y={2}, z={3}, v_x={4}, v_y={5}, v_z={6}, mass={7}, rad={8}{9}",
                Id, Dynamo.D2S(x),
                Dynamo.D2S(y),
                Dynamo.D2S(z),
                Dynamo.D2S(v_x),
                Dynamo.D2S(v_y),
                Dynamo.D2S(v_z),
                Dynamo.D2S(mass),
                Dynamo.D2S(radius),
                attr.ToString());
        }
        public string ToJson()
        {
            var attr = new StringBuilder();
            foreach (var kv in dicAttr)
            {
                attr.AppendFormat(", \"{0}\":\"{1}\"", kv.Key, kv.Value);
            }
            return string.Format("{{\"Id\":{0}, \"x\":{1}, \"y\":{2}, \"z\":{3}, \"v_x\":{4}, \"v_y\":{5}, \"v_z\":{6}, \"mass\":{7}, \"rad\":{8}{9}}}",
                Id, Dynamo.D2S(x),
                Dynamo.D2S(y),
                Dynamo.D2S(z),
                Dynamo.D2S(v_x),
                Dynamo.D2S(v_y),
                Dynamo.D2S(v_z),
                Dynamo.D2S(mass),
                Dynamo.D2S(radius),
                attr.ToString());
        }

        public GeOb Shape
        {
            get
            {
                return shape;
            }
            set
            {
                shape = value;
            }
        }

        public void AttrSet(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) return;
            if (dicAttr.ContainsKey(key)) dicAttr[key] = value;
            else dicAttr.Add(key, value);
        }
        public string AttrGet(string key)
        {
            if (string.IsNullOrEmpty(key)) return null;
            if (dicAttr.ContainsKey(key)) return dicAttr[key];
            return null;
        }

        /// <summary>
        /// get distance between 2 Phob's
        /// </summary>
        /// <param name="ph2">second Phob</param>
        public double Distance(Phob ph2)
        {
            double dx = x - ph2.x;
            double dy = y - ph2.y;
            double dz = z - ph2.z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
        /// <summary>
        /// get distance between Phob and point x,y,z
        /// </summary>
        /// <param name="x1">x</param>
        /// <param name="y1">y</param>
        /// <param name="z1">z</param>
        public double Distance(double x1, double y1, double z1)
        {
            double dx = x - x1;
            double dy = y - y1;
            double dz = z - z1;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        /// <summary>
        /// check if collision between 2 Phob's
        /// </summary>
        /// <param name="ph2">second Phob</param>
        public bool Collision(Phob ph2)
        {
            double dx = x - ph2.x;
            double dy = y - ph2.y;
            double dz = z - ph2.z;
            double d = Math.Sqrt(dx * dx + dy * dy + dz * dz);
            return d < (radius + ph2.radius);
        }

        /// <summary>
        /// vector from current Phob to second one
        /// </summary>
        /// <param name="ph2">second Phob</param>
        public void Vector(Phob ph2, out double dx, out double dy, out double dz)
        {
            dx = ph2.x - x;
            dy = ph2.y - y;
            dz = ph2.z - z;
        }

        /// <summary>
        /// add vector to current Phob
        /// </summary>
        public void VectorAdd(double dx, double dy, double dz)
        {
            x += dx;
            y += dy;
            z += dz;
        }

        /// <summary>
        /// Kinetic Energy of current Phob
        /// </summary>
        public double KineticEnergy()
        {
            return mass * (v_x * v_x + v_y * v_y + v_z * v_z) / 2.0;
        }

        /// <summary>
        /// Impulse vector of current Phob
        /// </summary>
        public void Impulse(out double dx, out double dy, out double dz)
        {
            dx = mass * v_x;
            dy = mass * v_y;
            dz = mass * v_z;
        }

        /// <summary>
        /// current Phob hits a second one
        /// </summary>
        /// <param name="ph2">second Phob</param>
        public void Hit(Phob ph2)
        {   //iteraction force through centers - P
            double px = ph2.x - x;
            double py = ph2.y - y;
            double pz = ph2.z - z;
            //переходим в новую инерциальную систему, движущуюся со скоростью ph2
            //в ней он неподвижен
            //скорость текущего в новой
            double v1_x = v_x - ph2.v_x;
            double v1_y = v_y - ph2.v_y;
            double v1_z = v_z - ph2.v_z;
            double m2 = ph2.mass;
            double k = 2 * mass * (v1_x * px + v1_y * py + v1_z * pz) /
                ((px * px + py * py + pz * pz) * (mass + m2));
            double dmd = m2 / mass;

            v1_x -= dmd * px * k;
            v1_y -= dmd * py * k;
            v1_z -= dmd * pz * k;
            //new speed
            v_x = v1_x + ph2.v_x;
            v_y = v1_y + ph2.v_y;
            v_z = v1_z + ph2.v_z;

            ph2.v_x += k * px;
            ph2.v_y += k * py;
            ph2.v_z += k * pz;

            //adjust positions
            double dist = Math.Sqrt(px * px + py * py + pz * pz);
            double dExt = (radius + ph2.radius - dist) / (2 * dist);
            if (dExt > 0.001)
            {
                ph2.VectorAdd(dExt * px, dExt * py, dExt * pz);
                VectorAdd(-dExt * px, -dExt * py, -dExt * pz);
            }
        }

        /// <summary>
        /// save coordinates
        /// </summary>
        public void SaveCoord()
        {
            x_temp = x;
            y_temp = y;
            z_temp = z;
            radius_temp = radius;
        }
        /// <summary>
        /// restore coordinates from saved
        /// </summary>
        public void RestoreCoord()
        {
            x = x_temp;
            y = y_temp;
            z = z_temp;
            radius = radius_temp;
        }
    }

    /// <summary>
    /// Box to draw a scene
    /// </summary>
    public class Box
    {
        public double x0, x1, y0, y1, z0, z1;
        public Box()
        {
            this.x0 = 0;
            this.x1 = 1;
            this.y0 = 0;
            this.y1 = 1;
            this.z0 = 0;
            this.z1 = 1;
        }
        public Box(double x0, double x1, double y0, double y1, double z0, double z1)
        {
            this.x0 = x0;
            this.x1 = x1;
            this.y0 = y0;
            this.y1 = y1;
            this.z0 = z0;
            this.z1 = z1;
        }
        public void Copy(double x0, double x1, double y0, double y1, double z0, double z1)
        {
            this.x0 = x0;
            this.x1 = x1;
            this.y0 = y0;
            this.y1 = y1;
            this.z0 = z0;
            this.z1 = z1;
        }
    }


    public class Scene
    {
        public double z_cam = 100;  //z-позиция камеры
        public double x_cam_angle = 120;  //угол камеры
        public double y_cam_angle = 90;  //угол камеры
        //размеры изображения в 0,0,0
        public double physWidth;
        public double physHeight;
        //проектируется на экран html-canvas (в пикселях)
        public int dScreenWidth = 800;
        public int dScreenHeight = 600;
        //ящик - система координат, в которой размещены объекты
        public Box box = new Box();  //границы ящика для рисования
        public double xBoXTrans = 0, yBoXTrans = 0, zBoXTrans = -100;//смещение ящика в системе камеры
        public double xRotor = 0; //вращение ящика вокруг оси X
        public double yRotor = 0; //вращение ящика вокруг оси Y
        public double zRotor = 0; //вращение ящика вокруг оси Z
        public List<Phob> lst = new List<Phob>();
        public Dictionary<string, string> dic = new Dictionary<string, string>();
        public Scene()
        {
            physWidth = z_cam* Math.Tan(x_cam_angle* Math.PI / 360.0);
            physHeight = z_cam * Math.Tan(y_cam_angle * Math.PI / 360.0);

            dScreenWidth = (int)((600 * physWidth) / physHeight);
            dScreenHeight = 600;
        }
    }
}
