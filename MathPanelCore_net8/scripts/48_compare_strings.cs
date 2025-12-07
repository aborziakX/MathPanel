//test48_compare_strings

        public void Execute()
        {
            Dynamo.Console("test48_compare_strings");
            //strings to compare
            string[] fnames = { "optimal construction", "optimization const" };
            //string[] fnames = { "ANDREI", "ALEXEI" };

            //create an instance of Similarica class
            var solv = new Similarica();
            //get the score and weights
            double dScore = solv.Calc(fnames[0], fnames[1]);
            Dynamo.Console("Score=" + dScore);
            //generate the table of weights
            var sWg = solv.Printweights("font-size:14pt;");
            //generate alignments
            var sRs = solv.PrintStrings("font-size:14pt;");

            for (int i = 0; i < 10; i++)
            {
                //display results
                Dynamo.SetHtml(i % 2 == 0 ? sWg : sRs);
                //sleep
                System.Threading.Thread.Sleep(2000);
            }
            
        }
Execute();

