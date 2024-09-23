import { TCustomer } from "./customer.type";

export interface TAccount {
    id: string;
    accountNumber: string;
    balance: number;
    objectStatus: string;
    createdDate: Date;
    customer: TCustomer;
}

export type TAccountDeposit = {
    customerId: string;
    accountNumber: string;
    initialBalance: number;
    reference: string;
}

export type TAccountWithdraw = {
    amount: number;
    reference: string;
}

export type TAccountTransfer = {
    targetAccountId: string;
    amount: number;
    reference: string;
}

export type TAccountDepositRes = {
    depositStatus: number;
}