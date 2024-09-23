import Cookies from 'js-cookie';
import { create } from 'zustand';
import { persist } from 'zustand/middleware';
import { TAccount } from '../types/account.type';
import { TCustomer } from '../types/customer.type';
import { TTransaction } from '../types/transaction.type';

interface MainState {
    customerId: string | null;
    accountNumber: string | null;
    accountData: TAccount;
    customerData: TCustomer;
    transactionsData: TTransaction[];
    jwtToken: string;
    accountBalance: TAccount["balance"];
    setAccountData: (accountData: TAccount) => void;
    setCustomerData: (customerData: TCustomer) => void;
    setTransactionsData: (transactionsData: TTransaction[]) => void;
    setCustomerId: (customerId: string) => void;
    setAccountNumber: (accountNumber: string) => void;
    setJwtToken: (token: string) => void;
    setAccountBalance: (accountBalance: number) => void;
    clearUserData: () => void;
}

const customStorage = {
    getItem: (name: string) => {
        const value = Cookies.get(name);
        if (!value) return null;
        return JSON.parse(value);
    },
    setItem: (name: string, value: unknown) => {
        Cookies.set(name, JSON.stringify(value), { expires: 7 });
    },
    removeItem: (name: string) => {
        Cookies.remove(name);
    }
};


export const useMainState = create<MainState>()(
    persist(
        (set) => ({
            accountNumber: null,
            customerId: null,
            accountData: {} as TAccount,
            customerData: {} as TCustomer,
            transactionsData: {} as TTransaction[],
            jwtToken: "",
            accountBalance: 0,

            setAccountData: (accountData: TAccount) => {
                set({ accountData })
            },

            setCustomerData: (customerData: TCustomer) => {
                set({ customerData })
            },

            setTransactionsData: (transactionsData: TTransaction[]) => {
                set({ transactionsData })
            },

            setCustomerId: (customerId: string) => {
                set({ customerId });
                Cookies.set('customerId', customerId, { expires: 7 });
            },

            setAccountNumber: (accountNumber: string) => {
                set({ accountNumber: accountNumber });
            },

            setJwtToken: (token: string) => {
                set({ jwtToken: token })
                Cookies.set('jwt', token, { expires: 7 });
            },

            setAccountBalance: (accountBalance: number) => {
                set({ accountBalance })
            },

            clearUserData() {
                set({
                    customerId: null,
                    accountNumber: null,
                    accountData: {} as TAccount,
                    customerData: {} as TCustomer,
                    transactionsData: [],
                    jwtToken: "",
                    accountBalance: 0,
                });
                Cookies.remove('customerId');
                Cookies.remove('jwt');
            },
        }),
        {
            name: 'user-storage',
            storage: customStorage,
        }
    )
);