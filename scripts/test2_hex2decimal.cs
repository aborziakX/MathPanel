﻿Dynamo.Allert("q");
//Dynamo.Allert(""w"");
//string hz = "0xFFFE3C003F0078006D006C002000760065007200730069006F006E003D00220031002E0030002200200065006E0063006F00640069006E0067003D0022007500740066002D003100360022003F003E003C004400200054003D002200380064003100310038003500660033002D0063003500630039002D0034006200300034002D0061003800330065002D0037006300640035006300310062003600610030003700640022003E003C00730020004E003D002200630064003800360037006200370035002D0061003200320033002D0034003600320064002D0039006400640063002D0061006600610033003000390065006400370030003900640022003E001C04350440043A04430440043804390420003200300033002E00320054002C00200016213100310035003500330033003C002F0073003E003C0063006C0020004E003D002200630065006200340039003300300035002D0037006100310031002D0034003900300031002D0062006600630035002D0035003200640065006600650039006300640039003700300022003E00330034003400360031003500360066002D0035003800640032002D0034003300660065002D0039003600370065002D006100320062006300350064003600310034006100320066003C002F0063006C003E003C002F0044003E00";
//byte[] bt = Dynamo.HexStringToByteArray(hz);
//string res = System.Text.Encoding.Unicode.GetString(bt);
//Dynamo.Console(res);
string hz = "0x4F2B4000";
byte[] bt = Dynamo.HexStringToByteArray(hz);
string res = Dynamo.ByteArrayReverseToInt(bt).ToString();
Dynamo.Console(res);