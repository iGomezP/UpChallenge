using MediatR;

namespace UpBack.Domain.Abstractions
{
    public interface IDomainEvent : INotification
    {
        // Domain events: disparar un evento cuando cambia el estado o sucede algún cambio de estado dentro de una clase entidad
        // Se implementa el patron publish - subscriber
    }
}
