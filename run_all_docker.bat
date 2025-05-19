@echo off
echo === [PARADA E REMOÇÃO DO CONTAINER EXISTENTE] ===
docker stop solucao-cdb-api-container >nul 2>&1
docker rm solucao-cdb-api-container >nul 2>&1

echo === [RESOLVENDO CAMINHO ABSOLUTO DO CERTIFICADO PFX] ===
FOR /F "delims=" %%i IN ('cd') DO set ROOT=%%i
set PFX_PATH=%ROOT%\src\Backend\WebAPI\aspnetapp.pfx

echo === [BUILD DO BACKEND EM RELEASE PARA GERAR O XML DE DOCUMENTAÇÃO] ===
dotnet build ./src/Backend/WebAPI/WebAPI.csproj -c Release

echo === [REBUILD DA IMAGEM DO BACKEND] ===
docker build -t solucao-cdb-api .

echo === [EXECUÇÃO DO CONTAINER COM MAPEAMENTO DE VOLUME ABSOLUTO] ===
docker run -d ^
 -p 8080:80 ^
 -p 8443:443 ^
 --name solucao-cdb-api-container ^
 -e ASPNETCORE_ENVIRONMENT=Development ^
 -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx ^
 -e ASPNETCORE_Kestrel__Certificates__Default__Password=MySecurePassword123 ^
 -v "%PFX_PATH%":/https/aspnetapp.pfx ^
 solucao-cdb-api
echo === [CONTAINER API EXECUTADO] ===

echo === [PARADA E REMOÇÃO DO CONTAINER FRONTEND EXISTENTE] ===
docker stop solucao-cdb-frontend-container >nul 2>&1
docker rm solucao-cdb-frontend-container >nul 2>&1

echo === [BUILD DO FRONTEND] ===
docker build -t solucao-cdb-frontend ./src/frontend

echo === [EXECUTANDO FRONTEND] ===
docker run -d ^
 -p 4200:80 ^
 --name solucao-cdb-frontend-container ^
 --link solucao-cdb-api-container ^
 solucao-cdb-frontend

echo === [CONTAINER FRONT EM EXECUÇÃO] ===
docker ps