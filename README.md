
# Project Management System

## Table of Contents
- [Overview](#overview)
- [Architecture](#architecture)
- [Modules](#modules)
- [Features](#features)
- [Technologies](#technologies)
- [Setup and Installation](#setup-and-installation)
- [Running the Application](#running-the-application)

## Overview
Project Management System built with ASP.NET Core designed to handle project tasks, assignments, and user collaboration. This system is similar to tools like Trello, focusing on task assignments, user roles, and real-time collaboration.

## Architecture
The system is designed using the CQRS (Command Query Responsibility Segregation) and MediatR patterns to separate write and read operations, promoting scalability and maintainability:
- **Command Layer**: Handles write operations, such as creating, updating, and deleting resources.
- **Query Layer**: Handles read operations, focusing on fetching data efficiently.
- **MediatR Layer**: Acts as a communication bridge between commands/queries and handlers, enabling clean and decoupled code.
- **Handler Layer**: Contains the business logic, handling requests coming from the MediatR layer. Each command or query has its dedicated handler.
- **Repository Layer**: Manages data access, typically interacting with the database using Entity Framework Core.
- **Controller Layer**: Handles incoming HTTP requests and directs them to the appropriate command/query via MediatR.

## Modules
- **Projects**: Create and manage projects.
- **Tasks**: Create and assign tasks to users.
- **User Management**: Role-based access control.

## Features
- **CQRS Pattern**: Separates write and query operations, making the system more scalable and easier to maintain.
- **Mediator Pattern**: Facilitates communication between classes without direct references, improving code flexibility.
- **User Authentication and Authorization**: Secure access with role-based control.
- **Confirmation Email System**: Sends confirmation emails with account activation links.
- **Reset Password System**: Encrypts and decrypts reset password codes sent via email.
- **Role-based Permissions**: Users can be assigned different roles with specific access levels.
- **Task Assignment**: Assign users to tasks within projects.
- **Real-time Notifications**: Users receive notifications for task updates and assignments.
- **Task Status Tracking**: Track task progress through various stages.
- **Pagination Schema**: Efficiently organizes large responses.
- **Readable Response Schema**: Provides detailed response structures with status codes, operation status, data, and metadata.
- **Dependency Injection**: Manages dependencies efficiently.
- **SOLID Principles**: Follows SOLID principles for robust and maintainable code.

## Technologies
- **ASP.NET Core 8**: The main framework for building the Web API.
- **Entity Framework Core**: For handling database operations.
- **AutoFac**: Dependency injection management.
- **AutoMapper**: Automates mapping between ViewModels and DTOs.
- **Hangfire**: Manages background processing and task scheduling.
- **MediatR**: Implements the mediator pattern for clean architecture.
- **Serilog**: Provides structured logging and application diagnostics.
- **SQL Server**: Database management.
- **Swagger**: Automatically generates API documentation.
- **Fluent Validation**: Provides an easy way to set up validations with custom error handling.

## Setup and Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/atbasyoni/ProjectManagement.git
   cd ProjectManagement
2. Restore the .NET dependencies:
    ```bash
    dotnet restore
3. Configure the database connection string in appsettings.json:
    ```bash
    Server=.;Database=ProjectManagement; Trusted_Connection= True; TrustServerCertificate= True;encrypt=false
    
## Database Migration
- Run the following command to apply the latest database migrations:
    ```bash
    dotnet ef database update

## Running the Application
- To run the application locally:
    ```bash
    dotnet run
