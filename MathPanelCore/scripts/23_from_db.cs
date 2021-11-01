//test23_from_db
public void Execute()
{
    Dynamo.SceneClear();
    Dynamo.Console("23_from_db");
    string scid = "7", scrid = "";
    string[] res = Dynamo.LoadScripresult(scid, scrid);
    if (res == null || res.Length == 0) return;

    for (int i = 0; i < 1000; i++)
    {
        var s = res[i % res.Length];
        Dynamo.SceneJson(s);
        System.Threading.Thread.Sleep(50);
    }
}
Execute();
