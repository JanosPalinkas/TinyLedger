import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5078/api",
});

export const getBalance = async (accountId: string) => {
  const response = await api.get(`/accounts/${accountId}/balance`);
  return response.data;
};

export const postTransaction = async (
  accountId: string,
  transaction: { amount: number; description: string; type: string }
) => {
  const response = await api.post(
    `/accounts/${accountId}/transactions`,
    transaction
  );
  return response.data;
};

export default api;
