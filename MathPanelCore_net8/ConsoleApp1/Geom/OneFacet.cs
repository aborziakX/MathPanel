//2020, Andrei Borziak
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPanel
{
    /// <summary>
    /// OneFacet derived from GeOb
    /// просто одна или несколько граней
    /// </summary>
    public class OneFacet : GeOb
    {
        //треугольник
        public OneFacet(Vec3 v0, Vec3 v1, Vec3 v2, string color = null, bool bDouble = false) : base()
        {
            name = "Facet" + id_counter;
            ColorSet(color);

            //грани:
            Facet3 fac0_a = new Facet3(v0, v1, v2);
            if (!string.IsNullOrEmpty(color)) fac0_a.clr.Copy(clr);
            fac0_a.name = name + "fac" + id_fac++;
            lstFac.Add(fac0_a);
            if (bDouble)
            {
                //back
                Facet3 fac1_a = new Facet3(v2, v1, v0);
                if (!string.IsNullOrEmpty(color)) fac1_a.clr.Copy(clr);
                fac1_a.name = name + "fac" + id_fac++;
                lstFac.Add(fac1_a);
            }
            radius = Math.Sqrt(fac0_a.area / 2.0);
        }
        //трапеция
        public OneFacet(Vec3 v0, Vec3 v1, Vec3 v2, Vec3 v3, string color = null, bool bDouble = false, bool bDivide = true) : base()
        {
            name = "Facet" + id_counter;
            ColorSet(color);

            if (bDivide)
            {
                //грани:
                Facet3 fac0_a = new Facet3(v0, v1, v2), fac1_a;
                if (!string.IsNullOrEmpty(color)) fac0_a.clr.Copy(clr);
                fac0_a.name = name + "fac" + id_fac++;
                lstFac.Add(fac0_a);
                if (bDouble)
                {
                    //back
                    fac1_a = new Facet3(v2, v1, v0);
                    if (!string.IsNullOrEmpty(color)) fac1_a.clr.Copy(clr);
                    fac1_a.name = name + "fac" + id_fac++;
                    lstFac.Add(fac1_a);
                }
                radius = Math.Sqrt(fac0_a.area / 2.0);

                //2-я грань
                fac0_a = new Facet3(v0, v2, v3);
                if (!string.IsNullOrEmpty(color)) fac0_a.clr.Copy(clr);
                fac0_a.name = name + "fac" + id_fac++;
                lstFac.Add(fac0_a);
                if (bDouble)
                {
                    //back
                    fac1_a = new Facet3(v3, v2, v0);
                    if (!string.IsNullOrEmpty(color)) fac1_a.clr.Copy(clr);
                    fac1_a.name = name + "fac" + id_fac++;
                    lstFac.Add(fac1_a);
                }
            }
            else
            {
                Facet3 fac0_a = new Facet3(v0, v1, v2, v3);
                if (!string.IsNullOrEmpty(color)) fac0_a.clr.Copy(clr);
                fac0_a.name = name + "fac" + id_fac++;
                lstFac.Add(fac0_a);
                if (bDouble)
                {
                    //back
                    Facet3 fac1_a = new Facet3(v3, v2, v1, v0);
                    if (!string.IsNullOrEmpty(color)) fac1_a.clr.Copy(clr);
                    fac1_a.name = name + "fac" + id_fac++;
                    lstFac.Add(fac1_a);
                }
                radius = Math.Sqrt(fac0_a.area);
            }
        }

        public OneFacet(Vec3 [] vv, string color = null) : base()
        {
            name = "Facet" + id_counter;
            ColorSet(color);

            //грани:
            for( int i = 0; i < vv.Length / 3; i++)
            { 
                Facet3 fac0_a = new Facet3(vv[i * 3], vv[i * 3 + 1], vv[i * 3 + 2]);
                if (!string.IsNullOrEmpty(color)) fac0_a.clr.Copy(clr);
                fac0_a.name = name + "fac" + id_fac++;
                lstFac.Add(fac0_a);

                if( i == 0 ) 
                    radius = Math.Sqrt(fac0_a.area / 2.0);
            }           
        }
    }
}
