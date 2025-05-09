import http from "k6/http";
import { check } from "k6/http";

export const options = {
  insecureSkipTLSVerify: true,
  noConnectionReuse: false,
  stages: [
    { duration: "1m", target: 1400 }, // ramp-up to 10 users
    { duration: "10s", target: 0 }, // scale down to 0 users
  ],
};

const accountId = "1";
const API_BASE_URL = "http://localhost:5078";
const apiTransactionsUrl = `/api/accounts/${accountId}/transactions`;
const apiBalanceUrl = `/api/accounts/${accountId}/balance`;

const depositPayload = JSON.stringify({
  amount: 100.0,
  type: "Deposit",
  description: "Initial deposit",
});
/* 
const withdrawPayload = JSON.stringify({
  amount: 15.0,
  type: "Withdraw",
  description: "Groceries",
}); */

const headers = {
  "Content-Type": "application/json",
};

export default () => {
  // Deposit
  const res = http.post(
    `${API_BASE_URL}${apiTransactionsUrl}`,
    depositPayload,
    {
      headers,
    }
  );

  /*
  // Withdraw
  http.post(`${API_BASE_URL}${apiTransactionsUrl}`, withdrawPayload, {
    headers,
  });
  /
  // Get balance
  //const res = http.get(`${API_BASE_URL}${apiBalanceUrl}`);
  

  check(res, {
    "is status 200": (r) => r.status === 200,
  });
  */
};
