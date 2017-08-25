using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Telemetry
{
    // SELECT * FROM `brot` WHERE `id` = 1
    static class SQCon
    {
        public static string FetchSQL(SQLiteConnection connection,string table, string where, string value)
        {
            string _return = "";
            string sql = "SELECT " + value + " FROM " + table + " WHERE " + where;
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                _return = reader[0].ToString();
            return _return;
        }

        public static string[] FetchSQL(SQLiteConnection connection, string table, string where, int count)
        {
            string[] _return = new string[count];
            string sql = "SELECT * FROM " + table + " WHERE " + where;
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                for(int i=0; i < count; i++)
                    _return[i] = reader[i].ToString();
            return _return;
        }
    }
}
