# PatientTracking.API

## Project Overview
PatientTracking.API is a simple RESTful Web API application developed to manage patient information.  
Currently, it uses in-memory data storage. A SQL Server database integration is planned for future versions.

## Technologies Used
- .NET 8 (ASP.NET Core Web API)
- Entity Framework Core (planned)
- xUnit (Unit Testing)
- Moq (Mock Testing)
- Git & GitHub

## Project Features
- Create, read, update, and delete patient records (CRUD)
- Layered architecture (Controller, Service, Data, DTO, Models)
- Basic unit tests with xUnit and Moq

## API Endpoints
| Method | Route               | Description             |
| ------ | ------------------- | ----------------------- |
| GET    | `/api/patient`      | Retrieve all patients   |
| GET    | `/api/patient/{id}` | Retrieve a patient by ID|
| POST   | `/api/patient`      | Add a new patient       |
| PUT    | `/api/patient/{id}` | Update patient details  |
| DELETE | `/api/patient/{id}` | Delete a patient        |

## Getting Started
1. Clone the repository:
   ```bash
   git clone https://github.com/ArslanTugba/PatientTracking.API.git
