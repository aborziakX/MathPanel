using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPanel
{
    //public class Engine
    public partial class Dynamo //: Window
    {
        /*int xClick = -1; ///x позиция клика мыши
        int yClick = -1; ///y позиция клика мыши
        int xMouse = -1; ///x позиция мыши
        int yMouse = -1; ///y позиция мыши
        int xMouseUp = -1; ///x позиция окончании клика мыши
        int yMouseUp = -1; ///y позиция окончании клика мыши
        bool b_mouseDown = false; ///мышь нажата
        bool b_clickDone = false; ///произошел клик мыши

        /// <summary>
        /// конструктор для Engine
        /// </summary>
        public Engine()
        {
            //string s = d.ToString("G4", );
            //double.Parse()
        }*/

        public static double ToDouble(string s)
        {
            return Double.Parse(s.Replace(",", "."), 
                System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        }

        /// <summary>
        /// получить информацию о мыши в канвасе
        /// </summary>
        /// <param name="_info">строка с информацией</param>
        /// <param name="_xClick">x позиция клика мыши</param>
        /// <param name="_yClick">y позиция клика мыши</param>
        /// <param name="_xMouse"x позиция мыши</param>
        /// <param name="_yMouse">y позиция мыши</param>
        /// <param name="_xMouseUp">x позиция окончании клика мыши</param>
        /// <param name="_yMouseUp">y позиция окончании клика мыши</param>
        /// <param name="_b_mouseDown">мышь нажата</param>
        /// <param name="_b_clickDone">клик произошел</param>
        public static void ParseCanvasMouseInfo(ref string _info, ref int _xClick, ref int _yClick,
            ref int _xMouse, ref int _yMouse, ref int _xMouseUp, ref int _yMouseUp,
            ref bool _b_mouseDown, ref bool _b_clickDone)
        {
            var arr = _info.Split(';');
            foreach(var s in arr)
            {
                if (s == "") continue;
                var arr2 = s.Split('=');
                if (arr2.Length != 2) continue;
                if (arr2[0] == "xClick")
                {
                    _xClick = (int)Math.Round(ToDouble(arr2[1]), 0);
                    //xClick = _xClick;
                }
                else if (arr2[0] == "yClick")
                {
                    _yClick = (int)Math.Round(ToDouble(arr2[1]), 0);
                    //yClick = _yClick;
                }
                else if (arr2[0] == "xMouse")
                {
                    _xMouse = (int)Math.Round(ToDouble(arr2[1]), 0);
                    //xMouse = _xMouse;
                }
                else if (arr2[0] == "yMouse")
                {
                    _yMouse = (int)Math.Round(ToDouble(arr2[1]), 0);
                    //yMouse = _yMouse;
                }
                else if (arr2[0] == "xMouseUp")
                {
                    _xMouseUp = (int)Math.Round(ToDouble(arr2[1]), 0);
                    //xMouseUp = xMouseUp;
                }
                else if (arr2[0] == "yMouseUp")
                {
                    _yMouseUp = (int)Math.Round(ToDouble(arr2[1]), 0);
                    //yMouseUp = _yMouseUp;
                }
                else if (arr2[0] == "b_mouseDown")
                {
                    _b_mouseDown = bool.Parse(arr2[1]);
                    //b_mouseDown = _b_mouseDown;
                }
                else if (arr2[0] == "b_clickDone")
                {
                    _b_clickDone = bool.Parse(arr2[1]);
                    //b_clickDone = _b_clickDone;
                }
            }
        }
    }
}
