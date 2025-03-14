# 1. Aþama: Build aþamasý
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Proje dosyalarýný kopyala ve restore yap
COPY LogService/LogService.Api/LogService.Api.csproj LogService/LogService.Api/
WORKDIR /app/LogService/LogService.Api
RUN dotnet restore

# Uygulama kodlarýný kopyala
COPY LogService/ LogService/
RUN dotnet publish LogService/LogService.Api/LogService.Api.csproj -c Release -o /app/publish

# 2. Aþama: Çalýþtýrma aþamasý
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 3000
ENV ASPNETCORE_URLS=http://0.0.0.0:2000
ENTRYPOINT ["dotnet", "LogService.Api.dll"]
