
using DNH.BusinessLayer.Filter;
using DNHCore;
using Microsoft.OpenApi.Models;

namespace detect_object_api.StartUp
{
    public class SwaggerStartUp : IDNHStartup
    {
        public int Order => 20;

        public bool Active => true;



        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddSwaggerGen(options =>
            {
                // options.OperationFilter<SwaggerDefaultValues>();
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Notification API", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                // Thêm custom attribute để hỗ trợ tải lên tệp
                options.OperationFilter<SwaggerFileUploadOperationFilter>();

            });
        }
        public void Configure(IApplicationBuilder application)
        {
            application.UseSwagger();
            application.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GenerateCode v1");

            });
        }
    }
}
