import React from "react";
import { Transaction } from "../../types/types";

interface Props {
  transactions: Transaction[];
}

const TransactionTable: React.FC<Props> = ({ transactions }) => (
  <div>
    <h3>Transaction History</h3>
    <table border={1} cellPadding={8} cellSpacing={0}>
      <thead>
        <tr>
          <th>Date</th>
          <th>Amount</th>
          <th>Type</th>
          <th>Description</th>
        </tr>
      </thead>
      <tbody>
        {transactions.map((t) => (
          <tr key={t.id}>
            <td>{new Date(t.timestamp).toLocaleString()}</td>
            <td>${t.amount.toFixed(2)}</td>
            <td>{t.type}</td>
            <td>{t.description}</td>
          </tr>
        ))}
      </tbody>
    </table>
  </div>
);

export default TransactionTable;
