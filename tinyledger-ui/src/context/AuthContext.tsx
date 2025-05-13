import React, { createContext, useState } from "react";

interface AuthData {
  token: string;
  email: string;
  name: string;
  roles: string[];
}

interface AuthContextType {
  authData: AuthData | null;
  setAuthData: (data: AuthData | null) => void;
}

export const AuthContext = createContext<AuthContextType>({
  authData: null,
  setAuthData: () => {},
});

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [authData, setAuthData] = useState<AuthData | null>(null);

  return (
    <AuthContext.Provider value={{ authData, setAuthData }}>
      {children}
    </AuthContext.Provider>
  );
};
