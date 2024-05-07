FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Timers/Timers.csproj", "Timers/"]
RUN dotnet restore "Timers/Timers.csproj"
COPY . .
WORKDIR "/src/Timers"
RUN dotnet build "Timers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Timers.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Timers.dll"]
