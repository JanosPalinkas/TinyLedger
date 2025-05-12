import React, { useState } from "react";

interface Props {
  onSubmit: (amount: number, description: string, type: string) => void;
}

const TransactionForm: React.FC<Props> = ({ onSubmit }) => {
  const [amount, setAmount] = useState<number>(0);
  const [description, setDescription] = useState<string>("");
  const [type, setType] = useState<string>("Deposit");

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit(amount, description, type);
    setAmount(0);
    setDescription("");
    setType("Deposit");
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
        <option value="Deposit">Deposit</option>
        <option value="Withdraw">Withdraw</option>
      </select>
      <button type="submit">Submit</button>
    </form>
  );
};

export default TransactionForm;
