# DoramaFlix API

A REST API for managing and analyzing a K-Drama catalog using ASP.NET Core, SQL Server and Transact-SQL.

The project combines a persistent CRUD API with an analytical SQL layer, demonstrating backend development, relational database integration and T-SQL queries.

---

## Project Overview

DoramaFlix simulates the backend of a streaming catalog focused on Korean dramas.

The application allows users to:

- list dramas
- search dramas by ID
- filter dramas by genre
- create new dramas
- update existing dramas
- delete dramas
- persist data in SQL Server
- run analytical queries over the catalog

The API initially used an in-memory collection and was later migrated to a persistent SQL Server architecture.

---

## Architecture

```text
Client / HTTP Request
        |
        v
ASP.NET Core Minimal API
        |
        v
DramaRepository
        |
        v
Microsoft.Data.SqlClient
        |
        v
Parameterized T-SQL
        |
        v
SQL Server