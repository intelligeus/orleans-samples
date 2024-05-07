FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GrainPlacement/GrainPlacement.csproj", "GrainPlacement/"]
RUN dotnet restore "GrainPlacement/GrainPlacement.csproj"
COPY . .
WORKDIR "/src/GrainPlacement"
RUN dotnet build "GrainPlacement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GrainPlacement.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrainPlacement.dll"]
