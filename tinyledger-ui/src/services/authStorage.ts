import { AuthInfo } from "./authService";

export const persistAuthData = (authInfo: AuthInfo) => {
  localStorage.setItem("token", authInfo.token);
  localStorage.setItem("accountId", authInfo.accountId);
};

export const clearAuthData = () => {
  localStorage.removeItem("token");
  localStorage.removeItem("accountId");
};
