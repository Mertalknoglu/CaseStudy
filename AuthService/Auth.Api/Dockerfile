# 1. A�ama: Build a�amas�
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Proje dosyalar�n� kopyala ve restore yap
COPY AuthService/Auth.Api/Auth.Api.csproj AuthService/Auth.Api/
WORKDIR /app/AuthService/Auth.Api
RUN dotnet restore

# Uygulama kodlar�n� kopyala
COPY AuthService/ AuthService/
RUN dotnet publish AuthService/Auth.Api/Auth.Api.csproj -c Release -o /app/publish

# 2. A�ama: �al��t�rma a�amas�
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 2000
ENV ASPNETCORE_URLS=http://0.0.0.0:2000
ENTRYPOINT ["dotnet", "Auth.Api.dll"]
