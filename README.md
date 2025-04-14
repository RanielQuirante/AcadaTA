# AdacaTA

This project showcases a .NET 8 Web API using Entity Framework Core (Code First), AutoMapper, and Repository Pattern. It also contains unit tests for both controllers and services.

## Getting Started

### Prerequisites
- .NET 8 SDK installed on your machine.
- MSSQL server set up for the database.
- EF tools (dotnet-ef) installed globally:

### Setup

1. Clone the repository to your local machine:
   ```sh
   git clone https://github.com/RanielQuirante/AdacaTA.git

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


### Running the Application
1. Open the solution in Visual Studio.
2. Set the WebApi project as the startup project.
3. Run the application.

## Technologies Used

- .NET 8.0
- AutoMapper
- MSSQL
- Entity Framework Core (EF Core) - Code-First Approach
- Repository Pattern

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
