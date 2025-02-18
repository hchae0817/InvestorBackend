# Backend - Investors Management API

This backend is built using **.NET 7 (C#)** and provides an API to manage investors and their financial commitments.  
It integrates with **SQLite** for data storage and supports data import from a **CSV file**.

---

## **1. Objectives**
- **Provide an API** to fetch investor data and commitments.
- **Store data in SQLite** using Entity Framework Core.
- **Import investor data from a CSV file** and populate the database.
- **Follow a structured architecture** with DTOs, services and controllers.
- **Enable frontend integration** via REST API.

---
## **2. Steps Taken in This Exercise**

1. **Project Setup** – Created a .NET Web API project and configured dependencies.
Define Data Models – Structured investor and commitment data in Investor.cs.
2. **Setup Database** – Configured SQLite and created database tables.
3. **Implement CSV Import** – Wrote logic in DatabaseHelper to import CSV data into SQLite.
4. **Create DTOs** – Used InvestorDto and CommitmentDto to structure API responses.
5. **Develop Business Logic** – Implemented InvestorService to group commitments per investor.
6. **Build API Endpoints** – Created InvestorController to expose RESTful API endpoints.
7. **Handle Edge Cases** – Managed database errors, empty CSV files, and invalid data handling.

---

## **3. Project Structure**
- **Models** – Defines database entities (Investor).
- **DataTransferObject** (DTOs) – Maps database models into API response-friendly objects.
- **Services** (InvestorService.cs) – Handles business logic, restructure models to DTO.
- **Controllers** (InvestorController.cs) – Exposes API endpoints for frontend interaction.
- **Database Context** (ApplicationDbContext.cs) – Manages SQLite database operations.

---

## **4. Database Configuration (SQLite)**
- The database is stored in `investors.db`.
- **Entity Framework Core (EF Core)** is used for migrations and database management.

---

## **4. Final Thoughts**

This project demonstrates how to build a full-stack application that integrates both a frontend (React.js) and a backend (.NET API with SQLite). By using DTOs, Services, and Controllers, ensuring a clean and maintainable architecture that allows for easy scaling and further development.

Throughout this exercise, focus lies on:

Efficiently handling data management by importing CSV data into SQLite.
Ensuring data integrity by using DTOs to shape the data exposed through the API.
Structuring the backend with clear separations of concerns between the controller (API), service (business logic), and helper functions (data import).
This project serves as a foundation that can be easily extended, whether to add more complex business logic, integrate other data sources, or scale to accommodate larger datasets.

Thank you for reviewing this project! 