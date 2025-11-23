//test21_springs - пружины
static int N = 10;
static System.Drawing.Color[] clrs =
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
static double dLink = 20;
static double dKoefSpring = 0.01;
static double dKoefSpeed = 0.9;
static double DT = 0.5;

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
public void Execute()
{
    Dynamo.Console("Script started!");
    //Dynamo.Scriplet("test21_springs", "Шары на пружинах");

    Dynamo.SceneClear();
    //создаем сцену
    int SPEED = 1;
    var rnd = new Random();
    //generate PhOb's
    for (int i = 0; i < N; i++)
    {
        double x = rnd.NextDouble() * 40;
        double y = rnd.NextDouble() * 40;
        double z = rnd.NextDouble() * 40;
        int id = Dynamo.PhobNew(x, y, z);
        var hz0 = Dynamo.PhobGet(id) as Phob;
        int sz = rnd.Next(1, 5);
        hz0.radius = sz;
        hz0.mass = sz;
        hz0.AttrSet("clr", Facet3.ColorHtml(clrs[rnd.Next(0, clrs.Length - 1)]));
    }
    //generate connections
    int[] arr = Dynamo.SceneIds();
    List< Tuple<int, int, int> > lstConnect = new List< Tuple<int, int, int> > ();
    List<int> lstNum = new List<int>();
    HashSet<int> hs = new HashSet<int>();
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
        lstNum.Add(num);
        hs.Add(one);
        hs.Add(two);
    }
    for (int i = 0; i < N; i++)
    {
        if (hs.Contains(i)) continue;
        if(i == N - 1)
        {
            int id = Dynamo.PhobNew(0,0,0);
            var hz = Dynamo.PhobGet(id) as Phob;
            hz.bDrawAsLine = true;
            Tuple<int, int, int> tup = new Tuple<int, int, int>(arr[i], arr[0], id);
            lstConnect.Add(tup);
        }
        else
        {
            int id = Dynamo.PhobNew(0,0,0);
            var hz = Dynamo.PhobGet(id) as Phob;
            hz.bDrawAsLine = true;
            Tuple<int, int, int> tup = new Tuple<int, int, int>(arr[i], arr[i + 1], id);
            lstConnect.Add(tup);
        }
    }

    Dynamo.SceneBox = new Box(0, 40, 0, 40, 0, 40);
    DrawSprings(lstConnect);
    Dynamo.SceneDraw();
    int iTotalRes = 0;

    Box bx = Dynamo.SceneBox;
    double dx, dy, dz;
    for (int i = 0; i < 4000; i++)
    {
        //calc new positions
        for (int j = 0; j < arr.Length; j++)
        {
            var hz = Dynamo.PhobGet(arr[j]) as Phob;
            //затухание!
            hz.v_x *= dKoefSpeed;
            hz.v_y *= dKoefSpeed;
            hz.v_z *= dKoefSpeed;

            hz.x += DT * hz.v_x;
            hz.y += DT * hz.v_y;
            hz.z += DT * hz.v_z;
        }

        //calc force                   
        for (int j = 0; j < lstConnect.Count; j++)
        {
            Tuple<int, int, int> tup = lstConnect[j];
            int one = tup.Item1;
            int two = tup.Item2;
            var hz1 = Dynamo.PhobGet(one) as Phob;
            var hz2 = Dynamo.PhobGet(two) as Phob;

            double d = hz1.Distance(hz2);
            double dKoef = dKoefSpring;
            //mass & distance
            if( d < dLink )
            {   //отталкивание
                dKoef *= (dLink / d);
                hz2.v_x += dKoef * (hz2.x - hz1.x) / hz2.mass;
                hz2.v_y += dKoef * (hz2.y - hz1.y) / hz2.mass;
                hz2.v_z += dKoef * (hz2.z - hz1.z) / hz2.mass;

                hz1.v_x -= dKoef * (hz2.x - hz1.x) / hz1.mass;
                hz1.v_y -= dKoef * (hz2.y - hz1.y) / hz1.mass;
                hz1.v_z -= dKoef * (hz2.z - hz1.z) / hz1.mass;
            }
            else
            {   //притяжение
                dKoef *= (d / dLink);
                hz2.v_x -= dKoef * (hz2.x - hz1.x) / hz2.mass;
                hz2.v_y -= dKoef * (hz2.y - hz1.y) / hz2.mass;
                hz2.v_z -= dKoef * (hz2.z - hz1.z) / hz2.mass;

                hz1.v_x += dKoef * (hz2.x - hz1.x) / hz1.mass;
                hz1.v_y += dKoef * (hz2.y - hz1.y) / hz1.mass;
                hz1.v_z += dKoef * (hz2.z - hz1.z) / hz1.mass;
            }
        }

        DrawSprings(lstConnect);
        Dynamo.SceneDraw();
        if (i % 5 == 0 && iTotalRes < 100)
        {
            //Dynamo.SaveScripresult();
            iTotalRes++;
        }

        if (i % 40 == 0)
        {
            double ix, iy, iz;
            Dynamo.SceneImpulse(out ix, out iy, out iz);
            Dynamo.Console(Dynamo.SceneEnergy().ToString() + ", ix=" + ix + ", iy=" + iy + ", iz=" + iz);
        }
        System.Threading.Thread.Sleep(50); //Мы ждем 1/20 секунду в даном потоке   
    }
}
Execute();