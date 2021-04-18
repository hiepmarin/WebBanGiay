using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebBanGiay.Models
{
    public class DBConnection
    {
        public DbSet<taikhoan> taikhoans { get; set; }
        public DbSet<product> products { get; set; }
        string strCon;
        public DBConnection()
        {
            strCon = ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(strCon);
        }
    }
}