import { InputAdornment, TextField } from '@mui/material';
import React, { useState } from 'react';
import { useTransactionState } from '../../context/useTransactionStore';

const MoneyInput = () => {
    const { quantity, setQuantity } = useTransactionState();
    const [error, setError] = useState<string | null>(null);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const inputValue = e.target.value.replace(/[^0-9.]/g, ''); // Limpiar la entrada, solo números y punto decimal

        // Validación para que tenga máximo 2 decimales
        if (!/^(\d+(\.\d{0,2})?)$/.test(inputValue)) {
            setError('Please enter a valid amount');
        } else {
            setError(null); // El formato es válido
        }

        setQuantity(inputValue);
    };

    const handleBlur = () => {
        const numberValue = parseFloat(quantity);

        // Si el valor es un número válido, lo formateamos
        if (!isNaN(numberValue)) {
            const formattedValue = numberValue.toFixed(2); // Asegurarse de tener 2 decimales
            setQuantity(formattedValue); // Guardamos el valor formateado sin el símbolo $
        } else {
            setQuantity(""); // Si no es un número válido, limpiar el campo
        }
    };

    return (
        <TextField
            required
            label="Quantity"
            variant="standard"
            value={quantity ? `${quantity}` : ""} // Mostrar el valor con $
            onChange={handleChange}
            onBlur={handleBlur}
            name="quantity"
            error={!!error}
            helperText={error}
            fullWidth
            slotProps={{
                input: {
                    inputMode: 'decimal', // Asegurar que el input sea numérico
                    startAdornment: <InputAdornment position="start">$</InputAdornment>
                }
            }}
        />
    );
};

export default MoneyInput;
