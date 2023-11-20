using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
//HashFunctions

namespace MathPanelExt 
{
    public class Base64Sha
    {
        public static string ToHexString(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }
        public static string CreateMD5(string input)
        {
            MD5 MD5Hash = MD5.Create(); //создаем объект для работы с MD5
            byte[] inputBytes = Encoding.ASCII.GetBytes(input); //преобразуем строку в массив байтов
            byte[] hash = MD5Hash.ComputeHash(inputBytes); //получаем хэш в виде массива байтов
            return ToHexString(hash); //преобразуем хэш из массива в строку, состоящую из шестнадцатеричных символов в верхнем регистре
        }
        public static string CreateSHA256(string input)
        {
            SHA256 hash = SHA256.Create();
            return ToHexString(hash.ComputeHash(Encoding.ASCII.GetBytes(input)));
        }
        public static string CreateSHA256Byte(byte[] input)
        {
            SHA256 hash = SHA256.Create();
            return ToHexString(hash.ComputeHash(input));
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        static void Main2(string[] args)
        {
            Console.ReadKey();

            string input = "Hello, World"; //входное сообщение
            string output; //дайджест сообщения (хэш)
            MD5 MD5Hash = MD5.Create(); //создаем объект для работы с MD5
            byte[] inputBytes = Encoding.ASCII.GetBytes(input); //преобразуем строку в массив байтов
            byte[] hash = MD5Hash.ComputeHash(inputBytes); //получаем хэш в виде массива байтов
            output = ToHexString(hash); //преобразуем хэш из массива в строку, состоящую из шестнадцатеричных символов в верхнем регистре
            Console.WriteLine(output);

            string input2 = "Hello, world";
            output = CreateSHA256(input2);
            Console.WriteLine(output);

            //byte[] hash2 = File.ReadAllBytes(@"C:\Users\boraa\Downloads\VTK-9.2.5.tar.gz");
            byte[] hash2 = File.ReadAllBytes(@"C:\boost\boost_1_82_0_rc2.zip");
            output = CreateSHA256Byte(hash2);
            Console.WriteLine(output); 

            /*hash2 = File.ReadAllBytes(@"C:\Users\boraa\Downloads\vtkDocHtml-9.2.5.tar.gz");
            output = CreateSHA256Byte(hash2);
            Console.WriteLine(output);*/

            /*Console.WriteLine("");
            string input3 = "48AD8AeABtAGwAIAB2AGUAcgBzAGkAbwBuAD0AIgAxAC4AMAAiACAAZQBuAGMAbwBkAGkAbgBnAD0AIgB1AHQAZgAtADEANgAiAD8APgA8AEQAIABfAHYAPQAiADIAIgAgAFQAPQAiAC0ANAA4ADgAMwAiAD4APABzACAATgA9ACIALQAzADgANwA5ADYAIgA+ADAAYQBlADYAMABhAGUANQAtAGEAMwAwAGQALQA0ADcANwA2AC0AOAAxADMANwAtADYANgA2ADgAMQA4ADAANwBmAGUAMgA2ADwALwBzAD4APABzACAATgA9ACIALQAyADAAOAA2ADAAIgA+AB8EQAQ1BDIESwRIBDUEPQQgAD8ENQRABDgEPgQ0BCAAMQQ1BDcENAQ1BDkEQQRCBDIEOARPBCAAPwQ+BDsETAQ3BD4EMgQwBEIENQQ7BE8EPAAvAHMAPgA8AGMAbAAgAE4APQAiAC0AMQA0ADQANgAzACIAPgAtADEANAA0ADUANwA8AC8AYwBsAD4APABpACAATgA9ACIALQAxADQANAA0ADcAIgA+ADEAOAAwADAAPAAvAGkAPgA8AEEAIABOAD0AIgAtADcAMAAzADYANwAiACAAVAA9ACIARQB2AGUAbgB0AFIAZQBhAGMAdABpAG8AbgBUAGUAbQBwAGwAYQB0AGUAIgA+ADwAYwBsAD4AMwA0ADIAOQAyADwALwBjAGwAPgA8AC8AQQA+ADwAUwAgAE4APQAiAC0ANAA4ADgAMQAiACAAVAA9ACIALQA0ADgANwA1ACIAPgA8AFMAIABOAD0AIgAtADQAOAA3ADkAIgAgAFQAPQAiAC0AMgA1ADIAMgAiAD4APABkAHQAIABOAD0AIgAtADIANQAyADYAIgA+ADIAMAAyADMALQAwADcALQAxADAAIAAxADQAOgAxADcAOgAyADUALgA4ADcANQA8AC8AZAB0AD4APABjAGwAIABOAD0AIgAtADIANQAyADQAIgA+ADMANgAxADQAMwA8AC8AYwBsAD4APABkAHQAIABOAD0AIgAtADIANQAyADAAIgA+ADIAMAAyADMALQAwADcALQAxADAAIAAxADQAOgAxADcAOgAyADUALgA4ADcANAA8AC8AZAB0AD4APABjAGwAIABOAD0AIgAtADIANQAxADgAIgA+AC0AMwAzADgAOAA8AC8AYwBsAD4APAAvAFMAPgA8AGMAbAAgAE4APQAiAC0ANAA4ADcANwAiAD4ALQAxADAANgAzADUAPAAvAGMAbAA+ADwALwBTAD4APAAvAEQAPgA=";// eJz7z8DA8B+EAQ76Av4=AQAAAACAAAAkAAAAEAAAAA==eJxjYGiwZ8AJIHIAHX4Bfw==AQAAAACAAAAMAAAAEQAAAA==eJxjYGBgYARiJiAGAAAcAAQ=AQAAAACAAAAMAAAAEQAAAA==eJxjZGBgYAJiZiAGAAA0AAc=AAAAAACAAAAAAAAAAAAAAACAAAAAAAAAAAAAAACAAAAAAAAAAAAAAACAAAAAAAAAAAAAAACAAAAAAAAAAAAAAACAAAAAAAAA";
            output = Base64Decode(input3);
            Console.WriteLine(output);*/

            Console.ReadKey();
        }
    }
}
