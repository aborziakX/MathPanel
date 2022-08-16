//test63_spider.cs
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
        //удалить html теги
        const string HTML_TAG_PATTERN = "<.*?>";
        static string StripHTML(string inputString)
        {
            return System.Text.RegularExpressions.Regex.Replace(inputString, HTML_TAG_PATTERN, string.Empty);
        }

        public void Execute()
        {

            Dynamo.Console("test63_spider started!");

            string url = "https://www.pvobr.ru/search.aspx?txtSearch=";
            string search = "/бубен";
            url += Rest.EncodeString(search);
            Dynamo.Console(url);

            string data = Rest.Get(url);
            Dynamo.Console("сырые=" + data);

            Dynamo.Console("текст=" + StripHTML(data));
        }
    }  
}