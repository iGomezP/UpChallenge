namespace UpBack.Domain.Shared
{
    public record Currency(decimal Amount, CurrencyType CurrencyType)
    {
        // Crear un acumulador del dinero que se va entregando
        // Se debe validar el tipo de moneda porque no es lo mismo USD que MXN
        // Si no es el mismo tipo de moneda lanzar una excepcion
        public static Currency operator +(Currency first, Currency second)
        {
            if (first.CurrencyType != second.CurrencyType)
            {
                throw new InvalidOperationException("The currency type must be the same");
            }

            return new Currency(first.Amount + second.Amount, first.CurrencyType);
        }

        // Metodo que devuelva una instancia de moneda en 0 y sin tipo de moneda
        public static Currency Zero() => new(0, CurrencyType.None);
        // Metodo que devuelva una instancia de moneda en 0 y con tipo de moneda
        public static Currency Zero(CurrencyType currencyType) => new(0, currencyType);

        // Si el monto recibido es 0
        public bool IsZero() => this == Zero(CurrencyType);
    }
}
