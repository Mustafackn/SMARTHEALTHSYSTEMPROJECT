# 🏥 Smart Health System — Intelligent Health Analysis, Prediction and Decision Support Platform

## 1. 📌 Project Overview & Purpose

Smart Health System is an AI-powered, multi-layered enterprise health platform designed to process individual biometric and clinical laboratory parameters (e.g., HbA1c, fasting blood glucose, blood pressure, cholesterol). It predicts health risk probabilities for Type 2 Diabetes, Cardiovascular Diseases, and Breast Cancer, serving as a Clinical Decision Support System (CDSS) for both end-users and clinicians.

### 🎯 Core Objectives & Problem Statement
Chronic conditions such as diabetes and cardiovascular diseases are primary causes of mortality worldwide, yet their severe outcomes can be mitigated through early detection and preventative medicine. The core objectives of this platform are to:
- Process unstructured and complex clinical datasets through high-accuracy machine learning models.
- Identify potential disease risks prior to clinical onset.
- Provide personalized lifestyle recommendations to improve health literacy and patient outcomes.

---

## 🏛️ 2. Software Architecture & Engineering Standards

The backend is structured according to Clean Architecture (Onion / Layered Architecture) principles under the SMARTHEALTHSYSTEMPROJECT solution, partitioned into 6 distinct modules:

SMARTHEALTHSYSTEMPROJECT
 ├── 🏢 SmartHealthSystem.Domain        # Core Entities, Database Models, Enums
 ├── ⚙️ SmartHealthSystem.Application   # Business Logic, DTOs, Interfaces, Service Contracts
 ├── 🏗️ SmartHealthSystem.Infrastructure# External Integrations, Python AI HTTP Clients
 ├── 💾 SmartHealthSystem.Persistence   # DbContext, SQL Server Migrations, Repositories
 ├── 🌐 SmartHealthSystem.WebApi        # RESTful API Endpoints, JWT Authentication, Swagger
 └── 🖥️ SmartHealthSystem.WebApp        # Razor / MVC Admin & Clinician Management Dashboard

### 🛢️ On-Premises & N-Layer Deployment Strategy
The architecture is explicitly designed for local/on-premises execution without mandatory external cloud or container runtime overhead. Benefits of this choice include:
1. Minimized Network Latency: In-process/local communication between MS SQL Server and the .NET 8 Web API ensures sub-millisecond data access rates during real-time prediction calls.
2. Academic Reproducibility: Allows straightforward setup using standard dependency managers (NuGet, pip) and MS SQL Server without requiring containerization environments.

---

## 🧠 3. Artificial Intelligence Integration & Data Flow

The platform utilizes a hybrid Microservice Architecture that bridges Python's data science ecosystem with the robust .NET 8 enterprise infrastructure.

[Flutter Mobile / WebApp] 
        │
        ▼ (HTTP / JSON)
[ .NET 8 Web API ]  ── (C# Typed HttpClient) ──►  [ Python FastAPI ML Engine ]
        │                                                     │
        ▼ (EF Core)                                           ▼ (Scikit-Learn / Pandas)
[ MS SQL Server ]                                  [ Random Forest / Logistic Reg. ]

### 🔄 End-to-End Prediction Lifecycle:
1. Data Ingestion: Biometric and lab entries from the Mobile app or Web dashboard are transmitted via HTTPS/JSON to the REST Web API.
2. Globalization & Formatting: Decimal parsing logic (such as in Result.cshtml) uses CultureInfo.InvariantCulture to prevent locale-specific float conversion bugs.
3. Inter-Service Communication: The .NET Infrastructure layer dispatches an asynchronous HTTP POST request to the internal Python FastAPI microservice.
4. Model Execution:
   - Type 2 Diabetes: Random Forest / Logistic Regression (96.34% Accuracy)
   - Cardiovascular: Multi-variate Risk Assessment Engine (92.48% Accuracy)
   - Breast Cancer: WDBC Diagnostic Model (98.25% Accuracy, FN=1 Type II Error Optimization)
5. Recommendation & Score Parsing Engine: The response string from the ML service is parsed via a custom delimiter pipeline (rawRecommendations.Split('|')) and categorized into contextual UI alerts (Success/Warning/Danger).

---

## 📱 4. Mobile & Web Interfaces

The platform provides dual-channel access tailored for different user roles:
- Web Management Portal (SmartHealthSystem.WebApp): Built using Razor/MVC for clinicians and system administrators. Includes user management, aggregate analytical reports, and system telemetry dashboards.
- Cross-Platform Mobile Application (Flutter): Target application for individual end-users (iOS/Android).
  - Secure authentication via JWT Bearer Tokens.
  - Streamlined laboratory data entry workflows.
  - Historical risk trend tracking over time.
  - Exportable PDF Smart Health Analysis Report generator.

---

## 🔄 5. Comparative Analysis with Existing Solutions

| Feature / Criteria | Standard Health Tracking Apps | Standalone Python Notebooks | Smart Health System (This Project) |
| --- | --- | --- | --- |
| System Role | Static metric logging | Isolated offline research | Active Decision Support System (CDSS) |
| Architecture | Monolithic / Coupled UI | No API / Web layer | Clean Architecture (.NET 8 + Python Microservice) |
| Clinical Type II Error (FN) | Unoptimized | Accuracy-focused only | Recall-prioritized (FN=1 on Breast Cancer) |
| Multi-Platform Support | Single platform only | Non-existent | Web Admin Dashboard + Cross-Platform Flutter Mobile |
| Recommendation Engine | Hardcoded strings | None | Dynamic Parsed AI Advice Pipeline |

---

## 🛠️ 6. Environment Setup & Execution

### Prerequisites
- .NET 8.0 SDK
- Microsoft SQL Server
- Python 3.10+ (scikit-learn, pandas, fastapi, uvicorn)
- Flutter SDK (for mobile application deployment)

### Execution Steps

1. Clone the Repository:
   git clone [https://github.com/username/SmartHealthSystem.git](https://github.com/username/SmartHealthSystem.git)
   cd SmartHealthSystem

2. Run the Python AI Microservice:
   cd PythonMlService
   pip install -r requirements.txt
   uvicorn main:app --reload --port 8000

3. Initialize Backend & Web Client:
   - Update the SQL Server connection string in appsettings.json.
   - Execute migrations in Package Manager Console:
     Update-Database
   - Configure Visual Studio for Multiple Startup Projects (SmartHealthSystem.WebApi and SmartHealthSystem.WebApp) and launch.

---

## 📄 License

Developed as part of the Computer Engineering Senior Graduation Project at Van Yüzüncü Yıl University. All rights reserved.

<img width="945" height="428" alt="image" src="https://github.com/user-attachments/assets/8f8736f0-c7eb-4536-98ca-2d2fbb50d768" />
<img width="945" height="469" alt="image" src="https://github.com/user-attachments/assets/669c9796-792a-444c-b3e7-a22504bbacb3" />
<img width="945" height="436" alt="image" src="https://github.com/user-attachments/assets/2cbbf5d1-ce6c-4dcd-ab82-7fc12c12fea9" />
<img width="945" height="452" alt="image" src="https://github.com/user-attachments/assets/384c105c-d419-4a63-8aca-cdb4050bfd41" />
<img width="945" height="434" alt="image" src="https://github.com/user-attachments/assets/7d49e9a5-dcc8-45eb-97c4-769a461cc9af" />
<img width="945" height="434" alt="image" src="https://github.com/user-attachments/assets/315d59c2-c15e-496d-8ef7-f8331af9b4b6" />
<img width="945" height="429" alt="image" src="https://github.com/user-attachments/assets/cdd51d1b-8376-4b94-8b42-656f6c5f1ddd" />






