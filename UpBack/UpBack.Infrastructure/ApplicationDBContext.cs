using MediatR;
using Microsoft.EntityFrameworkCore;
using UpBack.Application.Exceptions;
using UpBack.Domain.Abstractions;

namespace UpBack.Infrastructure
{
    public sealed class ApplicationDBContext : DbContext, IUnitOfWork
    {
        private readonly IPublisher _publisher;

        public ApplicationDBContext(DbContextOptions options, IPublisher publisher) : base(options)
        {
            _publisher = publisher;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // El assembly contiene el DB context
            // Cuando el modelo este configurado va a escanear este assembly encontrando todas las configuraciones de las entidades
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                // De persistencia en memoria la BD
                var result = await base.SaveChangesAsync(cancellationToken);

                await PublishDomainEventsAsync();

                return result;
            }
            // Ocurre cuando hay una violacion a las reglas en la BD en el proceso de insertar
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException(ex.Message, ex);
            }
        }

        private async Task PublishDomainEventsAsync()
        {
            // Obtener todos los entity domain events
            var domainEvents = ChangeTracker
                .Entries<Entity>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    var domainEvents = entity.GetDomainEvents();
                    entity.ClearDomainEvents();
                    return domainEvents;
                })
                .ToList();

            // Publicar
            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent);
            }
        }
    }
}
