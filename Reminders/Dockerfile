FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Reminders/Reminders.csproj", "Reminders/"]
RUN dotnet restore "Reminders/Reminders.csproj"
COPY . .
WORKDIR "/src/Reminders"
RUN dotnet build "Reminders.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Reminders.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Reminders.dll"]
