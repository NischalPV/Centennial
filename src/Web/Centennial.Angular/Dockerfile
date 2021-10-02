#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
ARG NODE_IMAGE=node:14.18

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
RUN apt-get update && apt-get install -y curl
RUN curl -sL https://deb.nodesource.com/setup_14.x |  bash -
RUN apt-get install -y nodejs
WORKDIR /app
EXPOSE 80

FROM ${NODE_IMAGE} as node-build
WORKDIR "/web"
COPY "src/Web/Centennial.Angular/ClientApp/package.json" .
COPY "src/Web/Centennial.Angular/ClientApp/package-lock.json" .
COPY "src/Web/Centennial.Angular/ClientApp" .
RUN npm install && npm run build:prod

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
RUN apt-get update && apt-get install -y curl
RUN curl -sL https://deb.nodesource.com/setup_14.x |  bash -
RUN apt-get install -y nodejs

WORKDIR /Centennial

COPY "Centennial.sln" "Centennial.sln"

COPY "src/BuildingBlocks/EventBus/EventBus/EventBus.csproj" "src/BuildingBlocks/EventBus/EventBus/EventBus.csproj"
COPY "src/BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ/EventBusRabbitMQ.csproj" "src/BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ/EventBusRabbitMQ.csproj"
COPY "src/BuildingBlocks/EventBus/IntegrationEventLogEF/IntegrationEventLogEF/IntegrationEventLogEF.csproj" "src/BuildingBlocks/EventBus/IntegrationEventLogEF/IntegrationEventLogEF/IntegrationEventLogEF.csproj"

COPY "src/Services/Centennial.Identity/Centennial.Identity.csproj" "src/Services/Centennial.Identity/Centennial.Identity.csproj"
COPY "src/Services/Centennial.Api/Centennial.Api.csproj" "src/Services/Centennial.Api/Centennial.Api.csproj"
COPY "src/Web/Centennial.Angular/Centennial.Angular.csproj" "src/Web/Centennial.Angular/Centennial.Angular.csproj"
# COPY "src/Web/Centennial.Angular/ClientApp/package.json" "src/Web/Centennial.Angular/ClientApp/package.json"
# COPY "src/Web/Centennial.Angular/ClientApp/package-lock.json" "src/Web/Centennial.Angular/ClientApp/package-lock.json"


COPY "docker-compose.dcproj" "docker-compose.dcproj"

COPY "NuGet.config" "NuGet.config"
RUN dotnet restore "Centennial.sln"

COPY . .
COPY --from=node-build /web /Centennial/src/Web/Centennial.Angular/ClientApp
WORKDIR "/Centennial/src/Web/Centennial.Angular"
RUN dotnet build "Centennial.Angular.csproj" -c Release -o /app/build
# RUN dotnet publish "Centennial.Angular.csproj" -c Release -o /app/publish

FROM build AS publish
COPY --from=node-build /web /Centennial/src/Web/Centennial.Angular/ClientApp
# COPY --from=node-build /web/package.json /Centennial/src/Web/Centennial.Angular/ClientApp/package.json
RUN dotnet publish "Centennial.Angular.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Centennial.Angular.dll"]
