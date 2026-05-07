using AWSLambdaStardedFromSQS.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AWSLambdaStardedFromSQS.Config
{
    internal class LambdaConfig
    {
        /// <summary>
        /// Protected constructor to prevent instantiation of this static configuration class
        /// </summary>
        protected LambdaConfig() { }


        internal readonly static Func<IConfiguration, IServiceProvider> ConfigureServices = (configuration) =>
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
            });

            ConfigureHttpClient(serviceCollection, configuration);
            ConfigureCache(serviceCollection, configuration);
            ConfigureAuthentication(serviceCollection, configuration);
            ConfigureExternalServices(serviceCollection, configuration);

            return serviceCollection.BuildServiceProvider();
        };

        private static void ConfigureExternalServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IUserService, UserService>();
        }

        private static void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
        {

        }

        private static void ConfigureHttpClient(IServiceCollection services, IConfiguration configuration)
        {

        }

        private static bool IsCacheRedisEnabled(IConfiguration configuration) => configuration.GetValue<bool>("Cache:RedisCacheEnabled");

        private static void ConfigureCache(IServiceCollection services, IConfiguration configuration)
        {

        }
    }
}
