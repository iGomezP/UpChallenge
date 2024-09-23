import axios from "axios";
import { TRegisterReq, TRegisterResp, TLogin } from "../types/customer.type";
import { TError } from "../types/error.type";

const API_URL = "https://localhost:7158/api";

const apiService = axios.create({
    baseURL: API_URL,
    headers: {
        "Content-Type": "application/json",
        "accept": "*",
    },

});

export const registerUserAsync = async (data: TRegisterReq): Promise<TRegisterResp | TError> => {
    try {
        const response = await apiService.post("user/register", data);
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error) && error.response) {
            console.log(error.response)
            return {
                type: error.response.data?.type || "https://httpstatuses.com/" + error.response.status,
                title: error.response.data?.code || "Error",
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

export const loginUserAsync = async (data: TLogin): Promise<TRegisterResp | TError> => {
    try {
        const response = await apiService.post("user/login", data);
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error) && error.response) {
            return {
                type: error.response.data?.type || "https://httpstatuses.com/" + error.response.status,
                title: error.response.data?.code || "Error",
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