/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable @typescript-eslint/no-explicit-any */
import { useEffect } from "react";
import { getAccountByCustomerIdAsync, getAccountByNumberAsync } from "../services/accountService"; // Ajusta la ruta al servicio
import { TError } from "../types/error.type";
import { getTransactionsByAccountIdAsync } from "../services/transactionService";
import { TAccount } from "../types/account.type";
import { useMainState } from "../context/useMainStore";
import { useTransactionState } from "../context/useTransactionStore";
import { showToast } from "./useCustomToast";

export const useAccountData = (customerId: string) => {
    const { setAccountData, setCustomerData, setAccountBalance, setTransactionsData, setAccountNumber } = useMainState();
    const { setOriginAccount, targetAccountNumber, setTargetAccountId } = useTransactionState();

    const getAccountData = async () => {
        try {
            const response = await getAccountByCustomerIdAsync(customerId);

            if (response && "status" in response) {
                throw {
                    type: response.type,
                    title: response.title,
                    status: response.status,
                    detail: response.detail,
                } as TError;
            } else {
                setOriginAccount(response.id);
                setAccountData(response);
                setCustomerData(response.customer);
                setAccountBalance(response.balance);
                getTransactionsData(response.id);
                setAccountNumber(response.accountNumber)
            }
        } catch (error: any) {
            showToast({
                message: `${error.title} - ${error.status} - ${error.detail}`,
                severity: "error"
            });
            console.error("Error fetching account data", error);
        }
    };

    useEffect(() => {
        if (customerId) {
            getAccountData();
        }
    }, [customerId]);

    const getTransactionsData = async (accountId: TAccount["id"]) => {
        try {
            const response = await getTransactionsByAccountIdAsync(accountId);

            if (response && "status" in response) {
                throw {
                    type: response.type,
                    title: response.title,
                    status: response.status,
                    detail: response.detail,
                } as TError;
            } else {
                setTransactionsData(response)
            }
        } catch (error: any) {
            if (error.status != 404)
                showToast({
                    message: `${error.title} - ${error.status} - ${error.detail}`,
                    severity: "error"
                })
            console.warn("Error logging in", error);
        }
    }

    const getTargetAccountData = async () => {
        const result = await getAccountByNumberAsync(targetAccountNumber!);

        if (result && "status" in result) {
            console.log(result.detail)
        } else {
            setTargetAccountId(result.id)
        }
    }

    useEffect(() => {
        if (targetAccountNumber) {
            getTargetAccountData();
        }
    }, [targetAccountNumber]);

    return { getAccountData };
};

