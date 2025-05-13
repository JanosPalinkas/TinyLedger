export interface Transaction {
  id: string;
  timestamp: string;
  amount: number;
  type: string;
  description: string;
}

export interface User {
  id: string;
  name: string;
  email: string;
  accountId: string;
}

export interface NewUser {
  name: string;
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  accountId: string;
}
