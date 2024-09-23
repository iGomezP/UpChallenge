export type TTransaction = {
    id: string;
    accountId: string;
    transactionType: string;
    quantity: number;
    transactionDate: Date;
    reference: string;
    status: string;
}