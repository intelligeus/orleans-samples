FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GPSDataDriver/GPSDataDriver.csproj", "GPSDataDriver/"]
RUN dotnet restore "GPSDataDriver/GPSDataDriver.csproj"
COPY . .
WORKDIR "/src/GPSDataDriver"
RUN dotnet build "GPSDataDriver.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GPSDataDriver.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GPSDataDriver.dll"]
