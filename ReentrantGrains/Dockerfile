FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ReentrantGrains/ReentrantGrains.csproj", "ReentrantGrains/"]
RUN dotnet restore "ReentrantGrains/ReentrantGrains.csproj"
COPY . .
WORKDIR "/src/ReentrantGrains"
RUN dotnet build "ReentrantGrains.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ReentrantGrains.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReentrantGrains.dll"]
