using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MathPanel
{
    /// <summary>
    /// NodesPegasus derived from GeOb; to display scientific workflows
    /// </summary>
    public class NodesPegasus : GeOb
    {
        static Random rand = new Random(); //генератор случайных чисел

        public NodesPegasus(string fname, double size = 1, string color = null) : base()
        {
            name = "NodesPegasus" + id_counter;
            radius = size / 2.0;
            ColorSet(color);

            /* content like
<child ref="ID00248">
    <parent ref="ID00022"/>
    <parent ref="ID00067"/>
</child>
            */
            //парсить файл
            var dic = new Dictionary<string, List<string>>();
            List<string> lst = null;
            string[] lines = File.ReadAllLines(fname, Encoding.UTF8);
            string s, s_2;
            int iPar = 0;
            for(int i = 0; i < lines.Length; i++)
            {
                s = lines[i].Trim();
                if( s.IndexOf("<child ") == 0 )
                {
                    int ip = s.IndexOf("\"");
                    int ip_2 = s.LastIndexOf("\"");
                    if( ip < ip_2 )
                    {
                        s_2 = s.Substring(ip + 1, ip_2 - ip - 1);
                        if( !dic.ContainsKey(s_2) )
                        {
                            lst = new List<string>();
                            dic.Add(s_2, lst);
                        }
                    }
                }

                if (s.IndexOf("<parent ") == 0)
                {
                    int ip = s.IndexOf("\"");
                    int ip_2 = s.LastIndexOf("\"");
                    if (ip < ip_2)
                    {
                        s_2 = s.Substring(ip + 1, ip_2 - ip - 1);
                        lst.Add(s_2);
                        iPar++;
                    }
                }
            }

            //найти вершины не явлющиеся дочерними
            var hs = new HashSet<string>();
            foreach(var pair in dic)
            {
                foreach (var q in pair.Value)
                    hs.Add(q);
            }
            foreach (var pair in dic)
            {
                hs.Remove(pair.Key);
            }

            int iMain = hs.Count; //не явлющиеся дочерними
            int iChild = dic.Count;//явлющиеся дочерними
            int iTotal = iMain + iChild; //всего вершин
            double radPart = iTotal < 100 ? 0.33 : 0.1;

            //по спирали вершины не явлющиеся дочерними
            var dicPhob = new Dictionary<string, Phob>();
            Phob ph = null;
            double edge = 40.0; //ребро куба
            double x = 0, y = 0, z = edge * 0.5, fi = 0, radi = 0,
                dfi = 4.0 * Math.PI / iMain,
                dh = edge / iTotal,
                drad = edge * 0.5 / iMain;
            foreach (var q in hs)
            {
                x = radi * Math.Cos(fi);
                y = radi * Math.Sin(fi);
                int id = Dynamo.PhobNew(x, y, z);
                //Dynamo.PhobAttrSet(id, "text", q);
                ph = Dynamo.PhobGet(id);
                ph.radius = radPart;
                dicPhob.Add(q, ph);

                z -= dh;
                fi += dfi;
                radi += drad;
            }
            dh = (z + edge * 0.5) / iChild; //новый шаг по Z с учетом уровней

            bool bDone = false;
            int level = 1;
            while (!bDone)
            {
                bDone = true;
                var hsNow = new HashSet<string>();
                foreach (var pair in dic)
                {
                    if (hsNow.Contains(pair.Key))
                    {
                        bDone = false;
                        continue;
                    }

                    if (!dicPhob.ContainsKey(pair.Key))
                    {   //вершина еще не создана
                        bool bCan = true;
                        x = 0;
                        y = 0;
                        foreach (var q in pair.Value)
                        {   //проход по родителям
                            if (!dicPhob.ContainsKey(q))
                            {
                                bCan = false;
                                break;
                            }
                            ph = dicPhob[q];
                            x += ph.x;
                            y += ph.y;
                        }
                        if (!bCan)
                        {
                            bDone = false;
                            continue;
                        }
                        x = x / pair.Value.Count + rand.NextDouble() * 2.0;
                        y = y / pair.Value.Count + rand.NextDouble() * 2.0;
                        int id = Dynamo.PhobNew(x, y, z);
                        //Dynamo.PhobAttrSet(id, "text", pair.Key);
                        ph = Dynamo.PhobGet(id);
                        ph.radius = radPart;
                        dicPhob.Add(pair.Key, ph);
                        hsNow.Add(pair.Key);
                        z -= dh;
                    }
                }
                
                level++;
            }

            level += 0;
            //построить связи
            foreach (var pair in dic)
            {
                Phob ph_0 = dicPhob[pair.Key];
                foreach (var q in pair.Value)
                {
                    Phob ph_1 = dicPhob[q];
                    x = (ph_0.x + ph_1.x) / 2;
                    y = (ph_0.y + ph_1.y) / 2;
                    z = (ph_0.z + ph_1.z) / 2;
                    int id = Dynamo.PhobNew(x, y, z);
                    ph = Dynamo.PhobGet(id);
                    ph.bDrawAsLine = true;
                    ph.p1.Copy(ph_0.x, ph_0.y, ph_0.z);
                    ph.p2.Copy(ph_1.x, ph_1.y, ph_1.z);
                    Dynamo.PhobAttrSet(id, "clr", "#009900");
                    Dynamo.PhobAttrSet(id, "lnw", "1");
                }
            }
        }
    }
}
