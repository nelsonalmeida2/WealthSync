# WealthSync 💰

A robust, offline-first personal finance and net-worth tracking application built for multi-tenant household budgeting. Designed with strict adherence to Domain-Driven Design (DDD), Clean Architecture, and SOLID principles.

## 🏗️ Architecture Overview

This project implements a **Modular Monolith** using **Clean Architecture** to ensure the Domain logic remains isolated from external frameworks, UI, and databases.

* **Domain:** The core business rules (TDD enforced). Multi-tenancy is handled via the `Household` aggregate root.
* **Application:** Use cases and DTOs.
* **Infrastructure:** EF Core (PostgreSQL), external API integrations (Brokers), and Background Jobs (Hangfire).
* **API:** ASP.NET Core REST API serving as the entry point.
* **Frontend:** Angular SPA structured by features.

## 🛠️ Tech Stack

**Backend**
* C# 12 / .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* xUnit (Test-Driven Development)
* Serilog (File-based rolling logs)

**Frontend**
* Angular 17+ (SPA)
* Tailwind CSS (UI Styling)
* Dexie.js / IndexedDB (Offline-first data layer)

**Infrastructure & Ops**
* PostgreSQL (Relational Database)
* Docker & Docker Compose (Containerization)
* GitHub Actions (CI/CD Pipeline)
* Bash + S3 Compatible Storage (Daily automated backups)

## ✨ Key Features & Business Rules

1.  **Tracking & Targets:** Uses a limit-tracking budgeting system (rather than zero-based envelope budgeting) to maintain simplicity (KISS).
2.  **Net Worth Tracking:** Supports multiple `FinancialAccounts` (Checking, Savings, Brokerage). 
3.  **Double-Entry Transfers:** Moving money between accounts uses a `Transfer` entity, ensuring Cash Flow (Incomes/Expenses) charts are not distorted by capital allocation.
4.  **Offline-First:** Angular utilizes IndexedDB to store transactions when the device is offline. A background sync service pushes a queue to the C# API upon network restoration (Last-Write-Wins strategy).
5.  **Multi-Tenant by Design:** All entities are strictly bound to a `HouseholdId`. Users are members of a Household, allowing for joint data sharing or isolated personal accounts (`OwnerUserId`).

## 🌿 Git Branching Strategy

We follow a simplified, cost-effective branching model with no dedicated staging environment.

* `main`: Production-ready code. Commits here trigger CI/CD deployment to the live VPS.
* `dev`: Local integration and testing branch.
* `feature/US-XXX-name`: Active development branches for User Stories.

**Workflow:**
```bash
git checkout -b feature/US-001-setup
# Work... Commit...
git checkout dev
git merge feature/US-001-setup
# Test locally...
git checkout main
git merge dev
# Push to trigger deployment