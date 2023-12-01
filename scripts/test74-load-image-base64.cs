Dynamo.SceneClear();
Dynamo.Console("test74-load-image-base64.cs");

//Dynamo.LoadImage("iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==");
var fname = @"c:\temp\b64.jpg";
var bm = new BitmapSimple(800, 600, System.Drawing.Color.White, System.Drawing.Color.Blue, false);
bm.Save(fname);

var bts = System.IO.File.ReadAllBytes(fname);
var s = System.Convert.ToBase64String(bts);
Dynamo.Console(s);
Dynamo.LoadImageDraw(s, "jpg", "img2", 20, 20);

System.Threading.Thread.Sleep(150);
Dynamo.GraphExample("1"); 
Dynamo.GraphExample("2"); 
Dynamo.GraphExample("3");