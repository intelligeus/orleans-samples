﻿FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GrainPlacementClient/GrainPlacementClient.csproj", "GrainPlacementClient/"]
RUN dotnet restore "GrainPlacementClient/GrainPlacementClient.csproj"
COPY . .
WORKDIR "/src/GrainPlacementClient"
RUN dotnet build "GrainPlacementClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GrainPlacementClient.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrainPlacementClient.dll"]
