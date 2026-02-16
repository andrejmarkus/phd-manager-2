# PhD Manager
*Modern Information System for PhD Study Management*

## 📝 Project Overview
PhD Manager is a comprehensive web application designed to streamline the administration and management of doctoral studies. Developed as part of a Bachelor's thesis, the system provides a robust platform for students, teachers, and administrators to handle study plans, dissertation topics, and evaluations.

## 🚀 Key Features
- **Multi-Authentication Support**: Seamless integration with LDAP and Google Authentication.
- **Role-Based Access Control**: Granular permissions for Admins, Teachers, Students, and Reviewers.
- **Dissertation Management**: Add, edit, and approve dissertation topics with a built-in review system.
- **Individual Study Plans**: Create and manage personalized study paths for PhD students.
- **User Management**: Administrative tools for managing user roles and profiles.
- **External User Registration**: Secure registration for external users via time-limited invitation links.

## 🛠️ Tech Stack
- **Framework**: [Blazor Web App (Interactive Server)](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor)
- **Runtime**: .NET 9
- **Database**: [PostgreSQL](https://www.postgresql.org/)
- **Infrastructure**: [Docker](https://www.docker.com/) & Docker Compose
- **Design**: [MudBlazor](https://mudblazor.com/) Component Library

## ⚙️ Getting Started

### Prerequisites
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) (for local development)

### Configuration
1. Clone the repository.
2. Locate `PhDManager/sample-secrets.json`.
3. Configure your **User Secrets** in the `PhDManager` project.
4. Fill in LDAP credentials (`Username`, `Password`) and (optional) Google Auth `ClientId`/`ClientSecret`.
5. The application uses a local PostgreSQL instance provided by Docker.

### Running with Docker
The easiest way to get the project running is using Docker Compose:
```bash
docker compose up --build
```
The application will be accessible at `http://localhost:8081`.

### First-Time Setup (Admin Access)
After first login:
1. Access your database (e.g., using DBeaver).
2. Navigate to `AspNetUserRoles` table.
3. Assign the **Admin** role ID to your user ID.
4. Refresh the application to access administrative features.

---
*Developed as part of a Bachelor's Thesis.*
