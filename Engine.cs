using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPanel
{
    public class Engine
    {
        int xClick = -1; ///x позиция клика мыши
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
        }

        public static double ToDouble(string s)
        {
            return Double.Parse(s.Replace(",", "."), 
                System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        }

        /// <summary>
        /// получить информацию о мыши в канвасе
        /// </summary>
        /// <param name="info">строка с информацией</param>
        /// <param name="_xClick">x позиция клика мыши</param>
        /// <param name="_yClick">y позиция клика мыши</param>
        /// <param name="_xMouse"x позиция мыши</param>
        /// <param name="_yMouse">y позиция мыши</param>
        /// <param name="_xMouseUp">x позиция окончании клика мыши</param>
        /// <param name="_yMouseUp">y позиция окончании клика мыши</param>
        /// <param name="_b_mouseDown">мышь нажата</param>
        /// <param name="_b_clickDone">клик произошел</param>
        public void ParseCanvasMouseInfo(ref string info, ref int _xClick, ref int _yClick,
            ref int _xMouse, ref int _yMouse, ref int _xMouseUp, ref int _yMouseUp,
            ref bool _b_mouseDown, ref bool _b_clickDone)
        {
            var arr = info.Split(';');
            foreach(var s in arr)
            {
                if (s == "") continue;
                var arr2 = s.Split('=');
                if (arr2.Length != 2) continue;
                if (arr2[0] == "xClick")
                {
                    xClick = (int)Math.Round(ToDouble(arr2[1]), 0);
                    _xClick = xClick;
                }
                else if (arr2[0] == "yClick")
                {
                    yClick = (int)Math.Round(ToDouble(arr2[1]), 0);
                    _yClick = yClick;
                }
                else if (arr2[0] == "xMouse")
                {
                    xMouse = (int)Math.Round(ToDouble(arr2[1]), 0);
                    _xMouse = xMouse;
                }
                else if (arr2[0] == "yMouse")
                {
                    yMouse = (int)Math.Round(ToDouble(arr2[1]), 0);
                    _yMouse = yMouse;
                }
                else if (arr2[0] == "xMouseUp")
                {
                    xMouseUp = (int)Math.Round(ToDouble(arr2[1]), 0);
                    _xMouseUp = xMouseUp;
                }
                else if (arr2[0] == "yMouseUp")
                {
                    yMouseUp = (int)Math.Round(ToDouble(arr2[1]), 0);
                    _yMouseUp = yMouseUp;
                }
                else if (arr2[0] == "b_mouseDown")
                {
                    b_mouseDown = bool.Parse(arr2[1]);
                    _b_mouseDown = b_mouseDown;
                }
                else if (arr2[0] == "b_clickDone")
                {
                    b_clickDone = bool.Parse(arr2[1]);
                    _b_clickDone = b_clickDone;
                }
            }
        }
    }
}
