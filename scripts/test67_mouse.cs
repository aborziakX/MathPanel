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
        System.Drawing.Color[] clrs = new System.Drawing.Color[64];

        public void DrawTable(int pos)
        {
            int k = 0;
            string s3 = "";
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int m = (i + j) % 2;
                    clrs[k] = (m == 0 ? System.Drawing.Color.AliceBlue : System.Drawing.Color.Aqua);
                    if (k == pos)
                    {
                        s3 += ",";
                        s3 += QuadroEqu.DrawPoint(i + 0.5, j + 0.5, "", "circle", "#ff0000", "0.3", "20");
                    }
                    k++;
                }
            }
            string s1 = QuadroEqu.DrawBitmap(8, 8, clrs, 0.5, 0.5); //default from bottom
            string s2 = "{\"options\":{\"x0\": -0.05, \"x1\": 11.05, \"y0\": -0.05, \"y1\": 8.05, \"clr\": \"#ff0000\", \"sty\": \"dots\", \"size\":74, \"lnw\": 3, \"wid\": 800, \"hei\": 600 }";
            s2 += ", \"data\":[" + s1 + s3 + "]}";
            //Dynamo.Console(s2);
            Dynamo.SceneJson(s2);        
        }

        public void Execute()
        {
            Dynamo.Console("Скрипт стартовал!");
            int xClick = -1; ///x позиция клика мыши
            int yClick = -1; ///y позиция клика мыши
            int xMouse = -1; ///x позиция мыши
            int yMouse = -1; ///y позиция мыши
            int xMouseUp = -1; ///x позиция окончании клика мыши
            int yMouseUp = -1; ///y позиция окончании клика мыши
            bool b_mouseDown = false; ///мышь нажата
            bool b_clickDone = false; ///произошел клик мыши

            bool bUseUp = true;

            DrawTable(9);
            int sz = 75;
            //100 секунд активности
            for (int i = 0; i < 1000; i++)
            {
                //заснуть
                System.Threading.Thread.Sleep(100);
                //Dynamo.Console("sleep done=" + i);
                string resp = Dynamo.KeyConsole;
                if (resp == "Q")
                {
                    break;
                }
                Dynamo.GetCanvasMouseInfo(ref xClick, ref yClick,
                    ref xMouse, ref yMouse, ref xMouseUp, ref yMouseUp,
                    ref b_mouseDown, ref b_clickDone);
                if (b_clickDone)
                {
                    Dynamo.Console(i + "=" + xClick + ";" + yClick + ";" +
                        xMouse + ";" + yMouse + ";" + xMouseUp + ";" + yMouseUp + ";" +
                        b_mouseDown + ";" + b_clickDone);
                    int row = (bUseUp ? xMouseUp : xClick) / sz;
                    int col = (600 - (bUseUp ? yMouseUp : yClick)) / sz;
                    if (col >= 8 || col < 0 || row >= 8 || row < 0) continue;
                    Dynamo.Console(row + "," + col);
                    DrawTable(row * 8 + col);
                }
                if (b_mouseDown)
                {
                    int row = (xMouse) / sz;
                    int col = (600 - yMouse) / sz;
                    if (col >= 8 || col < 0 || row >= 8 || row < 0) continue;
                    //Dynamo.Console(row + "," + col);
                    DrawTable(row * 8 + col);
                }
            }
        }
    }  
}