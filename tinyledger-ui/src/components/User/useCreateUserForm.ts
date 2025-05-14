import { useState } from "react";
import { createUser } from "../../api/userApi";

export const useCreateUserForm = () => {
  const [formData, setFormData] = useState({
    name: "",
    email: "",
    password: "",
    role: "User",
  });
  const [submitted, setSubmitted] = useState(false);

  const handleChange = (field: string, value: string) => {
    setFormData((prev) => ({ ...prev, [field]: value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const { name, email, password, role } = formData;
    try {
      await createUser(name, email, password, role);
      setSubmitted(true);
    } catch (error) {
      console.error("Failed to create user:", error);
    }
  };

  return { formData, handleChange, handleSubmit, submitted };
};
