import axios from "axios";
import { TCustomer } from "../types/customer.type";
import { TError } from "../types/error.type";
import upBackApi from "../utils/addJwtInterceptor";

export const getCustomerInfoAsync = async (customerId: TCustomer["id"]): Promise<TCustomer | TError> => {
    try {
        const response = await upBackApi.get<TCustomer>(`/customers/${customerId}`);
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error) && error.response) {
            return {
                type: error.response.data?.type || "https://httpstatuses.com/" + error.response.status,
                title: error.response.data?.title || "Error",
                status: error.response.status,
                detail: error.response.data?.detail || "An error occurred while fetching customer data.",
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

