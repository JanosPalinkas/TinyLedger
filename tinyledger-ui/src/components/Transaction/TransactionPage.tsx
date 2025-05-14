import React from "react";
import { useTransactions } from "../../hooks/useTransactions";
import TransactionForm from "./TransactionForm";
import TransactionTable from "./TransactionTable";
import AccountBalance from "../Shared/AccountBalance";
import UserInfo from "../Shared/UserInfo";

const TransactionPage: React.FC = () => {
  const { balance, transactions, submitTransaction } = useTransactions();

  return (
    <div>
      <AccountBalance balance={balance} />
      <TransactionForm onSubmit={submitTransaction} />
      <TransactionTable transactions={transactions} />
      <UserInfo />
    </div>
  );
};

export default TransactionPage;
