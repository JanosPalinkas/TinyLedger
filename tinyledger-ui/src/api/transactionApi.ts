import api from "./axiosInstance";

export const getBalance = async () => {
  const response = await api.get("/accounts/balance");
  return response.data;
};

export const getTransactions = async () => {
  const response = await api.get("/accounts/transactions");
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
