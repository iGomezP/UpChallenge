import { create } from 'zustand';

interface TransactionState {
    targetAccountNumber: string;
    targetAccountId: string;
    originAccount: string;
    quantity: string;
    reference: string;
    setTargetAccountNumber: (targetAccount: string) => void;
    setTargetAccountId: (targetAccountId: string) => void;
    setOriginAccount: (originAccount: string) => void;
    setQuantity: (quantity: string) => void;
    setReference: (reference: string) => void;
    clearTransactionState: () => void;
}

export const useTransactionState = create<TransactionState>()((set) => ({
    targetAccountNumber: "",
    targetAccountId: "",
    originAccount: "",
    quantity: "",
    reference: "",

    setTargetAccountNumber: (targetAccount: string) => {
        set({ targetAccountNumber: targetAccount })
    },

    setTargetAccountId: (targetAccountId: string) => {
        set({ targetAccountId })
    },

    setOriginAccount: (originAccount: string) => {
        set({ originAccount })
    },

    setQuantity: (quantity: string) => {
        set({ quantity })
    },

    setReference: (reference: string) => {
        set({ reference })
    },

    clearTransactionState() {
        set({
            targetAccountNumber: "",
            quantity: ""
        })
    },
}));