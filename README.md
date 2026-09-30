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

> ⚠️ `appsettings.json` is gitignored on purpose — never commit real database credentials. Only `appsettings.Example.json` (with placeholder values) is tracked in this repo.

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

> **Note:** Make sure the API's CORS policy in `Program.cs` allows the frontend's origin (`http://localhost:5173` by default), and that `src/api/axios.js` in the frontend points to the correct backend URL/port.

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

## Architecture Notes

- **Layered design:** Controllers handle HTTP concerns only; Services hold business rules (like "can't borrow if no copies available"); Repositories are the only layer that talks to MongoDB. This keeps the codebase testable and makes it easy to swap out the database layer if needed.
- **Document modeling:** `BorrowRecord.ReturnedAt` being `null` represents an active (not-yet-returned) loan, rather than a separate status field.
- **Known limitation:** The borrow flow currently performs two separate writes (decrementing `AvailableCopies` and inserting a `BorrowRecord`), which isn't atomic. A production version would use MongoDB's `FindOneAndUpdate` with a conditional filter (`AvailableCopies > 0`) to prevent a race condition on the last available copy.

## Roadmap / Possible Improvements
- [ ] Atomic borrow operation to eliminate race condition
- [ ] Proper DTO usage across all endpoints (currently models are exposed directly on some routes)
- [ ] Authentication (JWT) and role-based access control (Admin / Librarian / Member)
- [ ] Frontend migration to TypeScript (TSX)
- [ ] Pagination and search/filter on the book catalog
- [ ] Due dates and overdue tracking
- [ ] Unit tests for services and repositories

## Security Note
This repo previously had a real MongoDB connection string accidentally committed and has since been rebuilt with clean history. Config secrets are now gitignored by default — see `appsettings.Example.json` for the required structure.

## License
This project was built for learning purposes.