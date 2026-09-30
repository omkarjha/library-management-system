# 📚 Library Management System

A full-stack mini-project for managing a library's books, members, and borrow/return activity — built to learn and demonstrate **.NET 10**, **MongoDB**, and **React**.

## Tech Stack

**Backend**
- ASP.NET Core 10 Web API (Controllers)
- MongoDB (via official `MongoDB.Driver`)
- Repository + Service layered architecture
- Dependency Injection (built-in ASP.NET Core container)

**Frontend**
- React (Vite)
- Axios for API calls
- React Router (for multi-page navigation)

**Database**
- MongoDB (local instance or MongoDB Atlas)

## Project Structure

```
LibraryManagementSystem/
├── LibraryApi/                 # ASP.NET Core 10 Web API
│   ├── Controllers/            # HTTP endpoints (Books, Members, Borrow)
│   ├── Models/                 # MongoDB document schemas (Book, Member, BorrowRecord)
│   ├── DTOs/                   # Data transfer objects
│   ├── Repositories/           # Data access layer (MongoDB queries)
│   ├── Services/                # Business logic (e.g. borrow/return rules)
│   ├── Data/                    # MongoDbContext (collection accessors)
│   ├── Settings/                # Strongly-typed config (MongoDbSettings)
│   ├── Program.cs               # App startup, DI registration, middleware pipeline
│   ├── appsettings.Example.json # Template config (copy to appsettings.json and fill in real values)
│   └── appsettings.json         # Real local configuration (gitignored, not committed)
│
└── library-client/              # React (Vite) frontend
    ├── src/
    │   ├── api/                 # Axios instance / API config
    │   ├── components/          # BookList, AddBookForm, etc.
    │   ├── App.jsx
    │   └── App.css
    └── package.json
```

## Features

- ✅ Add, view, update, and delete books
- ✅ Track total vs. available copies per book
- ✅ Borrow a book (decrements available copies)
- ✅ Return a book (increments available copies)
- ✅ Manage library members
- ✅ Interactive API docs (Swagger / OpenAPI)

## Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (v18+)
- MongoDB — either:
  - [MongoDB Community Server](https://www.mongodb.com/try/download/community) running locally, or
  - A free [MongoDB Atlas](https://www.mongodb.com/cloud/atlas) cluster

### 1. Clone the repo
```bash
git clone https://github.com/omkarjha/library-management-system.git
cd library-management-system
```

### 2. Run the backend
```bash
cd LibraryApi
```

Copy the example config and fill in your own MongoDB details:
```bash
cp appsettings.Example.json appsettings.json
```

Edit `appsettings.json`:
```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "LibraryDb"
  }
}
```


Then run:
```bash
dotnet restore
dotnet run
```
The API will start at `http://localhost:5001` (check your terminal output for the exact port).

### 3. Run the frontend
In a separate terminal:
```bash
cd library-client
npm install
npm run dev
```
The app will be available at `http://localhost:5173`.


## API Endpoints

| Method | Endpoint                  | Description                     |
|--------|----------------------------|----------------------------------|
| GET    | `/api/books`               | Get all books                   |
| GET    | `/api/books/{id}`          | Get a single book by ID         |
| POST   | `/api/books`                | Add a new book                  |
| PUT    | `/api/books/{id}`          | Update a book                   |
| DELETE | `/api/books/{id}`          | Delete a book                   |
| GET    | `/api/members`              | Get all members                 |
| POST   | `/api/members`              | Add a new member                |
| POST   | `/api/borrow`                | Borrow a book                   |
| POST   | `/api/borrow/{id}/return`   | Return a borrowed book          |



## License
This project was built for learning purposes.
