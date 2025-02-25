# CHaters-project-backend
This is the backend of the CHaters project. It is a RESTful API that provides the necessary endpoints for the frontend to interact with the database.

## Backend Technologies
- .NET 8.0
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- Swagger

## Build and Run
1. Clone the repository:
```shell
git clone https://github.com/kovalllllll/CHaters-project-backend.git
```
2. Check ConnectionStrings in `appsettings.json` file:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=chaters;Username=postgres;Password=postgres"
  }
}
```
3. Go to the project backend directory:
```shell
cd CHaters-project-backend/
```
4. Migration:
```shell
dotnet ef migrations add InitialCreate
dotnet ef database update
```
5. Build & Run the project:
```shell
dotnet build
dotnet run
```
