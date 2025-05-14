import api from "./axiosInstance";

export const loginUser = async (email: string, password: string) => {
  const response = await api.post("/auth/login", { email, password });
  return response.data.token;
};

export const createUser = async (
  name: string,
  email: string,
  password: string,
  role: string
) => {
  const response = await api.post("/users", { name, email, password, role });
  return response.data;
};
