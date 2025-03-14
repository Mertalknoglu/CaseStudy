# 1. Aşama: Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Proje dosyalarını kopyala ve restore yap
COPY ProductService/Product.Api/Product.Api.csproj ProductService/Product.Api/
COPY ProductService/Product.Application/Product.Application.csproj ProductService/Product.Application/
COPY ProductService/Product.Domain/Product.Domain.csproj ProductService/Product.Domain/
COPY ProductService/Product.Infrastructure/Product.Infrastructure.csproj ProductService/Product.Infrastructure/

WORKDIR /app/ProductService/Product.Api
RUN dotnet restore

# Uygulama kodlarını kopyala
COPY ProductService/ ProductService/
RUN dotnet publish ProductService/Product.Api/Product.Api.csproj -c Release -o /app/publish

# 2. Aşama: Çalıştırma aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 4000
ENV ASPNETCORE_URLS=http://+:4000
ENTRYPOINT ["dotnet", "Product.Api.dll"]
