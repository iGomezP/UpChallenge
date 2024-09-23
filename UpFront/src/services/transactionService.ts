import axios from "axios";
import { TError } from "../types/error.type";
import { TAccount } from "../types/account.type";
import { TTransaction } from "../types/transaction.type";
import upBackApi from "../utils/addJwtInterceptor";

export const getTransactionsByAccountIdAsync = async (accountId: TAccount["id"]): Promise<TTransaction[] | TError> => {
    try {
        const result = await upBackApi.get<TTransaction[]>(`/transactions/account/${accountId}`);
        return result.data;
    } catch (error) {
        if (axios.isAxiosError(error) && error.response) {
            return {
                type: error.response.data?.type || "https://httpstatuses.com/" + error.response.status,
                title: error.response.data?.title || "Error",
                status: error.response.status,
                detail: error.response.data?.description || "An error occurred while fetching customer data.",
            } as TError;
        }
        return {
            type: "https://httpstatuses.com/500",
            title: "An unexpected error occurred",
            status: 500,
            detail: "An unexpected error occurred",
        } as TError;
    }
};