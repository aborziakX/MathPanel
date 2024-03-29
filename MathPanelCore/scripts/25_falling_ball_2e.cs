﻿//test25_falling_ball_2d
/*we add the action of the air resistance force. 
It is approximately proportional to the square of the speed and always against the velocity.
*/

            const double G = 9.8; //acceleration of gravity
            const double DT = 0.050; //time step in seconds
            const double RADIUS = 0.5; //ball radius
            Dynamo.Console("test25_falling_ball_2d");
            //регистрация на сервере
            Dynamo.Scriplet("test25_falling_ball_2e", "Падающий мяч 2e");

            //ball's center initial height
            double y_0 = 100;
            //ground position
            double yGround = 0;
            //ball position
            double y = y_0;
            //ball velocity
            double v = 0;
            //number of points
            const int NPOINTS = 1000;
            bool bGraph = true;// true;false
            double dAirRes = 0.0003; //air resistance coefficient
            int iTotalRes = 0;

            //time - x-axis
            double[] dTime = new double[NPOINTS];
            //height - y-axis
            double[] dHeight = new double[NPOINTS];
            //define drawing optional parameters: use small green circles in canvas 800 by 600
            string sOpt = "{\"options\":{\"x0\": 0, \"x1\": " + NPOINTS + ", \"y0\": 0, \"y1\": 100, \"clr\": \"#00ff00\", \"sty\": \"circle\", \"size\":2, \"lnw\": 2, \"wid\": 800, \"hei\": 600 }";
            string s = "{{\"x\":{0},\"y\":{1}}}";

            for (int i = 0; i < NPOINTS; i++)
            {
                System.Threading.Thread.Sleep(50);
                string resp = Dynamo.KeyConsole;
                if (resp == "Q")
                {
                    break;
                }
                if ( i % 20 == 0 )
                    Dynamo.Console(string.Format("y={0}, v={1}", y, v));

                y += v * DT; //a ball is moving
                if (y - RADIUS < yGround)
                {   //groun hit, reverse
                    Dynamo.Console("hit");
                    y = yGround + RADIUS;
                    v = -v;
                }
                v -= G * DT;//apply the acceleration of gravity
                //air resistance
                if (v > 0) v -= dAirRes * v * v;
                else v += dAirRes * v * v;

                //save
                dTime[i] = bGraph ? i : NPOINTS/2;
                dHeight[i] = y;

                //prepare to draw
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                if (bGraph)
                {
                    for (int j = 0; j <= i; j++)
                    {
                        if (j != 0) sb.Append(",");
                        sb.AppendFormat(s, Dynamo.D2S(dTime[j]), Dynamo.D2S(dHeight[j]));
                    }
                }
                else sb.AppendFormat(s, Dynamo.D2S(dTime[i]), Dynamo.D2S(dHeight[i]));//current position only

                string data = sb.ToString();

                //display
                var sJson = sOpt + ", \"data\":[" + data + "]}";
                Dynamo.SceneJson(sJson);
                if (i % 5 == 0 && iTotalRes < 100)
                {   //сохранить изображение на сервере
                    Dynamo.SaveScripresult();
                    iTotalRes++;
                }
            }
