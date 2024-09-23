using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using UpBack.Application.Abstractions.Clock;
using UpBack.Application.Abstractions.Data;
using UpBack.Application.Abstractions.Email;
using UpBack.Application.Services;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Accounts.Repositories;
using UpBack.Domain.Customers.Repositories;
using UpBack.Domain.Roles.Repositories;
using UpBack.Domain.Transactions.Repositories;
using UpBack.Infrastructure.Clock;
using UpBack.Infrastructure.Data;
using UpBack.Infrastructure.Email;
using UpBack.Infrastructure.Repositories;

namespace UpBack.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IEmailService, EmailService>();

            var mongoConnectionString = configuration.GetConnectionString("MongoDB");
            var mongoDatabaseName = configuration.GetConnectionString("MongoDBName");

            services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                return new MongoClient(mongoConnectionString);
            });

            services.AddScoped<IMongoDatabase>(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(mongoDatabaseName);
            });


            var connectionString = configuration.GetConnectionString("MsSql")
                ?? throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(connectionString).UseSnakeCaseNamingConvention();
            });

            services.AddScoped<IGuidValidationService, GuidValidationService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            // MSSql (CUD)
            services.AddScoped<ICustomerSqlRepository, CustomerSqlRepository>();
            services.AddScoped<IAccountSqlRepository, AccountSqlRepository>();
            services.AddScoped<ITransactionSqlRepository, TransactionSqlRepository>();
            services.AddScoped<IRoleSqlRepository, RoleSqlRepository>();

            // Mongo (R)
            services.AddScoped<ICustomerMongoRepository, CustomerMongoRepository>();
            services.AddScoped<IAccountMongoRepository, AccountMongoRepository>();
            services.AddScoped<ITransactionMongoRepository, TransactionMongoRepository>();
            services.AddScoped<IRoleMongoRepository, RoleMongoRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDBContext>());

            services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

            return services;
        }
    }
}
