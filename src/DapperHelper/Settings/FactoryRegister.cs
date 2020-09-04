using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Data.SqlClient;

namespace DapperHelper.Settings
{
    /// <summary>
    /// Handle register factory
    /// </summary>
    public sealed class FactoryRegister
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static FactoryRegister()
        {
        }

        private FactoryRegister()
        {
        }

        public static FactoryRegister Instance { get; } = new FactoryRegister();        
        private DbProviderFactory factory;
        private static bool _isAdded = false;
        private static void AddFactoryInstance(string providerName = null)
        {
            if(providerName == null)
            {
                DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
                DbProviderFactories.RegisterFactory("Mysql.Data.MysqlClient", MySqlClientFactory.Instance);
                DbProviderFactories.RegisterFactory("Microsoft.Data.Sqlite", SqliteFactory.Instance);
            }
            else
            {
                switch (providerName.ToLowerInvariant())
                {
                    case "system.data.sqlclient":
                        // Register the factory
                        DbProviderFactories.RegisterFactory(providerName, SqlClientFactory.Instance);
                        break;
                    case "mysql.data.mysqlclient":
                        DbProviderFactories.RegisterFactory(providerName, MySqlClientFactory.Instance);
                        break;
                    case "microsoft.data.sqlite":
                        DbProviderFactories.RegisterFactory(providerName, SqliteFactory.Instance);
                        break;
                    default:
                        break;
                }
            }
            _isAdded = true;
        }
        public DbProviderFactory GetFactoriesInstance(string providerName)
        {
            if (factory != null)
                return factory;
            if (!_isAdded)
                AddFactoryInstance(providerName);
            factory = DbProviderFactories.GetFactory(providerName);
            return factory;
        }
    }
}
