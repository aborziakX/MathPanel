
            Dynamo.Console("nakop02.cs");
            DateTime dt1 = DateTime.Now;
            int i, k, m, n;
            double volNak = 5;  // объем накопителя
            double powerNak = 1;// мощность накопителя
            int maxLoadSteps = (int)(volNak / powerNak); //за сколько шагов загрузим накопитель
            double[] loadNak = new double[24+1];//в накопителе в начале часа
            for (i = 0; i < loadNak.Length; i++) loadNak[i] = 0;
            //??not in use
            double[] loadSched = new double[24];//загружаем в накопитель в течении часа
            for (i = 0; i < loadSched.Length; i++) loadSched[i] = 0;

            //потери - берем powerNak, отдаем powerNak * (1 - dLoss) //??
            //?? добавить ограничение на число циклов

            double thresh = 0.1;    //порог
            //почасовое потребление
            double[] volumes = { 1, 1, 1, 1, 1, 2, 1, 1, 2, 3, 4, 2, 1, 1, 1, 0, 1, 2, 1, 0, 0, 0, 1, 1 };
            //почасовые цены
            double[] prices = { 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 1, 1, 1, 1, 2, 2, 1, 1, 1, 1, 1, 1 };
            if(loadNak.Length-1 != prices.Length || loadNak.Length-1 != volumes.Length)
            {
                Dynamo.Console("ошибка в размерности параметров!");
                return;
            }
            //находим средне взвешенную цену 
            double vol_sum = 0, tot = 0;
            for (i = 0; i < prices.Length; i++)
            {
                vol_sum += volumes[i];
                tot += prices[i] * volumes[i];
            }
            double aver_price = tot / vol_sum;
            Dynamo.Console("total_vol=" + vol_sum);
            Dynamo.Console("total_pay=" + tot);
            Dynamo.Console("aver_price=" + aver_price);

            //определяем начало отрицательных зон
            int nNegative = 0;
            int[] negStart = new int[24];
            int iNow = 0;
            for (i = 0; i < prices.Length; i++)
            {
                if (prices[i] > aver_price + thresh)
                {   //вышли в позитив
                    iNow = 1;
                }
                else if (iNow >= 0 && prices[i] < aver_price - thresh)
                {   //вышли в негатив
                    iNow = -1;
                    negStart[nNegative++] = i;
                }
            }
            if (nNegative == 0)
            {
                Dynamo.Console("серая зона");
                return;
            }
            for (i = 0; i < nNegative; i++) Dynamo.Console("neg=" + negStart[i]);

            //число активных точек - для статистики
            int iPlus = 0, iMinus = 0;
            for (i = 0; i < prices.Length; i++)
            {
                if (prices[i] > aver_price + thresh)
                {   //вышли в позитив
                    iPlus++;
                }
                else if (prices[i] < aver_price - thresh)
                {   //вышли в негатив
                    iMinus++;
                }
            }

            int iShiftBest = -1;
            double dTotolBestShift = double.MaxValue;   //лучшее на всех сдвигах
            double dTotolBest = double.MaxValue;
            int[] hoursBestShift = new int[24];
            int[] hoursBest = new int[24];
            int[] hours = new int[24];
            for (i = 0; i < hoursBestShift.Length; i++) hoursBestShift[i] = 0;

            for (n = 0; n < nNegative; n++)
            {   //перебор по всем отрицательным диапазонам
                int iShift = negStart[n];
                Dynamo.Console("iShift=" + iShift);

                //т.к. у нас цикличность по дням, делаем сдвиг влево цен и потребления, чтобы все начиналось с отрицательного диапазона
                for (k = 0; k < iShift; k++)
                {
                    double d = prices[0];
                    for (i = 1; i < prices.Length; i++) prices[i - 1] = prices[i];
                    prices[prices.Length - 1] = d;

                    d = volumes[0];
                    for (i = 1; i < volumes.Length; i++) volumes[i - 1] = volumes[i];
                    volumes[volumes.Length - 1] = d;
                }

                //предполагаем объем в накопителе нуль на начало отрицательного цикла
                //генерируем варианты: 0-не загружать, 1-загружать, -1 - выгружать
                for (i = 0; i < hoursBest.Length; i++) hoursBest[i] = 0;
                for (i = 0; i < hours.Length; i++) hours[i] = 0;

                int curNode = 0;
                int nIter = 0;
                while (curNode <= 23)
                {
                    //сгенерировать новый профиль загрузки
                    bool bNew = false;
                    while (!bNew)
                    {
                        if (hours[curNode] == 0)
                        {
                            if (prices[curNode] < aver_price - thresh)
                            {
                                hours[curNode] = 1;
                                bNew = true;
                                for (i = 0; i < curNode; i++) hours[i] = 0;
                                curNode = 0;
                            }
                            else if (prices[curNode] > aver_price + thresh)
                            {
                                hours[curNode] = -1;
                                bNew = true;
                                for (i = 0; i < curNode; i++) hours[i] = 0;
                                curNode = 0;
                            }
                        }

                        if (!bNew)
                        {
                            curNode++;
                            if (curNode > 23) break;
                        }
                    }
                    if (nIter % 100000 == 0)
                    {
                        string w = "";
                        for (m = 0; m < hours.Length; m++) w += hours[m].ToString();
                        Dynamo.Console("curNode=" + curNode + ", " + w);
                    }
                    //сравнить с ранее оптимальным
                    tot = 0;
                    int balance = 0;
                    for (i = 0; i < hours.Length; i++)
                    {
                        double extra = 0;
                        if (hours[i] == 1)
                        {
                            if (balance >= maxLoadSteps)
                            {   //нельзя превышать емкость накопителя
                                break;
                            }
                            extra = powerNak;
                            balance++;
                        }
                        else if (hours[i] == -1)
                        {
                            if (balance <= 0)
                            {   //нельзя иметь отрицательную загрузку накопителя
                                balance = -1;
                                break;
                            }
                            extra = -powerNak;
                            balance--;
                        }
                        tot += prices[i] * (volumes[i] + extra);
                    }
                    if (balance == 0 && dTotolBest > tot)
                    {
                        for (i = 0; i < hoursBest.Length; i++) hoursBest[i] = hours[i];
                        dTotolBest = tot;
                        Dynamo.Console("new best=" + tot);
                    }
                    nIter++;
                    //if (nIter > 1000000) break;
                }

                //проверить на ненулевой накопитель, снизить загрузку
                tot = 0;
                for (i = 0; i < hoursBest.Length; i++)
                {
                    double extra = 0;
                    if (hoursBest[i] == 1) extra = powerNak;
                    else if (hoursBest[i] == -1) extra = -powerNak;
                    tot += prices[i] * (volumes[i] + extra);
                    Dynamo.Console(i + ", extra=" + extra);
                }
                Dynamo.Console("tot2=" + tot + ", dTotolBest=" + dTotolBest + ", nIter=" + nIter + ", iPlus=" + iPlus + ", iMinus=" + iMinus);
                DateTime dt2 = DateTime.Now;
                Dynamo.Console("done in " + (dt2 - dt1).TotalMilliseconds + "ms");

                //делаем сдвиг вправо цен и потребления, чтобы все начиналось как было
                for (k = 0; k < iShift; k++)
                {
                    double d = prices[prices.Length - 1];
                    for (i = prices.Length - 1; i > 0; i--) prices[i] = prices[i - 1];
                    prices[0] = d;

                    d = volumes[volumes.Length - 1];
                    for (i = volumes.Length - 1; i > 0; i--) volumes[i] = volumes[i - 1];
                    volumes[0] = d;

                    m = hoursBest[hoursBest.Length - 1];
                    for (i = hoursBest.Length - 1; i > 0; i--) hoursBest[i] = hoursBest[i - 1];
                    hoursBest[0] = m;
                }

                if(dTotolBestShift > dTotolBest)
                {
                    dTotolBestShift = dTotolBest;
                    iShiftBest = iShift;
                    for (i = 0; i < hoursBest.Length; i++) hoursBestShift[i] = hoursBest[i];
                }
            }
            Dynamo.Console("dTotolBestShift=" + dTotolBestShift + ", iShiftBest=" + iShiftBest);

            //правая часть дневного цикла
            loadNak[iShiftBest] = 0;
            for (i = iShiftBest; i < hoursBestShift.Length; i++)
            {
                double extra = 0;
                if (hoursBestShift[i] == 1) extra = powerNak;
                else if (hoursBestShift[i] == -1) extra = -powerNak;
                tot += prices[i] * (volumes[i] + extra);
                //Dynamo.Console(i + ", loadNak=" + loadNak[i] + ", extra=" + extra);
                loadNak[i + 1] = loadNak[i] + extra;
            }

            //левая часть дневного цикла
            loadNak[0] = loadNak[loadNak.Length - 1];
            for (i = 0; i < iShiftBest; i++)
            {
                double extra = 0;
                if (hoursBestShift[i] == 1) extra = powerNak;
                else if (hoursBestShift[i] == -1) extra = -powerNak;
                tot += prices[i] * (volumes[i] + extra);
                //Dynamo.Console(i + ", loadNak=" + loadNak[i] + ", extra=" + extra);
                loadNak[i + 1] = loadNak[i] + extra;
            }
            for (i = 0; i < loadNak.Length; i++) Dynamo.Console(i + ", nak=" + loadNak[i]);

            //рисуем
            double shift_x = 0.3;
            double shift_y = 0.1;
            //потребление
            string s = MathPanelExt.QuadroEqu.DrawText(0, 5, "потребление");
            for (i = 0; i < volumes.Length; i++)
            {
                if (s != "") s += ",";
                s += MathPanelExt.QuadroEqu.DrawLine(i, volumes[i], i + 1, volumes[i]);
            }
            string s10 = "{\"options\":{\"x0\": -0, \"x1\": 26, \"y0\": -0.5, \"y1\": 5.5, \"clr\": \"#ffff00\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 1000, \"hei\": 750, \"_second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            //цены
            s = MathPanelExt.QuadroEqu.DrawText(0, 4.5, "цены");
            for (i = 0; i < prices.Length; i++)
            {
                if (s != "") s += ",";
                s += MathPanelExt.QuadroEqu.DrawLine(shift_x + i, shift_y + prices[i], shift_x + i + 1, shift_y + prices[i]);
            }
            s10 = "{\"options\":{\"x0\": -0, \"x1\": 26, \"y0\": -0.5, \"y1\": 5.5, \"clr\": \"#00ff00\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 1000, \"hei\": 750, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            //средне взвешенная цена
            s = MathPanelExt.QuadroEqu.DrawText(5, 4.5, "средне взвешенная цена");
            s += ",";
            s += MathPanelExt.QuadroEqu.DrawLine(0, aver_price, 24, aver_price);
            s10 = "{\"options\":{\"x0\": -0, \"x1\": 26, \"y0\": -0.5, \"y1\": 5.5, \"clr\": \"#ff0000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 1000, \"hei\": 750, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            //загрузка накопителя
            s = MathPanelExt.QuadroEqu.DrawText(5, 5, "загрузка накопителя");
            for (i = 0; i < loadNak.Length; i++)
            {
                if (s != "") s += ",";
                s += MathPanelExt.QuadroEqu.DrawPoint(shift_x * 1 + i, shift_y * 1 + loadNak[i], "", "line");
            }
            s10 = "{\"options\":{\"x0\": -0, \"x1\": 26, \"y0\": -0.5, \"y1\": 5.5, \"clr\": \"#ff00ff\", \"sty\": \"hist\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"fromzero\":1, \"textonbottom\":1, \"wid\": 1000, \"hei\": 750, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            //часы
            s = "";
            for (i = 0; i < 25; i++)
            {
                if (s != "") s += ",";
                s += MathPanelExt.QuadroEqu.DrawPoint(shift_x * 1 + i, 0, i.ToString(), "text");
            }
            s10 = "{\"options\":{\"x0\": -0, \"x1\": 26, \"y0\": -0.5, \"y1\": 5.5, \"clr\": \"#ffffff\", \"sty\": \"text\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"fromzero\":1, \"textonbottom\":1, \"wid\": 1000, \"hei\": 750, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            //Dynamo.Console(Dynamo.ScreenJson);


