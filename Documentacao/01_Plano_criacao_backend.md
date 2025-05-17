# Plano de criação do backend

O plano proposto considera que o desenvolvedor utiliza o Windows 10 ou superior como sistema operacional e o Visual Studio Code como ferramenta de desenvolvimento. O Ambiente de execução é "containerizado" no Docker para facilitar a portabilidade a posteriori.
---

## Etapas de Implementação do Backend

### 1. Estrutura Inicial

```bash
solucao-cdb/
├── solucao-cdb.sln
├── src/
│   └── backend/
│       ├── Domain/
│       │   └── Domain.csproj
│       ├── Application/
│       │   └── Application.csproj
│       ├── Infrastructure/
│       │   └── Infrastructure.csproj
│       └── WebAPI/
│           └── WebAPI.csproj
├── tests/
│   ├── Domain.Tests/
│   └── Application.Tests/
└── docker-compose.yml
```

---

### 2. Criação e Configuração dos Projetos (.NET CLI)

Execute os comandos abaixo na raiz (solucao-CDB\):

```bash
dotnet new sln -n solucao-cdb

# Criar projetos
dotnet new classlib -n Domain -o src/backend/Domain
dotnet new classlib -n Application -o src/backend/Application
dotnet new classlib -n Infrastructure -o src/backend/Infrastructure
dotnet new webapi -n WebAPI -o src/backend/WebAPI

# Adicionar referências
dotnet add src/backend/Application/Application.csproj reference src/backend/Domain/Domain.csproj
dotnet add src/backend/Infrastructure/Infrastructure.csproj reference src/backend/Domain/Domain.csproj
dotnet add src/backend/WebAPI/WebAPI.csproj reference src/backend/Application/Application.csproj
dotnet add src/backend/WebAPI/WebAPI.csproj reference src/backend/Infrastructure/Infrastructure.csproj

# Adicionar projetos à solução
dotnet sln solucao-cdb.sln add src/backend/Domain/Domain.csproj
dotnet sln solucao-cdb.sln add src/backend/Application/Application.csproj
dotnet sln solucao-cdb.sln add src/backend/Infrastructure/Infrastructure.csproj
dotnet sln solucao-cdb.sln add src/backend/WebAPI/WebAPI.csproj
```

---

### 3. Pacotes Necessários

```bash
cd src/backend/WebAPI

dotnet add package Swashbuckle.AspNetCore
dotnet add package Swashbuckle.AspNetCore.SwaggerUI
dotnet add package Microsoft.Extensions.Options.ConfigurationExtensions
```

---

### 4. Implementações Iniciais

#### Domain

* `InvestmentInput.cs`
* `InvestmentResult.cs`
* `ICdbCalculator.cs`

#### Application

* `CdbCalculator.cs` (implementa lógica de cálculo)
* `ICdbService.cs`, `CdbService.cs` (uso de aplicação)

#### Infrastructure

* Sem dependência inicial (placeholder para futura persistência ou serviços externos)

#### WebAPI

* `CdbController.cs`
* Customização completa do Swagger (incluindo logo da B3)

---

### 5. Customização do Swagger (SwaggerConfig.cs)

```csharp
builder.Services.AddSwaggerGen(c =>
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
```

#### Logo da B3 no Swagger:

Será adicionado via arquivo `swagger-custom.css` e configuração em `index.html` customizado (será inserido via `UseSwaggerUI`).

---

### 6. Program.cs (HTTPS)

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICdbCalculator, CdbCalculator>();
builder.Services.AddScoped<ICdbService, CdbService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Cálculo CDB v1");
        c.InjectStylesheet("/swagger-ui/custom.css");
        c.DocumentTitle = "B3 - Simulador de CDB";
        c.HeadContent = "<link rel='icon' type='image/png' href='/swagger-ui/b3-favicon.png' />";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

---

### 7. Dockerfile para o Backend

Crie em `src/backend/WebAPI/Dockerfile`:

```dockerfile
# SDK
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]
COPY ["Application/Application.csproj", "../Application/"]
COPY ["Domain/Domain.csproj", "../Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "../Infrastructure/"]
RUN dotnet restore "WebAPI/WebAPI.csproj"
COPY . .
WORKDIR "/src/WebAPI"
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish

# Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]
```

---

### 8. docker-compose.yml (root da solução)

```yaml
version: '3.4'

services:
  api:
    image: solucao-cdb-api
    build:
      context: ./src/backend/WebAPI
      dockerfile: Dockerfile
    ports:
      - "5000:80"
      - "5001:443"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
```

---

### 9. README.md (comando de execução)

````md
## Execução do Backend

### Requisitos
- Docker Desktop
- .NET 8 SDK

### Build e execução via Docker
```bash
docker-compose up --build
````

### Acesso

* Swagger UI: [https://localhost:5001/swagger](https://localhost:5001/swagger)

```