# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Definindo o diretório de trabalho para a etapa de build
WORKDIR /app

# Copiando o diretório 'src' da máquina local para o container
COPY ./src ./src

# Copia o arquivo XML de documentação
COPY src/Backend/WebAPI/bin/Release/net8.0/WebAPI.xml /app/publish/WebAPI.xml

# Crie uma pasta certs no container
COPY src/Backend/WebAPI/aspnetapp.pfx /app/certs/aspnetapp.pfx

# Restaura as dependências do projeto WebAPI
RUN dotnet restore "src/Backend/WebAPI/WebAPI.csproj"

# Compila a aplicação
RUN dotnet build "src/Backend/WebAPI/WebAPI.csproj" -c Release -o /app/build

# Publica a aplicação
RUN dotnet publish "src/Backend/WebAPI/WebAPI.csproj" -c Release -o /app/publish

# Estágio final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Definindo o diretório de trabalho para a etapa final
WORKDIR /app

# Copiando os arquivos compilados para a imagem final
COPY --from=build /app/publish .

# Definindo o comando de execução da aplicação
ENTRYPOINT ["dotnet", "WebAPI.dll"]