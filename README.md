### README for GameStore Project 🎮

---

## Project Overview 📋

The **GameStore** project demonstrates the implementation of CRUD operations, database migrations, and best practices for API development. While simple, the project is structured to emphasize clean code and modularization, serving as an excellent starting point for learning **ASP.NET Core**.

---

## Features 🚀

- **Minimal API Architecture:** Implemented lightweight and fast APIs for CRUD operations.
- **Database Integration:** Connected APIs to a **SQL database** using **Entity Framework Core** for persistence.
- **Async Programming:** Ensured non-blocking operations for better performance.
- **API Versioning:** Integrated version control for evolving APIs.
- **Swagger UI:** Documentation and testing interface for all endpoints.
- **Modular Codebase:** Organized logic using extension methods for scalability.

---

## Commit Highlights 📝

- **Database Setup:** Created tables and migrations for entities (`Nov 21, 2024`).
- **CRUD Operations:** Developed endpoints for creating, reading, updating, and deleting games (`Nov 19, 2024`).
- **API Optimization:** Shifted from hardcoded data to database-driven responses (`Nov 23, 2024`).
- **Asynchronous Operations:** Prevented thread blocking by implementing async methods (`Nov 23, 2024`).
- **Validation Enhancements:** Replaced manual validation with `minimalApis.Extensions` package for cleaner code (`Nov 20, 2024`).
- **Swagger Integration:** Simplified API testing with a well-documented interface (`Nov 19, 2024`).

---

## Technology Stack 💻

- **Framework:** ASP.NET Core Minimal APIs
- **Database:** SQL with Entity Framework Core
- **API Tools:** Swagger for API documentation and testing
- **Language:** C#

---

## Getting Started 🛠️

### Prerequisites
1. **.NET 6 SDK or higher** installed.
2. A **SQL database** instance.

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/para-commando/GameStore.git
   ```
2. Navigate to the project directory:
   ```bash
   cd GameStore
   ```
3. Run the application:
   ```bash
   dotnet run
   ```

### Configuration
- Update the `appsettings.json` file with your database connection string.

---

## Future Improvements 🌟

- Add **frontend integration** for a complete web experience.
- Implement **user authentication** to secure the API.
- Extend the project to include **game genres**, **reviews**, and **ratings**.

---

Enjoy building and expanding the **GameStore**! 🎮