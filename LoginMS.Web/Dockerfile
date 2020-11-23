FROM microsoft/dotnet:3.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:3.1-sdk AS build
WORKDIR /src
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