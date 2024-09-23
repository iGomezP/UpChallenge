import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Register from "./components/Register";
import Login from "./components/Login";
import Account from "./components/Accounts/Account";
import Transactions from "./components/Transactions/Transactions";
import Customer from "./components/Customers/Customer";
import Dashboard from './components/Dashboard';
import { Navigation } from "./components/Navigation";
import { Flip, ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import { useCheckUserAuthentication } from "./hooks/useCheckUserAuthentication";


function App() {
  return (
    <>
      <ToastContainer
        hideProgressBar={false}
        newestOnTop
        closeOnClick
        rtl={false}
        transition={Flip}
        closeButton={false}
        limit={3}
        pauseOnFocusLoss={false}
        position="top-center"
        theme="colored"
      />
      <Router>
        <AppRoutes />
      </Router>
    </>
  );
}

function AppRoutes() {
  useCheckUserAuthentication();

  return (
    <Routes>
      <Route index path="/" element={<Login />} />
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} />

      <Route path="/index" element={<Navigation />}>
        <Route index element={<Dashboard />} />
        <Route path="account" element={<Account />} />
        <Route path="transactions" element={<Transactions />} />
        <Route path="profile" element={<Customer />} />
      </Route>
    </Routes>
  );
}

export default App
