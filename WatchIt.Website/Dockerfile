FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WatchIt.Website/WatchIt.Website.csproj", "WatchIt.Website/"]
COPY ["WatchIt.DTO/WatchIt.DTO.csproj", "WatchIt.DTO/"]
COPY ["WatchIt.Database/WatchIt.Database.csproj", "WatchIt.Database/"]
RUN dotnet restore "WatchIt.Website/WatchIt.Website.csproj"
COPY . .
WORKDIR "/src/WatchIt.Website"
RUN dotnet build "WatchIt.Website.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WatchIt.Website.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WatchIt.Website.dll"]
