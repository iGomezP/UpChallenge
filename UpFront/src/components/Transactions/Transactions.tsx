import { Box } from '@mui/material';
import { useMainState } from '../../context/useMainStore';
import { NewTransaction } from './NewTransaction';

const Transactions = () => {
    const { accountBalance } = useMainState();

    const balance = accountBalance.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });

    return (
        <Box className="flex flex-col justify-center items-center min-h-screen w-full gap-5">
            <Box sx={{ p: "24px", borderRadius: "16px", backgroundColor: "#F7F7FC", width: '100%', maxWidth: '600px', height: "500  px" }}           >
                <h2 className="text-center text-2xl font-bold mb-4">New Transaction</h2>
                <div className="mb-8">
                    <h3 className="text-center text-xl font-semibold mb-2">Current Balance</h3>
                    <p className="text-center text-3xl font-bold text-blue-600">${balance}</p>
                </div>
                <NewTransaction />
            </Box>
        </Box>
    );
};

export default Transactions;
