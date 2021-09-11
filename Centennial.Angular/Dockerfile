#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
RUN apt-get update && apt-get install -y curl
RUN curl -sL https://deb.nodesource.com/setup_14.x |  bash -
RUN apt-get install -y nodejs
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
RUN apt-get update && apt-get install -y curl
RUN curl -sL https://deb.nodesource.com/setup_14.x |  bash -
RUN apt-get install -y nodejs
WORKDIR /src
COPY ["Centennial.Angular/Centennial.Angular.csproj", "Centennial.Angular/"]
RUN dotnet restore "Centennial.Angular/Centennial.Angular.csproj"
COPY . .
WORKDIR "/src/Centennial.Angular"
RUN dotnet build "Centennial.Angular.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Centennial.Angular.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Centennial.Angular.dll"]
