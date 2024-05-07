FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GrainLifeCycle/GrainLifeCycle.csproj", "GrainLifeCycle/"]
RUN dotnet restore "GrainLifeCycle/GrainLifeCycle.csproj"
COPY . .
WORKDIR "/src/GrainLifeCycle"
RUN dotnet build "GrainLifeCycle.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GrainLifeCycle.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrainLifeCycle.dll"]
