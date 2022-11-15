using web_api.Config;
using web_api.State;

namespace web_api.Startup
{
    public static class Utils
    {
        public static void ConfigureAppConfiguration(HostBuilderContext hostContext, IConfigurationBuilder configurationBuilder)
        {
            // Adding extra configuration files is done here
            if (File.Exists(Path.Combine(hostContext.HostingEnvironment.ContentRootPath, "secrets.json")))
            {
                _ = configurationBuilder.SetBasePath(hostContext.HostingEnvironment.ContentRootPath)
                .AddJsonFile("secrets.json");
            }
        }
        public static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            IConfiguration config = hostContext.Configuration;
            _ = services.AddConfigCustom<ResponseArray>(config, "ResponseArray");
            services.AddSingleton<IState, DictionaryState>();
            services.AddSingleton<IHighLevelStateManager, BasicHighLevelStateManager>();
        }
        public static IServiceCollection AddConfigCustom<T>(
            this IServiceCollection services,
            IConfiguration config,
            string sectionName,
            Func<T, bool>? customValidator = null)
            where T : class
        {
            customValidator ??= (configClass) => true;
            _ = services.AddOptions<T>()
                   .Bind(config.GetSection(sectionName), binderOptions =>
                   {
                       binderOptions.ErrorOnUnknownConfiguration = true;
                   })
               .ValidateDataAnnotations()
               .Validate(customValidator)
               .ValidateOnStart();
            return services;

        }
    }
}
