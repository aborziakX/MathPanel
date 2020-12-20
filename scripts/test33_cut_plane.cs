//test33_cut_plane
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
            Dynamo.Console("test33_cut_plane");
            //Dynamo.Scriplet("test33_cut_plane", "Просто грань");
            Dynamo.SceneClear();

            int id = Dynamo.PhobNew(-10, 0, 0);
            var hz = Dynamo.PhobGet(id) as Phob;
            var t1 = new Cube(10, "Yellow");
            t1.CutByPlane(0, 0, 1, 1);
            hz.Shape = t1;
            
            int id2 = Dynamo.PhobNew(10, 0, 0);
            var hz2 = Dynamo.PhobGet(id2) as Phob;
            var t2 = new Cube(10, "Green");
            t2.CutByPlane(1, 1, 0, 7);
            hz2.Shape = t2;

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
