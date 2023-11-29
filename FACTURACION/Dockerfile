#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FACTURACION/FACTURACION.csproj", "FACTURACION/"]
COPY ["acme-backend/acme-backend.csproj", "acme-backend/"]
COPY ["BusinessLayer/BusinessLayer.csproj", "BusinessLayer/"]
COPY ["DataAccessLayer/DataAccessLayer.csproj", "DataAccessLayer/"]
RUN dotnet restore "FACTURACION/FACTURACION.csproj"
COPY . .
WORKDIR "/src/FACTURACION"
RUN dotnet build "FACTURACION.csproj" -c Release -o /app/build

RUN apt-get update \
    && apt-get install -y libfontconfig1 libgdiplus \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

RUN apt-get update && apt-get install -y ttf-mscorefonts-installer fontconfig libc6 libc6-dev libgtk2.0-0 libnss3 libatk-bridge2.0-0 libx11-xcb1 libxcb-dri3-0 libdrm-common libgbm1 libasound2 libappindicator3-1 libxrender1 libfontconfig1 libxshmfence1
RUN chmod 777 .

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DataExportService.csproj", "DataExportService/"]
RUN dotnet restore "DataExportService/DataExportService.csproj"

WORKDIR "/src/DataExportService"
COPY . .
RUN dotnet build "DataExportService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DataExportService.csproj" -c Release -o /app/publish

FROM build AS publish
RUN dotnet publish "FACTURACION.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FACTURACION.dll"]