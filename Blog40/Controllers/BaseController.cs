using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;

namespace Blog40.Controllers
{
    public abstract class BaseController : Controller
    {
        public SqlConnection getConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["BlogDb"].ConnectionString;
            return new SqlConnection(connString);
        }
    }
}
