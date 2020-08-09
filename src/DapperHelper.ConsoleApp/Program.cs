using Dapper;
using DapperHelper.Contract;
using DapperHelper.Implementation;
using DapperHelper.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DapperHelper.ConsoleApp
{
    class Program
    {
        public static IConfiguration configuration;
        public static string con = "Data Source=localhost;Initial Catalog=Demo;Integrated Security=True";
        public static string prov = "System.Data.SqlClient";
        static void Main(string[] args)
        {
            // Build configuration
            configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

            var serviceProvider = new ServiceCollection()
            .DapperHelperAddSettings(configuration.GetSection("Connection1"))
            .AddSingleton<IDAL, DAL>()
            .BuildServiceProvider();

            // Set up configuration sources.
            var dal = serviceProvider.GetService<IDAL>();            
            try
            {
                using (var dapperDI = dal.CreateConnection())
                {
                    int a = dapperDI.ExecuteScalar<int>("select count(*) from [User]");
                    Console.WriteLine("Hello World!");
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                dal.Dispose();
            }
            //direct call with using DI
            var connectionString = serviceProvider.GetService<ConnectionSettings>();
            using (var dapperNoDI = new DAL(connectionString.ConnectionString, connectionString.ProviderName).CreateConnection())
            {
                try
                {
                    int a = dapperNoDI.ExecuteScalar<int>("select count(*) from [User]");
                    Console.WriteLine("Hello World! " + a.ToString());
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    dapperNoDI.Dispose();
                }              
            }
        }
    }
}
