import React, { useEffect, useState } from "react";
import { getBalance, postTransaction, getTransactions } from "../api/client";
import TransactionForm from "./TransactionForm";
import TransactionTable from "./TransactionTable";
import { Transaction } from "../types/types";

interface AuthData {
  email: string;
  name: string;
  role: string;
  accountId: string;
}

const TransactionList: React.FC = () => {
  const [transactions, setTransactions] = useState<Transaction[]>([]);
  const [balance, setBalance] = useState<number>(0);
  const [authData, setAuthData] = useState<AuthData | null>(null);

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) {
      try {
        const decoded = JSON.parse(atob(token.split(".")[1]));
        setAuthData({
          email: decoded.email,
          name: decoded.name,
          role: decoded.role,
          accountId: decoded.accountId,
        });
      } catch (error) {
        console.error("Invalid JWT", error);
      }
    }
  }, []);

  const fetchData = async () => {
    try {
      const balance = await getBalance();
      setBalance(balance);

      const res = await getTransactions();
      setTransactions(res.items || []);
    } catch (err) {
      console.error("Failed to fetch data:", err);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  const handleSubmit = async (
    amount: number,
    description: string,
    type: string
  ) => {
    await postTransaction({ amount, description, type });
    fetchData();
  };

  return (
    <div>
      <h2>Account Balance: ${balance.toFixed(2)}</h2>
      <TransactionForm onSubmit={handleSubmit} />
      <TransactionTable transactions={transactions} />

      {authData && (
        <div
          style={{
            marginTop: "2rem",
            borderTop: "1px solid #ccc",
            paddingTop: "1rem",
          }}
        >
          <h3>User Info from JWT</h3>
          <p>
            <strong>Name:</strong> {authData.name}
          </p>
          <p>
            <strong>Email:</strong> {authData.email}
          </p>
          <p>
            <strong>Account ID:</strong> {authData.accountId}
          </p>
          <p>
            <strong>Role:</strong> {authData.role}
          </p>
        </div>
      )}
    </div>
  );
};

export default TransactionList;
