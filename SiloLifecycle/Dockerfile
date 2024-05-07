FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SiloLifecycle/SiloLifecycle.csproj", "SiloLifecycle/"]
RUN dotnet restore "SiloLifecycle/SiloLifecycle.csproj"
COPY . .
WORKDIR "/src/SiloLifecycle"
RUN dotnet build "SiloLifecycle.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SiloLifecycle.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SiloLifecycle.dll"]
