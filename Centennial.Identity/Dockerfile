#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Centennial.Identity/Centennial.Identity.csproj", "Centennial.Identity/"]
RUN dotnet restore "Centennial.Identity/Centennial.Identity.csproj"
COPY . .
WORKDIR "/src/Centennial.Identity"
RUN dotnet build "Centennial.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Centennial.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Centennial.Identity.dll"]
