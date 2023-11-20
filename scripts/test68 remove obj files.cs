using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;

///assemblies to use
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{  
	public class Script
	{  
		public void Execute()
        {
            Dynamo.Console("test68 remove obj files started!");
            int n = MathPanelExt.FileSystemClean.CleanDir(@"C:\VTK\", "*.obj", true);
            Dynamo.Console("removed =" + n);
        }  
    }  
}