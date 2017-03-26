using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Blog40.Repository
{
    public class BaseRepository
    {
        public static SqlConnection getConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["BlogDb"].ConnectionString;
            return new SqlConnection(connString);
        }
    }
}