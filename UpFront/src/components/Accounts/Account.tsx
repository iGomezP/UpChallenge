import { Link } from 'react-router-dom';
import { useMainState } from '../../context/useMainStore';
import { Box, Button } from '@mui/material';
import { useTransactionState } from '../../context/useTransactionStore';

const Account = () => {
    const { accountData, accountBalance, transactionsData } = useMainState();
    const { clearTransactionState } = useTransactionState();
    const balance = accountBalance.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });

    if (!accountData) {
        return <div>Loading...</div>;
    }

    return (
        <Box className="flex flex-col justify-center items-center min-h-screen w-full gap-5">
            <Box sx={{ p: "24px", borderRadius: "16px", backgroundColor: "#F7F7FC", width: '100%', maxWidth: '600px' }}     >
                <h2 className="text-center text-2xl font-bold mb-4">Account Details</h2>
                <Box className="mb-8">
                    <h3 className="text-center text-xl font-semibold mb-2">Current Balance</h3>
                    <p className="text-center text-3xl font-bold text-blue-600">${balance}</p>
                </Box>
                <Box className="mb-8">
                    <Link to="/index/transactions">
                        <Button variant='contained' fullWidth onClick={() => clearTransactionState()}>Make a Transaction</Button>
                    </Link>
                </Box>
                <Box>
                    <h3 className="text-xl font-semibold mb-2">Recent Transactions</h3>
                    <table className="min-w-full divide-y divide-gray-200">
                        <thead className="bg-gray-50">
                            <tr>
                                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Date</th>
                                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Description</th>
                                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Amount</th>
                                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Status</th>
                            </tr>
                        </thead>
                        <tbody className="bg-white divide-y divide-gray-200">
                            {transactionsData && transactionsData.length > 0 ? (
                                transactionsData.map((transaction) => (
                                    <tr key={transaction.id}>
                                        <td className="px-6 py-4 whitespace-nowrap">
                                            {new Intl.DateTimeFormat('es-ES', {
                                                day: '2-digit',
                                                month: '2-digit',
                                                year: 'numeric',
                                            }).format(new Date(transaction.transactionDate))}
                                        </td>
                                        <td className="px-6 py-4 whitespace-nowrap">{transaction.reference}</td>
                                        <td className={`px-6 py-4 whitespace-nowrap ${transaction.quantity >= 0 ? 'text-green-600' : 'text-red-600'}`}>
                                            ${transaction.quantity.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
                                        </td>
                                        <td className="px-6 py-4 whitespace-nowrap">{transaction.status}</td>
                                    </tr>
                                ))
                            ) : (
                                <tr>
                                    <td colSpan={4} className="text-center py-4">
                                        No transactions available.
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </Box>
            </Box>
        </Box>
    )
};

export default Account;
