FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WatchIt.WebAPI/WatchIt.WebAPI.csproj", "WatchIt.WebAPI/"]
COPY ["WatchIt.Database/WatchIt.Database.csproj", "WatchIt.Database/"]
COPY ["WatchIt.DTO/WatchIt.DTO.csproj", "WatchIt.DTO/"]
RUN dotnet restore "WatchIt.WebAPI/WatchIt.WebAPI.csproj"
COPY . .
WORKDIR "/src/WatchIt.WebAPI"
RUN dotnet build "WatchIt.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WatchIt.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WatchIt.WebAPI.dll"]
