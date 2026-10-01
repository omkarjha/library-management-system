import { useEffect, useState } from "react";
import api from "../api/axios";
import type { Book, Member } from "../types";

interface BorrowFormProps {
  onBorrowed: () => void;
}

export default function BorrowForm({ onBorrowed }: BorrowFormProps) {
  const [books, setBooks] = useState<Book[]>([]);
  const [members, setMembers] = useState<Member[]>([]);
  const [bookId, setBookId] = useState("");
  const [memberId, setMemberId] = useState("");
  const [error, setError] = useState("");

  useEffect(() => {
    api.get<Book[]>("/books").then((res) => setBooks(res.data));
    api.get<Member[]>("/members").then((res) => setMembers(res.data));
  }, []);

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setError("");
    try {
      await api.post("/borrow", { bookId, memberId });
      onBorrowed();
      setBookId("");
      setMemberId("");
    } catch (err: any) {
      setError(err.response?.data?.error ?? "Something went wrong");
    }
  };

  return (
    <form className="book-form" onSubmit={handleSubmit}>
      <select value={bookId} onChange={(e) => setBookId(e.target.value)} required>
        <option value="">Select a book</option>
        {books.map((b) => (
          <option key={b.id} value={b.id}>
            {b.title} ({b.availableCopies} available)
          </option>
        ))}
      </select>

      <select value={memberId} onChange={(e) => setMemberId(e.target.value)} required>
        <option value="">Select a member</option>
        {members.map((m) => (
          <option key={m.id} value={m.id}>
            {m.name}
          </option>
        ))}
      </select>

      <button type="submit">Borrow</button>
      {error && <p style={{ color: "#d34141", gridColumn: "span 2" }}>{error}</p>}
    </form>
  );
}