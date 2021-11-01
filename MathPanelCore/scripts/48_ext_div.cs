//test48_ext_div
public void Execute()
{
    Dynamo.Console("test48_ext_div");
    //messages
    string[] fnames = { "<h5>Wau!</h5>", "<h4>Wau!</h4>", "<h3>Wau!</h3>", "<h2>Wau!</h2>", "<h1>Wau!</h1>" };

    //loop through all messages
    for (int i = 0; i < fnames.Length; i++)
    {
        var fn = fnames[i];
        //send the message to a graphical window
        Dynamo.SetHtml(fn);
        //sleep a little bit
        System.Threading.Thread.Sleep(500);
    }
}
Execute();
