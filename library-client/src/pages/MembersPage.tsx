import { useState } from "react";
import MemberList from "../components/MemberList";
import AddMemberForm from "../components/AddMemberForm";

export default function MembersPage() {
  const [refreshKey, setRefreshKey] = useState(0);

  return (
    <>
      <div className="card">
        <h2>Add a Member</h2>
        <AddMemberForm onAdded={() => setRefreshKey((k) => k + 1)} />
      </div>
      <div className="card">
        <h2>Members</h2>
        <MemberList key={refreshKey} />
      </div>
    </>
  );
}