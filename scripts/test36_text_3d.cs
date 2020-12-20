//test36_text_3d
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
            Dynamo.Console("test36_text_3d");
            //Dynamo.Scriplet("test36_text_3d", "Просто текст в 3Д");
            Dynamo.SceneClear();
            //red
            int id = Dynamo.PhobNew(-0, 0, 0);
            var hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "clr", "#ff0000");
            Cube cub = new Cube(5, "Red");
            cub.scaleZ = 2;
            cub.scaleX = 0.5;
            cub.XRotor = 0.3;
            hz.Shape = cub;

            //yellow
            id = Dynamo.PhobNew(-10, 0, -10);
            hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "clr", "#ffff00");
            hz.radius = 0.5;
            //green
            id = Dynamo.PhobNew(10, 0, 10);
            hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "clr", "#00ff00");
            hz.radius = 2;
            //white
            id = Dynamo.PhobNew(0, -10, 5);
            hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "clr", "#ffffff");
            Dynamo.PhobAttrSet(id, "txt", "white");
            Dynamo.PhobAttrSet(id, "fontsize", "20");
            hz.radius = 0.3;

            //мои оси
            id = Dynamo.PhobNew(-20, -20, -20);
            hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "clr", "#ff00ff");
            Dynamo.PhobAttrSet(id, "txt2", "purple");
            Dynamo.PhobAttrSet(id, "txt1", "O");
            hz.bDrawAsLine = true;
            hz.p1.Copy(-20, -20, -20);
            hz.p2.Copy(20, -20, -20);
            /*//purple
            id = Dynamo.PhobNew(20, -20, -20);
            hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "clr", "#ff00ff");
            Dynamo.PhobAttrSet(id, "txt", "purple");
            hz.radius = 0;*/

            id = Dynamo.PhobNew(-20, -20, -20);
            hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "clr", "#00ffff");
            hz.bDrawAsLine = true;
            hz.p1.Copy(-20, -20, -20);
            hz.p2.Copy(-20, 20, -20);
            //cyan
            id = Dynamo.PhobNew(-20, 20, -20);
            hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "clr", "#00ffff");
            Dynamo.PhobAttrSet(id, "txt", "cyan");
            hz.radius = 0;

            id = Dynamo.PhobNew(-20, -20, -20);
            hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "clr", "#ffff00");
            Dynamo.PhobAttrSet(id, "lnw", "1");
            hz.bDrawAsLine = true;
            hz.p1.Copy(-20, -20, -20);
            hz.p2.Copy(-20, -20, 20);
            //yellow
            id = Dynamo.PhobNew(-20, -20, 20);
            hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "clr", "#ffff00");
            Dynamo.PhobAttrSet(id, "txt", "yellow");
            hz.radius = 0;

            Dynamo.Console("total fac=" + Dynamo.SceneFacets());

            Dynamo.SceneBox = new Box(-20, 20, -20, 20, -20, 20);
            Dynamo.BAxes = false;
            Dynamo.SceneDrawShape(false, true);

            for (int i = 0; i < 1000; i++)
            {
                DateTime dt1 = DateTime.Now;
                Dynamo.SceneDrawShape(false, false);
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
