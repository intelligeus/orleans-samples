FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["OrleansPersistence/OrleansPersistence.csproj", "OrleansPersistence/"]
RUN dotnet restore "OrleansPersistence/OrleansPersistence.csproj"
COPY . .
WORKDIR "/src/OrleansPersistence"
RUN dotnet build "OrleansPersistence.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrleansPersistence.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrleansPersistence.dll"]
