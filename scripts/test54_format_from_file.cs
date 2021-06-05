using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;
using System.IO;
using System.Collections.Generic;

//сравнить данные

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
            var lst = new List<string>();
            var lst_2 = new List<string>();
            string[] lines = File.ReadAllLines(@"C:\temp\Pyramid\events.txt", System.Text.Encoding.UTF8);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < lines.Length; i++)
            {
                var s = lines[i];
                int ipos = s.IndexOf("0x");
                if (ipos > 0)
                {
                    var s2 = "case " + s.Substring(ipos);
                    s2 = s2.Replace("//", ": descrClean = \"") + "\"; break;";
                    Dynamo.Console(s2);
                    sb.AppendLine(s2);
                    lst.Add(s2);
                }
            }
            File.WriteAllText(@"C:\temp\Pyramid\events_1.txt", sb.ToString(), System.Text.Encoding.UTF8);

        }            
    }  
}