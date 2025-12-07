//test21_springs_runge - 2 шара и пружина, решатель Рунге-Кутта

        //решатель Рунге-Кутта
        public class SolverRK
        {
            public delegate double DifFunc2(int index, double x, double[] y);
            public delegate void DifYtoModel(double[] y);

            public DifYtoModel YtoModel = null;

            //исправленный метод Эйлера 2-го порядка для системы
            public void rk2array(double x, double[] y, double[] y_tmp, double[] k1, double[] k2, double h, DifFunc2[] operator2)
            {
                for (int i = 0; i < y.Length; i++)
                {
                    k1[i] = operator2[i](i, x, y);
                }
                for (int i = 0; i < y.Length; i++)
                {
                    y_tmp[i] = y[i] + h * k1[i];
                }
                for (int i = 0; i < y.Length; i++)
                {
                    k2[i] = operator2[i](i, x + h, y_tmp);
                }

                for (int i = 0; i < y.Length; i++)
                {
                    y[i] = y[i] + 0.5 * h * (k1[i] + k2[i]);
                }
            }

            //метод Рунге-Кутта 4-го порядка для системы
            public void rk4array(double x, double[] y, double[] y_tmp, double[] k1, double[] k2, double[] k3, double[] k4, double h, DifFunc2[] operator2)
            {
                for (int i = 0; i < y.Length; i++)
                {
                    k1[i] = operator2[i](i, x, y);
                }
                for (int i = 0; i < y.Length; i++)
                {
                    y_tmp[i] = y[i] + h * k1[i] * 0.5;
                }
                if( YtoModel != null ) YtoModel(y_tmp);

                for (int i = 0; i < y.Length; i++)
                {
                    k2[i] = operator2[i](i, x + h * 0.5, y_tmp);
                }
                for (int i = 0; i < y.Length; i++)
                {
                    y_tmp[i] = y[i] + h * k2[i] * 0.5;
                }
                if (YtoModel != null) YtoModel(y_tmp);

                for (int i = 0; i < y.Length; i++)
                {
                    k3[i] = operator2[i](i, x + h * 0.5, y_tmp);
                }
                for (int i = 0; i < y.Length; i++)
                {
                    y_tmp[i] = y[i] + h * k3[i];
                }
                if (YtoModel != null) YtoModel(y_tmp);

                for (int i = 0; i < y.Length; i++)
                {
                    k4[i] = operator2[i](i, x + h, y_tmp);
                }

                for (int i = 0; i < y.Length; i++)
                {
                    y[i] = y[i] + (k1[i] + 2 * k2[i] + 2 * k3[i] + k4[i]) * h / 6;
                }
                if (YtoModel != null) YtoModel(y);
            }

            //метод прогноза и коррекции 4-го порядка для системы
            public void predict4array(double x, double[] y, double[] yPrev, double[] y_tmp, double[] k1, double[] k2, double h, DifFunc2[] operator2)
            {
                for (int i = 0; i < y.Length; i++)
                {
                    k1[i] = operator2[i](i, x, y);
                }
                //прогноз
                for (int i = 0; i < y.Length; i++)
                {
                    y_tmp[i] = yPrev[i] + 2 * h * k1[i];
                }
                for (int i = 0; i < y.Length; i++)
                {
                    k2[i] = operator2[i](i, x + h, y_tmp);
                }

                //коррекция 1
                for (int i = 0; i < y.Length; i++)
                {
                    y[i] = y[i] + 0.5 * h * (k1[i] + k2[i]);
                }
            }
        }

        static int N = 10; //число шаров
        static double dLink = 20; //число связей максимальное
        static double dKoefSpring = 0.051; //коэффициент пружины
        static double dKoefSpeed = 0.05; //торможение по скорости
        static double DT = 0.125; //шаг по времени
        static System.Drawing.Color[] clrs = //цвета для шаров
        {
            System.Drawing.Color.DarkBlue,
            System.Drawing.Color.Gray,
            System.Drawing.Color.Brown,
            System.Drawing.Color.Yellow,
            System.Drawing.Color.Orange,
            System.Drawing.Color.Green,
            System.Drawing.Color.Blue,
            System.Drawing.Color.Magenta,
            System.Drawing.Color.Cyan,
            System.Drawing.Color.White,
            System.Drawing.Color.DarkGreen,
        };

        //рисовать систему
        static void DrawSprings(List<Tuple<int, int, int>> lstConnect)
        {
            double x1, y1, z1, x2, y2, z2;
            for (int i = 0; i < lstConnect.Count; i++)
            {
                Tuple<int, int, int> tup = lstConnect[i];
                int one = tup.Item1;
                int two = tup.Item2;
                int three = tup.Item3;
                var hz1 = Dynamo.PhobGet(one) as Phob;  //sphere 1
                var hz2 = Dynamo.PhobGet(two) as Phob;  //sphere 2
                var hz3 = Dynamo.PhobGet(three) as Phob;    //spring

                x1 = hz1.x;
                y1 = hz1.y;
                z1 = hz1.z;

                x2 = hz2.x;
                y2 = hz2.y;
                z2 = hz2.z;

                double dx = (x2 - x1);
                double dy = (y2 - y1);
                double dz = (z2 - z1);
                double len = Math.Sqrt(dx * dx + dy * dy + dz * dz);

                x1 = x1 + (dx * hz1.radius) / len;
                y1 = y1 + (dy * hz1.radius) / len;
                z1 = z1 + (dz * hz1.radius) / len;

                x2 = x2 - (dx * hz2.radius) / len;
                y2 = y2 - (dy * hz2.radius) / len;
                z2 = z2 - (dz * hz2.radius) / len;

                hz3.x = (x1 + x2) / 2;
                hz3.y = (y1 + y2) / 2;
                hz3.z = (z1 + z2) / 2;
                hz3.p1.Copy(x1, y1, z1);
                hz3.p2.Copy(x2, y2, z2);
            }
        }
        //вычислить dv /dt
        double calcForce(int i, double t, double[] y)
        {
            //i четный Vi
            //i нечетный Ai
            int n = i / 6;
            int m = i % 6;
            int ind = arr[n];
            var hz = Dynamo.PhobGet(ind) as Phob;
            if (m == 0) return hz.v_x;
            else if (m == 2) return hz.v_y;
            else if (m == 4) return hz.v_z;

            //calc force
            double acc = 0;
            //пружины
            for (int j = 0; j < lstConnect.Count; j++)
            {
                Tuple<int, int, int> tup = lstConnect[j];
                int one = tup.Item1;
                int two = tup.Item2;
                //Dynamo.Console("one=" + one + ", two=" + two + ", n=" + n + ", m=" + m + ", ind=" + ind);
                if (one != ind && two != ind) continue;
                var hz1 = Dynamo.PhobGet(one) as Phob;
                var hz2 = Dynamo.PhobGet(two) as Phob;

                double d = hz1.Distance(hz2);
                double dKoef = dKoefSpring;
                //mass & distance
                if (d < dLink)
                {   //отталкивание
                    dKoef *= (dLink / d);
                    if (one != ind)
                    {
                        if (m == 1) acc += dKoef * (hz2.x - hz1.x) / hz2.mass;
                        if (m == 3) acc += dKoef * (hz2.y - hz1.y) / hz2.mass;
                        if (m == 5) acc += dKoef * (hz2.z - hz1.z) / hz2.mass;
                    }
                    else
                    {
                        if (m == 1) acc -= dKoef * (hz2.x - hz1.x) / hz1.mass;
                        if (m == 3) acc -= dKoef * (hz2.y - hz1.y) / hz1.mass;
                        if (m == 5) acc -= dKoef * (hz2.z - hz1.z) / hz1.mass;
                    }
                }
                else
                {   //притяжение
                    dKoef *= (d / dLink);
                    if (one != ind)
                    {
                        if (m == 1) acc -= dKoef * (hz2.x - hz1.x) / hz2.mass;
                        if (m == 3) acc -= dKoef * (hz2.y - hz1.y) / hz2.mass;
                        if (m == 5) acc -= dKoef * (hz2.z - hz1.z) / hz2.mass;
                    }
                    else
                    {
                        if (m == 1) acc += dKoef * (hz2.x - hz1.x) / hz1.mass;
                        if (m == 3) acc += dKoef * (hz2.y - hz1.y) / hz1.mass;
                        if (m == 5) acc += dKoef * (hz2.z - hz1.z) / hz1.mass;
                    }
                }
            }

            //затухание
            if (m == 1) acc -= dKoefSpeed * hz.v_x;
            else if (m == 3) acc -= dKoefSpeed * hz.v_y;
            else if (m == 5) acc -= dKoefSpeed * hz.v_z;

            //Dynamo.Console("acc=" + acc + ", n=" + n + ", m=" + m + ", ind=" + ind);
            return acc;
        }
        //связь 2-шаров через пружину
        static List<Tuple<int, int, int>> lstConnect = new List<Tuple<int, int, int>>();
        static int[] arr = null;

        static void YtoModel(double[] y_x)
        {
            for (int k = 0; k < N * 6; k += 6)
            {
                var hz = Dynamo.PhobGet(arr[k/6]) as Phob;
                hz.x = y_x[k + 0];
                hz.v_x = y_x[k + 1];
                hz.y = y_x[k + 2];
                hz.v_y = y_x[k + 3];
                hz.z = y_x[k + 4];
                hz.v_z = y_x[k + 5];
            }
        }

        public void Execute()
        {
            Dynamo.Console("Script started! Нажать 'q' для завершения");
            //Dynamo.Scriplet("test21_springs_runge", "Шары на пружинах");

            Dynamo.SceneClear();
            //создаем сцену
            var rnd = new Random();
            //generate PhOb's
            for (int i = 0; i < N; i++)
            {
                double x = rnd.NextDouble() * 40;
                double y = rnd.NextDouble() * 40;
                double z = rnd.NextDouble() * 40;
                int id = Dynamo.PhobNew(x, y, z);
                var hz0 = Dynamo.PhobGet(id) as Phob;
                int sz = 1;// rnd.Next(1, 5);
                hz0.radius = sz;
                hz0.mass = 1;// sz;
                hz0.AttrSet("clr", Facet3.ColorHtml(clrs[rnd.Next(0, clrs.Length - 1)]));
            }
            //generate connections
            arr = Dynamo.SceneIds();
            List<int> lstNum = new List<int>(); //для избежания дубликатов связей
            HashSet<int> hs = new HashSet<int>();//индексы объектов сцены
            for (int i = 0; i < 2 * N; i++)
            {
                int one = rnd.Next(0, N - 1);
                int two = rnd.Next(0, N - 1);
                if (one == two) continue;
                int num = (one > two ? one * N + two : two * N + one);
                if (lstNum.Contains(num)) continue;
                int id = Dynamo.PhobNew(0, 0, 0);
                var hz = Dynamo.PhobGet(id) as Phob;
                hz.bDrawAsLine = true;
                Tuple<int, int, int> tup = new Tuple<int, int, int>(arr[one], arr[two], id);
                lstConnect.Add(tup);
                Dynamo.Console("first=" + arr[one] + "," + arr[two] + ",one=" + one + ",two=" + two);
                lstNum.Add(num);
                hs.Add(arr[one]);
                hs.Add(arr[two]);
            }
            //чтобы все объекты были использованы
            for (int i = 0; i < N; i++)
            {
                if (hs.Contains(arr[i])) continue;
                if (i == N - 1)
                {
                    int id = Dynamo.PhobNew(0, 0, 0);
                    var hz = Dynamo.PhobGet(id) as Phob;
                    hz.bDrawAsLine = true;
                    Tuple<int, int, int> tup = new Tuple<int, int, int>(arr[i], arr[0], id);
                    lstConnect.Add(tup);
                    Dynamo.Console(arr[i] + "," + arr[0]);
                }
                else
                {
                    int id = Dynamo.PhobNew(0, 0, 0);
                    var hz = Dynamo.PhobGet(id) as Phob;
                    hz.bDrawAsLine = true;
                    Tuple<int, int, int> tup = new Tuple<int, int, int>(arr[i], arr[i + 1], id);
                    lstConnect.Add(tup);
                    hs.Add(arr[i]);
                    hs.Add(arr[i + 1]);
                    Dynamo.Console(arr[i] + "," + arr[i + 1]);
                }
            }

            Dynamo.Console("arr length=" + arr.Length + ", lstConnect.Count=" + lstConnect.Count);

            Dynamo.SceneBox = new Box(0, 40, 0, 40, 0, 40);
            Dynamo.BDrawBox = false;
            DrawSprings(lstConnect);
            Dynamo.SceneDraw();
            int iTotalRes = 0;

            Box bx = Dynamo.SceneBox;
            double dx, dy, dz, tm = 0;

            SolverRK rk = new SolverRK();
            rk.YtoModel = YtoModel;
            //метод Рунге-Кутта 4-го, система уравнений
            double[] y_x = new double[N * 6];
            double[] y_tmp = new double[N * 6];
            double[] k1 = new double[N * 6];
            double[] k2 = new double[N * 6];
            double[] k3 = new double[N * 6];
            double[] k4 = new double[N * 6];
            SolverRK.DifFunc2[] ops = new SolverRK.DifFunc2[N * 6];
            for (int i = 0; i < ops.Length; i++) ops[i] = calcForce;
            for (int i = 0; i < N * 6; i += 6)
            {
                var hz = Dynamo.PhobGet(arr[i/6]) as Phob;
                y_x[i + 0] = hz.x;
                y_x[i + 1] = hz.v_x;
                y_x[i + 2] = hz.y;
                y_x[i + 3] = hz.v_y;
                y_x[i + 4] = hz.z;
                y_x[i + 5] = hz.v_z;
            }

            for (int i = 0; i < 4000; i++)
            {
                DateTime dt1 = DateTime.Now;
                rk.rk4array(tm, y_x, y_tmp, k1, k2, k3, k4, DT, ops);
                tm += DT;

                DrawSprings(lstConnect);
                Dynamo.SceneDraw();
                if (i % 5 == 0 && iTotalRes < 100)
                {
                    //Dynamo.SaveScripresult();
                    iTotalRes++;
                }

                if (i % 50 == 0)
                {
                    DateTime dt2 = DateTime.Now;
                    double ix, iy, iz;
                    Dynamo.SceneImpulse(out ix, out iy, out iz);
                    Dynamo.Console("en=" + Dynamo.SceneEnergy().ToString() + 
                        ", ix=" + ix + ", iy=" + iy + ", iz=" + iz + ", ms=" + (dt2-dt1).TotalMilliseconds);
                }
                System.Threading.Thread.Sleep(20); //Мы ждем 20 ms в даном потоке   
                //проверить клавиатуру
                string resp = Dynamo.KeyConsole;
                if (resp == "Q")
                {   //давай до свидания
                    break;
                }
            }
}
Execute();
