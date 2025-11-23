using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;
using System.IO;
using System.Collections.Generic;

//test56_dir_recursive

///сборки для добавления
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{  
	public class Script
	{
        public void ProcDir(string path)
        {
            string[] files = Directory.GetFiles(path);
            foreach (var d in files)
            {
                Dynamo.Console("  " + d.Replace(path, ""));
            }
            string[] dirs = Directory.GetDirectories(path);
            foreach (var d in dirs)
            {
                Dynamo.Console(d.Replace(path, ""));
                ProcDir(d);
            }
        }
        public void Execute()
        {
            //Dynamo.Console("test56_dir_recursive started!");
            string path = @"C:\Users\boraa\Documents\reports_my\";

            ProcDir(path);
        }            
    }  
}