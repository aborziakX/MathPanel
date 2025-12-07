//test31_pipe
public void Execute()
{
    Dynamo.Console("test31_pipe");
    //Dynamo.Scriplet("test31_pipe", "Просто труба");
    Dynamo.SceneClear();

    string[] color = { "Green", "Yellow", "Red" };
    double[] size = { 6, 6, 5, 4, 3, 2 };
    Vec3[] center = { new Vec3(5, 0, 0), new Vec3(10, 0, 5), new Vec3(15, 0, 5), 
        new Vec3(15, 10, 5), new Vec3(15, 10, -5), new Vec3(5, 10, -10) };

    int id = Dynamo.PhobNew(0, 0, 0);
    var hz = Dynamo.PhobGet(id) as Phob;
    var t1 = new Pipe(size, center, color, 32);
    //t1.Fractal(1);
    hz.Shape = t1;

    //like 1-t segment
    int id2 = Dynamo.PhobNew(0, 0, 0);
    var hz2 = Dynamo.PhobGet(id2) as Phob;
    hz2.bDrawAsLine = true;
    hz2.x = 7.5;
    hz2.y = 0;
    hz2.z = 8.5;
    hz2.p1.Copy(5, 0, 6);
    hz2.p2.Copy(10, 0, 11);

    //like 2-в segment
    id2 = Dynamo.PhobNew(0, 0, 0);
    hz2 = Dynamo.PhobGet(id2) as Phob;
    hz2.bDrawAsLine = true;
    hz2.x = 12.5;
    hz2.y = 0;
    hz2.z = 11;
    hz2.p1.Copy(10, 0, 11);
    hz2.p2.Copy(15, 0, 11);

    Dynamo.Console("total fac=" + Dynamo.SceneFacets());

    Dynamo.SceneBox = new Box(-20, 20, -20, 20, -20, 20);
    Dynamo.BDrawBox = false;
    Dynamo.SceneDrawShape(true, false);

    for (int i = 0; i < 1000; i++)
    {
        DateTime dt1 = DateTime.Now;
        Dynamo.SceneDrawShape(true, false);
        DateTime dt2 = DateTime.Now;
        TimeSpan diff = dt2 - dt1;
        int ms = (int)diff.TotalMilliseconds;
        if (i % 40 == 0)
        {
            Dynamo.Console("ms=" + ms);
        }
        if (i == 0 || Dynamo.KeyConsole == "S")
        {
            //Dynamo.SaveScripresult();
        }
        System.Threading.Thread.Sleep(ms < 50 ? 50 - ms : 1);
    }
}
Execute();
