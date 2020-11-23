FROM microsoft/dotnet:3.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:3.1-sdk AS build
WORKDIR /src
COPY VikleAPIMS.Web/VikleAPIMS.Web.csproj VikleAPIMS.Web/
RUN dotnet restore VikleAPIMS.Web/VikleAPIMS.Web.csproj
COPY . .
WORKDIR /src/VikleAPIMS.Web
RUN dotnet build VikleAPIMS.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish VikleAPIMS.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "VikleAPIMS.Web.dll"]