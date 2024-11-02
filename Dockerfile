# Usar uma imagem base oficial do .NET para build e publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copiar os arquivos de projeto e restaurar as dependências
COPY . ./
RUN dotnet restore

# Publicar o projeto em modo Release para uma pasta chamada out
RUN dotnet publish -c Release -o out

# Usar uma imagem base menor para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expôr a porta na qual a aplicação rodará
EXPOSE 80

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "blog_c#.dll"]
