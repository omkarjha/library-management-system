import { useState } from "react";
import BookList from "../components/BookList";
import AddBookForm from "../components/AddBookForm";

export default function BooksPage() {
  const [refreshKey, setRefreshKey] = useState(0);

  return (
    <>
      <div className="card">
        <h2>Add a Book</h2>
        <AddBookForm onAdded={() => setRefreshKey((k) => k + 1)} />
      </div>
      <div className="card">
        <h2>Book Catalog</h2>
        <BookList key={refreshKey} />
      </div>
    </>
  );
}