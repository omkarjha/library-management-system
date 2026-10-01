import { NavLink } from "react-router-dom";

export default function Navbar() {
  return (
    <nav className="navbar">
      <NavLink to="/" end className={({ isActive }) => isActive ? "active" : ""}>Books</NavLink>
      <NavLink to="/members" className={({ isActive }) => isActive ? "active" : ""}>Members</NavLink>
      <NavLink to="/borrow" className={({ isActive }) => isActive ? "active" : ""}>Borrow</NavLink>
    </nav>
  );
}