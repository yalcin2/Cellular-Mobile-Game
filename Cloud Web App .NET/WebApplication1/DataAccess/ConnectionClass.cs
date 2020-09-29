using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Npgsql;


namespace WebApplication1.DataAccess
{
    public class ConnectionClass
    {

        public NpgsqlConnection MyConnection { get; set; }
        public NpgsqlTransaction MyTransaction { get; set; } //Transaction will keep track of what records within the database have been affected

        public ConnectionClass()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["postgresql"].ConnectionString;

            MyConnection = new NpgsqlConnection(connectionString);
        }
    }
}