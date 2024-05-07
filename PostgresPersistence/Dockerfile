FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PostgresPersistence/PostgresPersistence.csproj", "PostgresPersistence/"]
RUN dotnet restore "PostgresPersistence/PostgresPersistence.csproj"
COPY . .
WORKDIR "/src/PostgresPersistence"
RUN dotnet build "PostgresPersistence.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PostgresPersistence.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PostgresPersistence.dll"]
