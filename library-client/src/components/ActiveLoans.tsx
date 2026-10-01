import { useEffect, useState } from "react";
import api from "../api/axios";
import type { BorrowRecord, Book, Member } from "../types";

interface ActiveLoansProps {
  refreshKey: number;
  onReturned: () => void;
}

export default function ActiveLoans({ refreshKey, onReturned }: ActiveLoansProps) {
  const [records, setRecords] = useState<BorrowRecord[]>([]);
  const [books, setBooks] = useState<Book[]>([]);
  const [members, setMembers] = useState<Member[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    Promise.all([
      api.get<BorrowRecord[]>("/borrow"),
      api.get<Book[]>("/books"),
      api.get<Member[]>("/members"),
    ])
      .then(([recordsRes, booksRes, membersRes]) => {
        setRecords(recordsRes.data);
        setBooks(booksRes.data);
        setMembers(membersRes.data);
      })
      .catch((err) => console.error(err))
      .finally(() => setLoading(false));
  }, [refreshKey]);

  const activeLoans = records.filter((r) => r.returnedAt === null);

  const getBookTitle = (bookId: string) =>
    books.find((b) => b.id === bookId)?.title ?? "Unknown Book";

  const getMemberName = (memberId: string) =>
    members.find((m) => m.id === memberId)?.name ?? "Unknown Member";

  const handleReturn = async (recordId: string) => {
    await api.post(`/borrow/${recordId}/return`);
    onReturned();
  };

  if (loading) return <p className="empty-state">Loading...</p>;
  if (activeLoans.length === 0) return <p className="empty-state">No active loans.</p>;

  return (
    <table>
      <thead>
        <tr>
          <th>Book</th>
          <th>Member</th>
          <th>Borrowed On</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        {activeLoans.map((r) => (
          <tr key={r.id}>
            <td>{getBookTitle(r.bookId)}</td>
            <td>{getMemberName(r.memberId)}</td>
            <td>{new Date(r.borrowedAt).toLocaleDateString()}</td>
            <td>
              <button onClick={() => handleReturn(r.id)}>Return</button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}