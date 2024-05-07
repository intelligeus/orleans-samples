FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["OrleansDashboard/OrleansDashboard.csproj", "OrleansDashboard/"]
RUN dotnet restore "OrleansDashboard/OrleansDashboard.csproj"
COPY . .
WORKDIR "/src/OrleansDashboard"
RUN dotnet build "OrleansDashboard.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrleansDashboard.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrleansDashboard.dll"]
