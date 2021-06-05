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
            Dynamo.Console("Script started!");
string test = @"‪C:\ASKP\fider2021.csv,4, error:Данный формат пути не поддерживается.";
            char [] chs = test.ToCharArray();
            for(int i = 0; i < chs.Length; i++)
            {
                Dynamo.Console(chs[i] + "=" + ((int)chs[i]));
            }
        }  
    }  
}