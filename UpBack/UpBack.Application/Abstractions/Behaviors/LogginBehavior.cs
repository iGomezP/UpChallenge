using MediatR;
using Microsoft.Extensions.Logging;
using UpBack.Application.Abstractions.Messaging;

namespace UpBack.Application.Abstractions.Behaviors
{
    // Aplicamos el loggin solo en las operaciones CUD sin consultar ni lectura de datos
    // Esto porque las consultas deben ser rápidas en la aplicación y debe tener las menos dependencias posibles
    public class LogginBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
    {
        private readonly ILogger<TRequest> _logger = logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Recuperar nombre de la clase al que pertenece
            var name = request.GetType().Name;

            try
            {
                _logger.LogInformation($"Executing command: {name}");
                var result = await next();
                _logger.LogInformation($"The command {name} was executed successfully.");

                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"The command {name} encountered errors.");
                throw;
            }
        }
    }
}
