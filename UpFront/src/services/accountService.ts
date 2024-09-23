import axios from "axios";
import { TError } from "../types/error.type";
import { TAccount, TAccountDeposit, TAccountDepositRes, TAccountTransfer, TAccountWithdraw } from "../types/account.type";
import { TCustomer } from "../types/customer.type";
import upBackApi from "../utils/addJwtInterceptor";

export const getAccountByIdAsync = async (accountId: TAccount["id"]): Promise<TAccount | TError> => {
    try {
        const response = await upBackApi.get<TAccount>(`/account/${accountId}`);
        return response.data;
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

export const getAccountByCustomerIdAsync = async (customerId: TCustomer["id"]): Promise<TAccount | TError> => {
    try {
        const response = await upBackApi.get<TAccount>(`/account/customer/${customerId}`);
        return response.data;
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

export const getAccountByNumberAsync = async (accountNumber: TAccount["accountNumber"]): Promise<TAccount | TError> => {
    try {
        const response = await upBackApi.get<TAccount>(`/account/account-number/${accountNumber}`);
        return response.data;
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

export const depositAccountAsync = async (accountId: TAccount["id"], depositBody: TAccountDeposit): Promise<TAccountDepositRes | TError> => {
    try {
        const response = await upBackApi.post<TAccountDepositRes>(`/account/${accountId}/deposit`, depositBody);
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error) && error.response) {
            return {
                type: error.response.data?.type || "https://httpstatuses.com/" + error.response.status,
                title: error.response.data?.title || "Error",
                status: error.response.status,
                detail: error.response.data?.description || error.response.data?.detail,
            } as TError;
        }
        return {
            type: "https://httpstatuses.com/500",
            title: "An unexpected error occurred",
            status: 500,
            detail: "An unexpected error occurred",
        } as TError;
    }

}

export const withdrawAccountAsync = async (accountId: TAccount["id"], withdrawAmount: TAccountWithdraw): Promise<TAccountDepositRes | TError> => {
    try {
        const response = await upBackApi.post<TAccountDepositRes>(`/account/${accountId}/withdraw`, withdrawAmount);
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error) && error.response) {
            return {
                type: error.response.data?.type || "https://httpstatuses.com/" + error.response.status,
                title: error.response.data?.title || "Error",
                status: error.response.status,
                detail: error.response.data?.description || error.response.data?.detail,
            } as TError;
        }
        return {
            type: "https://httpstatuses.com/500",
            title: "An unexpected error occurred",
            status: 500,
            detail: "An unexpected error occurred",
        } as TError;
    }
}

export const transferAccountAsync = async (sourceAccountId: TAccount["id"], transferBody: TAccountTransfer): Promise<TAccountDepositRes | TError> => {
    try {
        const response = await upBackApi.post<TAccountDepositRes>(`/account/${sourceAccountId}/transfer`, transferBody);
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error) && error.response) {
            return {
                type: error.response.data?.type || "https://httpstatuses.com/" + error.response.status,
                title: error.response.data?.title || "Error",
                status: error.response.status,
                detail: error.response.data?.description || error.response.data?.detail,
            } as TError;
        }
        return {
            type: "https://httpstatuses.com/500",
            title: "An unexpected error occurred",
            status: 500,
            detail: "An unexpected error occurred",
        } as TError;
    }
}