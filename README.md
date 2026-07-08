# WealthSync 💰

A robust, offline-first personal finance and net-worth tracking application built for multi-tenant household budgeting. Designed with strict adherence to Domain-Driven Design (DDD), Clean Architecture, and SOLID principles.

## 🏗️ Architecture Overview

This project implements a **Modular Monolith** using **Clean Architecture** to ensure the Domain logic remains isolated from external frameworks, UI, and databases.

* **Domain:** The core business rules (TDD enforced using xUnit). Multi-tenancy is handled via the `Household` aggregate root.
* **Application:** Use cases, DTOs, and external service interfaces.
* **Infrastructure:** EF Core (PostgreSQL), external API integrations (Stock Brokers), and Background Jobs.
* **API:** ASP.NET Core REST API serving as the entry point.
* **Frontend:** Angular PWA/SPA structured by features.

## 🛠️ Tech Stack

**Backend**
* C# 12 / .NET 8
* ASP.NET Core Web API
* Entity Framework Core (Auto-migrations on startup)
* xUnit (Test-Driven Development)
* Serilog (File-based rolling logs)
* Hangfire / Quartz.NET (Nightly background jobs for broker API calls)

**Frontend**
* Angular 17+ (Progressive Web App - PWA)
* Tailwind CSS + Component Library (e.g., Spartan/DaisyUI)
* Dexie.js / IndexedDB (Offline-first data layer)

**Infrastructure, Storage & DevOps**
* PostgreSQL (Relational Database)
* S3-Compatible Storage (Cloudflare R2 / Backblaze B2) for storing invoices, receipts, and photos.
* Docker & Docker Compose (Containerization)
* Terraform (Infrastructure as Code for provisioning the VPS, S3 buckets, and DNS)
* GitHub Actions (CI/CD Pipeline: test, build, push to GHCR, and deploy)
* Bash Scripts + Cron Jobs (Daily automated Postgres `pg_dump` backups pushed to S3)

## ✨ Key Features & Business Rules

1. **Tracking & Targets Budgeting:** Uses a limit-tracking budgeting system (setting monthly limits per category) to maintain simplicity (KISS) and enable fast dashboard generation.
2. **Net Worth & Portfolio Tracking:** Supports multiple `FinancialAccounts` (Checking, Savings, Brokerage). Nightly background jobs securely use encrypted API keys to fetch stock portfolio valuations.
3. **Double-Entry Transfers:** Moving money between accounts (e.g., Checking to Savings) uses a `Transfer` entity, ensuring Cash Flow (Incomes/Expenses) charts are not distorted by capital allocation.
4. **PWA & Offline-First:** Designed as a mobile-first Progressive Web App. Angular utilizes IndexedDB to store transactions when the device has no internet connection. A background sync service pushes a queue to the C# API upon network restoration using a Last-Write-Wins strategy.
5. **Multi-Tenant by Design:** All entities are strictly bound to a `HouseholdId`. Users are members of a Household, allowing for joint data sharing or isolated personal accounts (`OwnerUserId`).
6. **File Uploads:** Secure upload of expense receipts and invoices directly to S3 buckets.

## 🌿 Git Branching & CI/CD Strategy

We follow a simplified, cost-effective branching model optimized for solo developers, bypassing a dedicated staging server to keep cloud costs minimal.

* `main`: Production-ready code. Commits here trigger the GitHub Actions CI/CD pipeline (build, test, deploy to the live VPS).
* `dev`: Local integration and testing branch.
* `feature/user-story-code`: Active development branches for User Stories (tracked via [Trello Kanban](https://trello.com/b/LiJyFhYx/wealthsync)).

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
