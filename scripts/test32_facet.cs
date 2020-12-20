//test32_facet
using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;

///сборки для добавления
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{
    public class Script
    {
        public void Execute()
        {
            Dynamo.Console("test32_facet");
            //Dynamo.Scriplet("test32_facet", "Просто грань");
            Dynamo.SceneClear();

            int id = Dynamo.PhobNew(-0, 0, 0);
            var hz = Dynamo.PhobGet(id) as Phob;
            var t1 = new OneFacet(new Vec3(-10, 0, 0), new Vec3(0, 0, 7), new Vec3(10, 0, 0), "Yellow", true);
            //t1.Fractal(1);
            hz.Shape = t1;

            id = Dynamo.PhobNew(-0, 0, 10);
            hz = Dynamo.PhobGet(id) as Phob;
            Vec3[] vv =
            {
                new Vec3(-10, 0, 0), new Vec3(-5, 0, 7), new Vec3(0, 0, 0),
                new Vec3(-0, 0, 0), new Vec3(5, 0, 7), new Vec3(10, 0, 0),
            };
            t1 = new OneFacet(vv, "Green");
            //t1.Fractal(1);
            hz.Shape = t1;

            //var bm = new BitmapSimple(@"world400.jpg");
            var bm = new BitmapSimple(@"arrow_red2.png");
            id = Dynamo.PhobNew(-0, 0, -10);
            hz = Dynamo.PhobGet(id) as Phob;
            t1 = new OneFacet(new Vec3(-5, -7, -5), new Vec3(5, -7, -5),
                new Vec3(5, -7, 5), new Vec3(-5, -7, 5), "Red");
            //t1.Divide(5);
            //t1.SetBitmapPlane(bm);
            t1.iFill = 3;

            t1.DivideFractal(1);
            hz.Shape = t1;

            Dynamo.Console("total fac=" + Dynamo.SceneFacets());

            Dynamo.SceneBox = new Box(-20, 20, -20, 20, -20, 20);
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
    }
}
