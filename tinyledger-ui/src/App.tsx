import React from "react";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
  useLocation,
} from "react-router-dom";
import TransactionPage from "./components/Transaction/TransactionPage";
import CreateUserForm from "./components/User/CreateUserForm";
import LoginForm from "./components/Login/LoginForm";
import NavBar from "./components/Shared/NavBar";

const AppRoutes: React.FC = () => {
  const location = useLocation();
  const token = localStorage.getItem("token");
  const isAuthenticated = !!token;
  const showNav = isAuthenticated && location.pathname !== "/login";

  return (
    <>
      {showNav && <NavBar />}
      <Routes>
        <Route path="/login" element={<LoginForm />} />
        <Route
          path="/"
          element={
            isAuthenticated ? (
              <TransactionPage />
            ) : (
              <Navigate to="/login" replace />
            )
          }
        />
        <Route
          path="/users"
          element={
            isAuthenticated ? (
              <CreateUserForm />
            ) : (
              <Navigate to="/login" replace />
            )
          }
        />
        {/* Fallback for unknown paths */}
        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </>
  );
};

const App: React.FC = () => {
  return (
    <Router>
      <AppRoutes />
    </Router>
  );
};

export default App;
