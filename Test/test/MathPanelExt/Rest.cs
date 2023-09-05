//2020, Andrei Borziak
using System;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace MathPanelExt
{
	/// <summary>
	/// класс для запросов через REST-протокол и просто к веб-ресурсам
	/// </summary>

	public class Rest
	{
		public Rest()
		{
		}

        /// <summary>
        /// urlencode кодирование
        /// </summary>
        /// <param name="str">строка для кодирования</param>
        public static string EncodeString(string str)
        {
            //максимальная длина строки .NET < 4.5 = 32765;
            //максимальная длина строки .NET >= 4.5 = 65519;
            int maxLengthAllowed = 65519;
            StringBuilder sb = new StringBuilder();
            int loops = str.Length / maxLengthAllowed;

            for (int i = 0; i <= loops; i++)
            {
                sb.Append(Uri.EscapeDataString(i < loops
                    ? str.Substring(maxLengthAllowed * i, maxLengthAllowed)
                    : str.Substring(maxLengthAllowed * i)));
            }

            return sb.ToString();
        }
        /// <summary>
        /// urlencode декодирование
        /// </summary>
        /// <param name="encodedString">строка для декодирования</param>
        public static string DecodeString(string encodedString)
        {
            //максимальная длина строки .NET < 4.5 = 32765;
            //максимальная длина строки .NET >= 4.5 = 65519;
            int maxLengthAllowed = 65519;

            int charsProcessed = 0;
            StringBuilder sb = new StringBuilder();

            while (encodedString.Length > charsProcessed)
            {
                var stringToUnescape = encodedString.Substring(charsProcessed).Length > maxLengthAllowed
                    ? encodedString.Substring(charsProcessed, maxLengthAllowed)
                    : encodedString.Substring(charsProcessed);

                // если цикл режет тег кодирования (%xx), мы режем раньше тега, чтобы не потерять данные
                var incorrectStrPos = stringToUnescape.Length == maxLengthAllowed ? stringToUnescape.IndexOf("%", stringToUnescape.Length - 4, StringComparison.InvariantCulture) : -1;
                if (incorrectStrPos > -1)
                {
                    stringToUnescape = encodedString.Substring(charsProcessed).Length > incorrectStrPos
                        ? encodedString.Substring(charsProcessed, incorrectStrPos)
                        : encodedString.Substring(charsProcessed);
                }

                sb.Append(Uri.UnescapeDataString(stringToUnescape));
                charsProcessed += stringToUnescape.Length;
            }

            var decodedString = sb.ToString();

            // ensure the string is sanitized here or throw exception if XSS / SQL Injection is found
            //SQLHelper.SecureString(decodedString);
            return decodedString;
        }

        /// <summary>
        /// GET запрос
        /// </summary>
        /// <param name="url">веб-адрес ресурса</param>
        public static string Get(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            string result = null;
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                StreamReader reader = new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
            }
            return result;
        }

        /// <summary>
        /// application/x-www-form-urlencoded POST запрос
        /// </summary>
        /// <param name="url">веб-адрес ресурса</param>
        /// <param name="data">данные в запросе</param>
        /// <param name="user">логин</param>
        /// <param name="pass">пароль</param>
        public static string Post(string url, string data, string user, string pass)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            //req.ContentType = "application/json";
            req.UserAgent = "Mozilla/5.0"; //"curl/7.61.0";
            req.Accept = "*/*";

            if (user != "" && pass != "")
            {
                String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(user + ":" + pass));
                req.Headers.Add("Authorization", "Basic " + encoded);

                /*ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                CredentialCache credentialCache = new CredentialCache();
                credentialCache.Add(new System.Uri(url), "Basic", new NetworkCredential(user, pass));
                req.Credentials = credentialCache;
                req.PreAuthenticate = true;*/
            }

            // Закодировать данные как форму
            byte[] formData = UTF8Encoding.UTF8.GetBytes(data);
            req.ContentLength = formData.Length;
            string result;

            try
            {
                // послать запрос:
                Stream post = req.GetRequestStream();
                post.Write(formData, 0, formData.Length);

                // получить ответ:
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
            }
            catch (Exception e)
            {
                result = e.ToString();
            }
            return result;
        }

        /// <summary>
        /// multipart/form-data POST запрос
        /// List<Tuple<string, string, string, bool>> - название поля, значение поля (файл), mime type, признак файла
        /// </summary>
        //based on https://stackoverflow.com/questions/1688855/httpwebrequest-c-sharp-uploading-a-file
        public static string Upload(string url, string user, string pass, List<Tuple<string, string, string, bool>> data)
        {
            //сепаратор
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            //Encoding
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            //создать запрос
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            if (user != "" && pass != "")
            {
                String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(user + ":" + pass));
                wr.Headers.Add("Authorization", "Basic " + encoded);
            }

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}"; //For the POST's format
            WebResponse wresp = null;
            string responseData = "";
            try
            {
                Stream rs = wr.GetRequestStream();
                foreach (var tup in data)
                {
                    rs.Write(boundarybytes, 0, boundarybytes.Length);

                    string field = tup.Item1;
                    string value = tup.Item2;
                    string contentType = tup.Item3;
                    bool bFile = tup.Item4;
                    if (!bFile)
                    {
                        string header0 = string.Format(formdataTemplate, field, EncodeString(value));
                        byte[] header0bytes = System.Text.Encoding.UTF8.GetBytes(header0);
                        rs.Write(header0bytes, 0, header0bytes.Length);
                        continue;
                    }

                    string fname = value;
                    int ipos = fname.LastIndexOf("\\");
                    if (ipos > 0) fname = fname.Substring(ipos + 1);

                    string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
                    string header = string.Format(headerTemplate, field, fname, contentType);
                    byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
                    rs.Write(headerbytes, 0, headerbytes.Length);

                    FileStream fileStream = new FileStream(value, FileMode.Open, FileAccess.Read);
                    byte[] buffer = new byte[4096];
                    int bytesRead = 0;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        rs.Write(buffer, 0, bytesRead);
                    }
                    fileStream.Close();
                }

                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                rs.Write(trailer, 0, trailer.Length);
                rs.Close();
                rs = null;

                //получить ответ
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                responseData = reader2.ReadToEnd();
            }
            catch (Exception ex)
            {
                responseData = ex.ToString();
            }
            finally
            {
                if (wresp != null)
                {
                    wresp.Close();
                }
            }
            return responseData;
        }


    }
}