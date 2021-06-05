//test37_bitmap6
using MathPanel;
//using MathPanelExt;
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
            Dynamo.Console("test37_bitmap7");
            //the path to the images folder 
            string sDir = @"c:\temp\";

            var bm = new BitmapSimple(sDir + "o4.jpg");
            bm.BlackWhite();
            bm.Save(sDir + "o4_bw.jpg");
            
        }
    }
}
