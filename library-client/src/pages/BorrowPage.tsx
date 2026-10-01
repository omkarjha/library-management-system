import { useState } from "react";
import BorrowForm from "../components/BorrowForm";
import ActiveLoans from "../components/ActiveLoans";

export default function BorrowPage() {
  const [refreshKey, setRefreshKey] = useState(0);

  return (
    <>
      <div className="card">
        <h2>Borrow a Book</h2>
        <BorrowForm onBorrowed={() => setRefreshKey((k) => k + 1)} />
      </div>
      <div className="card">
        <h2>Active Loans</h2>
        <ActiveLoans refreshKey={refreshKey} onReturned={() => setRefreshKey((k) => k + 1)} />
      </div>
    </>
  );
}