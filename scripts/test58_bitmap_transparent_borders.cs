//test58_bitmap_transparent_borders
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
            Dynamo.Console("test58_bitmap_transparent_borders");
            //the path to the images folder 
            string sDir = @"C:\Andrei\Sana2\1000017\images\";

            //create our BitmapSimple object
            var bm = new BitmapSimple(sDir + "facebook2.png");//phone2//email2//instagram2
            bm.Transparent(true, 25, 200);
            //save it to a file
            var fn = sDir + "facebook2.png";
            bm.Save(fn);
            //and pass the file to our Image component
            Dynamo.SetBitmapImage(fn);
        }
    }
}
