# Store API

A modern ASP.NET Core 9.0 REST API for managing an online store with products and categories. Built with a layered architecture following SOLID principles and best practices.

## 📋 Features

### Core Features
- **Category Management**: Create, read, update, and delete product categories with full CRUD operations
- **Product Management**: Comprehensive product management with details like name, description, price, stock, and image support
- **Image Handling**: Upload, store, and delete product images with dedicated image controller
- **Pagination & Filtering**: Advanced pagination and filtering capabilities for efficient data retrieval
  - Support for page size, page number, and custom filtering parameters
  - Filter products by various criteria and retrieve paginated results
- **API Versioning**: Built-in API versioning support for backward compatibility and API evolution
- **Comprehensive Logging**: Serilog integration for detailed application logging and monitoring
- **Data Validation**: FluentValidation for request validation with custom validators
- **Consistent API Responses**: Standardized response format with success/error handling
- **Entity Framework Core**: Database operations with code-first migrations
- **Repository Pattern**: Generic repository with Unit of Work pattern for data access abstraction
- **Dependency Injection**: Built-in service container configuration for loose coupling
- **Auto Mapping**: AutoMapper for DTO transformations between models
- **Interactive API Documentation**: Scalar UI for exploring and testing API endpoints
- **CORS Support**: Cross-Origin Resource Sharing configured for cross-domain requests
- **Static File Hosting**: Serve images and static files efficiently

### Validation Features
- **Request Validation**: Comprehensive validation of all incoming requests
- **Image Upload Validation**: Specific validation rules for image uploads (file type, size, dimensions)
- **Product Validation**: Validation of product details (price, stock, name, description)
- **Category Validation**: Validation of category information
- **Error Reporting**: Detailed validation error messages with field-level feedback

## 🛠️ Architectural Approaches

### Design Patterns
- **Layered Architecture**: Clean separation of concerns across presentation, business logic, and data access layers
- **Repository Pattern**: Abstract data access logic with generic repositories for each entity
- **Unit of Work Pattern**: Manage multiple repository operations within a single transaction context
- **Dependency Injection**: Centralized service configuration and registration in `Program.cs`
- **Data Transfer Objects (DTOs)**: Decouple API contracts from internal models
- **Mapper Pattern**: Use AutoMapper for consistent object-to-object transformations

### SOLID Principles
- **Single Responsibility**: Each class has one reason to change (Controllers, Managers, Repositories)
- **Open/Closed**: Extensible architecture for adding new features without modifying existing code
- **Liskov Substitution**: Generic repositories and interfaces enable substitutable implementations
- **Interface Segregation**: Focused interfaces for specific operations (IRepository, IUnitOfWork, IManager)
- **Dependency Inversion**: Depend on abstractions (interfaces) rather than concrete implementations

### Data Access Strategy
- **Generic Repository**: Base repository with common CRUD operations for all entities
- **Entity-Specific Repositories**: Specialized repositories for Category and Product with custom queries
- **Entity Framework Core**: ORM for database operations with LINQ queries
- **Code-First Migrations**: Database schema versioning through C# migrations

### Business Logic Organization
- **Manager Classes**: Encapsulate business logic for Categories and Products
- **Validation Layer**: Separate validation concerns using FluentValidation
- **Custom Validators**: Domain-specific validators for complex validation rules
- **Error Mapping**: Centralized error handling and transformation

### API Design Approach
- **RESTful Endpoints**: Standard REST conventions for resource operations
- **Versioning Strategy**: API versioning for managing multiple API versions simultaneously
- **Pagination**: Cursor-based and offset-based pagination for large datasets
- **Filtering**: Query-based filtering with extensible filter parameters
- **Standardized Responses**: Consistent response wrapper (GeneralResult<T>) for all endpoints
- **Error Handling**: Centralized error handling with meaningful error messages

### Logging & Monitoring
- **Serilog Integration**: Structured logging for tracking application behavior
- **Log Levels**: Appropriate logging at Debug, Information, Warning, and Error levels
- **File Logging**: Logs persisted to files for audit and debugging purposes

## 🏗️ Project Structure

The solution follows a layered architecture pattern:

```
Store.APIs/          # Presentation layer (API Controllers)
├── Controllers/     # REST API endpoints
├── Program.cs       # Application configuration
└── Store.APIs.http  # HTTP client requests

Store.BLL/           # Business Logic Layer
├── Managers/        # Business logic for Categories and Products
├── Validations/     # Custom validation rules
├── Validators/      # FluentValidation validators
├── DTOs/            # Data Transfer Objects
├── Mappers/         # Custom mapping logic
└── MappingProfiles/ # AutoMapper profiles

Store.DAL/           # Data Access Layer
├── Data/            # Database configuration and models
├── Repo/            # Repository implementations
├── UnitOfWork/      # Unit of Work pattern
└── Migrations/      # EF Core migrations

Store.Common/        # Common utilities and models
├── GeneralResult/   # API response wrappers
└── Error.cs         # Error models
```

## 🚀 Getting Started

### Prerequisites
- .NET 9.0 SDK
- Visual Studio 2022 or VS Code with C# extension
- SQL Server (or configured database provider)

### Installation

1. **Clone the repository**
```bash
git clone https://github.com/iAhmadMahmoud/StoreManagmentSystemAPI_ITI_Task.git
cd StoreManagmentSystemAPI_ITI_Task
```

2. **Restore dependencies**
```bash
dotnet restore
```

3. **Configure database connection**
   - Update connection string in `Store.APIs/appsettings.json`
   - Example:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=StoreDB;Trusted_Connection=true;"
     }
   }
   ```

4. **Apply database migrations**
```bash
dotnet ef database update --project Store.DAL
```

5. **Run the application**
```bash
dotnet run --project Store.APIs
```

The API will be available at `https://localhost:5001` (or as configured in `launchSettings.json`)

## 📡 API Endpoints

### Categories
- `GET /api/category` - Get all categories
- `GET /api/category/{id}` - Get category by ID
- `POST /api/category` - Create a new category
- `PUT /api/category` - Update a category
- `DELETE /api/category/{id}` - Delete a category

### Products
- `GET /api/product` - Get all products
- `GET /api/product/{id}` - Get product by ID
- `POST /api/product` - Create a new product
- `PUT /api/product` - Update a product
- `DELETE /api/product/{id}` - Delete a product

## 📚 API Documentation

The API includes interactive documentation via Scalar UI. Once the application is running:
- Open your browser to `https://localhost:5001/scalar/v1` to view the API documentation
- You can test API endpoints directly from the documentation interface

## 📦 Response Format

All API responses follow a consistent format using `GeneralResult<T>`:

**Success Response:**
```json
{
  "success": true,
  "message": "Operation successful",
  "data": { /* response data */ }
}
```

**Error Response:**
```json
{
  "success": false,
  "message": "Error message",
  "errors": {
    "fieldName": ["Error details"]
  }
}
```

## 🛠️ Development

### Building the Solution
```bash
dotnet build
```

### Running Tests
If tests are added in the future:
```bash
dotnet test
```

### Database Migrations
Create a new migration:
```bash
dotnet ef migrations add MigrationName --project Store.DAL
```

Update database:
```bash
dotnet ef database update --project Store.DAL
```

## 📝 Architecture Details

### Layered Architecture
- **Presentation Layer (APIs)**: Handles HTTP requests and responses
- **Business Logic Layer (BLL)**: Contains business rules and operations
- **Data Access Layer (DAL)**: Manages database operations
- **Common Layer**: Shared utilities and models

### Design Patterns
- **Repository Pattern**: Abstracts data access logic
- **Unit of Work Pattern**: Manages multiple repositories
- **Dependency Injection**: Manages service lifetimes
- **DTO Pattern**: Decouples API contracts from domain models
- **Mapper Pattern**: Transforms between DTOs and entities

## 📦 Key Dependencies
- **Entity Framework Core 9.0.16**: ORM for database operations
- **AutoMapper**: Object-to-object mapping
- **Scalar.AspNetCore**: Interactive API documentation
- **.NET 9.0**: Latest .NET framework

## 📄 License
See [LICENSE.txt](LICENSE.txt) for license information.

## 🤝 Contributing
Contributions are welcome. Please feel free to submit a pull request.

## 📞 Support
For issues or questions, please open an issue in the repository.
