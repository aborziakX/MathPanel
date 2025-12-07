//test48_compare_text_files2
public void Execute()
        {
            Dynamo.Console("test48_compare_text_files2");
            string sDir = @"/home/RSD.CORP/borzyak-aa/develop/temp/";
            string[] fnames = { 
"/home/RSD.CORP/borzyak-aa/develop/oldGitMathPanel/MathPanelCore/MathPanelCore/Geom/Bitmap.cs", 
"/home/RSD.CORP/borzyak-aa/develop/MathPanel/MathPanelCore_net8/ConsoleApp1/Geom/Bitmap.cs" };

            var solv = new Similarica();
            var dat0 = System.IO.File.ReadAllLines(fnames[0], System.Text.Encoding.UTF8);
            var dat1 = System.IO.File.ReadAllLines(fnames[1], System.Text.Encoding.UTF8);

            double dScore = solv.Calc(dat0, dat1);
            Dynamo.Console("Score=" + dScore);
            //var sWg = solv.Printweights("font-size:14pt;");
            //Dynamo.SetHtml(sWg);

            System.Threading.Thread.Sleep(200);
            var sRs = solv.PrintStrings("font-size:14pt;");
            Dynamo.SetHtml(sRs);
        }
Execute();
