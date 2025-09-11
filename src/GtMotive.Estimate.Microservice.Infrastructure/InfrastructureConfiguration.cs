using System;
using System.Diagnostics.CodeAnalysis;
using GtMotive.Estimate.Microservice.Domain.Factories;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Services;
using GtMotive.Estimate.Microservice.Infrastructure.Factories;
using GtMotive.Estimate.Microservice.Infrastructure.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Logging;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using GtMotive.Estimate.Microservice.Infrastructure.Repositories;
using GtMotive.Estimate.Microservice.Infrastructure.Telemetry;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: CLSCompliant(false)]

namespace GtMotive.Estimate.Microservice.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        [ExcludeFromCodeCoverage]
        public static IInfrastructureBuilder AddBaseInfrastructure(
            this IServiceCollection services,
            bool isDevelopment)
        {
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            if (!isDevelopment)
            {
                services.AddScoped<ITelemetry, AppTelemetry>();
            }
            else
            {
                services.AddScoped<ITelemetry, NoOpTelemetry>();
            }

            return new InfrastructureBuilder(services);
        }

        /// <summary>
        /// Adds Vehicle Renting Infrastructure to the ServiceCollection.
        /// </summary>
        /// <param name="builder">Infrastructure builder.</param>
        /// <param name="configuration">Configuration.</param>
        /// <returns>The modified builder.</returns>
        [ExcludeFromCodeCoverage]
        public static IInfrastructureBuilder AddVehicleRentingInfrastructure(
            this IInfrastructureBuilder builder,
            IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(builder);
            ArgumentNullException.ThrowIfNull(configuration);

            // MongoDB Configuration
            builder.Services.Configure<MongoDbSettings>(configuration.GetSection("MongoDb"));
            builder.Services.AddSingleton<MongoService>();

            // Unit of Work
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Repositories
            builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IRentalRepository, RentalRepository>();

            // Factories
            builder.Services.AddScoped<IVehicleFactory, VehicleFactory>();
            builder.Services.AddScoped<ICustomerFactory, CustomerFactory>();
            builder.Services.AddScoped<IRentalFactory, RentalFactory>();

            // Domain Services
            builder.Services.AddScoped<VehicleRentalService>();

            return builder;
        }

        private sealed class InfrastructureBuilder(IServiceCollection services) : IInfrastructureBuilder
        {
            public IServiceCollection Services { get; } = services;
        }
    }
}
