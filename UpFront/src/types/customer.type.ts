export type TCustomer = {
    id: string;
    name: string;
    lastName: string;
    email: string;
    password?: string;
    phoneNumber: string;
    birthDay: TBirthDay;
    address: TAddress;
    createdDate: Date;
    objectStatus: string;
}

export type TRegisterReq =
    Pick<TCustomer,
        "name" |
        "lastName" |
        "email" |
        "password" |
        "phoneNumber" |
        "birthDay" |
        "createdDate" |
        "objectStatus"> & {
            street: string;
            city: string;
            state: string;
            zipCode: string;
            country: string;
        }

export type TRegisterResp = {
    customerId: string;
    token: string;
}

export type TLogin = Pick<TCustomer, "email" | "password">;

type TAddress = {
    street: string;
    city: string;
    state: string;
    zipCode: string;
    country: string;
}

type TBirthDay = {
    year: number;
    month: number;
    day: number;
}


