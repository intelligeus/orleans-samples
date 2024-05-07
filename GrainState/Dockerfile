FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GrainState/GrainState.csproj", "GrainState/"]
RUN dotnet restore "GrainState/GrainState.csproj"
COPY . .
WORKDIR "/src/GrainState"
RUN dotnet build "GrainState.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GrainState.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrainState.dll"]
