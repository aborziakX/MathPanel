using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

namespace MathPanel
{
    /// <summary>
    /// 2-х мерное изображение, ARGB в карте слева направо и сверху вниз
    /// </summary>
    public class BitmapSimple
    {
        public int width, height;
        public int[] map;
        /// <summary>
        /// 2-х мерное изображение
        /// </summary>
        /// <param name="w">ширина</param>
        /// <param name="h">высота</param>
        /// <param name="colors">цвета повторяющиеся</param>
        public BitmapSimple(int w, int h, System.Drawing.Color[] colors)
        {
            /* The Color Structure represents colors in terms of alpha, red, green, and blue (ARGB) channels. 
            The color of each pixel is represented as a 32-bit number: 8 bits each for the alpha, red, green, and blue channels (ARGB). 
            Each of the four components is a number from 0 to 255, where 0 means no intensity and 255 represents full intensity. 
            The alpha component sets the color transparency: 0 is fully transparent, and 255 is fully opaque. 
            To define the alpha, red, green, or blue component of a color, use the A, R, G, or B property, respectively. 
            You can create a custom color using one of the FromArgb() methods.
            The ToArgb() method returns the 32-bit ARGB value of this Color structure.
            */
            width = w;
            height = h;
            map = new int[width * height];
            for (int i = 0; i < width * height; i++)
            {
                map[i] = colors[i % colors.Length].ToArgb();
            }
        }

        /// <summary>
        /// 2-х мерное изображение как паттерн
        /// </summary>
        /// <param name="w">ширина</param>
        /// <param name="h">высота</param>
        /// <param name="colors">цвета повторяющиеся для битов 0 и 1</param>
        /// <param name="patterns">массив байтов, каждый управляет своим слоем в 8 бит</param>
        public BitmapSimple(int w, int h, System.Drawing.Color[] colors, byte [] patterns)
        {
            width = w;
            height = h;
            map = new int[width * height];
            if(colors == null || colors.Length < 2)
            {
                for (int i = 0; i < width * height; i++)
                {
                    map[i] = System.Drawing.Color.Black.ToArgb();
                }
                return;
            }
            for (int y = 0; y < height; y++)
            {
                var bt = patterns[y % patterns.Length];
                for (int x = 0; x < width; x++)
                {
                    int k = y * width + x;
                    int n = x % 8;
                    int bit = (bt >> n) & 0x1;
                    map[k] = colors[bit % colors.Length].ToArgb();
                }
            }
        }

        /// <summary>
        /// 2-х мерное изображение, с градиентом
        /// </summary>
        /// <param name="w">ширина</param>
        /// <param name="h">высота</param>
        /// <param name="col1">цвет сверху (слева)</param>
        /// <param name="col2">цвет снизу (справа)</param>
        /// <param name="bVert">true сверху (col1) вниз (col2), else слева направо</param>
        //gradient, bVert ? vertical : horizontal
        public BitmapSimple(int w, int h, System.Drawing.Color col1, System.Drawing.Color col2, bool bVert = true)
        {
            width = w;
            height = h;
            map = new int[width * height];
            int k = 0, alpha, red, green, blue;
            byte a1 = col1.A;
            byte r1 = col1.R;
            byte g1 = col1.G;
            byte b1 = col1.B;
            byte a2 = col2.A;
            byte r2 = col2.R;
            byte g2 = col2.G;
            byte b2 = col2.B;
            if (bVert)
            {
                //If the gradient is vertical, then everything is simple. 
                //For each line of the image, we calculate the ARGB pixel values as a linear relationship
                for (int j = 0; j < height; j++)
                {
                    alpha = a1 + ((a2 - a1) * j) / height;
                    red = r1 + ((r2 - r1) * j) / height;
                    green = g1 + ((g2 - g1) * j) / height;
                    blue = b1 + ((b2 - b1) * j) / height;
                    int argb = System.Drawing.Color.FromArgb(alpha, red, green, blue).ToArgb();
                    for (int i = 0; i < width; i++)
                    {
                        //Write to the map array
                        map[k++] = argb;
                    }
                }
            }
            else
            {
                //If the gradient is horizontal, it is slightly more difficult. 
                //For each column of the image, we calculate the ARGB pixel values. 
                for (int j = 0; j < width; j++)
                {
                    alpha = a1 + ((a2 - a1) * j) / width;
                    red = r1 + ((r2 - r1) * j) / width;
                    green = g1 + ((g2 - g1) * j) / width;
                    blue = b1 + ((b2 - b1) * j) / width;
                    int argb = System.Drawing.Color.FromArgb(alpha, red, green, blue).ToArgb();
                    //Forming a loop over the column elements, calculating the index of the element
                    for (int i = 0; i < height; i++)
                    {
                        map[i * width + j] = argb;
                    }
                }
            }
        }

        /// <summary>
        /// 2-х мерное изображение, с градиентом
        /// </summary>
        /// <param name="w">ширина</param>
        /// <param name="h">высота</param>
        /// <param name="focus"> несколько цветовых фокусов
        public BitmapSimple(int w, int h, Tuple<System.Drawing.Color, int, int>[] focus)
        {
            width = w;
            height = h;
            map = new int[width * height];
            int k = 0, alpha, red, green, blue, i, j, n;
            if (focus == null || focus.Length == 0)
            {
                alpha = 255;
                red = 255;
                green = 255;
                blue = 255;
                int argb = System.Drawing.Color.FromArgb(alpha, red, green, blue).ToArgb();
                for (j = 0; j < height; j++)
                {
                    for (i = 0; i < width; i++)
                    {
                        map[k++] = argb;
                    }
                }
                return;
            }
            double a1, r1, g1, b1;

            double[] dist = new double[focus.Length];

            for (j = 0; j < height; j++)
            {
                for (i = 0; i < width; i++)
                {
                    double total = 0;
                    for (n = 0; n < focus.Length; n++)
                    {
                        Tuple<System.Drawing.Color, int, int> f = focus[n];
                        double d = ((i - f.Item2) * (i - f.Item2) + (j - f.Item3) * (j - f.Item3));
                        dist[n] = 1.0 / (d == 0 ? 1 : d);
                        total += dist[n];
                    }
                    a1 = 0;
                    r1 = 0;
                    g1 = 0;
                    b1 = 0;
                    for (n = 0; n < focus.Length; n++)
                    {
                        Tuple<System.Drawing.Color, int, int> f = focus[n];
                        a1 += f.Item1.A * dist[n];
                        r1 += f.Item1.R * dist[n];
                        g1 += f.Item1.G * dist[n];
                        b1 += f.Item1.B * dist[n];
                    }

                    alpha = SafeColor((int)(a1 / total));
                    red = SafeColor((int)(r1 / total));
                    green = SafeColor((int)(g1 / total));
                    blue = SafeColor((int)(b1 / total));
                    map[k++] = System.Drawing.Color.FromArgb(alpha, red, green, blue).ToArgb();
                }
            }
        }

        /// <summary>
        /// добавить случайности
        /// </summary>
        /// <param name="nNoise">число отклонений</param>
        /// <param name="iNoiceStrenth">сила отклонения</param>
        public void Randomize(int nNoise, int iNoiceStrenth = 2)
        {
            if (nNoise <= 0 || iNoiceStrenth < 1) return;
            int k, alpha, red, green, blue, i;
            //generate noise Using the random number generator
            Random rnd = new Random();
            for (i = 0; i < nNoise; i++)
            {
                //in the loop, we find a random element in the array by the number of variations 
                k = rnd.Next(0, width * height);
                int argb = map[k];
                var clr = System.Drawing.Color.FromArgb(argb);
                //Then we slightly change the values in the channels
                alpha = clr.A;
                red = SafeColor(clr.R + rnd.Next(0, iNoiceStrenth) - iNoiceStrenth / 2);
                green = SafeColor(clr.G + rnd.Next(0, iNoiceStrenth) - iNoiceStrenth / 2);
                blue = SafeColor(clr.B + rnd.Next(0, iNoiceStrenth) - iNoiceStrenth / 2);
                //and save pixels
                map[k] = System.Drawing.Color.FromArgb(alpha, red, green, blue).ToArgb();
            }
        }

        /// <summary>
        /// 2-х мерное изображение из файла
        /// </summary>
        /// <param name="fname">файл изображения</param>
        //from file
        public BitmapSimple(string fname)
        {
            FromFile(fname);
        }

        /// <summary>
        /// 2-х мерное изображение из файла
        /// </summary>
        /// <param name="fname">файл изображения</param>
        //from file
        public void FromFile(string fname)
        {
            /*Creating an imageOrig object of the Image type (an abstract class that outputs Bitmap and Metafile) 
            directly from the file name (there is a necessary method in the class).
            Create an image object of the Bitmap type from this object and release the resource
            */
            Image imageOrig = new Bitmap(fname);
            Bitmap image = new Bitmap(imageOrig);
            imageOrig.Dispose();

            //Find the image size and allocate the required memory
            width = image.Width;
            height = image.Height;
            map = new int[width * height];

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int offset = 0;
            int stride = bmData.Stride;
            //image.LockBits locks the Bitmap object in system memory so that pixels can be read/modified.
            //Getting a refference to the allocated memory block
            IntPtr Scan0 = bmData.Scan0;

            int bBl, bGr, bRd, bAl;
            int nOffset = stride - image.Width * 4;
            int k = 0;
            //Then in a double loop we read the pixel data from the image and copy it to our array map
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    bBl = Marshal.ReadByte(Scan0, offset);
                    bGr = Marshal.ReadByte(Scan0, offset + 1);
                    bRd = Marshal.ReadByte(Scan0, offset + 2);
                    bAl = Marshal.ReadByte(Scan0, offset + 3);
                    map[k++] = System.Drawing.Color.FromArgb(bAl, bRd, bGr, bBl).ToArgb();
                    offset += 4;
                }
                offset += nOffset;
            }
            //unlock the memory block to free up resources
            image.UnlockBits(bmData);
        }

        /// <summary>
        /// 2-х мерное изображение из файла
        /// </summary>
        /// <param name="fname">поток изображения</param>
        //from file
        public void FromStream(Stream fname)
        {
            Image imageOrig = new Bitmap(fname);
            Bitmap image = new Bitmap(imageOrig);
            imageOrig.Dispose();

            width = image.Width;
            height = image.Height;
            map = new int[width * height];

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int offset = 0;
            int stride = bmData.Stride;
            IntPtr Scan0 = bmData.Scan0;

            int bBl, bGr, bRd, bAl;
            int nOffset = stride - image.Width * 4;
            int k = 0;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    bBl = Marshal.ReadByte(Scan0, offset);
                    bGr = Marshal.ReadByte(Scan0, offset + 1);
                    bRd = Marshal.ReadByte(Scan0, offset + 2);
                    bAl = Marshal.ReadByte(Scan0, offset + 3);
                    map[k++] = System.Drawing.Color.FromArgb(bAl, bRd, bGr, bBl).ToArgb();
                    offset += 4;
                }
                offset += nOffset;
            }

            image.UnlockBits(bmData);
        }

        /// <summary>
        /// сохранить 2-х мерное изображение в файл
        /// </summary>
        /// <param name="fname">файл</param>
        public void Save(string fname)
        {
            /*
            Creating an object of the Bitmap type, which is used for working with images defined by pixel data. 
            Encapsulates a GDI + bitmap consisting of pixel data from the graphic image and drawing attributes. 
            Creating a Graphics object that encapsulates the GDI+ drawing surface.
            The GDI+ interface is a General-purpose drawing model for .NET applications. 
            In the .NET environment the GDI+ interface is used in several places, including when sending documents 
            to a printer, displaying graphics in Windows applications, and rendering graphic elements on a web page.
            
            */
            Bitmap image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(image);

            /*
            image.LockBits locks the Bitmap object in system memory so that pixels can be changed.
            Getting a pointer to the allocated memory block
            */
            BitmapData bmData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int offset = 0;
            int stride = bmData.Stride;
            IntPtr Scan0 = bmData.Scan0;

            int nOffset = stride - image.Width * 4;
            int k = 0;
            //Next, in a double loop, we move through our map array, extracting from each element of the structure Color
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    int argb = map[k];
                    var cc = System.Drawing.Color.FromArgb(argb);
                    //Writing colors to memory, increasing the offset
                    Marshal.WriteByte(Scan0, offset, (byte)cc.B);
                    Marshal.WriteByte(Scan0, offset + 1, (byte)cc.G);
                    Marshal.WriteByte(Scan0, offset + 2, (byte)cc.R);
                    Marshal.WriteByte(Scan0, offset + 3, (byte)cc.A);
                    k++;
                    offset += 4;
                }
                offset += nOffset;
            }
            //then we unlock the memory, save it to a PNG file, and free up resources
            image.UnlockBits(bmData);

            g.Flush();
            if (File.Exists(fname)) File.Delete(fname);
            image.Save(fname, fname.IndexOf(".png") > 0 ? System.Drawing.Imaging.ImageFormat.Png :
                 System.Drawing.Imaging.ImageFormat.Jpeg);
            image.Dispose();
        }

        /// <summary>
        /// сохранить 2-х мерное изображение в файл со сжатием
        /// </summary>
        /// <param name="fname">файл</param>
        /// <param name="dSqueeze">сжатие</param>
        public void Save(string fname, double dSqueeze)
        {
            if (dSqueeze < 1) dSqueeze = 1;
            int w = (int)(width / dSqueeze);
            int h = (int)(height / dSqueeze);
            int rad = (int)Math.Round(dSqueeze);
            Bitmap image = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(image);

            BitmapData bmData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int offset = 0;
            int stride = bmData.Stride;
            IntPtr Scan0 = bmData.Scan0;

            int nOffset = stride - image.Width * 4;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    //average from neighboors
                    int y_0 = (int)Math.Floor(y * dSqueeze);
                    int x_0 = (int)Math.Floor(x * dSqueeze);
                    long n = 0, al = 0, bl = 0, gr = 0, rd = 0;
                    for (int x1 = x_0; x1 < x_0 + rad; x1++)
                    {
                        if (x1 >= 0 && x1 < width)
                        {
                            for (int y1 = y_0; y1 < y_0 + rad; y1++)
                            {
                                if (y1 >= 0 && y1 < height)
                                {
                                    int c = map[x1 + y1 * width];
                                    bl += (c & 0xFF);
                                    gr += ((c >> 8) & 0xFF);
                                    rd += ((c >> 16) & 0xFF);
                                    al += ((c >> 24) & 0xFF);
                                    n++;
                                }
                            }
                        }
                    }
                    if (n > 0)
                    {
                        al /= n;
                        bl /= n;
                        gr /= n;
                        rd /= n;
                    }
                    var cc = System.Drawing.Color.FromArgb((int)al, (int)rd, (int)gr, (int)bl);

                    Marshal.WriteByte(Scan0, offset, (byte)cc.B);
                    Marshal.WriteByte(Scan0, offset + 1, (byte)cc.G);
                    Marshal.WriteByte(Scan0, offset + 2, (byte)cc.R);
                    Marshal.WriteByte(Scan0, offset + 3, (byte)cc.A);
                    offset += 4;
                }
                offset += nOffset;
            }

            image.UnlockBits(bmData);

            g.Flush();
            if (File.Exists(fname)) File.Delete(fname);
            image.Save(fname, fname.IndexOf(".png") > 0 ? System.Drawing.Imaging.ImageFormat.Png :
                 System.Drawing.Imaging.ImageFormat.Jpeg);
            image.Dispose();
        }

        /// <summary>
        /// сохранить 2-х мерное изображение в поток
        /// </summary>
        /// <param name="fname">поток</param>
        public void Save(Stream fname)
        {
            Bitmap image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(image);

            BitmapData bmData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int offset = 0;
            int stride = bmData.Stride;
            IntPtr Scan0 = bmData.Scan0;

            int nOffset = stride - image.Width * 4;
            int k = 0;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    int argb = map[k];
                    var cc = System.Drawing.Color.FromArgb(argb);
                    Marshal.WriteByte(Scan0, offset, (byte)cc.B);
                    Marshal.WriteByte(Scan0, offset + 1, (byte)cc.G);
                    Marshal.WriteByte(Scan0, offset + 2, (byte)cc.R);
                    Marshal.WriteByte(Scan0, offset + 3, (byte)cc.A);
                    k++;
                    offset += 4;
                }
                offset += nOffset;
            }

            image.UnlockBits(bmData);

            g.Flush();
            image.Save(fname, System.Drawing.Imaging.ImageFormat.Png);
            image.Dispose();
        }

        /// <summary>
        /// капля
        /// </summary>
        /// <param name="clr">цвет капли</param>
        /// <param name="x_0">позиция по горизонтали</param>
        /// <param name="y_0">позиция по вертикали</param>
        /// <param name="radius">радиус капли</param>
        /// <param name="dInitForce">начальная сила капли</param>
        public void Drop(System.Drawing.Color clr, int x_0, int y_0, int radius, double dInitForce)
        {
            if (radius <= 0 || x_0 < 0 || x_0 >= width || y_0 < 0 || y_0 >= height) return;
            if (dInitForce <= 0 || dInitForce > 1) dInitForce = 1;
            int k, i, j, x, y;
            double step = dInitForce / radius;
            for (i = 0; i <= radius; i++)
            {
                //top from left to right
                x = x_0 - i;
                y = y_0 - i;
                for (j = 0; j <= 2 * i; j++)
                {
                    if (x < 0 || x >= width || y < 0 || y >= height) { }
                    else
                    {
                        k = y * width + x;
                        int argb = map[k];
                        var clr_orig = System.Drawing.Color.FromArgb(argb);
                        map[k] = Merge(clr_orig, clr, dInitForce);
                    }
                    x++;
                }
                //down from left to right
                x = x_0 - i;
                y = y_0 + i;
                for (j = 0; j <= 2 * i; j++)
                {
                    if (x < 0 || x >= width || y < 0 || y >= height) { }
                    else
                    {
                        k = y * width + x;
                        int argb = map[k];
                        var clr_orig = System.Drawing.Color.FromArgb(argb);
                        map[k] = Merge(clr_orig, clr, dInitForce);
                    }
                    x++;
                }
                //left from top to down
                x = x_0 - i;
                y = y_0 - i + 1;
                for (j = 1; j < 2 * i; j++)
                {
                    if (x < 0 || x >= width || y < 0 || y >= height) { }
                    else
                    {
                        k = y * width + x;
                        int argb = map[k];
                        var clr_orig = System.Drawing.Color.FromArgb(argb);
                        map[k] = Merge(clr_orig, clr, dInitForce);
                    }
                    y++;
                }
                //right from top to down
                x = x_0 + i;
                y = y_0 - i + 1;
                for (j = 1; j < 2 * i; j++)
                {
                    if (x < 0 || x >= width || y < 0 || y >= height) { }
                    else
                    {
                        k = y * width + x;
                        int argb = map[k];
                        var clr_orig = System.Drawing.Color.FromArgb(argb);
                        map[k] = Merge(clr_orig, clr, dInitForce);
                    }
                    y++;
                }
                dInitForce -= step;
            }
        }

        /// <summary>
        /// капля
        /// </summary>
        /// <param name="clr">цвет капли</param>
        /// <param name="x_0">позиция по горизонтали</param>
        /// <param name="y_0">позиция по вертикали</param>
        /// <param name="wi">ширина капли</param>
        /// <param name="he">высота капли</param>
        /// <param name="dInitForce">начальная сила капли</param>
        public void Drop(System.Drawing.Color clr, int x_0, int y_0, int wi, int he, double dInitForce, bool bLinear = true)
        {
            //it simulates a drop of paint
            if (wi <= 0 || he <= 0 || x_0 < 0 || x_0 >= width || y_0 < 0 || y_0 >= height) return;
            if (dInitForce <= 0 || dInitForce > 1) dInitForce = 1;
            int k, i, j, x, y;
            double dEllipse = (wi * 1.0) / he;
            double dRadiusMax = Math.Sqrt(wi * wi + he * he * dEllipse * dEllipse) / 2;
            for (i = 0; i < wi; i++)
            {
                for (j = 0; j < he; j++)
                {
                    x = x_0 - wi / 2 + i;
                    y = y_0 - he / 2 + j;
                    if (x < 0 || x >= width || y < 0 || y >= height) { }
                    else
                    {
                        double dRadius = Math.Sqrt((x - x_0) * (x - x_0) + (y - y_0) * (y - y_0) * dEllipse * dEllipse);
                        if (dRadiusMax <= dRadius) continue;
                        k = y * width + x;
                        int argb = map[k];
                        var clr_orig = System.Drawing.Color.FromArgb(argb);
                        int argb_new = Merge(clr_orig, clr, dInitForce * Math.Pow((dRadiusMax - dRadius) / dRadiusMax, bLinear ? 1 : 2));
                        map[k] = argb_new;
                        //string s = string.Format("x={0}, y={1}, k={2}, i={3}, j={4}, argb={5}, new={6}", x, y, k, i, j, argb, argb_new);
                        //Dynamo.Log(s);
                    }
                }
            }
        }

        /// <summary>
        /// устанавливает значение в диапазоне 0-255
        /// </summary>
        /// <param name="cc">цвет</param>
        public static int SafeColor(int cc)
        {
            //Don't allow the value to go out of range 0-255
            if (cc < 0) return 0;
            if (cc > 255) return 255;
            return cc;
        }

        /// <summary>
        /// слить 2 цвета
        /// </summary>
        /// <param name="clr_orig">цвет основной</param>
        /// <param name="clr">цвет капли</param>
        /// <param name="dInitForce">начальная сила капли</param>
        public static int Merge(System.Drawing.Color clr_orig, System.Drawing.Color clr, double dInitForce)
        {
            //We sum up two values – the new color with the dInitForce weight and the original color 
            //with the weight(1 - dInitForce).And so on all channels
            int alpha, red, green, blue;
            alpha = clr_orig.A;
            red = SafeColor((int)(dInitForce * clr.R + clr_orig.R * (1 - dInitForce)));
            green = SafeColor((int)(dInitForce * clr.G + clr_orig.G * (1 - dInitForce)));
            blue = SafeColor((int)(dInitForce * clr.B + clr_orig.B * (1 - dInitForce)));
            return System.Drawing.Color.FromArgb(alpha, red, green, blue).ToArgb();
        }

        //transformations
        /// <summary>
        /// серый
        /// </summary>
        public void Gray(bool bAver = true)
        {   //2021-07-13
            int alpha, red, green, blue;
            int k = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int argb = map[k];
                    var cc = System.Drawing.Color.FromArgb(argb);
                    alpha = cc.A;
                    //Converting the image to grayscale. Find the average value for the channels 
                    if( bAver )red = (cc.R + cc.G + cc.B) / 3;
                    else
                    {
                        red = cc.R;
                        if (red < cc.G) red = cc.G;
                        if (red < cc.B) red = cc.B;
                    }
                    green = red;
                    blue = red;
                    map[k++] = System.Drawing.Color.FromArgb(alpha, red, green, blue).ToArgb();
                }
            }
        }

        /// <summary>
        /// серый со сжатием
        /// </summary>
        public void GrayAdaptive(int col, int row, bool bAver = true)
        {   //2021-07-13
            int alpha, red, green, blue;
            int k = 0, i, j, x, y, x_0, y_0, x_1, y_1, n;
            //размеры клетки для усреднения
            double wi = (double)width / col;  //шаг по горизонтали
            double he = (double)height / row; //шаг по вертикали
            //if (wi < 1 || he < 1) return;
            
            int[] map_2 = new int[col * row];
            int m = 0;
            for (j = 0; j < row; j++)
            {
                for (i = 0; i < col; i++)
                {
                    x_0 = (int)Math.Round(i * wi);
                    y_0 = (int)Math.Round(j * he);
                    x_1 = (int)Math.Round((i + 1) * wi);
                    y_1 = (int)Math.Round((j + 1) * he);
                    if (x_1 > width) x_1 = width;
                    if (y_1 > height) y_1 = height;
                    n = 0;
                    alpha = 0;
                    red = 0;
                    for (y = y_0; y < y_1; y++)
                    {
                        for (x = x_0; x < x_1; x++)
                        {
                            k = y * width + x;
                            int argb = map[k];
                            var cc = System.Drawing.Color.FromArgb(argb);
                            alpha += cc.A;
                            //Converting the image to grayscale. Find the average value for the channels 
                            if (bAver) red += (cc.R + cc.G + cc.B) / 3;
                            else
                            {
                                int redMax = cc.R;
                                if (redMax < cc.G) redMax = cc.G;
                                if (redMax < cc.B) redMax = cc.B;
                                red += redMax;
                            }
                            n++;
                        }
                    }
                    if (n > 0)
                    {
                        alpha = alpha / n;
                        red = red / n;
                    }
                    else
                    {
                        alpha = 255;
                        red = 127;
                    }
                    green = red;
                    blue = red;
                    map_2[m++] = System.Drawing.Color.FromArgb(alpha, red, green, blue).ToArgb();
                }
            }

            width = col;
            height = row;
            map = map_2;
        }

        /// <summary>
        /// зеркалка
        /// </summary>
        public void Flip(bool bHorizontal = true)
        {   //2021-07-16
            int k, m;
            if (bHorizontal)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width / 2; x++)
                    {
                        k = y * width + x;
                        m = y * width + (width - 1 - x);
                        int argb1 = map[k];
                        int argb2 = map[m];
                        map[k] = argb2;
                        map[m] = argb1;
                    }
                }
            }
            else
            {
                for (int y = 0; y < height /2; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        k = y * width + x;
                        m = (height - 1 - y) * width + x;
                        int argb1 = map[k];
                        int argb2 = map[m];
                        map[k] = argb2;
                        map[m] = argb1;
                    }
                }
            }
        }


        /// <summary>
        /// черно-белый
        /// </summary>
        public void BlackWhite(int threshold = 127)
        {
            int alpha, red, green, blue;
            int k = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int argb = map[k];
                    var cc = System.Drawing.Color.FromArgb(argb);
                    alpha = cc.A;
                    //Converting the image to black and white. Find the average value for the channels 
                    //and compare it with the threshold
                    red = (cc.R + cc.G + cc.B) / 3;
                    red = red > threshold ? 255 : 0;
                    green = red;
                    blue = red;
                    map[k++] = System.Drawing.Color.FromArgb(alpha, red, green, blue).ToArgb();
                }
            }
        }

        /// <summary>
        /// сделать расплавчатым
        /// </summary>
        public void Smooth(int num = 1, int step = 1)
        {
            //Reducing the image contrast. Allocating memory for a temporary array
            int[] map_2 = new int[width * height];
            int alpha, red, green, blue;
            int k, m, n, x_2, y_2;
            for (int l = 0; l < num; l++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        k = y * width + x;
                        int argb = map[k];
                        map_2[k] = argb;
                        alpha = 0;
                        red = 0;
                        green = 0;
                        blue = 0;
                        n = 0;
                        //Find the neighbors of the current point , accumulate values by channels
                        for (int i = -step; i <= step; i++)
                        {
                            y_2 = y + i;
                            if (y_2 < 0 || y_2 >= height) continue;
                            for (int j = -step; j <= step; j++)
                            {
                                x_2 = x + j;
                                if (x_2 < 0 || x_2 >= width) continue;
                                n++;
                                m = y_2 * width + x_2;
                                argb = map[m];
                                var cc = System.Drawing.Color.FromArgb(argb);
                                alpha += cc.A;
                                red += cc.R;
                                green += cc.G;
                                blue += cc.B;
                            }
                        }
                        //n – number of neighbors found
                        if (n > 0)
                        {   
                            map_2[k] = System.Drawing.Color.FromArgb(alpha / n, red / n, green / n, blue / n).ToArgb();
                        }
                    }
                }
                //Finally, we copy from the temporary array to the main one
                for (int y = 0; y < height * width; y++) map[y] = map_2[y];
            }
        }

        /// <summary>
        /// фильтровать
        /// </summary>
        public void Filter(List<Tuple<int, int, double>> fil, int num = 1)
        {
            int[] map_2 = new int[width * height];
            int alpha, red, green, blue;
            int k, m, n, x_2, y_2;
            for (int l = 0; l < num; l++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        k = y * width + x;
                        int argb = map[k];
                        map_2[k] = argb;
                        alpha = 255;
                        red = 0;
                        green = 0;
                        blue = 0;
                        n = 0;

                        foreach (var tup in fil)
                        {
                            y_2 = y + tup.Item2;
                            if (y_2 < 0 || y_2 >= height) continue;
                            x_2 = x + tup.Item1;
                            if (x_2 < 0 || x_2 >= width) continue;
                            n++;
                            m = y_2 * width + x_2;
                            argb = map[m];
                            var cc = System.Drawing.Color.FromArgb(argb);
                            //alpha += (int)(cc.A * tup.Item3);
                            red += (int)(cc.R * tup.Item3);
                            green += (int)(cc.G * tup.Item3);
                            blue += (int)(cc.B * tup.Item3);
                        }
                        if (n > 0)
                        {   //??negative
                            map_2[k] = System.Drawing.Color.FromArgb(SafeColor(alpha),
                                SafeColor(red), SafeColor(green), SafeColor(blue)).ToArgb();
                        }

                    }
                }

                for (int y = 0; y < height * width; y++) map[y] = map_2[y];
            }
        }

        /// <summary>
        /// Sobel фильтр
        /// если фон белый, то раннее определение, если фон черный, то позднее.
        /// </summary>
        public void Sobel(bool bHoriz = true, int num = 1)
        {
            List<Tuple<int, int, double>> fil = new List<Tuple<int, int, double>>();
            Tuple<int, int, double> m00, m01, m02, m10, m11, m12, m20, m21, m22;
            if (bHoriz)
            {
                m00 = new Tuple<int, int, double>(-1, -1, 1);
                m01 = new Tuple<int, int, double>(-1, 0, 2);
                m02 = new Tuple<int, int, double>(-1, 1, 1);

                m10 = new Tuple<int, int, double>(0, -1, 0);
                m11 = new Tuple<int, int, double>(0, 0, 0);
                m12 = new Tuple<int, int, double>(0, 1, 0);

                m20 = new Tuple<int, int, double>(1, -1, -1);
                m21 = new Tuple<int, int, double>(1, 0, -2);
                m22 = new Tuple<int, int, double>(1, 1, -1);
            }
            else
            {
                m00 = new Tuple<int, int, double>(-1, -1, 1);
                m01 = new Tuple<int, int, double>(-1, 0, 0);
                m02 = new Tuple<int, int, double>(-1, 1, -1);

                m10 = new Tuple<int, int, double>(0, -1, 2);
                m11 = new Tuple<int, int, double>(0, 0, 0);
                m12 = new Tuple<int, int, double>(0, 1, -2);

                m20 = new Tuple<int, int, double>(1, -1, 1);
                m21 = new Tuple<int, int, double>(1, 0, 0);
                m22 = new Tuple<int, int, double>(1, 1, -1);
            }

            fil.Add(m00);
            fil.Add(m01);
            fil.Add(m02);

            fil.Add(m10);
            fil.Add(m11);
            fil.Add(m12);

            fil.Add(m20);
            fil.Add(m21);
            fil.Add(m22);

            Filter(fil, num);
        }

        /// <summary>
        /// изменить пиксель
        /// </summary>
        public void Pixel(int x0, int y0, int alpha, int red, int green, int blue, int size_x = 1, int size_y = 1)
        {
            /*Changing the pixel at position x0, y0. To do this, use the parameters alpha, red, green, blue to form the color cc. 
            Find an element of the map array and assign it. 
            Not only one pixel can be changed, but also its neighbors in a rectangle with the size size_x, size_y (by default, they are equal to 1).
            */
            var cc = System.Drawing.Color.FromArgb(alpha, red, green, blue).ToArgb();
            int k;
            for (int y = y0; y < height && y < y0 + size_y; y++)
            {
                for (int x = x0; x < width && x < x0 + size_x; x++)
                {
                    k = y * width + x;
                    map[k] = cc;
                }
            }
        }

        /// <summary>
        /// изменить альфу
        /// </summary>
        public void Alpha(int x0, int y0, int alpha, int size_x = 1, int size_y = 1)
        {
            int k, red, green, blue;
            for (int y = y0; y < height && y < y0 + size_y; y++)
            {
                for (int x = x0; x < width && x < x0 + size_x; x++)
                {
                    k = y * width + x;
                    var cc = System.Drawing.Color.FromArgb(map[k]);
                    red = cc.R;
                    green = cc.G;
                    blue = cc.B;
                    var cc2 = System.Drawing.Color.FromArgb(alpha, red, green, blue).ToArgb();
                    map[k] = cc2;
                }
            }
        }

        /// <summary>
        /// наложить битмап с учетом альфы
        /// </summary>
        public void Put(BitmapSimple bm)
        {
            if (width != bm.width || height != bm.height) return;
            int alpha, red, green, blue, k;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    k = y * width + x;
                    var clr = System.Drawing.Color.FromArgb(bm.map[k]);
                    if (clr.A == 255) map[k] = bm.map[k]; //замещение
                    else
                    {   //сложение
                        var clr_orig = System.Drawing.Color.FromArgb(map[k]);
                        alpha = clr_orig.A;
                        double dInitForce = (1.0 * clr.A) / 255;
                        red = SafeColor((int)(dInitForce * clr.R + clr_orig.R * (1 - dInitForce)));
                        green = SafeColor((int)(dInitForce * clr.G + clr_orig.G * (1 - dInitForce)));
                        blue = SafeColor((int)(dInitForce * clr.B + clr_orig.B * (1 - dInitForce)));
                        var cc2 = System.Drawing.Color.FromArgb(alpha, red, green, blue).ToArgb();
                        map[k] = cc2;
                    }
                }
            }
        }

        /// <summary>
        /// найти угол поворота, при котором максимальное число белых и/или черных линий
        /// предполаем серый битмап
        /// </summary>
        public double Angle(int iType = 1, int iThreshPixel = 10, double dThreshPerc = 95)
        {
            double dAngleBest = 0;
            int nBest = -1;
            //перебираем углы поворота
            //формируем смещения
            //проходим по всем новым линиям
            //определяем: белая, черная или серая
            for (double dan = 45; dan >= -45; dan -= 0.5)
            {
                var tan = Math.Tan((dan / 180) * Math.PI);
                int nLnWhite = 0, nLnBlack = 0, nLnGray = 0;//число соотв.линий
                for (int y = 0; y < height; y++)
                {
                    int nWhite = 0, nBlack = 0, nGray = 0;//число соотв.точек
                    for (int x = 0; x < width; x++)
                    {
                        int yNew = (int)Math.Round(y - (x - width / 2) * tan);
                        if (yNew < 0 || yNew >= height) continue;
                        int k = yNew * width + x;
                        var clr = System.Drawing.Color.FromArgb(map[k]);
                        if (clr.R > 255 - iThreshPixel) nWhite++;
                        else if (clr.R < iThreshPixel) nBlack++;
                        else nGray++;
                    }
                    int nTotal = nWhite + nBlack + nGray;
                    if (nTotal == 0) continue;

                    if ((100.0 * nWhite) / nTotal >= dThreshPerc) nLnWhite++;
                    else if ((100.0 * nBlack) / nTotal >= dThreshPerc) nLnBlack++;
                    else nLnGray++;
                }

                int cnt = (iType & 0x1) > 0 ? nLnWhite : 0;
                cnt += (iType & 0x2) > 0 ? nLnBlack : 0;
                if (nBest == -1)
                {
                    nBest = cnt;
                    dAngleBest = dan;
                }
                else if(nBest < cnt)
                {
                    nBest = cnt;
                    dAngleBest = dan;
                }
            }
            Dynamo.Console("nBest=" + nBest);
            return dAngleBest;
        }

        /// <summary>
        /// повернуть против часовой на заданный угол
        /// </summary>
        public void Rotate(double degreeAngle)
        {
            string sfi = "tmp_rotate.png";
            string sfi_2 = "tmp_rotate_2.png";
            if (File.Exists(sfi)) File.Delete(sfi);
            Save(sfi);

            Image image = Image.FromFile(sfi);
            Bitmap bitmap = new Bitmap(image.Width, image.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.WhiteSmoke);

            //move rotation point to center of image
            g.TranslateTransform((float)image.Width / 2, (float)image.Height / 2);
            //rotate
            g.RotateTransform((float)-degreeAngle);
            //move rotation point from center of image
            g.TranslateTransform((float)-image.Width / 2, (float)-image.Height / 2);
            g.DrawImage(image, 0, 0);

            g.Flush();
            if (File.Exists(sfi_2)) File.Delete(sfi_2);

            bitmap.Save(sfi_2, System.Drawing.Imaging.ImageFormat.Png);
            g.Dispose();
            bitmap.Dispose();
            image.Dispose();

            FromFile(sfi_2);
        }

        /// <summary>
        /// повернуть против часовой на заданный угол используя память
        /// </summary>
        public void RotateInMemory(double degreeAngle)
        {
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_2 = new MemoryStream();
            Save(ms);

            Image image = Image.FromStream(ms);//Image.FromFile(sfi);
            Bitmap bitmap = new Bitmap(image.Width, image.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.WhiteSmoke);

            //move rotation point to center of image
            g.TranslateTransform((float)image.Width / 2, (float)image.Height / 2);
            //rotate
            g.RotateTransform((float)-degreeAngle);
            //move rotation point from center of image
            g.TranslateTransform((float)-image.Width / 2, (float)-image.Height / 2);
            g.DrawImage(image, 0, 0);

            g.Flush();

            bitmap.Save(ms_2, System.Drawing.Imaging.ImageFormat.Png);
            g.Dispose();
            bitmap.Dispose();
            image.Dispose();

            FromStream(ms_2);
        }

        /// <summary>
        /// generate the hash of BitmapSimple - A hash function is any function 
        /// that can be used to map data of arbitrary size to fixed-size values. 
        /// The values returned by a hash function are called hash values, hash codes, digests, or simply hashes.
        /// </summary>
        /// <param name="nx">divide by 'nx' horizontally</param>
        /// <param name="ny">divide by 'nx' vertically</param>
        /// <param name="palette">array of colors to match</param>
        /// <param name="paletteCode">string to codify our palette colors</param>
        /// <param name="bUpdate">if true, modify itself</param>
        public string Hash(int nx, int ny, Color[] palette, string paletteCode, bool bUpdate = false)
        {
            StringBuilder s = new StringBuilder();
            int k, n, alpha, red, green, blue, iMin, index;
            //division parameters
            int d_x = width / nx;   //rectangle width
            int d_y = height / ny;  //rectangle height
            //divide all area into small rectangles
            //loop through our map
            for (int i = 0; i < nx; i++)
            {
                for (int j = 0; j < ny; j++)
                {
                    //find an average color in the small rectangle
                    //init counters
                    alpha = 0;
                    red = 0;
                    green = 0;
                    blue = 0;
                    n = 0;
                    //go through rectangle pixels, accumulate in channels
                    for (int y = j * d_y; y < (j + 1) * d_y; y++)
                    {
                        for (int x = i * d_x; x < (i + 1) * d_x; x++)
                        {
                            k = y * width + x;
                            var cc = System.Drawing.Color.FromArgb(map[k]);
                            alpha += cc.A;
                            red += cc.R;
                            green += cc.G;
                            blue += cc.B;
                            n++;
                        }
                    }
                    //get average
                    alpha /= n;
                    red /= n;
                    green /= n;
                    blue /= n;

                    //find a nearest color in the palette
                    iMin = int.MaxValue;
                    index = 0;
                    for (int m = 0; m < palette.Length; m++)
                    {
                        var clr = palette[m];
                        int diff = Math.Abs(clr.A - alpha) + Math.Abs(clr.R - red) +
                            Math.Abs(clr.G - green) + Math.Abs(clr.B - blue);
                        if( diff < iMin )
                        {
                            iMin = diff;
                            index = m;
                        }
                    }

                    //add to hash a letter with found index
                    s.Append(paletteCode.Substring(index, 1));

                    if (bUpdate)
                    {   //set pixels inside the small rectangle to a new selected color
                        var best = palette[index].ToArgb();
                        for (int y = j * d_y; y < (j + 1) * d_y; y++)
                        {
                            for (int x = i * d_x; x < (i + 1) * d_x; x++)
                            {
                                k = y * width + x;
                                map[k] = best;
                            }
                        }
                    }
                }
            }
            
            return s.ToString();
        }


    }
}
