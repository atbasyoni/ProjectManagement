
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
The system is designed using a layered architecture:
- **Repository Layer**: Handles data access using Entity Framework Core.
- **Service Layer**: Business logic.
- **Mediator Layer**: Acts as a communication bridge between controllers and services.
- **Controller Layer**: Handles HTTP requests and sends responses.

## Modules
- **Projects**: Create and manage projects.
- **Tasks**: Create and assign tasks to users.
- **User Management**: Role-based access control.

## Features
- **User Authentication and Authorization**: Secure access with role-based control.
- **Task Assignment**: Assign users to tasks within projects.
- **Real-time Notifications**: Users receive notifications for task updates and assignments.
- **Task Status Tracking**: Track task progress through various stages.
- **Role-based Permissions**: Users can be assigned different roles with specific access levels.

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
    Server=.;Database=ProjectsManagement; Trusted_Connection= True; TrustServerCertificate= True;encrypt=false
    
## Database Migration
- Run the following command to apply the latest database migrations:
    ```bash
    dotnet ef database update

## Running the Application
- To run the application locally:
    ```bash
    dotnet run
