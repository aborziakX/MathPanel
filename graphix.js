//2020, Andrei Borziak
//простой модуль для рисования графиков на "холсте", где левый верхний пиксель (0, 0)
if (typeof GRAPHIX == "undefined") {
    GRAPHIX = {
        PADDING: 20,        //отступ
        AX_BG: "#000000",   //фон
        AX_CL: "#00FF00",   //цвет осей
        borders: 3, //побитовое ИЛИ: 1-горизонтальная граница, 2-вертикальная, 4-горизонтальная сверху, 8-вертикальная сверху
        displaySubzero: false, //если размер элемента меньше 1, то при displaySubzero == false,  размер будет 0, иначе 1

        //залить фон, нарисовать оси
        drawAxes: function(canvas) {
            var cnv = document.getElementById(canvas);//найти canvas по id
            var w = cnv.width;//ширина холста
            var h = cnv.height;//высота холста
            var ctx = cnv.getContext('2d');//найти котекст
            //залить фон
            ctx.fillStyle = this.AX_BG;
            ctx.fillRect(0, 0, w, h);
            ctx.strokeStyle = this.AX_CL;
            if (this.borders == 0) return;
            //границы
            ctx.lineWidth = 1;
            ctx.beginPath();
            //вертикальная
            if( (this.borders & 0x2) > 0 ) {
                ctx.moveTo(this.PADDING, this.PADDING);
                ctx.lineTo(this.PADDING, h - this.PADDING);
                ctx.stroke();
            }
            //горизонтальная
            if( (this.borders & 0x1) > 0 ) {
                ctx.moveTo(this.PADDING, h - this.PADDING);
                ctx.lineTo(w - this.PADDING, h - this.PADDING);
                ctx.stroke();
            }
            //вертикальная сверху
            if( (this.borders & 0x8) > 0 ) {
                ctx.moveTo(w - this.PADDING, this.PADDING);
                ctx.lineTo(w - this.PADDING, h - this.PADDING);
                ctx.stroke();
            }
            //горизонтальная сверху
            if( (this.borders & 0x4) > 0 ) {
                ctx.moveTo(this.PADDING, this.PADDING);
                ctx.lineTo(w - this.PADDING, this.PADDING);
                ctx.stroke();
            }
        },

        //отобразить данные
        //canvas - id html-элемента, data - двухмерный массив с данными, opt - дополнительные параметры
        drawData: function(canvas, data, opt) {
            var cnv = document.getElementById(canvas);//найти canvas по id
            var w = cnv.width - 2 * this.PADDING;//ширина без отступа
            var h = cnv.height - 2 * this.PADDING;//высота без отступа
            var x0 = opt.x0;    //минимальное значение по X-оси
            var x1 = opt.x1;    //максимальное значение по X-оси
            var y0 = opt.y0;    //минимальное значение по Y-оси
            var y1 = opt.y1;    //максимальное значение по Y-оси
            var dx = (x1 - x0); //диапазон по X
            var dy = (y1 - y0); //диапазон по Y

            var sz = "" + opt.size; //размер
            if (sz == "undefined") sz = 1;
            else sz = sz * 1;

            var clr = "" + opt.clr;//основной цвет
            if (clr == "undefined") clr = "#ff0000";

            var colorstroke = "" + opt.csk;//цвет для линий
            if (colorstroke == "undefined") colorstroke = clr;

            var linewidth = "" + opt.lnw;//ширина линий
            if (linewidth == "undefined") linewidth = 1;
            else linewidth = linewidth * 1;
            
            var sz_old;
            var style = opt.sty;//стиль
            var bLine = false;//признак рисования линии
            var height = 10;
            var text = "";

            var fromzero = "" + opt.fromzero;//1-от оси X
            if (fromzero == "undefined") fromzero = 0;

            var textonly = "" + opt.textonly;//1-рисовать только текст
            if (textonly == "undefined") textonly = 0; 
            
            var textdown = "" + opt.textdown;//1-текст под столбиком
            if (textdown == "undefined") textdown = 0; 

            var fontsize = "" + opt.fontsize;//размер шрифта
            if (fontsize == "undefined") fontsize = 10; 

            var textonbottom = "" + opt.textonbottom;//1-текст снизу оси X
            if (textonbottom == "undefined") textonbottom = 0;
 
            var ctx = cnv.getContext('2d');//найти котекст
            ctx.beginPath();
            //заполнить контекст шириной и цветами
            ctx.lineWidth = linewidth;
            ctx.strokeStyle = colorstroke;
            ctx.fillStyle = clr;
            ctx.font = fontsize + "px Verdana";

            var sqrt2_2 = Math.sqrt(2) * 0.3;
            var m0;

            var bRestoreSize = false, bRestoreColor = false, bRestoreStyle = false, bRestoreLw = false, bRestoreColorstroke = false, bRestoreFont = false;
            //проход по данным
            for (var i = 0; i < data.length; i++) {
                var r = data[i];//каждый элемент тоже массив
                var x = r[0];
                var y = r[1];
                if( r.length > 2 && r[2] != null ) {//задан размер
                    bRestoreSize = true;
                    sz_old = sz;
                    sz = r[2];
                }
                if (r.length > 3 && r[3] != null) {//задан цвет заполнения
                    bRestoreColor = true;
                    ctx.fillStyle = r[3];
                }
                if (r.length > 4 && r[4] != null) {//задан стиль
                    bRestoreStyle = true;
                    style = r[4];
                }
                if (r.length > 5 && r[5] != null) {//задана высота гистограммы
                    height = r[5];
                }
                if (r.length > 6 && r[6] != null) {//задан текст
                    text = r[6];
                }
                else text = "";

                if (r.length > 7 && r[7] != null) {//задана ширина линий
                    ctx.lineWidth = r[7];
                    bRestoreLw = true;
                }
                if (r.length > 8 && r[8] != null) {//задан цвет линий
                    ctx.strokeStyle = r[8];
                    bRestoreColorstroke = true;
                }
                if (r.length > 9 && r[9] != null) {//задан размер шрифта
                    ctx.font = r[9] + "px Verdana";
                    bRestoreFont = true;
                }

                //найти позицию для элемента данных
                var l = Math.round((w * (x - x0)) / dx + this.PADDING);
                var m = Math.round((h - (h * (y - y0)) / dy) + this.PADDING);
                //применить стиль
                if (style == "dots") {//прямоугольники
                    ctx.fillRect(l-sz/2, m-sz/2, sz, sz);
                    if (text != "") ctx.fillText(text, l, m - 10);
                }
                else if (style == "circle") {//окружности
                    ctx.beginPath();
                    ctx.arc(l, m, sz, 0, 2 * Math.PI);
                    ctx.stroke();
                    ctx.fill();
                    if (text != "") ctx.fillText(text, l, m - 10);
                }
                else if (style == "hist") {//гистограмма
                    m0 = h + this.PADDING;
                    if (fromzero == 0) {
                        ctx.moveTo(l, m0);
                    } else {
                        m0 = Math.round((h - (h * (0 - y0)) / dy) + this.PADDING);
                        ctx.moveTo(l, m0);
                    }
                    if (m == m0) m = m0 - 1;
                    ctx.lineTo(l, m);
                    ctx.stroke();
                    if (text != "") {
                        if (textonbottom == 0)
                            ctx.fillText(text, l, m - 10);
                        else ctx.fillText(text, l, m0 + fontsize * 1);
                    }
                }
                else if (style == "hist_3") {//гистограмма 3Д
                    if (textonly != 1) {
                        ctx.beginPath();
                        if (fromzero == 0) {
                            m0 = h + this.PADDING;
                            ctx.moveTo(l, m0);
                        } else {
                            m0 = Math.round((h - (h * (0 - y0)) / dy) + this.PADDING);
                            ctx.moveTo(l, m0);
                        }
                        ctx.lineTo(l, m);
                        ctx.lineTo(l + sz, m);
                        ctx.lineTo(l + sz, m0);
                        ctx.closePath();
                        ctx.fill();
                        ctx.stroke();

                        ctx.beginPath();
                        ctx.moveTo(l, m);
                        ctx.lineTo(l + sz, m);
                        ctx.lineTo(l + sz + sz * sqrt2_2, m - sz * sqrt2_2);
                        ctx.lineTo(l + sz * sqrt2_2, m - sz * sqrt2_2);
                        ctx.closePath();
                        ctx.fill();
                        ctx.stroke();

                        ctx.beginPath();
                        ctx.moveTo(l + sz, m);
                        ctx.lineTo(l + sz + sz * sqrt2_2, m - sz * sqrt2_2);
                        ctx.lineTo(l + sz + sz * sqrt2_2, m0 - sz * sqrt2_2);
                        ctx.lineTo(l + sz, m0);
                        ctx.closePath();
                        ctx.fill();
                        ctx.stroke();
                    }
                    if (text != "") ctx.fillText(text, l, textdown != 1 ? m - 10 : m0 + (10 + (i % 4) * 12));
                    //if (i == 0) alert("l=" + l + ", m0=" + m0 + ", m=" + m + ", sz=" + sz + ", l2=" + (l + sz));
                }
                else if (style == "histmap") {//прямоугольник из точки данных, размер height
                    ctx.moveTo(l, m);
                    ctx.lineTo(l, m - height);
                    ctx.stroke();
                    if( text != "" ) ctx.fillText(text, l, m - height - 10);
                }
                else if (style == "line" || style == "line_end" || style == "line_endf") {//линия или конец линии
                    if (!bLine) {
                        ctx.beginPath();
                        ctx.moveTo(l, m);
                        bLine = true;
                    }
                    else {
                        ctx.lineTo(l, m);
                    }
                    if (style == "line_end" || style == "line_endf") {//конец линии
                        bLine = false;
                        if( style == "line_endf") ctx.fill();//залить путь
                        //else 
                        ctx.stroke();
                    }                  
                    if (text != "") ctx.fillText(text, l, m - 10);
                }
                else if (style == "text") {
                    if (text != "") {
                        if (textonbottom == 0)
                            ctx.fillText(text, l, m - fontsize * 1);//текст над точкой
                        else ctx.fillText(text, l, m + fontsize * 1);//текст под точкой
                    }
                }
                //восстановить значения по умолчанию
                if(bRestoreSize) {
                    bRestoreSize = false;
                    sz = sz_old;
                }
                if(bRestoreColor) {
                    bRestoreColor = false;
                    ctx.fillStyle = clr;
                }
                if(bRestoreStyle) {
                    bRestoreStyle = false;
                    style = opt.sty;
                }                
                if(bRestoreLw) {
                    bRestoreLw = false;
                    ctx.lineWidth = linewidth;
                }
                if (bRestoreColorstroke) {
                    bRestoreColorstroke = false;
                    ctx.strokeStyle = colorstroke;
                }
                if (bRestoreFont) {
                    bRestoreFont = false;
                    ctx.font = fontsize + "px Verdana";
                }
            }
            if (bLine) ctx.stroke();
            //alert(bLine);
        },

        //отобразить текст
        drawText: function(canvas, title, x, y, color, size) {
            var cnv = document.getElementById(canvas);
            var ctx = cnv.getContext('2d');
            ctx.fillStyle = color;
            ctx.font = size + "px Verdana";
            ctx.fillText(title, x, y);
        },

        //отобразить картинку
        drawImage: function(canvas, imgid, x, y) {
            var cnv = document.getElementById(canvas);
            var ctx = cnv.getContext('2d');
            var img = document.getElementById(imgid);
            if( img != null ) ctx.drawImage(img, x, y);
        },
        
        //отобразить json-string
        drawJson: function (canvas, w) {
            //преобразовать из строки в объект
            if ("" + JSON != "undefined") s = JSON.parse(w);
            else if ("" + jQuery != "undefined") s = jQuery.parseJSON(w);
            else s = eval(w);
            var dpa = new Date().getTime();
            this.drawJsonObj(canvas, s); //отобразить json-object
            return dpa;
        },

        //отобразить json-array
        drawJsonArr: function (canvas, sArr) {
            //var sArr = JSON.parse(w);//eval(w);
            var dpa = new Date().getTime();
            for (var i = 0; i < sArr.length; i++) {
                if (i > 0) sArr[i]["options"]["second"] = true;
                this.drawJsonObj(canvas, sArr[i]); //отобразить json-object
            }
            return dpa;
        },

        //отобразить json-object
        drawJsonObj: function (canvas, s) {
            var cnv = document.getElementById(canvas);
            var ctx = cnv.getContext('2d');
            //alert(s + ",o3=" + s["options"]["x0"] + ",o2=" + s["data"][0]["x"]);
            var opt = s["options"];

            //установить размеры холста
            width_x = (opt["wid"]);
            height_x = (opt["hei"]);
            if (cnv.width != width_x) {
                cnv.width = width_x;
            }
            if (cnv.height != height_x) {
                cnv.height = height_x;
            }
            //определить сжатие
            var scale = cnv.width / (opt["x1"] - opt["x0"]);
            //создать пустой массив
            var dd = new Array();
            for (var i = 0; i < s["data"].length; i++) {
                var row = s["data"][i];//это массив
                var x = row["x"];//позиция по горизонтали
                var y = row["y"];//позиция по вертикали
                //размер точки
                var sz = ("" + row["rad"]);
                if( sz == "undefined" ) sz = null;
                else {
                    sz = sz * scale;
                    if (sz < 1) sz = this.displaySubzero ? 1 : 0;
                }
                //цвет
                var clr = ("" + row["clr"]);
                if( clr == "undefined" ) clr = null;
                //стиль
                var style = ("" + row["sty"]);
                if (style == "undefined") style = null;
                //высота гистограммы
                var height = ("" + row["hei"]);
                if (height == "undefined") height = null;
                //текст
                var text = ("" + row["txt"]);
                if (text == "undefined") text = null;
                //ширина линии
                var linewidth = ("" + row["lnw"]);
                if (linewidth == "undefined") linewidth = null;
                //цвет линии
                var colorstroke = ("" + row["csk"]);
                if (colorstroke == "undefined") colorstroke = null;
                //размер шрифта
                var fontsize = ("" + row["fontsize"]);
                if (fontsize == "undefined") fontsize = null;
                //запихнуть описание точки в массив
                dd.push(new Array(x, y, sz, clr, style, height, text, linewidth, colorstroke, fontsize));
            }
            //фон для холста?
            if( ("" + opt["bg"]) != "undefined" ) {
                this.AX_BG = opt["bg"];
            }
            //рисовать поверх?
            if( ("" + opt["second"]) == "undefined" ) {
                //рисовать фон и границы    
                this.AX_CL = "#ffff00";
                this.PADDING = 5;
                this.borders = 0;//15;
                this.drawAxes(canvas);
            }
            //задать картинку?
            if (("" + opt["img"]) != "undefined") {
                var obj = document.getElementById("img1");
                if (obj != null) try {
                    obj.src = opt["img"];
                    this.drawImage(canvas, "img1", 0, 0);
                } catch (e) {  };
            }
            //отрисовать данные
            this.drawData(canvas, dd, opt);
        },

        dumb:0
    }
}