//test19_monitor_client
//Dynamo.Scriplet("test19_monitor_client", "Визуализация нагрузки сервера");
System.Drawing.Color[] clrs = new System.Drawing.Color[300];

SocketClient tn = new SocketClient("Monitor", "127.0.0.1", 4661);
tn.nIter = 1;
tn.sSend = "Host: {0}\r\n\r\n";
Random rnd = new Random();

Dynamo.Console("Press 'q' to quit");
int step = 0;
while (true)
{
    tn.Run();
    var resp = tn.LastResponse();
//Dynamo.Console("$" + resp);

//ответ сервера содержит информацию о нагрузке, парсим и создаем bitmap
    for(int j = 0; j<clrs.Length; j++) clrs[j] = System.Drawing.Color.Black;
    DateTime dt0 = DateTime.Now, dt2;
    var arr = resp.Split(';');
    for(int j = 0; j < arr.Length; j++)
    {
        if( arr[j] != "" )
        {
            var arr2 = arr[j].Split('=');
            int k = Math.Abs(arr2[0].GetHashCode()) % 300;
            string sDt = arr2[1].Trim();
            if (sDt == "") continue;
            dt2 = DateTime.ParseExact(sDt, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan difference = dt0 - dt2; //create TimeSpan object
            int m = (int)Math.Max(255 - 10 * difference.TotalSeconds, 0);
            if( m > 255 ) m = 255;
            clrs[k] = System.Drawing.Color.FromArgb( m, 0, 0);
        }
    }

    var s1 = MathPanelExt.QuadroEqu.DrawBitmap(15, 20, clrs);
    string s2 = "{\"options\":{\"x0\": -0.5, \"x1\": 20, \"y0\": -0.5, \"y1\": 15, \"clr\": \"#ff0000\", \"sty\": \"dots\", \"size\":40, \"lnw\": 3, \"wid\": 800, \"hei\": 600, \"second\":1 }";
s2 += ", \"data\":[" + s1 + "]}";
    Dynamo.SceneJson(s2);
    //Dynamo.SaveScripresult();

    //if (step % 3== 0)
        //Dynamo.Console("*", false);
    Dynamo.Console("Press 'q' to quit");
    System.Threading.Thread.Sleep(1000);
    step++;
    if (Dynamo.KeyConsole.ToUpper() == "Q")//.Enter)
    {
        break;
    }
    //else Dynamo.Console(Dynamo.KeyConsole + "Press 'q' to quit");
}
    
