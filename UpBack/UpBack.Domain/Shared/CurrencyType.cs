namespace UpBack.Domain.Shared
{

    public record CurrencyType
    {
        public static readonly CurrencyType None = new("");
        public static readonly CurrencyType Usd = new("USD");
        public static readonly CurrencyType Mxn = new("MXN");
        // Constructor que genera una instancia en base a Codigo
        private CurrencyType(string code) => Code = code;

        public string? Code { get; init; }

        // Devolver una coleccion de monedas creadas
        public static readonly IReadOnlyCollection<CurrencyType> All = new[]
        {
            Usd, Mxn
        };

        // Devolver un solo tipo de moneda recibiendo el codigo como parametro
        public static CurrencyType FromCodigo(string code)
        {
            return All.FirstOrDefault(c => c.Code == code) ??
                throw new ApplicationException("Invalid currency type");
        }
    }
}
