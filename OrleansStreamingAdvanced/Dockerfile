FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OrleansStreamingAdvanced/OrleansStreamingAdvanced.csproj", "OrleansStreamingAdvanced/"]
RUN dotnet restore "OrleansStreamingAdvanced/OrleansStreamingAdvanced.csproj"
COPY . .
WORKDIR "/src/OrleansStreamingAdvanced"
RUN dotnet build "OrleansStreamingAdvanced.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OrleansStreamingAdvanced.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrleansStreamingAdvanced.dll"]
