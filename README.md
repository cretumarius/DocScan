# DocScan

A cross-platform mobile application for document scanning, OCR text extraction, and PDF management. Built with React Native and an ASP.NET Core backend.

> Developed as a Bachelor's degree final project.

## Features

- **Document Scanner** — Capture documents using the device camera with automatic edge detection and perspective correction.
- **OCR (Text Extraction)** — Extract text from images (JPG, BMP, TIFF, GIF) using Google Cloud Vision API.
- **PDF Merge** — Upload multiple images and PDFs and merge them into a single document stored in Azure Blob Storage.
- **Export** — Export OCR results to PDF or Word (.docx) format.
- **Authentication** — JWT-based auth with optional biometric login (fingerprint / Face ID) via native keychain storage.

## Tech Stack

### Client (React Native)

| Area | Technology |
| --- | --- |
| Framework | React Native 0.63 + TypeScript |
| Navigation | React Navigation (stack, drawer, tabs) |
| UI | React Native Paper, Vector Icons, Lottie |
| Document Scanning | `@woonivers/react-native-document-scanner` |
| Forms | React Hook Form |
| Auth storage | React Native Keychain |
| HTTP | apisauce |

### Server (ASP.NET Core)

| Area | Technology |
| --- | --- |
| Framework | ASP.NET Core 3.1 (C#) |
| Database | SQL Server + Entity Framework Core |
| Auth | JWT Bearer tokens |
| OCR | Google Cloud Vision API |
| PDF | IronPDF |
| Storage | Azure Blob Storage |
| DI | Autofac + AutoMapper |
| API Docs | Swagger / OpenAPI |

## Architecture

The server follows a clean layered architecture:

```text
API (Controllers)
  └── Business (Services)
        └── DataAccess (Repositories)
              └── Domain (Entities)
                    └── Infrastructure (DI, Mapping)
```

The client uses an atomic design component structure (`atoms → molecules → organisms`) with React Context for authentication state.

## Getting Started

### Prerequisites

- Node.js + npm (or yarn)
- React Native CLI and platform toolchains (Xcode for iOS, Android Studio for Android)
- .NET SDK 3.1
- SQL Server instance
- Google Cloud credentials (for OCR)
- Azure Storage account (for PDF merge)

### Server setup

```bash
cd server

# Restore dependencies
dotnet restore

# Apply database migrations
dotnet ef database update -p DataAccess -s API

# Start the API
dotnet run --project API
```

Configure credentials in `server/API/appsettings.json` (or via environment variables):

- `ConnectionStrings__DefaultConnection` — SQL Server connection string
- `AppSettings__Secret` — JWT signing secret
- `AzureStorageConnectionString` — Azure Blob Storage connection string
- Google Cloud Vision credentials path

### Client setup

```bash
cd client

# Install dependencies
npm install

# iOS — install pods
cd ios && pod install && cd ..

# Start Metro bundler
npm start

# Run on iOS
npm run ios

# Run on Android
npm run android
```

## Project Structure

```text
DocScan/
├── client/                  # React Native mobile app
│   └── src/
│       ├── components/      # Reusable UI components (atoms/molecules/organisms)
│       ├── contexts/        # Auth context
│       ├── core/            # Biometrics, storage, file download helpers
│       ├── models/          # TypeScript types and interfaces
│       ├── navigation/      # React Navigation configuration
│       ├── scenes/          # Screen-level components
│       └── services/        # API service layer
└── server/                  # ASP.NET Core backend
    ├── API/                 # Controllers, middleware, Swagger
    ├── Business/            # Application services (OCR, Merge, Export)
    ├── DataAccess/          # EF Core repositories
    ├── Domain/              # Entities (User, OcrResult)
    └── Infrastructure/      # DI registration, AutoMapper profiles
```

## API Endpoints

| Method | Endpoint | Description |
| ------ | -------- | ----------- |
| POST | `/account/register` | Create a new user account |
| POST | `/account/login` | Authenticate and receive JWT |
| POST | `/ocr/extract` | Extract text from an uploaded image |
| GET | `/ocr/results` | List OCR results for authenticated user |
| POST | `/merge/upload` | Upload documents for merging |
| POST | `/merge` | Merge uploaded documents into a PDF |
| GET | `/export/pdf/{id}` | Export an OCR result as PDF |
| GET | `/export/word/{id}` | Export an OCR result as Word document |

Interactive API documentation is available via Swagger UI when the server is running.
