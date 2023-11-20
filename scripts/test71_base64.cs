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
            Dynamo.Console("test71_base64 started!");

            string input = "Hello, World"; //входное сообщение

            string output = Base64Sha.CreateMD5(input);
            Dynamo.Console("CreateMD5 =" + output);

            output = Base64Sha.Base64Encode(input);
            Dynamo.Console("encode =" + output);

            output = Base64Sha.Base64Decode(output);
            Dynamo.Console("decode =" + output);
        }
    }  
}