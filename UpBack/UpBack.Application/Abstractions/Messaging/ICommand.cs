using MediatR;
using UpBack.Domain.Abstractions;

namespace UpBack.Application.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result>, IBaseCommand
    {
    }

    // En caso de que se necesite devolver algo desde los commands
    public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
    {
    }

    // Interface en común para agrarle a futuro constraints de verificación de interfaces o componentes
    public interface IBaseCommand
    {
    }
}
