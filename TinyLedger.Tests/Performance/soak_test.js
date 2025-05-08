import http from "k6/http";
import { sleep } from "k6";

export const options = {
  insecureSkipTLSVerify: true,
  noConnectionReuse: false,
  stages: [
    { duration: "2m", target: 400 }, // ramp-up to 400 users
    { duration: "3h56m", target: 400 }, // stay at 400 users for ~4 hours
    { duration: "2m", target: 0 }, // scale down to 0 users
  ],
};

const accountId = "1";
const API_BASE_URL = "http://localhost:5078";
const apiTransactionsUrl = `/api/accounts/${accountId}/transactions`;
const apiBalanceUrl = `/api/accounts/${accountId}/balance`;

const bodyRequestDeposit = JSON.stringify({
  amount: 100.0,
  type: "Deposit",
  description: "Initial deposit",
});

const bodyRequestWithdraw = JSON.stringify({
  amount: 15.0,
  type: "Withdraw",
  description: "Groceries",
});

const headers = {
  "Content-Type": "application/json",
};

export default () => {
  /*
  // Deposit
  http.post(`${API_BASE_URL}${apiTransactionsUrl}`, bodyRequestDeposit, {
    headers,
  });

  // Withdraw
  http.post(`${API_BASE_URL}${apiTransactionsUrl}`, bodyRequestWithdraw, {
    headers,
  });
*/
  // Get balance
  http.get(`${API_BASE_URL}${apiBalanceUrl}`);

  sleep(1);
};
