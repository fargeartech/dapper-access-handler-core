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
            connectionString = strcon;
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
