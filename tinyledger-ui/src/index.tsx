import React from "react";
import ReactDOM from "react-dom/client";
import TransactionList from "./components/TransactionList";

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <TransactionList />
  </React.StrictMode>
);