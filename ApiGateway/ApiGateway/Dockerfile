# 1. A�ama: Build a�amas�
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ApiGateway/ApiGateway/ApiGateway.csproj ApiGateway/ApiGateway/

WORKDIR /app/ApiGateway/ApiGateway
RUN dotnet restore


# Copy the rest of the application code
COPY ApiGateway/ ApiGateway/

RUN dotnet publish ApiGateway/ApiGateway/ApiGateway.csproj -c Release -o /app/publish



# 2. A�ama: �al��t�rma a�amas�
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 1000
ENV ASPNETCORE_URLS=http://+:1000
ENTRYPOINT ["dotnet", "ApiGateway.dll"]
