using detect_object_api;

namespace detect_object_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             //.ConfigureAppConfiguration((hostingContext, config) =>
             //{
             //    config
             //        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
             //        .AddJsonFile("appsettings.json", true, true)
             //        //.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
             //        //.AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: false, reloadOnChange: true)
             //        .AddEnvironmentVariables();
             //})
                // .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<DNHStartUp>();
            });
    }
}