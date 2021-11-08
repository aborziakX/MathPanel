using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;
using System.Text.RegularExpressions;

//test55_my_ip.cs

///сборки для добавления
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{  
	public class Script
	{
        public void Execute()
        {
            Dynamo.Console("test55_my_ip started!");
            //var s = Rest.Get("https://www.pvobr.ru/");//https://2ip.ru/");https://ip-lookup.net/
            var sData = System.IO.File.ReadAllText(@"c:\temp\my_ip.txt", System.Text.Encoding.UTF8);
            //Dynamo.Console(sData);

            string patt_out = "0={0}, 1={1}, 2={2}, i={3}";
            string patt = "<div class=\"ip\" id=\"d_clip_button\">.*?<span>(.*?)</span>.*?</div>";
            string patt_2 = "<a title=\"Посмотреть точное место на карте\".*?>(.*?)</a>";

            int i = 0;
            string sCode, sIp = "", sReg = "";

            Regex _regexInp = new Regex(patt, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var matches = _regexInp.Matches(sData);
            foreach (Match m in matches)
            {
                i++;
                sCode = string.Format(patt_out,
                    m.Groups["0"].Value,
                    m.Groups["1"] != null ? m.Groups["1"].Value.Replace("\n", "").Replace("\r", "").Trim() : "",
                    m.Groups["2"] != null ? m.Groups["2"].Value.Replace("\n", "").Replace("\r", "").Trim() : "",
                    i);
                //Dynamo.Console("si=" + sCode);

                if (m.Groups["1"] != null) sIp = m.Groups["1"].Value.Replace("\n", "").Replace("\r", "").Trim();
            }

            _regexInp = new Regex(patt_2, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            matches = _regexInp.Matches(sData);
            foreach (Match m in matches)
            {
                i++;
                sCode = string.Format(patt_out,
                    m.Groups["0"].Value,
                    m.Groups["1"] != null ? m.Groups["1"].Value.Replace("\n", "").Replace("\r", "").Trim() : "",
                    m.Groups["2"] != null ? m.Groups["2"].Value.Replace("\n", "").Replace("\r", "").Trim() : "",
                    i);
                //Dynamo.Console("si=" + sCode);

                if (m.Groups["1"] != null) sReg = m.Groups["1"].Value.Replace("\n", "").Replace("\r", "").Trim();
            }

            Dynamo.Console("sIp=" + sIp + ", sReg=" + sReg);

            //рисуем цветок
            string[] clr =
            {
                "#ff0000", "#ffaa00", "#ffff00", "#00ff00", "#0000ff", "#ff00ff"
            };
            double rad = 4;
            //большая окружность
            string s = MathPanelExt.QuadroEqu.DrawEllipse(rad * 2, rad * 2, 0, 0, 0, Math.PI * 2, 64);
            string s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ffffff\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"_second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            //6 малых окружностей вращаем
            for (i = 0; i < 6; i++)
            {
                double x = rad * Math.Cos((Math.PI * i) / 3);
                double y = rad * Math.Sin((Math.PI * i) / 3);
                s = MathPanelExt.QuadroEqu.DrawEllipse(rad, rad, x, y, 0, Math.PI * 2, 64);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"" + clr[i] + "\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

            }
            //малая окружность
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad, rad, 0, 0, 0, Math.PI * 2, 64);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ffffff\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

        }
    }  
}