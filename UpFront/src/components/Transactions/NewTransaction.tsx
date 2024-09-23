/* eslint-disable @typescript-eslint/no-explicit-any */
import { Box, Button, CircularProgress, Tab, Tabs, TextField } from '@mui/material'
import { useState } from 'react';
import MoneyInput from './MoneyInput';
import TargetAccountInput from './TargetAccountInput';
import { useTransactionState } from '../../context/useTransactionStore';
import { useMainState } from '../../context/useMainStore';
import { depositAccountAsync, transferAccountAsync, withdrawAccountAsync } from '../../services/accountService';
import { showToast } from '../../hooks/useCustomToast';
import { TError } from '../../types/error.type';
import { useNavigate } from 'react-router-dom';
import { TAccountDeposit, TAccountTransfer, TAccountWithdraw } from '../../types/account.type';
import { useAccountData } from '../../hooks/useAccountData';

export const NewTransaction = () => {
    const [currentTab, setCurrentTab] = useState(0);
    const navigate = useNavigate();
    const {
        originAccount,
        reference,
        quantity,
        setReference,
        clearTransactionState,
        targetAccountId
    } = useTransactionState();
    const {
        customerId,
        accountData
    } = useMainState();
    const { getAccountData } = useAccountData(customerId!);
    const [loading, setLoading] = useState(false);


    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;

        if (name === "reference") {
            setReference(value);
        }
    };

    const handleSubmit = async (e: any) => {
        e.preventDefault();
        const transactionType = e.target.name;

        if (transactionType === "deposit") {
            setLoading(true);
            const depositBody: TAccountDeposit = {
                customerId: customerId!,
                accountNumber: accountData.accountNumber,
                initialBalance: +quantity,
                reference: reference
            };

            try {

                const response = await depositAccountAsync(accountData.id, depositBody);

                if (response && "status" in response) {
                    throw {
                        type: response.type,
                        title: response.title,
                        status: response.status,
                        detail: response.detail,
                    } as TError;
                } else {
                    clearTransactionState();
                    getAccountData();
                    navigate("/index/account");
                }
            } catch (error: any) {
                showToast({
                    message: `${error.title} - ${error.status} - ${error.detail}`,
                    severity: "error"
                })
                console.error("Error", error);
            } finally {
                setLoading(false);
            }
        }

        if (transactionType === "withdraw") {

            const withdrawBody: TAccountWithdraw = {
                amount: +quantity,
                reference
            }
            try {
                setLoading(true);
                const response = await withdrawAccountAsync(accountData.id, withdrawBody);

                if (response && "status" in response) {
                    throw {
                        type: response.type,
                        title: response.title,
                        status: response.status,
                        detail: response.detail,
                    } as TError;
                } else {
                    clearTransactionState();
                    getAccountData();
                    navigate("/index/account");
                }
            } catch (error: any) {
                showToast({
                    message: `${error.title} - ${error.status} - ${error.detail}`,
                    severity: "error"
                })
                console.error("Error", error);
            } finally {
                setLoading(false);
            }
        }

        if (transactionType === "transfer") {
            const transferBody: TAccountTransfer = {
                targetAccountId: targetAccountId,
                amount: +quantity,
                reference
            }

            try {
                setLoading(true);
                const response = await transferAccountAsync(originAccount, transferBody);

                console.log(response)

                if (response && response && "status" in response) {
                    throw {
                        type: response.type,
                        title: response.title,
                        status: response.status,
                        detail: response.detail,
                    } as TError;
                } else {
                    clearTransactionState();
                    getAccountData();
                    navigate("/index/account");
                }
            } catch (error: any) {
                showToast({
                    message: `${error.title} - ${error.status} - ${error.detail}`,
                    severity: "error"
                })
                console.error("Error", error);
            } finally {
                setLoading(false);
            }
        }
    }

    const handleTabChange = (_event: React.SyntheticEvent, newValue: number) => {
        clearTransactionState();
        setCurrentTab(newValue);
    };

    return (
        <Box position="relative">
            {loading && (
                <Box
                    position="absolute"
                    top={0}
                    left={0}
                    right={0}
                    bottom={0}
                    display="flex"
                    alignItems="center"
                    justifyContent="center"
                    bgcolor="rgba(255, 255, 255, 0.5)"
                    zIndex={2}
                >
                    <CircularProgress />
                </Box>
            )}

            <Box className="flex flex-col w-full max-w-sm mx-auto gap-5">

                <Tabs value={currentTab} onChange={handleTabChange} centered>
                    <Tab label="Deposit" />
                    <Tab label="Withdrawal" />
                    <Tab label="Transfer" />
                </Tabs>

                {currentTab === 0 && (
                    <Box component="form" onSubmit={handleSubmit} sx={{ mt: 2 }} name='deposit'>
                        <TargetAccountInput isOwnAcccount={true} />
                        <MoneyInput />
                        <TextField
                            required
                            label="Reference"
                            type="text"
                            variant="standard"
                            onChange={handleChange}
                            name="reference"
                            fullWidth
                        />
                        <Button variant="contained" sx={{ mt: 5, mb: 5, ml: 'auto', display: 'block' }} fullWidth type='submit' >
                            Deposit
                        </Button>
                    </Box>
                )}

                {currentTab === 1 && (
                    <Box component="form" onSubmit={handleSubmit} sx={{ mt: 2 }} name='withdraw'>
                        <TargetAccountInput isOwnAcccount={true} />
                        <MoneyInput />
                        <TextField
                            required
                            label="Reference"
                            type="text"
                            variant="standard"
                            onChange={handleChange}
                            name="reference"
                            fullWidth
                        />
                        <Box className="flex flex-row justify-between">
                            <Button variant="contained" sx={{ mt: 5, mb: 5 }} fullWidth type='submit' >
                                Withdraw
                            </Button>
                        </Box>
                    </Box>
                )}

                {currentTab === 2 && (
                    <Box component="form" onSubmit={handleSubmit} sx={{ mt: 2 }} name='transfer'>
                        <TargetAccountInput isOwnAcccount={false} />
                        <MoneyInput />
                        <TextField
                            required
                            label="Reference"
                            type="text"
                            variant="standard"
                            onChange={handleChange}
                            name="reference"
                            fullWidth
                        />
                        <Box className="flex flex-row justify-between">
                            <Button variant="contained" sx={{ mt: 5, mb: 5 }} fullWidth type='submit' disabled={!targetAccountId} >
                                Transfer
                            </Button>
                        </Box>
                    </Box>
                )}
            </Box>
        </Box>
    )
}
