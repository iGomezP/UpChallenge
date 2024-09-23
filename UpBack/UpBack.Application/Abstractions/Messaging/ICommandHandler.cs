using MediatR;
using UpBack.Domain.Abstractions;

namespace UpBack.Application.Abstractions.Messaging
{
    // Aqui se recibe el mensaje que viene desde el command
    public interface ICommandHandler<TCommand>
        : IRequestHandler<TCommand, Result>
        where TCommand : ICommand
    {
    }

    public interface ICommandHandler<TCommand, TResponse>
        : IRequestHandler<TCommand, Result<TResponse>>
        where TCommand : ICommand<TResponse>
    {

    }
}
