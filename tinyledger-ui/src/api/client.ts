import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5078/api",
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token && config.headers) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export const getBalance = async () => {
  const response = await api.get("/accounts/balance");
  return response.data;
};

export const postTransaction = async (transaction: {
  amount: number;
  description: string;
  type: string;
}) => {
  const response = await api.post("/accounts/transactions", transaction);
  return response.data;
};

export const getTransactions = async () => {
  const response = await api.get("/accounts/transactions");
  return response.data;
};

export const loginUser = async (email: string, password: string) => {
  const response = await api.post("/auth/login", { email, password });
  return response.data.token;
};

export const createUser = async (
  name: string,
  email: string,
  password: string
) => {
  const response = await api.post("/users", { name, email, password });
  return response.data;
};

export default api;
