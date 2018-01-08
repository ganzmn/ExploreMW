using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExploreMidwest.Data
{
    public class Settings
    {
        private static string _connectionString;

        public static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                _connectionString = ConfigurationManager.ConnectionStrings["ExploreMidwest"].ConnectionString;

            }
            return _connectionString;

        }
    }
}

