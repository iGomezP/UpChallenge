using MediatR;
using UpBack.Domain.Abstractions;

namespace UpBack.Application.Abstractions.Messaging
{
    // Maneja todos los querys agregando la estructura de datos a sus hijos
    // Revuelve la respesta generica con la estructura de result
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
