//test22_sphere
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
            Dynamo.SceneClear();
            int id = Dynamo.PhobNew(0, 0, 0);
            Dynamo.Console(id.ToString());
            var hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.Console(hz.ToString());

            var t4 = new Sphere(20, "Yellow", 36, 1.5, 3.0);
            //t4.bDrawNorm = true;
            hz.Shape = t4;

            Dynamo.SceneBox = new Box(-20, 20, -20, 20, -20, 20);
            Dynamo.ZBoXTrans = -20;
            Dynamo.CameraZ = 50;
            Dynamo.XRotor = 0;
            Dynamo.YRotor = 0;
            Dynamo.ZRotor = 0;
            Dynamo.SceneDrawShape(true, false);
            Dynamo.ScreenJsonSave("json\\22_1.json");
            var q = Dynamo.ScreenJsonLoad("json\\22_1.json");
            Dynamo.Console(q);

            for (int i = 0; i < 1000; i++)
            {
                Dynamo.SceneDrawShape(true);
                //if (Dynamo.KeyConsole == "D")
                    //Dynamo.ScreenJsonSaveDB("1");
                if (i % 40 == 0)
                {
                    double ix, iy, iz;
                }
                System.Threading.Thread.Sleep(50);
            }
        }
    }
}
