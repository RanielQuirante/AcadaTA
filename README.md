# AcadaTA
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)](https://github.com/RanielQuirante/AcadaTA/actions/runs/14457371023/job/40543367055)
[![Static Badge](https://img.shields.io/badge/release-stable_tag_2504.0.0-blue)](https://github.com/RanielQuirante/AcadaTA/releases/tag/2504.0.0)

This project showcases a .NET 8 Web API using Entity Framework Core (Code First), AutoMapper, and Repository Pattern. It also contains unit tests for both controllers and services.

## Getting Started

### Prerequisites
- .NET 8 SDK installed on your machine.
- MSSQL server set up for the database.
- EF tools (dotnet-ef) installed globally:

### Setup

1. Clone the repository to your local machine:
   ```sh
   git clone https://github.com/RanielQuirante/AcadaTA.git

2. Open the solution in Visual Studio.

3. Set the AcadaTA.WebApi as the startup project.

4. Open your Developer PowerShell, cd to your repository project e.g {folderPath}\"NameOfYourChosenFolder"

5. Enter `dotnet ef --version` make sure it is version "9.0.4".

6. If not, enter this command `dotnet tool update --global dotnet-ef` to update your ef version

     ![image](https://github.com/user-attachments/assets/2ac7c34d-752d-4a3c-b552-e31e96cba36d)

7. Enter the `dotnet ef --version` again to make sure you are now using "9.0.4" version.

8. After that enter `dotnet ef database update -p AcadaTA.Infrastructure -s AcadaTA.WebApi` this should create the database for you automatically with the table named "Products"
   
    ![image](https://github.com/user-attachments/assets/4acc2ec4-e976-4e9c-bcc5-9570814594c2)

    ![image](https://github.com/user-attachments/assets/26378473-fc0d-4096-a65f-d2eab9b3a316)


# Running the Application

You can run the AcadaTA Web API in two different ways: locally on your machine or using Docker. Choose the one that best fits your development workflow.

## Option 1: Local Machine Setup

### Clone the Repository & Open Solution
Clone the repo and open the solution in Visual Studio.

### Set Startup Project
Set `AcadaTA.WebApi` as the startup project.

### Developer PowerShell
Open your Developer PowerShell, navigate to your project folder, and verify your EF Core version:

```sh
dotnet ef --version
```

If it’s not version 9.0.4, update it:

```sh
dotnet tool update --global dotnet-ef
```

Verify the version again.

### Database Setup
Run the following command to create the database and necessary tables (like “Products”):

```sh
dotnet ef database update -p AcadaTA.Infrastructure -s AcadaTA.WebApi
```

### Run the Application
Simply run the application from Visual Studio.

## Option 2: Docker Setup

If you prefer containerization, make sure Docker is installed. Then you can run the Web API using the command below:

```sh
docker run -d --name acadata-webapi -p 8080:8080 \
  -e ASPNETCORE_ENVIRONMENT=Development \
  -e ConnectionStrings__DefaultConnection="Server=host.docker.internal,1433;Database=YOUR_DATABASE_NAME;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;TrustServerCertificate=True;" \
  ghcr.io/ranielquirante/acadata-webapi:latest
```

## Technologies Used

- .NET 8.0
- AutoMapper
- MSSQL
- Entity Framework Core (EF Core) - Code-First Approach
- Repository Pattern
- Github Workflow (CI/CD) with Docker

## Project Structure

- **AcadaTA.WebApi**
  - Controllers, Mapping Profiles & startup configuration.

- **AcadaTA.Infrastructure**
  - Contains Entity Framework Core context and migrations setup.

- **AcadaTA.Models**
  - Holds entity classes and DTOs.

- **AcadaTA.Repositories**
  - Implements the Repository Pattern to handle data access.

- **AcadaTA.Services**
  - Business logic that is contained from service layer.

- **UnitTests**
  - AcadaTA.Controllers.UnitTests: Tests for the controller layer.
  - AcadaTA.Services.UnitTests: Tests for the service layer.
