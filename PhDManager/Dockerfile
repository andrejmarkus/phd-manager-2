﻿# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER root
RUN apt-get update && \
    apt-get install --upgrade -y libldap-2.5-0 && \
    arch=$(dpkg --print-architecture) && \
    if [ "$arch" = "amd64" ]; then \
        ln -s /usr/lib/x86_64-linux-gnu/libldap-2.5.so.0 /usr/lib/libldap.so.2; \
    elif [ "$arch" = "arm64" ]; then \
        ln -s /usr/lib/aarch64-linux-gnu/libldap-2.5.so.0 /usr/lib/libldap.so.2; \
    else \
        echo "Unsupported architecture: $arch" && exit 1; \
    fi
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PhDManager/PhDManager.csproj", "PhDManager/"]
RUN dotnet restore "./PhDManager/PhDManager.csproj"
COPY . .
WORKDIR "/src/PhDManager"
RUN dotnet build "./PhDManager.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PhDManager.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhDManager.dll"]