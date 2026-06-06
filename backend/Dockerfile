# ──────────────────────────────────────────────────────────────
# Portfolio API — multi-stage Dockerfile
# Build context: backend/
# ──────────────────────────────────────────────────────────────

# ── SDK stage ───────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Layer restore: copy project files first so NuGet packages cache
COPY Portfolio.slnx .
COPY Directory.Build.props .
COPY Directory.Packages.props .

COPY src/Api/Portfolio.Api.csproj             src/Api/
COPY src/Application/Portfolio.Application.csproj src/Application/
COPY src/Domain/Portfolio.Domain.csproj        src/Domain/
COPY src/Infrastructure/Portfolio.Infrastructure.csproj src/Infrastructure/

RUN dotnet restore src/Api/Portfolio.Api.csproj

# Copy remaining source and publish (restore + publish in one step to avoid cache mismatch)
COPY src/ src/

RUN dotnet publish src/Api/Portfolio.Api.csproj \
    -c Release \
    -o /app \
    -p:DebugType=None \
    -p:DebugSymbols=false

# ── Runtime stage ───────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app ./

# Non-root user for security
USER app

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Portfolio.Api.dll"]
