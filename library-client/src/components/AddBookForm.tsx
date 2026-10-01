import { useState } from "react";
import api from "../api/axios";

interface AddBookFormProps {
  onAdded: () => void;
}

export default function AddBookForm({ onAdded }: AddBookFormProps) {
  const [form, setForm] = useState({ title: "", author: "", isbn: "", totalCopies: 1 });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) =>
    setForm({ ...form, [e.target.name]: e.target.value });

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    await api.post("/books", {
      title: form.title,
      author: form.author,
      isbn: form.isbn,
      totalCopies: Number(form.totalCopies),
    });
    onAdded();
    setForm({ title: "", author: "", isbn: "", totalCopies: 1 });
  };

  return (
    <form className="book-form" onSubmit={handleSubmit}>
      <input name="title" placeholder="Title" value={form.title} onChange={handleChange} required />
      <input name="author" placeholder="Author" value={form.author} onChange={handleChange} required />
      <input name="isbn" placeholder="ISBN" value={form.isbn} onChange={handleChange} required />
      <input name="totalCopies" type="number" min="1" value={form.totalCopies} onChange={handleChange} />
      <button type="submit">Add Book</button>
    </form>
  );
}