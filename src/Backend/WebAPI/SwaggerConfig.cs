using Microsoft.OpenApi.Models;

namespace WebAPI
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Cálculo de CDB - API B3",
                    Version = "v1",
                    Description = "API para cálculo de investimento em CDB com valores brutos e líquidos.",
                    Contact = new OpenApiContact
                    {
                        Name = "Equipe B3",
                        Email = "suporte@b3.com.br",
                        Url = new Uri("https://www.b3.com.br")
                    }
                });

                var filePath = Path.Combine(AppContext.BaseDirectory, "WebAPI.xml");
                c.IncludeXmlComments(filePath);
            });
        }
    }
}