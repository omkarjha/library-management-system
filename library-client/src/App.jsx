import { useState } from "react";
import BookList from "./components/BookList";
import AddBookForm from "./components/AddBookForm";
import "./App.css";

function App() {
  const [refreshKey, setRefreshKey] = useState(0);

  return (
    <div className="app-container">
      <div className="app-header">
        <h1>📚 Library Management System</h1>
        <p>Manage your book catalog</p>
      </div>

      <div className="card">
        <h2>Add a Book</h2>
        <AddBookForm onAdded={() => setRefreshKey((k) => k + 1)} />
      </div>

      <div className="card">
        <h2>Book Catalog</h2>
        <BookList key={refreshKey} />
      </div>
    </div>
  );
}

export default App;