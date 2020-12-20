using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//pgsql

namespace MathPanelExt
{
    public class DB
    {
        public string LastError = "";// last command error

        public NpgsqlConnection conn = null;

        /// <summary>
        /// соединение с сервером строка соединения вид - "Host=myserver;Username=mylogin;Password=mypass;Database=mydatabase";
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool Connect(string c)
        {
            try
            {
                var connString = c;// "Host=myserver;Username=mylogin;Password=mypass;Database=mydatabase";
                conn = new NpgsqlConnection(connString);
                conn.Open();               
                return true;
            } catch (Exception e) {

                LastError = e.Message;
                return false;
            }
        }
        public void Close()
        {
            if (conn != null) conn.Close();
        }

        /// <summary>
        /// возвращает массив результат запроса или null - ошибка в LastError
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public object[][] Vals(string sql) {
            try
            {
                List<object[]> r = new List<object[]>();

                var cmd = new NpgsqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3000;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    List<object> row = new List<object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row.Add(reader.GetValue(i));
                    }
                    r.Add(row.ToArray());
                }
                reader.Close();
                return r.ToArray();
            }
            catch (Exception e) {

                LastError = e.Message;
                return null;
            }
        }

        public bool Execute(string sql) {
            try
            {
                var cmd = new NpgsqlCommand(sql, conn);
                //cmd.Parameters.AddWithValue("p", "Hello world");
                //cmd.Parameters.AddWithValue(,)
                cmd.ExecuteNonQuery();
            }
            catch (Exception e) {
                LastError = e.Message;
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

    }
}
