using Polly;
using ResilencyClient.Config;
namespace ResilencyClient.Startup
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
            Console.WriteLine(nameof(ApplicationConfig));
            _ = services.AddConfigCustom<ApplicationConfig>(config, nameof(ApplicationConfig));
            var appConfig = config.GetSection(nameof(ApplicationConfig)).Get<ApplicationConfig>();

            services.AddHttpClient("Basic", client =>
            {
                client.BaseAddress = appConfig.PrimaryBackendUrl;
                //client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            }).AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                }));
            //.AddTransientHttpErrorPolicy(builder => builder.FallbackAsync(;

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
