FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DynamoDBPersistence/DynamoDBPersistence.csproj", "DynamoDBPersistence/"]
RUN dotnet restore "DynamoDBPersistence/DynamoDBPersistence.csproj"
COPY . .
WORKDIR "/src/DynamoDBPersistence"
RUN dotnet build "DynamoDBPersistence.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DynamoDBPersistence.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DynamoDBPersistence.dll"]
