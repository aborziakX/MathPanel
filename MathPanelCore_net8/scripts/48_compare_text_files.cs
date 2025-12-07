//test48_compare_text_files
		public void Execute()
        {
            Dynamo.Console("test48_compare_text_files");
            //the path to the files folder 
            string sDir = @"~/develop/temp/";
            //the array of filenames
            string[] fnames = { "template.cs", "template_2.cs" };

            //create an instance of Similarica class
            var solv = new Similarica();

            //read data from 1 file
            var dat0 = System.IO.File.ReadAllLines(sDir + fnames[0], System.Text.Encoding.UTF8);
            //read data from 2 file
            var dat1 = System.IO.File.ReadAllLines(sDir + fnames[1], System.Text.Encoding.UTF8);

            //get the score and weights
            double dScore = solv.Calc(dat0, dat1);
            Dynamo.Console("Score=" + dScore);

            //display alignments
            System.Threading.Thread.Sleep(200);
            var sRs = solv.PrintStrings("font-size:14pt;");
            Dynamo.SetHtml(sRs);
        }
Execute();
