import { useEffect, useState } from "react";
import api from "../api/axios";
import type { Member } from "../types";

export default function MemberList() {
  const [members, setMembers] = useState<Member[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    api.get<Member[]>("/members")
      .then((res) => setMembers(res.data))
      .catch((err) => console.error(err))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <p className="empty-state">Loading...</p>;
  if (members.length === 0) return <p className="empty-state">No members yet — add one above.</p>;

  return (
    <table>
      <thead>
        <tr>
          <th>Name</th>
          <th>Email</th>
          <th>Joined</th>
        </tr>
      </thead>
      <tbody>
        {members.map((m) => (
          <tr key={m.id}>
            <td>{m.name}</td>
            <td>{m.email}</td>
            <td>{new Date(m.joinedAt).toLocaleDateString()}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}