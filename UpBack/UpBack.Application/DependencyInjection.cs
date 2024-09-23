using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UpBack.Application.Abstractions.Behaviors;

namespace UpBack.Application
{
    // Clase encargada de registrar los servicios
    public static class DependencyInjection
    {
        // Aqui se registran todos los servicios que van para la api
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Comunicación entre commands, querys y sus respectivos handlers usando mediaTR
            services.AddMediatR(configuration =>
            {
                // Inyección de commands, querys y sus respectivos handlers dentro del proyecto
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

                // Inyeccion del logging
                configuration.AddOpenBehavior(typeof(LogginBehavior<,>));

                configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            services.AddAuthentication("Bearer")
                .AddJwtBearer("UpBackScheme", x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = "https://localhost:7158/",
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = "https://localhost:5173/",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("6M2U7FX9!mbT$zVqHjRtWp+LfK%xS3Ed"))
                    };
                });

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}
