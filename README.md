# Personal Blogging Platform API

A RESTful API for a personal blog. Built with C#, .NET 10, ASP.NET Core, and PostgreSQL.

## Endpoints

| Method | Path             | Description                                 |
| ------ | ---------------- | ------------------------------------------- |
| GET    | /api/posts       | List all posts                              |
| GET    | /api/posts?term= | Search posts by title, content, or category |
| GET    | /api/posts/{id}  | Get a single post                           |
| POST   | /api/posts       | Create a post                               |
| PUT    | /api/posts/{id}  | Update a post                               |
| DELETE | /api/posts/{id}  | Delete a post                               |

## Tech Stack

- .NET 10 / ASP.NET Core (Controller-based)
- Entity Framework Core (Code-first migrations)
- PostgreSQL 16 (Docker)
- OpenAPI / Swagger UI

## Project Structure

```
Controllers/   API endpoints
DTOs/          Request validation
Models/        Database entities
Data/          EF Core DbContext
Migrations/    Database schema (auto-generated)
```

## Getting Started

### Prerequisites

- .NET 10 SDK
- Docker Desktop

### Run

```powershell
docker compose up -d
dotnet run
```

Open `http://localhost:5225/swagger` to test the API.

## Features

- Full CRUD on blog posts
- Case-insensitive search across title, content, and category
- Tags stored as JSONB in PostgreSQL
- Input validation with automatic 400 responses
- Proper HTTP semantics (201 Created, 204 No Content, 404 Not Found)
