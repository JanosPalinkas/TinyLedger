import http from "k6/http";
import { sleep } from "k6";

export const options = {
  insecureSkipTLSVerify: true,
  noConnectionReuse: false,
  stages: [
    { duration: "5m", target: 100 }, // simulate ramp-up of traffic from 1 to 100 users over 5 minutes
    { duration: "10m", target: 100 }, // stay at 100 users for 10 minutes
    { duration: "5m", target: 0 }, // ramp-down to 0 users
  ],
  thresholds: {
    http_req_duration: ["p(99)<150"], // 99% of requests must complete below 150ms
  },
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
