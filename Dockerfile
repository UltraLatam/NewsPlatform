# Etapa base: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["NewsPlatformApp/NewsPlatformApp.csproj", "NewsPlatformApp/"]
RUN dotnet restore "NewsPlatformApp/NewsPlatformApp.csproj"
COPY . .
WORKDIR "/src/NewsPlatformApp"
RUN dotnet build "NewsPlatformApp.csproj" -c Release -o /app/build

# Etapa de publish
FROM build AS publish
RUN dotnet publish "NewsPlatformApp.csproj" -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsPlatformApp.dll"]
