# Class Registration System
A full-stack Blazor Server (SSR) application for managing class registrations, subjects, surveys, and attendance tracking.
Designed with enterprise-grade authentication, email notifications, and a modular architecture for scalability.

## Features
- Subject & Class Management
- Create, edit, and manage subjects
- Create and schedule classes
- Survey Builder
- Built-in dynamic survey creation for classes and subjects
- Attendance Tracking
- Mark and view attendance records
- Export registration data via JS Interop
- User Roles
  - 3-tier permissions: Admin, Moderator, User
- Authentication & Communication
- LDAP Authentication for secure sign-in
- SMTP Email Service for notifications and updates

## Technologies Used

Fullstack: Blazor Server (SSR) with MudBlazor UI components

## Core Architechture

- Validation: FluentValidation for robust form handling
- Database: Microsoft SQL Server
- Architecture: Multi-project, clean architecture with:
- Application – business logic & interfaces
- Domain – entities & core models
- Infrastructure – database, repositories, external services
- Presentation – UI layer

### Custom Modules:

- In-house Survey Builder package

## 📂 Project Structure

ClassRegistration.Application    # Business logic & services
ClassRegistration.Domain         # Core entities and domain models
ClassRegistration.Infrastructure # Data access, repositories, external integrations
ClassRegistration.Presentation   # Blazor UI, components, helpers

## 🚀 Getting Started
### Clone the repository

```
git clone https://github.com/naykakashima/ClassRegistration.git
cd ClassRegistration
```

### Update appsettings.json with:

1. SQL Server connection string
2. LDAP server credentials
3. SMTP server settings

### Run Database Migrations

```
dotnet ef database update
```

### Run the Application

```
dotnet build
dotnet run
```

### 📧 SMTP & LDAP Setup
SMTP is used to send:
1. Registration confirmations

2. Attendance reminders

3. LDAP provides secure role-based login:
   - Admin, Moderator, User
