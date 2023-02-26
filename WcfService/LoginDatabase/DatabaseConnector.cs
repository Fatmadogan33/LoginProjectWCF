using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WcfService.LoginBusiness;

namespace WcfService.LoginDatabase
{
    public class DatabaseConnector
    {


        public SqlConnection ConnectToDb()
        {
            SqlConnection conn;
            SqlConnectionStringBuilder connStringBuilder;

            connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = Constants.DB_DATA_SOURCE;
            connStringBuilder.InitialCatalog = Constants.DB_INITIAL_CATALOG;
            connStringBuilder.Encrypt = true;
            connStringBuilder.TrustServerCertificate = true;
            connStringBuilder.ConnectTimeout = 50;
            connStringBuilder.AsynchronousProcessing = true;
            connStringBuilder.MultipleActiveResultSets = true;
            connStringBuilder.IntegratedSecurity = true;
            conn = new SqlConnection(connStringBuilder.ToString());

            return conn;

        }

    }
}