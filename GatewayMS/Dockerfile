FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY GatewayMS/GatewayMS.csproj GatewayMS/
RUN dotnet restore GatewayMS/GatewayMS.csproj
COPY . .
WORKDIR /src/GatewayMS
RUN dotnet build GatewayMS.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish GatewayMS.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GatewayMS.dll"]