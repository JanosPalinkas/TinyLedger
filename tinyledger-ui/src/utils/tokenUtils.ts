export interface DecodedUser {
  name: string;
  email: string;
  accountId: string;
  role: string;
}

export const getUserRoleFromToken = (): string | null => {
  const token = localStorage.getItem("token");
  if (!token) return null;

  try {
    const decoded = JSON.parse(atob(token.split(".")[1]));
    return decoded.role ?? null;
  } catch {
    return null;
  }
};

export const getDecodedUser = (): DecodedUser | null => {
  const token = localStorage.getItem("token");
  if (!token) return null;

  try {
    const decoded = JSON.parse(atob(token.split(".")[1]));
    return {
      name: decoded.name,
      email: decoded.email,
      accountId: decoded.accountId,
      role: decoded.role,
    };
  } catch {
    return null;
  }
};
