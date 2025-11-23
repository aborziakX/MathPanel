//test27_cone_pyramid
 public void Execute()
{
    Dynamo.Console("test27_cone_pyramid");
    Dynamo.SceneClear();

    int id = Dynamo.PhobNew(-0, 0, 0);
    var hz = Dynamo.PhobGet(id) as Phob;
    double[] radv = { 10, 9, 8.5, 8.2, 8.2, 8.5, 9, 10 };
    var t1 = new RadiusVar(19, radv, "Yellow", 12);
    //t1.Fractal(1);
    hz.Shape = t1;

    Dynamo.SceneBox = new Box(-20, 20, -20, 20, -20, 20);
    Dynamo.SceneDrawShape(true, false);

    for (int i = 0; i < 1000; i++)
    {
        Dynamo.SceneDrawShape(true);
        if (i % 40 == 0)
        {
            double ix, iy, iz;
        }
        System.Threading.Thread.Sleep(50);
    }
}
Execute();
