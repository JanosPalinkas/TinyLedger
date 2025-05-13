import React from "react";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  useLocation,
} from "react-router-dom";
import TransactionList from "./components/TransactionList";
import CreateUserForm from "./components/CreateUserForm";
import LoginForm from "./components/LoginForm";
import NavBar from "./components/NavBar";

const AppRoutes: React.FC = () => {
  const location = useLocation();
  const isAuthenticated = !!localStorage.getItem("token");
  const showNav = isAuthenticated && location.pathname !== "/login";

  return (
    <>
      {showNav && <NavBar />}
      <Routes>
        <Route path="/login" element={<LoginForm />} />
        <Route path="/" element={<TransactionList />} />
        <Route path="/users" element={<CreateUserForm />} />
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
