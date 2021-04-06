# dapper-access-handler-core
Simple Data Access Using Dapper for .Net Core

## How To Use
Always wrap the DBWrapper by utilizing *using statement* to ensure all connection has been dispose properly to prevent memory leak.

# Register your code on startup service
<pre lang=c#>
  var serviceProvider = new ServiceCollection()
            .DapperHelperAddSettings(configuration.GetSection("{yourdbconnectionname}"))
            .AddSingleton<IDAL, DAL>()
            .BuildServiceProvider();
</pre>

# How to use from DI
<pre lang=c#>
// Set up configuration sources.
            var dal = serviceProvider.GetService<IDAL>();            
            try
            {
                using (var dapperDI = dal.CreateConnection())
                {
                    int a = dapperDI.ExecuteScalar<int>("select count(*) from [User]"); //your query here :P
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
</pre>

# Direct call without using DI
<pre lang=c#>
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
</pre>
