//test32_facet_setZ
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
        double FuncZ(double x, double y)
        {
            return (x * x - y * y) * 0.1; 
        }
        public void Execute()
        {
            Dynamo.Console("test32_facet_setZ");
            //Dynamo.Scriplet("test32_facet", "Просто грань");
            Dynamo.SceneClear();

            int id = Dynamo.PhobNew(-0, 0, 0);
            var hz = Dynamo.PhobGet(id) as Phob;
            var t1 = new OneFacet(new Vec3(-10, 0, 0), new Vec3(0, 10, 0), new Vec3(10, 0, 0), new Vec3(0, -10, 0), 
                "Yellow", false);
            t1.Divide(3);
            t1.SetZ(FuncZ);
            t1.iFill = 3;
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
