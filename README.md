# universal-identity-api
back-end do Universal Identity
[front (mobile/web)](https://github.com/andrelara2002/universal-identity)

Publicação: [universal-identity-api](https://universal-identity-back.azurewebsites.net/)


# Status Pipeline
[![Build and deploy ASP.Net Core app to Azure Web App - universal-identity-back](https://github.com/joseBarreto/universal-identity-api/actions/workflows/main_universal-identity-back.yml/badge.svg)](https://github.com/joseBarreto/universal-identity-api/actions/workflows/main_universal-identity-back.yml)

# Config
Renomei o arquivo `appsettings.example` para `appsettings`
Adicione os valores das variaveis.

# Add Migration
Navegar ate a pasta `cd ./UniversalIdentity.Infra.Data`
Executar o seguinte comando
`dotnet ef migrations add {NOME_MIGRATION} --startup-project ..\UniversalIdentity.Application\UniversalIdentity.Application.csproj`



# Update Database
Navegar ate a pasta `cd ./UniversalIdentity.Infra.Data`
Executar o seguinte comando
`dotnet ef database update --startup-project ..\UniversalIdentity.Application\UniversalIdentity.Application.csproj`


# HealthCheks
- UI:     /healthchecks-ui
- api:    /healthchecks-ui-api


# Configuração da Azure
Para rodar na Azure em um Service APP Linux, é preciso adicionar o comando `dotnet UniversalIdentity.Application.dll`, dentro da aba configurações do Service App.