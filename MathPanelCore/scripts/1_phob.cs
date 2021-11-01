//Dynamo.Allert(""q"");
int id = Dynamo.PhobNew(1, 2, 3);
Dynamo.Console(id.ToString());
Dynamo.PhobSet(0, 11, 21, 33);
var hz = Dynamo.PhobGet(0) as Phob;
Dynamo.Console(hz.ToString());
