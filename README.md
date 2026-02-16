<div align="center">

<img src="PhDManager/wwwroot/uniza_logo.png" width="120" height="auto" alt="UNIZA Logo">

# 🎓 PhD Manager
### *A Modern Information System for PhD Study Management*

[![.NET 9](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/download/dotnet/9.0)
[![Blazor](https://img.shields.io/badge/Blazor-512BD4?style=for-the-badge&logo=blazor)](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=for-the-badge&logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)](https://www.docker.com/)

---

</div>

## 📖 Project Overview
**PhD Manager** is a comprehensive web application designed to streamline the administration and management of doctoral studies. Developed as part of a Bachelor's thesis at UNIZA, the system provides a robust platform for students, teachers, and administrators to handle study plans, dissertation topics, and academic evaluations through a modern, intuitive interface.

## 🌟 Key Features
*   **🔐 Secure Multi-Auth**: Support for **LDAP** integration and **Google Authentication**.
*   **🎭 RBAC (Role-Based Access Control)**: Custom permissions for Admins, Teachers, Students, and Reviewers.
*   **📑 Dissertation Management**: Workflow for adding, editing, and approving dissertation topics with a built-in feedback system.
*   **📅 Individual Study Plans**: Interactive tools to create and track personalized paths for PhD students.
*   **👥 User Administration**: Dedicated dashboard for role assignments and profile management.
*   **🔗 External Invitations**: Generate secure, time-limited registration links for external collaborators.

## 🛠️ Tech Stack
| Component | Technology |
| :--- | :--- |
| **Framework** | Blazor Web App (Interactive Server) |
| **Runtime** | .NET 9 |
| **Database** | PostgreSQL 17 |
| **Styling** | MudBlazor Component Library |
| **Containerization** | Docker & Docker Compose |

## 🚀 Getting Started

### 📋 Prerequisites
*   [Docker Desktop](https://www.docker.com/products/docker-desktop/)
*   [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) *(Only if running locally without Docker)*

### ⚙️ Configuration
1.  **Clone the Repository**:
    ```bash
    git clone https://github.com/gastyCode/phd-manager-2.git
    cd phd-manager-2
    ```
2.  **Setup Secrets**:
    *   Refer to `PhDManager/sample-secrets.json`.
    *   Initialize **User Secrets** in the `PhDManager` project.
    *   Configure your LDAP and (optional) Google Auth credentials.

### 🐳 Running with Docker
Fire up the entire environment with a single command:
```bash
docker compose up --build
```
> [!TIP]
> Once the containers are healthy, the application will be live at: **`http://localhost:8081`**

### 🔑 Gaining Admin Access
For the initial setup:
1.  Login to the application.
2.  Open your database (e.g., via DBeaver).
3.  In the `AspNetUserRoles` table, assign the `Admin` role ID to your user ID.
4.  Restart or refresh the session to see administrative menus.

---
<div align="center">
  <sub>Developed with ❤️ as part of a Bachelor's Thesis.</sub>
</div>
