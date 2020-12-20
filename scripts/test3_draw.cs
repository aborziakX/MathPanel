Dynamo.SceneClear();

int id = Dynamo.PhobNew(1, 2, 3);
Dynamo.Console(id.ToString());
var hz = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz.ToString());

id = Dynamo.PhobNew(11, 12, 3);
Dynamo.Console(id.ToString());
Dynamo.PhobAttrSet(id, "clr", "#ffaa00");
var hz1 = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz1.ToString());

id = Dynamo.PhobNew(11+2, 12+2, 5);
Dynamo.Console(id.ToString());
Dynamo.PhobAttrSet(id, "clr", "#00ffaa");
var hz2 = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz2.ToString());

id = Dynamo.PhobNew(11+1, 12+1, 4);
Dynamo.Console(id.ToString());
Dynamo.PhobAttrSet(id, "clr", "#0000ff");
var hz3 = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz3.ToString());

Dynamo.SceneDraw(true);
