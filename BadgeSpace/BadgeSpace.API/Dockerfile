#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BadgeSpace.API/BadgeSpace.API.csproj", "BadgeSpace.API/"]
COPY ["BadgeSpace.Infra/BadgeSpace.Infra.csproj", "BadgeSpace.Infra/"]
COPY ["BadgeSpace.Interfaces/BadgeSpace.Interfaces.csproj", "BadgeSpace.Interfaces/"]
COPY ["BadgeSpace.Models/BadgeSpace.Models.csproj", "BadgeSpace.Models/"]
COPY ["BadgeSpace.Repository/BadgeSpace.Repository.csproj", "BadgeSpace.Repository/"]
COPY ["BadgeSpace.Services/BadgeSpace.Services.csproj", "BadgeSpace.Services/"]
RUN dotnet restore "BadgeSpace.API/BadgeSpace.API.csproj"
COPY . .
WORKDIR "/src/BadgeSpace.API"
RUN dotnet build "BadgeSpace.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BadgeSpace.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BadgeSpace.API.dll"]