FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["LabPort.Backend.API/LabPort.Backend.API.csproj", "LabPort.Backend.API/"]
COPY ["LabPort.Backend.Application/LabPort.Backend.Application.csproj", "LabPort.Backend.Application/"]
COPY ["LabPort.Backend.Contracts/LabPort.Backend.Contracts.csproj", "LabPort.Backend.Contracts/"]
COPY ["LabPort.Backend.Domain/LabPort.Backend.Domain.csproj", "LabPort.Backend.Domain/"]
COPY ["LabPort.Backend.Infrastructure/LabPort.Backend.Infrastructure.csproj", "LabPort.Backend.Infrastructure/"]

RUN dotnet restore "LabPort.Backend.API/LabPort.Backend.API.csproj"

COPY . .

WORKDIR "/src/LabPort.Backend.API"

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LabPort.Backend.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "LabPort.Backend.API.dll"]