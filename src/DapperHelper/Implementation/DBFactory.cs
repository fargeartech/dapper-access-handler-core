using DapperHelper.Contract;
using DapperHelper.Settings;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DapperHelper.Implementation
{
    public sealed class DBFactory : IDBFactory
    {
        private IDbConnection conn;
        private string connectionString;
        private DbProviderFactory factory;

        public DBFactory(string strcon, string strprovider)
        {
            //switch (strprovider.ToLowerInvariant())
            //{
            //    case "system.data.sqlclient":

            //        break;
            //    case "mysql.data.mysqlclient":

            //        break;
            //    case "microsoft.data.sqlite":
            //        DbProviderFactories.RegisterFactory(strprovider, SqliteFactory.Instance);
            //        break;
            //    default:
            //        break;
            //}
            connectionString = strcon;
            //factory = DbProviderFactories.GetFactory(strprovider);
            factory = FactoryRegister.Instance.GetFactoriesInstance(strprovider);
        }

        public DBFactory(ConnectionSettings settings)
        {            
            connectionString = settings.ConnectionString;
            factory = FactoryRegister.Instance.GetFactoriesInstance(settings.ProviderName);
        }
        // faris Only inherited classes can call this.
        public IDbConnection CreateConnection()
        {
            conn = factory.CreateConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            return conn;
        }
    }
}
