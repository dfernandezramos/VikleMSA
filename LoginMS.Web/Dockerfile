FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["nuget.config", ""]
COPY ["MessageBroker.1.0.0.nupkg", ""]
COPY LoginMS.Web/LoginMS.Web.csproj LoginMS.Web/
RUN dotnet restore LoginMS.Web/LoginMS.Web.csproj
COPY . .
WORKDIR /src/LoginMS.Web
RUN dotnet build LoginMS.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish LoginMS.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LoginMS.Web.dll"]