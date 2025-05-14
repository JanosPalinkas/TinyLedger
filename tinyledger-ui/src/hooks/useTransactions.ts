import { useState, useEffect } from "react";
import {
  getBalance,
  getTransactions,
  postTransaction,
} from "../api/transactionApi";
import { Transaction } from "../types/types";

export const useTransactions = () => {
  const [transactions, setTransactions] = useState<Transaction[]>([]);
  const [balance, setBalance] = useState<number>(0);

  const fetchTransactions = async () => {
    const res = await getTransactions();
    setTransactions(res.items || []);
  };

  const fetchBalance = async () => {
    const result = await getBalance();
    setBalance(result);
  };

  const refreshData = async () => {
    await Promise.all([fetchBalance(), fetchTransactions()]);
  };

  const submitTransaction = async (
    amount: number,
    description: string,
    type: string
  ) => {
    await postTransaction({ amount, description, type });
    await refreshData();
  };

  useEffect(() => {
    refreshData();
  }, []);

  return { balance, transactions, submitTransaction };
};
