import { BrowserRouter, Routes, Route } from "react-router-dom";
import Navbar from "./components/Navbar";
import BooksPage from "./pages/BooksPage";
import MembersPage from "./pages/MembersPage";
import BorrowPage from "./pages/BorrowPage";
import "./App.css";

function App() {
  return (
    <BrowserRouter>
      <div className="app-container">
        <div className="app-header">
          <h1>📚 Library Management System</h1>
        </div>
        <Navbar />
        <Routes>
          <Route path="/" element={<BooksPage />} />
          <Route path="/members" element={<MembersPage />} />
          <Route path="/borrow" element={<BorrowPage />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;