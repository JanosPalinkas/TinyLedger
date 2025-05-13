import React, { useState } from "react";
import api from "../api/client";

const CreateUserForm: React.FC = () => {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [role, setRole] = useState("User");
  const [submitted, setSubmitted] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const response = await api.post("/users", {
        name,
        email,
        password,
        role,
      });
      console.log("User created:", response.data);
      setSubmitted(true);
    } catch (error) {
      console.error("Failed to create user:", error);
    }
  };

  return (
    <div>
      <h2>Create New User</h2>
      {submitted ? (
        <p>User created successfully!</p>
      ) : (
        <form onSubmit={handleSubmit}>
          <div>
            <label>Name: </label>
            <input
              type="text"
              value={name}
              onChange={(e) => setName(e.target.value)}
              required
            />
          </div>
          <div style={{ marginTop: "0.5rem" }}>
            <label>Email: </label>
            <input
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
          </div>
          <div style={{ marginTop: "0.5rem" }}>
            <label>Password: </label>
            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </div>
          <div style={{ marginTop: "0.5rem" }}>
            <label>Role: </label>
            <select
              value={role}
              onChange={(e) => setRole(e.target.value)}
              required
            >
              <option value="User">User</option>
              <option value="Admin">Admin</option>
            </select>
          </div>
          <button type="submit" style={{ marginTop: "1rem" }}>
            Create User
          </button>
        </form>
      )}
    </div>
  );
};

export default CreateUserForm;
