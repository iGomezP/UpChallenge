using System.Data;

namespace UpBack.Application.Abstractions.Data
{
    public interface ISqlConnectionFactory
    {
        // Crear un objeto connection
        IDbConnection CreateConnection();
    }
}
