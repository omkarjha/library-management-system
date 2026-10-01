import { useState } from "react";
import api from "../api/axios";

interface AddMemberFormProps {
  onAdded: () => void;
}

export default function AddMemberForm({ onAdded }: AddMemberFormProps) {
  const [form, setForm] = useState({ name: "", email: "" });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) =>
    setForm({ ...form, [e.target.name]: e.target.value });

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    await api.post("/members", form);
    onAdded();
    setForm({ name: "", email: "" });
  };

  return (
    <form className="book-form" onSubmit={handleSubmit}>
      <input name="name" placeholder="Name" value={form.name} onChange={handleChange} required />
      <input name="email" type="email" placeholder="Email" value={form.email} onChange={handleChange} required />
      <button type="submit">Add Member</button>
    </form>
  );
}