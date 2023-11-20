using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Configuration;

namespace MathPanelExt
{
    /// <summary>
    /// класс для сравнения папок, вычисления размера папки и очистки папки от файлов с расширением
    /// </summary>
    public class FileSystemClean
    {
        public static string sLogFile = "FileSystemClean.log";

        // логирование
        static void log(String s)
        {
            try
            {
                StreamWriter sw = new StreamWriter(sLogFile, true, Encoding.UTF8);
                sw.WriteLine(string.Format("{0:yyyy-MM-dd HH:mm:ss} ", DateTime.Now) + s);
                sw.Close();
            }
            catch (Exception se)
            {
                System.Console.WriteLine(se.Message);
            }
        }

        /// <summary>
        /// рекурсивная обработка - сравнение папки dir1 и папки dir2
        /// </summary>
        public static void ProcessDir(string dir1, string dir2, int level)
        {
            int i, j;
            string otstup = "";
            for (i = 0; i < level; i++) otstup += "   ";
            log(otstup + "dir1=" + dir1);
            log(otstup + "dir2=" + dir2);

            try
            {
                string[] files1 = Directory.GetFiles(dir1);
                string[] files2 = Directory.GetFiles(dir2);
                bool[] bMatch = new bool[files2.Length];
                for (i = 0; i < files1.Length; i++) files1[i] = files1[i].Replace(dir1, "");
                for (i = 0; i < files2.Length; i++) files2[i] = files2[i].Replace(dir2, "");
                for (i = 0; i < files2.Length; i++) bMatch[i] = false;

                for (i = 0; i < files1.Length; i++)
                {
                    //log(files1[i]);
                    for (j = 0; j < files2.Length; j++)
                    {
                        if( files1[i] == files2[j] ) break;
                    }
                    if( j >= files2.Length )
                    {
                        log(otstup + files1[i] + " BAD is missing in dir2");
                        continue;
                    }
                    bMatch[j] = true;
                    DateTime dtMod1 = File.GetLastWriteTime(dir1 + files1[i]);
                    DateTime dtMod2 = File.GetLastWriteTime(dir2 + files2[j]);
                    long length1 = new System.IO.FileInfo(dir1 + files1[i]).Length;
                    long length2 = new System.IO.FileInfo(dir2 + files2[j]).Length;
                    if (dtMod1 == dtMod2 && length1 == length2) log(otstup + files1[i] + " OK");
                    else
                    {
                        string status = "";
                        if (dtMod1 < dtMod2) status += " BAD in dir2 newer";
                        else if (dtMod1 > dtMod2) status += " BAD in dir1 newer";
                        if (length1 < length2) status += "; BAD in dir2 bigger";
                        else if (length1 > length2) status += "; BAD in dir1 bigger";
                        log(otstup + files1[i] + status);
                    }
                }

                for (i = 0; i < files2.Length; i++)
                {
                    if (bMatch[i] == false)
                        log(otstup + files2[i] + " BAD is missing in dir1");
                }

                //find dirs
                string[] subDir1 = Directory.GetDirectories(dir1);
                string[] subDir2 = Directory.GetDirectories(dir2);

                bMatch = new bool[subDir2.Length];
                for (i = 0; i < subDir1.Length; i++) subDir1[i] = subDir1[i].Replace(dir1, "");
                for (i = 0; i < subDir2.Length; i++) subDir2[i] = subDir2[i].Replace(dir2, "");
                for (i = 0; i < subDir2.Length; i++) bMatch[i] = false;

                for (i = 0; i < subDir1.Length; i++)
                {
                    //log(subDir1[i]);
                    for (j = 0; j < subDir2.Length; j++)
                    {
                        if (subDir1[i] == subDir2[j]) break;
                    }
                    if (j >= subDir2.Length)
                    {
                        log(otstup + subDir1[i] + " BAD is missing in dir2");
                        continue;
                    }
                    bMatch[j] = true;
                    ProcessDir(dir1 + subDir1[i], dir2 + subDir2[j], level++);
                }

                for (i = 0; i < subDir2.Length; i++)
                {
                    if (bMatch[i] == false)
                        log(otstup + subDir2[i] + " BAD is missing in dir1");
                }
                log(otstup + "===");
            }
            catch (Exception e)
            {
                log(e.ToString());
            }
        }

        /// <summary>
        /// вычисление размеров подпапок в dir1
        /// </summary>
        public static long CalcDir(string dir1)
        {
            int i;
            long total = 0, szFiles = 0;
            //find dirs
            string[] subDir1 = Directory.GetDirectories(dir1);
            for (i = 0; i < subDir1.Length; i++)
            {
                string sd = subDir1[i];
                long sz = DirSize(new DirectoryInfo(sd));
                log(sd + "=" + sz/1024000 + " MB " + (sz > 1000000000 ? "HUGE" : (sz > 100000000 ? "BIG" : "")));
                total += sz;
            }

            var d = new DirectoryInfo(dir1);
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                total += fi.Length;
                szFiles += fi.Length;
            }
            var res = dir1 + ": files in root=" + szFiles / 1024000 + " MB, total=" + total / 1024000 + " MB";
            log(res);
            System.Console.WriteLine(res);
            return total;
        }

        static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            try
            {
                // Add file sizes.
                FileInfo[] fis = d.GetFiles();
                foreach (FileInfo fi in fis)
                {
                    size += fi.Length;
                }
                // Add subdirectory sizes.
                DirectoryInfo[] dis = d.GetDirectories();
                foreach (DirectoryInfo di in dis)
                {
                    size += DirSize(di);
                }
            }
            catch (Exception ex)
            {
                //log(ex.ToString());
            }
            return size;
        }

        /// <summary>
        /// рекурсивная обработка - очистка папки dir1 от файлов с расширением ext
        /// </summary>
        public static int CleanDir(string dir1, string ext, bool bShowOnly)
        {
            int iRemoved = 0;
            try
            {
                string[] files = Directory.GetFiles(dir1, ext);
                foreach (var f in files)
                {
                    if (!bShowOnly)
                    {
                        File.Delete(f);
                        log("удален=" + f);
                    }
                    else log("будет удален=" + f);
                    iRemoved++;
                }

                string[] subDir1 = Directory.GetDirectories(dir1);
                foreach (var d in subDir1)
                {
                    iRemoved += CleanDir(d, ext, bShowOnly);
                }
            } catch (Exception) { };
            return iRemoved;
        }
    }
}
