using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capes.Helpers
{
    public class ConnectionString
    {
        public static string GetConnectionString()
        {
            //string server = System.Configuration.ConfigurationManager.AppSettings[$"Server"],
            //        database = System.Configuration.ConfigurationManager.AppSettings[$"Database"],
            //        user = System.Configuration.ConfigurationManager.AppSettings[$"User"],
            //        password = System.Configuration.ConfigurationManager.AppSettings[$"Password"];

            string server = "localhost";
            string database = "capes";
            string user = "root";
            string password = string.Empty;

            /* validations */
            if (string.IsNullOrWhiteSpace(server))
                throw new Exception($"Server not configured");
            if (string.IsNullOrWhiteSpace(database))
                throw new Exception($"Database name not configured");
            if (string.IsNullOrWhiteSpace(user))
                throw new Exception($"User not configured");

            return $"server={server};database={database};userid={user};password={password};";
        }
    }
}
