# Stage 1: Build and run tests
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files first (better caching)
COPY *.sln . 
COPY AcadaTA.Infrastructure/AcadaTA.Infrastructure.csproj AcadaTA.Infrastructure/
COPY AcadaTA.Models/AcadaTA.Models.csproj AcadaTA.Models/
COPY AcadaTA.Repositories/AcadaTA.Repositories.csproj AcadaTA.Repositories/
COPY AcadaTA.Services/AcadaTA.Services.csproj AcadaTA.Services/
COPY AcadaTA.WebApi/AcadaTA.WebApi.csproj AcadaTA.WebApi/
COPY UnitTests UnitTests/

# Restore packages
RUN dotnet restore

# Copy everything after restore step
COPY . .

# Run unit tests (optional: you can skip this step in final production build)
RUN dotnet test AcadaTA.sln --no-restore --verbosity normal

# Publish app
RUN dotnet publish AcadaTA.WebApi/AcadaTA.WebApi.csproj -c Release -o /app/publish

# Stage 2: Final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Expose port (ASP.NET Core defaults to 8080 inside containers in .NET 8)
EXPOSE 8080

# Run the app
ENTRYPOINT ["dotnet", "AcadaTA.WebApi.dll"]