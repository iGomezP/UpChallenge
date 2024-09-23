import { TextField } from '@mui/material';
import React, { useState } from 'react';
import { useTransactionState } from '../../context/useTransactionStore';
import { useMainState } from '../../context/useMainStore';

type TargetAccountInputProps = {
    isOwnAcccount: boolean;
}

const TargetAccountInput = ({ isOwnAcccount }: TargetAccountInputProps) => {
    const { targetAccountNumber, setTargetAccountNumber } = useTransactionState();
    const { accountNumber } = useMainState();
    const [error, setError] = useState<string | null>(null);

    const handleChange = async (e: React.ChangeEvent<HTMLInputElement>) => {
        const inputValue = e.target.value;

        const cleanedValue = inputValue.replace(/[^0-9]/g, '');
        if (cleanedValue.length < 10 || cleanedValue.length > 20) {
            setError('Account number must be between 10 and 20 digits');
        } else {
            setError(null);
        }
        setTargetAccountNumber(cleanedValue);
    };

    if (isOwnAcccount) {
        return (
            <TextField
                required
                label="Origin Account"
                variant="standard"
                value={accountNumber}
                name="originAccount"
                fullWidth
                inputProps={{ maxLength: 20, inputMode: 'numeric', pattern: '[0-9]*' }}
                disabled
            />
        )
    }

    return (
        <TextField
            required
            label="Target Account"
            variant="standard"
            value={targetAccountNumber}
            onChange={handleChange}
            error={!!error}
            helperText={error}
            name="targetAccount"
            fullWidth
            inputProps={{ maxLength: 20, inputMode: 'numeric', pattern: '[0-9]*' }} // Restricción de longitud máxima
        />
    );
};

export default TargetAccountInput;
