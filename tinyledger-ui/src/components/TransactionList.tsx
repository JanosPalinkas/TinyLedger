import React, { useEffect, useState } from "react";
import { getBalance, postTransaction } from "../api/client";
import TransactionForm from "./TransactionForm";
import TransactionTable from "./TransactionTable";

import { Transaction } from "../types/types";

const TransactionList: React.FC = () => {
  const [transactions, setTransactions] = useState<Transaction[]>([]);
  const [balance, setBalance] = useState<number>(0);
  const accountId = "1";

  const fetchData = async () => {
    const balance = await getBalance(accountId);
    setBalance(balance);

    const res = await fetch(
      `http://localhost:5078/api/accounts/${accountId}/transactions`
    );
    const data = await res.json();
    setTransactions(data.items);
  };

  useEffect(() => {
    fetchData();
  }, []);

  const handleSubmit = async (
    amount: number,
    description: string,
    type: string
  ) => {
    await postTransaction(accountId, { amount, description, type });
    fetchData();
  };

  return (
    <div>
      <h2>Account Balance: ${balance.toFixed(2)}</h2>
      <TransactionForm onSubmit={handleSubmit} />
      <TransactionTable transactions={transactions} />
    </div>
  );
};

export default TransactionList;
