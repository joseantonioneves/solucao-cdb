using Application.Interfaces;
using Application.Services;
using Application.Configuration;
using Domain.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80); // Porta sem HTTPS (útil para debug)
    options.ListenAnyIP(443, listenOptions =>
    {
        listenOptions.UseHttps("/https/aspnetapp.pfx", "MySecurePassword123");
    });
});

/* 
Configuração de CORS
--------------------
Só para fins de exercício foi liberada qualquer origem
Http ou https

Para uma liberação específica deve-se, implementada para homologação ou produção
devemos usar:

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

Nesse caso o backend só recebe de origem do moniker http://localhost:4200
*/
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Cálculo de CDB",
        Version = "v1",
        Description = "Esta API realiza simulações de investimento em CDB com base em CDI e taxa do título.\n\n" +
                      "**Fórmula usada:** Montante = ValorInicial × (1 + CDI × TB)^meses",
        Contact = new OpenApiContact
        {
            Name = "Equipe B3",
            Url = new Uri("https://www.b3.com.br")
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    var filePath = Path.Combine(AppContext.BaseDirectory, "WebAPI.xml");
    opt.IncludeXmlComments(filePath);
});
builder.Services.AddScoped<ICdbCalculator, CdbCalculator>();
builder.Services.AddScoped<ICdbService, CdbService>();
builder.Services.Configure<CdbSettings>(builder.Configuration.GetSection("CdbSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Cálculo CDB v1");
        c.InjectStylesheet("/swagger-ui/custom.css");
        c.DocumentTitle = "B3 - Simulador de CDB";
        c.HeadContent = "<link rel='icon' href='/wwwroot/swagger-ui/b3-logo.png' type='image/png'/>";
    });
}
app.UseCors(); // ativação da política de cors (:-<)
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();