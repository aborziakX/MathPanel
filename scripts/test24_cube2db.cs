//test24_cube2db
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
            Dynamo.ConsoleClear();
            Dynamo.Console("test24_cube2db: print 's' to save in DB");
            //Dynamo.Scriplet("test24_cube", "Желтый куб вращается");
            Dynamo.SceneClear();

            int id = Dynamo.PhobNew(0, 0, 0);
            Dynamo.Console(id.ToString());
            var hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.Console(hz.ToString());

            Cube cub = new Cube(20, "Yellow");
            cub.bDrawNorm = true;
            cub.scaleX = 0.5;
            hz.Shape = cub;

            Dynamo.SceneBox = new Box(-20, 20, -20, 20, -20, 20);
            Dynamo.SceneDrawShape(true, true);

            for (int i = 0; i < 100; i++)
            {
                cub.ZRotor += 0.1;
                cub.XRotor += 0.03;
                //Dynamo.Console("zr=" + cub.ZRotor);
                Dynamo.SceneDrawShape(true);
                //if (Dynamo.KeyConsole == "S")
                { 
                    //Dynamo.SaveScripresult();
                }
                System.Threading.Thread.Sleep(250);
            }
        }
    }
}
