#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /Centennial

COPY "Centennial.sln" "Centennial.sln"

COPY "src/BuildingBlocks/EventBus/EventBus/EventBus.csproj" "src/BuildingBlocks/EventBus/EventBus/EventBus.csproj"
COPY "src/BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ/EventBusRabbitMQ.csproj" "src/BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ/EventBusRabbitMQ.csproj"
COPY "src/BuildingBlocks/EventBus/IntegrationEventLogEF/IntegrationEventLogEF/IntegrationEventLogEF.csproj" "src/BuildingBlocks/EventBus/IntegrationEventLogEF/IntegrationEventLogEF/IntegrationEventLogEF.csproj"

COPY "src/Services/Centennial.Identity/Centennial.Identity.csproj" "src/Services/Centennial.Identity/Centennial.Identity.csproj"
COPY "src/Services/Centennial.Api/Centennial.Api.csproj" "src/Services/Centennial.Api/Centennial.Api.csproj"
COPY "src/Web/Centennial.Angular/Centennial.Angular.csproj" "src/Web/Centennial.Angular/Centennial.Angular.csproj"

COPY "docker-compose.dcproj" "docker-compose.dcproj"

COPY "NuGet.config" "NuGet.config"
RUN dotnet restore "Centennial.sln"

COPY . .
WORKDIR "/Centennial/src/Services/Centennial.Identity"
RUN dotnet build "Centennial.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Centennial.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Centennial.Identity.dll"]
