#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN apt-get update -yq && apt-get upgrade -yq && apt-get install -yq curl git nano
RUN curl -sL https://deb.nodesource.com/setup_20.x | bash - && apt-get install -yq nodejs build-essential

ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Vouzamo.ScoreStream.WebApp/Vouzamo.ScoreStream.WebApp.csproj", "Vouzamo.ScoreStream.WebApp/"]
RUN dotnet restore "./Vouzamo.ScoreStream.WebApp/Vouzamo.ScoreStream.WebApp.csproj"
COPY . .
WORKDIR "/src/Vouzamo.ScoreStream.WebApp"
RUN dotnet build "./Vouzamo.ScoreStream.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Vouzamo.ScoreStream.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vouzamo.ScoreStream.WebApp.dll"]