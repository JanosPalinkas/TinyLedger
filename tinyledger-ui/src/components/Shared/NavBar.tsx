import React from "react";
import { Link, useNavigate } from "react-router-dom";
import { getUserRoleFromToken } from "../../utils/tokenUtils";

const NavBar: React.FC = () => {
  const navigate = useNavigate();
  const role = getUserRoleFromToken();

  const handleLogout = () => {
    localStorage.removeItem("token");
    navigate("/login");
  };

  return (
    <nav style={{ padding: "1rem", background: "#eee", marginBottom: "1rem" }}>
      <Link to="/" style={{ marginRight: "1rem" }}>
        Transactions
      </Link>

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
