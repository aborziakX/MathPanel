//2020, Andrei Borziak
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathPanel
{
    //сериализуются только public! для Dictionary - трюк
    /// <summary>
    /// оболочка вокруг Dictionary - нужна для сериализации
    /// </summary>
    [Serializable]
    public class SerializableClassX
    {
        // так как Dictionary<> класс не сериализуется,
        // мы конвертируем его в массив элементов KeyValuePair<>
        public Dictionary<string, string> DictionaryX
        {
            get
            {
                return SerializableArray == null ? null :
                        SerializableArray.ToDictionary(item => item.Key, item => item.Value);
            }
            set
            {
                SerializableArray = (value == null ? null : value.ToArray());
            }
        }
        //массив пар (ключ-значение)
        KeyValuePair<string, string> [] SerializableArray { get; set; }
    }

    /// <summary>
    /// Физический объект (Physical object (Phob)) имеет предопределенные свойства и словарь для динамических атрибутов
    /// </summary>
    public class Phob
    {
        static int id_counter = 0;  //поддержка уникальных идентификаторов
        public int Id { get; }  //уникальный идентификатор
        public double x, y, z;  //декартовы координаты
        public double v_x, v_y, v_z;  //разложение вектора скорости движения по осям
        public double mass, radius;   //масса и радиус
        public bool bDrawAsLine = false;//признак рисования: true - линия, false - сфера
        //словарь для динамических атрибутов
        readonly Dictionary<string, string> dicAttr = new Dictionary<string, string>();
        //объект для сериализации
        public SerializableClassX serDic = new SerializableClassX();
        double x_temp, y_temp, z_temp, radius_temp; //для сохранения
        public Vec3 p1 = new Vec3(), p2 = new Vec3();//вектора для временных данных
        GeOb shape = null;  //форма

        /// <summary>
        /// конструктор для Phob
        /// </summary>
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

        /// <summary>
        /// конструктор для Phob с параметрами
        /// </summary>
        /// <param name="x1">x координата</param>
        /// <param name="y1">y координата</param>
        /// <param name="z1">z координата</param>
        /// <param name="v_x1">скорость вдоль x координаты</param>
        /// <param name="v_y1">скорость вдоль y координаты</param>
        /// <param name="v_z1">скорость вдоль z координаты</param>
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

        /// <summary>
        /// загрузить словарь в объект для сериализации
        /// </summary>
        public void Dic2List()
        {
            serDic.DictionaryX = dicAttr;
        }

        /// <summary>
        /// восстановить словарь из объекта для сериализации
        /// </summary>
        public void DicFromList()
        {
            dicAttr.Clear();
            foreach (var pair in serDic.DictionaryX)
                dicAttr.Add(pair.Key, pair.Value);
        }

        /// <summary>
        /// привести Phob к строковому представлению
        /// </summary>
        public new string ToString()
        {
            //создаем StringBuilder для ускорения
            var attr = new StringBuilder();
            foreach (var kv in dicAttr)
            {
                attr.AppendFormat(", \"{0}\"=\"{1}\"", kv.Key, kv.Value);
            }
            //возвращаем предопределенные свойства и словарь для динамических атрибутов
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

        /// <summary>
        /// привести Phob к строке в формате Json
        /// </summary>
        public string ToJson()
        {
            //создаем StringBuilder для ускорения
            var attr = new StringBuilder();
            foreach (var kv in dicAttr)
            {
                attr.AppendFormat(", \"{0}\":\"{1}\"", kv.Key, kv.Value);
            }
            //возвращаем предопределенные свойства и словарь для динамических атрибутов
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

        /// <summary>
        /// получить / присвоить форму
        /// </summary>
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

        /// <summary>
        /// заменить/добавить атрибут
        /// </summary>
        /// <param name="key">ключ атрибута</param>
        /// <param name="value">значения атрибута</param>
        public void AttrSet(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) return;
            if (dicAttr.ContainsKey(key)) dicAttr[key] = value;
            else dicAttr.Add(key, value);
        }

        /// <summary>
        /// получить значение атрибута по ключу
        /// </summary>
        /// <param name="key">ключ атрибута</param>
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
        /// получить расстояние между Phob и точкой x1,y1,z1
        /// </summary>
        /// <param name="x1">x координата точки</param>
        /// <param name="y1">y координата точки</param>
        /// <param name="z1">z координата точки</param>
        public double Distance(double x1, double y1, double z1)
        {
            double dx = x - x1;
            double dy = y - y1;
            double dz = z - z1;
            //декартово расстояние
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        /// <summary>
        /// проверка на столкновение двух Phob
        /// </summary>
        /// <param name="ph2">второй Phob</param>
        public bool Collision(Phob ph2)
        {
            double dx = x - ph2.x;
            double dy = y - ph2.y;
            double dz = z - ph2.z;
            //расстояние между центрами объектов
            double d = Math.Sqrt(dx * dx + dy * dy + dz * dz);
            //столкновение, если меньше суммы радиусов
            return d < (radius + ph2.radius);
        }

        /// <summary>
        /// получить координаты для вектора от центра Phob ко второму
        /// </summary>
        /// <param name="ph2">второй Phob</param>
        /// <param name="dx">x координата вектора</param>
        /// <param name="dy">y координата вектора</param>
        /// <param name="dz">z координата вектора</param>
        public void Vector(Phob ph2, out double dx, out double dy, out double dz)
        {
            dx = ph2.x - x;
            dy = ph2.y - y;
            dz = ph2.z - z;
        }

        /// <summary>
        /// изменить координаты Phob
        /// <param name="dx">приращение для x координаты</param>
        /// <param name="dy">приращение для y координаты</param>
        /// <param name="dz">приращение для z координаты</param>
        /// </summary>
        public void VectorAdd(double dx, double dy, double dz)
        {
            x += dx;
            y += dy;
            z += dz;
        }

        /// <summary>
        /// кинетическая энергия объекта
        /// </summary>
        public double KineticEnergy()
        {
            return mass * (v_x * v_x + v_y * v_y + v_z * v_z) / 2.0;
        }

        /// <summary>
        /// импульс объекта
        /// </summary>
        /// <param name="dx">x значение вектора импульса</param>
        /// <param name="dy">y значение вектора импульса</param>
        /// <param name="dz">z значение вектора импульса</param>
        public void Impulse(out double dx, out double dy, out double dz)
        {
            dx = mass * v_x;
            dy = mass * v_y;
            dz = mass * v_z;
        }

        /// <summary>
        /// текущий Phob задет вторым, абсолютно упругое столкновение
        /// </summary>
        /// <param name="ph2">второй Phob</param>
        public void Hit(Phob ph2)
        {   //взаимодействие происходит вдоль вектора, соединяющего центы объектов - от текущего ко второму
            double px = ph2.x - x;
            double py = ph2.y - y;
            double pz = ph2.z - z;

            //переходим в новую инерциальную систему, движущуюся со скоростью ph2
            //второй в ней неподвижен
            //скорость текущего в новой системе
            double v1_x = v_x - ph2.v_x;
            double v1_y = v_y - ph2.v_y;
            double v1_z = v_z - ph2.v_z;
            double m2 = ph2.mass;

            //какая часть импульса перейдет неподвижному второму объекту
            double k = 2 * mass * (v1_x * px + v1_y * py + v1_z * pz) /
                ((px * px + py * py + pz * pz) * (mass + m2));
            //соотношение масс
            double dmd = m2 / mass;

            //в новой системе координат первый объект теряет скорость пропорционально соотношению масс
            //на основании законов сохранения импульса и энергии
            v1_x -= dmd * px * k;
            v1_y -= dmd * py * k;
            v1_z -= dmd * pz * k;

            //новая скорость первого объекта (переходим в старую систему)
            v_x = v1_x + ph2.v_x;
            v_y = v1_y + ph2.v_y;
            v_z = v1_z + ph2.v_z;

            //новая скорость второго объекта
            ph2.v_x += k * px;
            ph2.v_y += k * py;
            ph2.v_z += k * pz;

            //уточняем позиции
            double dist = Math.Sqrt(px * px + py * py + pz * pz);
            double dExt = (radius + ph2.radius - dist) / (2 * dist);
            if (dExt > 0.001)
            {   //объекты сплющило, раздвинуть немного
                ph2.VectorAdd(dExt * px, dExt * py, dExt * pz);
                VectorAdd(-dExt * px, -dExt * py, -dExt * pz);
            }
        }

        /// <summary>
        /// запомнить координаты и радиус
        /// </summary>
        public void SaveCoord()
        {
            x_temp = x;
            y_temp = y;
            z_temp = z;
            radius_temp = radius;
        }

        /// <summary>
        /// восстановить координаты и радиус
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
    /// Ящик для рисования границ сцены
    /// </summary>
    public class Box
    {
        public double x0, y0, z0;//первая точка
        public double x1, y1, z1;//противоположная точка

        /// <summary>
        /// конструктор
        /// </summary>
        public Box()
        {
            this.x0 = 0;
            this.x1 = 1;
            this.y0 = 0;
            this.y1 = 1;
            this.z0 = 0;
            this.z1 = 1;
        }

        /// <summary>
        /// конструктор с параметрами
        /// </summary>
        /// <param name="x0">x координата первой точки</param>
        /// <param name="x1">x координата второй точки</param>
        /// <param name="y0">y координата первой точки</param>
        /// <param name="y1">y координата второй точки</param>
        /// <param name="z0">z координата первой точки</param>
        /// <param name="z1">z координата второй точки</param>
        public Box(double x0, double x1, double y0, double y1, double z0, double z1)
        {
            this.x0 = x0;
            this.x1 = x1;
            this.y0 = y0;
            this.y1 = y1;
            this.z0 = z0;
            this.z1 = z1;
        }

        /// <summary>
        /// установить новые размеры
        /// </summary>
        /// <param name="x0">x координата первой точки</param>
        /// <param name="x1">x координата второй точки</param>
        /// <param name="y0">y координата первой точки</param>
        /// <param name="y1">y координата второй точки</param>
        /// <param name="z0">z координата первой точки</param>
        /// <param name="z1">z координата второй точки</param>
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

    /// <summary>
    /// сцена
    /// </summary>
    public class Scene
    {
        public double z_cam = 100;  //z-позиция камеры
        public double x_cam_angle = 120;  //угол камеры по горизонтали
        public double y_cam_angle = 90;  //угол камеры по вертикали
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
        //список физических объектов
        public List<Phob> lst = new List<Phob>();
        //словарь свойств
        public Dictionary<string, string> dic = new Dictionary<string, string>();

        /// <summary>
        /// конструктор
        /// </summary>
        public Scene()
        {
            //"танцуем" от углов камеры и z-позиции камеры
            physWidth = z_cam * Math.Tan(x_cam_angle * Math.PI / 360.0);
            physHeight = z_cam * Math.Tan(y_cam_angle * Math.PI / 360.0);
            //определяем размер html-canvas
            dScreenWidth = (int)((600 * physWidth) / physHeight);
            dScreenHeight = 600;
        }
    }
}
