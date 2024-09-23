namespace UpBack.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        // Lo que va a hacer es tomar todo los cambios hechos en la persistencia y va a confirmar si se insertan en la BD
        /*
         * A veces que la transacción demora más de lo esperado por algún error, en cuyo caso no se puede bloquear el tráfico
         * por eso se indica que la operación se cancele usando CancellationToken
         *
         * */
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
