
using DNH.Infrastructure.Utility.AutoMap;
using DNHCore;
using DNHCore.Configuration;
using DNHCore.Infrastructure;
using DNHCore.Util;
using Microsoft.Extensions.DependencyInjection;
namespace detect_object_api
{
    public class DNHStartUp
    {
        public IConfiguration Configuration { get; }
        public DNHStartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMemoryCache();
            services.ConfigureApplicationServices(Configuration);
            var typeFinder = new AppDomainTypeFinder();
            services.ConfigureAutoMapperByAssemply(typeFinder.GetAssemblies());
            services.AddCors(options =>
            {

                options.AddPolicy(MainConfig.CorsPublic,
                    builder => builder
                        .WithOrigins("All")
                        // .WithOrigins(MainConfig.Setting.Cors)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                );
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            AppDependencyResolver.Init(services.BuildServiceProvider());
            ServiceHelper.Service = services.BuildServiceProvider();
            services.AddRouting();


        }

        public void Configure(IApplicationBuilder application, IWebHostEnvironment env)
        {
            application.UseRouting();
           
            if (env.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }

            application.UseHttpsRedirection();
            application.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials()); // allow credentials

            application.ConfigureRequestPipelines();

           // DynamicRouteBuilder.UseRoutes(application);
            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // Sử dụng DynamicRouteBuilder để cấu hình các routes runtime
          

        }
    }
}
