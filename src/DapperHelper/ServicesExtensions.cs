using DapperHelper.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DapperHelper
{
    /// <summary>
    /// Faris, should be execute in application startup
    /// </summary>
    public static class ServicesExtensions
    {
        public static IServiceCollection DapperHelperAddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigurePOCO<ConnectionSettings>(configuration);
            return services;
        }
        public static TConfig ConfigurePOCO<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var config = new TConfig();
            configuration.Bind(config);
            services.AddSingleton(config);
            return config;
        }
    }
}
