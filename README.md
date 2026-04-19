# E-Commerce Backend System 🚀

A robust, scalable, and maintainable E-Commerce Backend built with **.NET 8** following the **Onion Architecture** and industry best practices.

## 🛠️ Technical Stack & Tools
* **Framework:** .NET 8 (Web API)
* **Database:** SQL Server (EF Core)
* **Caching:** Redis
* **Security:** Identity Core & JWT Authentication
* **Payments:** Stripe Integration (with Webhooks)
* **Mapping:** AutoMapper
* **Documentation:** Swagger UI

## 🏗️ Architecture: Onion Layers
The project is structured into four main layers to ensure complete decoupling of business logic:
1.  **Domain:** Core entities, interfaces, and business logic.
2.  **Repository:** Data access implementation using **Generic Repository** and **Unit of Work** patterns.
3.  **Service:** Application logic and orchestration.
4.  **Presentation (API):** RESTful endpoints and middleware.

## 🌟 Key Features
-   ✅ **Clean Code:** Implementation of Specification Pattern for complex data filtering.
-   ✅ **Async Programming:** Fully asynchronous flow for high performance.
-   ✅ **Shopping Cart:** Fast and reliable basket management using Redis.
-   ✅ **Payment Lifecycle:** Full integration with Stripe API including automated order status updates via Webhooks.
-   ✅ **Authentication:** Secure identity management with JWT tokens.
-   ✅ **Error Handling:** Centralized global exception handling middleware.

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server
- Redis (Local or Docker)
- Stripe Account (for API Keys)

### Installation
1. Clone the repository:
   ```bash
   git clone [https://github.com/Abdallahsanad/E-Commerce.git](https://github.com/Abdallahsanad/E-Commerce.git)
