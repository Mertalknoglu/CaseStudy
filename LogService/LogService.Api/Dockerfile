# 1. A�ama: Build a�amas�
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Proje dosyalar�n� kopyala ve restore yap
COPY LogService/LogService.Api/LogService.Api.csproj LogService/LogService.Api/
WORKDIR /app/LogService/LogService.Api
RUN dotnet restore

# Uygulama kodlar�n� kopyala
COPY LogService/ LogService/
RUN dotnet publish LogService/LogService.Api/LogService.Api.csproj -c Release -o /app/publish

# 2. A�ama: �al��t�rma a�amas�
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 3000
ENV ASPNETCORE_URLS=http://0.0.0.0:2000
ENTRYPOINT ["dotnet", "LogService.Api.dll"]
