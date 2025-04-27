# PatientTracking.API

A RESTful Web API for patient tracking system.

## Technologies
- .NET 8
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger/OpenAPI

## Features
- User registration and login
- Patient management (CRUD)
- Medical history tracking
- Doctor comments
- AI predictions

## API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login and get token

### Patient Operations
- `GET /api/patient` - List all patients
- `GET /api/patient/{id}` - Get patient details
- `POST /api/patient` - Add new patient
- `PUT /api/patient/{id}` - Update patient
- `DELETE /api/patient/{id}` - Delete patient

## Setup
1. Clone the project
2. Update database connection in `appsettings.json`
3. Run database migrations: `dotnet ef database update`
4. Start the application: `dotnet run`

## API Documentation
Swagger UI: `https://localhost:44341/swagger`

## Security
- JWT authentication
- SHA256 password hashing
- Role-based authorization (planned)
