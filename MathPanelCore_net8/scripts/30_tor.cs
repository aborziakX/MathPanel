//test30_tor
public void Execute()
{
    Dynamo.Console("test30_tor");
    //Dynamo.Scriplet("test30_tor", "Просто бублик");
    Dynamo.SceneClear();

    int id = Dynamo.PhobNew(-0, 0, 0);
    var hz = Dynamo.PhobGet(id) as Phob;
    var t1 = new Tor(10, 3, "Yellow", 32);
    //t1.Fractal(1);
    hz.Shape = t1;
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
