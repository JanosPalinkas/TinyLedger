import React from "react";

interface Props {
  balance: number;
}

const AccountBalance: React.FC<Props> = ({ balance }) => (
  <h2>Account Balance: ${balance.toFixed(2)}</h2>
);

export default AccountBalance;
