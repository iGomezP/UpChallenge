namespace UpBack.Domain.Abstractions
{
    public abstract class Entity
    {
        protected Entity()
        {

        }

        // Este es el padre de las otras entidades, por lo que aqui se definen como se manejan los eventos
        // Inicializamos para preparalo para multiples eventos.
        // Para definir de manera concreta los eventos basados en las reglas de negocio, lo cual se hace en cada entidad
        private readonly List<IDomainEvent> _domainEvents = new();

        // Solo se puede usar por aquellas clases hijas de Entity
        protected Entity(Guid id)
        {
            Id = id;
        }

        // Identifica de forma unica a cada una de las entidades de nuestro dominio
        public Guid Id { get; init; }

        // Devolver la lista existente de Domain events
        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents.ToList();
        }

        // limpiarla del cache
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        // disparar el evento (agregar el evento a la lista)
        // Protected: solo puede ser implementado por los hijos de Entity
        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
