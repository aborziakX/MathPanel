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
                var s = Dynamo.GetCanvasMouseInfo();
                Dynamo.Console(i + "=" + s);
            }
        }
    }  
}