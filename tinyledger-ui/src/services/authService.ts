import { jwtDecode } from "jwt-decode";
import { loginUser } from "../api/userApi";

export interface AuthInfo {
  token: string;
  email: string;
  name: string;
  roles: string[];
  accountId: string;
}

export const authenticate = async (
  email: string,
  password: string
): Promise<AuthInfo> => {
  const token = await loginUser(email, password);
  const decoded: any = jwtDecode(token);
  const roles = decoded.role ? [decoded.role].flat() : [];

  return {
    token,
    email: decoded.email,
    name: decoded.name,
    roles,
    accountId: decoded.accountId,
  };
};
