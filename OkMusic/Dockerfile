#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["OkMusic/OkMusic.csproj", "OkMusic/"]
RUN dotnet restore "OkMusic/OkMusic.csproj"
COPY . .
WORKDIR "/src/OkMusic"
RUN dotnet build "OkMusic.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OkMusic.csproj" -c Release -o /app/publish

# Ç°¶Ë¹¹½¨
FROM node:12-alpine as build-fe
WORKDIR /src
COPY OkMusicApp/package.json ./
COPY OkMusicApp/yarn.lock ./

RUN yarn
COPY ResearchIntegritySiteApp ./
RUN yarn build

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build-fe /src/dist ./wwwroot
ENTRYPOINT ["dotnet", "OkMusic.dll"]