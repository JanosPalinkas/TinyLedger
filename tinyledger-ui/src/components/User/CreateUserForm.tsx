import React from "react";
import { useCreateUserForm } from "./useCreateUserForm";

const CreateUserForm: React.FC = () => {
  const { formData, handleChange, handleSubmit, submitted } =
    useCreateUserForm();

  return (
    <div>
      <h2>Create New User</h2>
      {submitted ? (
        <p>User created successfully!</p>
      ) : (
        <form onSubmit={handleSubmit}>
          <input
            type="text"
            value={formData.name}
            onChange={(e) => handleChange("name", e.target.value)}
            placeholder="Name"
            required
          />
          <input
            type="email"
            value={formData.email}
            onChange={(e) => handleChange("email", e.target.value)}
            placeholder="Email"
            required
          />
          <input
            type="password"
            value={formData.password}
            onChange={(e) => handleChange("password", e.target.value)}
            placeholder="Password"
            required
          />
          <select
            value={formData.role}
            onChange={(e) => handleChange("role", e.target.value)}
            required
          >
            <option value="User">User</option>
            <option value="Admin">Admin</option>
          </select>
          <button type="submit">Create User</button>
        </form>
      )}
    </div>
  );
};

export default CreateUserForm;
