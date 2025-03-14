# 1. Aþama: Build aþamasý
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ApiGateway/ApiGateway/ApiGateway.csproj ApiGateway/ApiGateway/

WORKDIR /app/ApiGateway/ApiGateway
RUN dotnet restore


# Copy the rest of the application code
COPY ApiGateway/ ApiGateway/

RUN dotnet publish ApiGateway/ApiGateway/ApiGateway.csproj -c Release -o /app/publish



# 2. Aþama: Çalýþtýrma aþamasý
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 1000
ENV ASPNETCORE_URLS=http://+:1000
ENTRYPOINT ["dotnet", "ApiGateway.dll"]
