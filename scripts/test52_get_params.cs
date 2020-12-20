//test52_get_params
using MathPanel;
//using MathPanelExt;
using System.Net.Sockets;
using System;
using System.Collections.Generic;

///assemblies to use
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{
    public class Script
    {
        public void Execute()
        {
            Dynamo.Console("test52_get_params");
            string data = "Number:<select id='hz1'><option value='1'>one<option><option value='2'>two<option></select>";
            data += " <input type='button' value='select' onclick='extra_params=$(\"#hz1\").val(); return false;' />";
            Dynamo.SetHtml(data);
            var hz1 = Dynamo.GetParams();
            Dynamo.Alert(hz1);

            data = "Number2:<select id='hz2'><option value='1'>one<option><option value='2'>two<option></select>";
            data += " <input type='button' value='select' onclick='extra_params=$(\"#hz2\").val(); return false;' />";
            Dynamo.SetHtml(data);
            var hz2 = Dynamo.GetParams();
            Dynamo.Alert(hz2);
        }
    }
}
