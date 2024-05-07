FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["HelloComputer/HelloComputer.csproj", "HelloComputer/"]
RUN dotnet restore "HelloComputer/HelloComputer.csproj"
COPY . .
WORKDIR "/src/HelloComputer"
RUN dotnet build "HelloComputer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HelloComputer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelloComputer.dll"]
