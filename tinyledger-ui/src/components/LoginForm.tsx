import React, { useState, useContext } from "react";
import axios from "axios";
import { jwtDecode } from "jwt-decode";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../context/AuthContext";

const LoginForm: React.FC = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const { setAuthData } = useContext(AuthContext);
  const navigate = useNavigate();

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const res = await axios.post("http://localhost:5078/api/auth/login", {
        email,
        password,
      });
      const token = res.data.token;
      localStorage.setItem("token", token);

      const decoded: any = jwtDecode(token);
      setAuthData({
        token,
        email: decoded.email,
        name: decoded.name,
        roles: decoded.role ? [decoded.role].flat() : [],
      });

      navigate("/");
    } catch (err) {
      console.error("Login failed:", err);
      alert("Invalid credentials");
    }
  };

  return (
    <form onSubmit={handleLogin}>
      <h2>Login</h2>
      <input
        type="email"
        placeholder="Email"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        required
      />
      <br />
      <input
        type="password"
        placeholder="Password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        required
      />
      <br />
      <button type="submit">Login</button>
    </form>
  );
};

export default LoginForm;
