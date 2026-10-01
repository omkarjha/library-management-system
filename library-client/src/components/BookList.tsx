import { useEffect, useState } from "react";
import api from "../api/axios";
import type { Book } from "../types";

export default function BookList() {
  const [books, setBooks] = useState<Book[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    api.get<Book[]>("/books")
      .then((res) => setBooks(res.data))
      .catch((err) => console.error(err))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <p className="empty-state">Loading...</p>;
  if (books.length === 0) return <p className="empty-state">No books yet — add one above.</p>;

  return (
    <table>
      <thead>
        <tr>
          <th>Title</th>
          <th>Author</th>
          <th>Available</th>
        </tr>
      </thead>
      <tbody>
        {books.map((b) => (
          <tr key={b.id}>
            <td>{b.title}</td>
            <td>{b.author}</td>
            <td>
              <span className={`badge ${b.availableCopies > 0 ? "available" : "low"}`}>
                {b.availableCopies} / {b.totalCopies}
              </span>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}