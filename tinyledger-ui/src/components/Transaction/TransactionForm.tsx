import React, { useState } from "react";

interface Props {
  onSubmit: (amount: number, description: string, type: string) => void;
}

const transactionTypes = ["Deposit", "Withdraw"];

const TransactionForm: React.FC<Props> = ({ onSubmit }) => {
  const [amount, setAmount] = useState(0);
  const [description, setDescription] = useState("");
  const [type, setType] = useState(transactionTypes[0]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit(amount, description, type);
    setAmount(0);
    setDescription("");
    setType(transactionTypes[0]);
  };

  return (
    <form onSubmit={handleSubmit} style={{ marginBottom: "20px" }}>
      <input
        type="number"
        value={amount}
        onChange={(e) => setAmount(parseFloat(e.target.value))}
        placeholder="Amount"
        required
      />
      <input
        type="text"
        value={description}
        onChange={(e) => setDescription(e.target.value)}
        placeholder="Description"
        required
      />
      <select value={type} onChange={(e) => setType(e.target.value)} required>
        {transactionTypes.map((option) => (
          <option key={option} value={option}>
            {option}
          </option>
        ))}
      </select>
      <button type="submit">Submit</button>
    </form>
  );
};

export default TransactionForm;
