FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/NoteWeb.API/NoteWeb.API.csproj", "src/NoteWeb.API/"]
COPY ["src/NoteWeb.Core/NoteWeb.Core.csproj", "src/NoteWeb.Core/"]
COPY ["src/NoteWeb.Persistence/NoteWeb.Persistence.csproj", "src/NoteWeb.Persistence/"]
RUN dotnet restore "src/NoteWeb.API/NoteWeb.API.csproj"
COPY . .
WORKDIR "/src/src/NoteWeb.API"
RUN dotnet build "NoteWeb.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NoteWeb.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NoteWeb.API.dll"]
