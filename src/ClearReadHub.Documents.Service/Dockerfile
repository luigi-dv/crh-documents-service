FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ClearReadHub.Documents/ClearReadHub.Documents.csproj", "ClearReadHub.Documents/"]
RUN dotnet restore "ClearReadHub.Documents/ClearReadHub.Documents.csproj"
COPY . .
WORKDIR "/src/ClearReadHub.Documents"
RUN dotnet build "ClearReadHub.Documents.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ClearReadHub.Documents.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClearReadHub.Documents.dll"]
