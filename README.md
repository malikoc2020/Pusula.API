# Pusula.API

## 📌 Overview

Pusula.API is a backend system built to manage employee operations, worksites, permissions, and organizational data for an internal company workflow system.

It provides secure authentication using JWT and ASP.NET Identity, with a layered architecture designed to support role-based access control and scalable business logic.

---

## 🎯 Purpose

The system is designed to centralize and manage internal company processes such as:

- Employee tracking and management  
- Worksite and worker assignment operations  
- Permission (leave) management  
- Organizational reference data (provinces, districts)  
- Role-based access control and authorization  

---

## 🏗 Architecture

The project follows a layered architecture:

- **API Layer**
  - ASP.NET Core Web API controllers  
  - JWT-secured endpoints  
  - Request/response handling  

- **Services Layer**
  - Business logic implementation  
  - Validation and workflow rules  
  - DTO-based communication  

- **Data Access Layer (DAL)**
  - Entity Framework Core  
  - Generic Repository Pattern  
  - Unit of Work Pattern  

- **Domain Layer**
  - Core business entities  
  - Identity models (User, Role)  
  - Base entity structure  

- **Core Layer**
  - Shared extensions and utilities  
  - Cross-cutting concerns  

---

## 🧱 Tech Stack

- ASP.NET Core Web API  (.NET 7) 
- Entity Framework Core  
- MySQL  
- ASP.NET Identity  
- JWT Authentication  
- Dependency Injection  
- LINQ  

---

## 🔐 Authentication & Authorization

- JWT-based authentication  
- ASP.NET Identity is configured with role support (Admin and User roles are defined and seeded)
- Protected endpoints using JWT authentication via `[Authorize]`

---

## 📦 Features

- User authentication (Login / Register)
- JWT token generation and validation
- Employee management
- Worksite management (workers, actions, types)
- Permission / leave tracking system
- Province & district reference data
- Role-based authorization (Admin/User)
- Centralized exception handling with custom error codes
- Database seeding on application startup

---

## 🗄 Database

- MySQL
- Code-First approach (Entity Framework Core)
- ASP.NET Identity tables + domain tables
- Seeded data:
  - Roles (Admin, User)
  - Users
  - Provinces & Districts
  - Worksite reference data

---

## ⚙ Design Patterns

- Repository Pattern (Generic Repository)
- Unit of Work Pattern
- Dependency Injection
- Layered Architecture
- DTO-based communication

---

## 🚨 Exception Handling

- Global exception middleware
- Standardized error codes (E0001, E0002, etc.)
- Consistent API error response structure

---

## 🚀 How to Run

1. Clone the repository

```bash
git clone https://github.com/malikoc2020/Pusula.API.git
```

2. Configure the connection string in `appsettings.Development.json`.

3. Apply migrations via Package Manager Console:

```powershell
Update-Database
```

4. Build and run the project.

## Status

Personal backend API project developed for practicing enterprise-level ASP.NET Core Web API architecture and authentication systems.
