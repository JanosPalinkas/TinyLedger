import React from "react";
import { Link, useNavigate } from "react-router-dom";

const NavBar: React.FC = () => {
  const navigate = useNavigate();
  const token = localStorage.getItem("token");

  let role: string | null = null;
  if (token) {
    try {
      const decoded = JSON.parse(atob(token.split(".")[1]));
      role = decoded.role;
    } catch (err) {
      console.error("Invalid JWT", err);
    }
  }

  const handleLogout = () => {
    localStorage.removeItem("token");
    navigate("/login");
  };

  return (
    <nav style={{ padding: "1rem", background: "#eee", marginBottom: "1rem" }}>
      <Link to="/" style={{ marginRight: "1rem" }}>
        Transactions
      </Link>

      {}
      {role === "Admin" && (
        <Link to="/users" style={{ marginRight: "1rem" }}>
          Create User
        </Link>
      )}

      <button onClick={handleLogout}>Logout</button>
    </nav>
  );
};

export default NavBar;
