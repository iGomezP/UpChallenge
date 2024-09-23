import { Link, Outlet, useNavigate } from 'react-router-dom'
import { useMainState } from '../context/useMainStore';
import { useEffect } from 'react';
import { showToast } from '../hooks/useCustomToast';
import { Box } from '@mui/material';
import { useAccountData } from '../hooks/useAccountData';
import { useTransactionState } from '../context/useTransactionStore';

export function Navigation() {
    const {
        customerId,
        customerData,
        clearUserData, } = useMainState();
    const { clearTransactionState } = useTransactionState();
    const navigate = useNavigate();
    const { getAccountData } = useAccountData(customerId!);

    useEffect(() => {
        if (customerId) {
            getAccountData();
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [customerId])

    const handleLogout = () => {
        clearUserData(); // Limpiar el estado del usuario
        clearTransactionState();
        showToast({
            message: "Hope to see you soon! Have a great day!",
            severity: "info",
        });
        navigate("/login"); // Redirigir al login
    };

    return (
        <Box>
            <nav className="bg-blue-600 text-white p-4">
                <Box className="container mx-auto flex justify-between items-center">
                    <Link to="/index" className="text-2xl font-bold">UpBack</Link>
                    <Box className="flex flex-row gap-10">
                        {customerData ?
                            <Box>
                                <h3>Welcome {customerData?.name}!</h3>
                            </Box> :
                            <></>}
                        <Box className="flex gap-4">
                            <Link to={`/index`} className="hover:underline">Dashboard</Link>
                            <Link to={`account`} className="hover:underline">Account</Link>
                            <Link to={`transactions`} className="hover:underline">Transactions</Link>
                            <Link to={`profile`} className="hover:underline">Profile</Link>
                            <button onClick={handleLogout} className="hover:underline">Logout</button>
                        </Box>
                    </Box>
                </Box>
            </nav>
            <Outlet />
        </Box>
    );
}