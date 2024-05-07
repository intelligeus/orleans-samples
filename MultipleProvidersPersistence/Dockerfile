FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MultipleProvidersPersistence/MultipleProvidersPersistence.csproj", "MultipleProvidersPersistence/"]
RUN dotnet restore "MultipleProvidersPersistence/MultipleProvidersPersistence.csproj"
COPY . .
WORKDIR "/src/MultipleProvidersPersistence"
RUN dotnet build "MultipleProvidersPersistence.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MultipleProvidersPersistence.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MultipleProvidersPersistence.dll"]
