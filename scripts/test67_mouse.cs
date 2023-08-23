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
            Dynamo.Console("Скрипт стартовал!");
            int xClick = -1; ///x позиция клика мыши
            int yClick = -1; ///y позиция клика мыши
            int xMouse = -1; ///x позиция мыши
            int yMouse = -1; ///y позиция мыши
            int xMouseUp = -1; ///x позиция окончании клика мыши
            int yMouseUp = -1; ///y позиция окончании клика мыши
            bool b_mouseDown = false; ///мышь нажата
            bool b_clickDone = false; ///произошел клик мыши

            for (int i = 0; i < 100; i++)
            {
                //заснуть
                System.Threading.Thread.Sleep(500);
                //Dynamo.Console("sleep done=" + i);
                string resp = Dynamo.KeyConsole;
                if (resp == "Q")
                {
                    break;
                }
                Dynamo.GetCanvasMouseInfo(ref xClick, ref yClick,
                    ref xMouse, ref yMouse, ref xMouseUp, ref yMouseUp,
                    ref b_mouseDown, ref b_clickDone);
                Dynamo.Console(i + "=" + xClick + ";" + yClick + ";" +
                    xMouse + ";" + yMouse + ";" + xMouseUp + ";" + yMouseUp + ";" +
                    b_mouseDown + ";" + b_clickDone);
            }
        }
    }  
}