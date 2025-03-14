# 1. Aþama: Build aþamasý
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Proje dosyalarýný kopyala ve restore yap
COPY AuthService/Auth.Api/Auth.Api.csproj AuthService/Auth.Api/
WORKDIR /app/AuthService/Auth.Api
RUN dotnet restore

# Uygulama kodlarýný kopyala
COPY AuthService/ AuthService/
RUN dotnet publish AuthService/Auth.Api/Auth.Api.csproj -c Release -o /app/publish

# 2. Aþama: Çalýþtýrma aþamasý
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 2000
ENV ASPNETCORE_URLS=http://0.0.0.0:2000
ENTRYPOINT ["dotnet", "Auth.Api.dll"]
