// <copyright file="ServiceMockConfigurator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GtMotive.Estimate.Microservice.Tests.Infrastructure;

using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using Moq;

/// <summary>
/// Mock configurator for infrastructure tests.
/// Provides convenient methods for configuring mocked services.
/// </summary>
internal static class ServiceMockConfigurator
{
    /// <summary>
    /// Configures a basic IUnitOfWork mock that simulates successful operations.
    /// </summary>
    /// <param name="services">Service collection to configure.</param>
    /// <param name="saveResult">Result to return from Save method (default: 1).</param>
    public static void ConfigureUnitOfWorkMock(this IServiceCollection services, int saveResult = 1)
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(x => x.Save()).ReturnsAsync(saveResult);

        RemoveService<IUnitOfWork>(services);
        services.AddSingleton(mockUnitOfWork.Object);
    }

    /// <summary>
    /// Configures a basic IVehicleRepository mock for infrastructure tests.
    /// </summary>
    /// <param name="services">Service collection to configure.</param>
    /// <param name="vehicleExistsById">Whether a vehicle exists by ID (default: false).</param>
    public static void ConfigureVehicleRepositoryMock(this IServiceCollection services, bool vehicleExistsById = false)
    {
        var mockVehicleRepository = new Mock<IVehicleRepository>();
        mockVehicleRepository.Setup(x => x.ExistsByIdAsync(It.IsAny<VehicleId>()))
                            .ReturnsAsync(vehicleExistsById);

        RemoveService<IVehicleRepository>(services);
        services.AddSingleton(mockVehicleRepository.Object);
    }

    /// <summary>
    /// Minimal configuration for tests that only require HTTP validation.
    /// Does not execute business logic, only allows HTTP calls to reach the controller.
    /// </summary>
    /// <param name="services">Service collection to configure.</param>
    public static void ConfigureForHttpValidationOnly(this IServiceCollection services)
    {
        // Only the minimal mocks necessary for HTTP call to work
        services.ConfigureUnitOfWorkMock();
        services.ConfigureVehicleRepositoryMock();
    }

    /// <summary>
    /// Configures mocks for tests that need to simulate conflicts (vehicle already exists).
    /// </summary>
    /// <param name="services">Service collection to configure.</param>
    public static void ConfigureForConflictScenarios(this IServiceCollection services)
    {
        services.ConfigureUnitOfWorkMock();
        services.ConfigureVehicleRepositoryMock(vehicleExistsById: true); // Simulates conflict
    }

    /// <summary>
    /// Configures mocks for tests that need to simulate persistence errors.
    /// </summary>
    /// <param name="services">Service collection to configure.</param>
    public static void ConfigureForPersistenceErrors(this IServiceCollection services)
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(x => x.Save()).ThrowsAsync(new InvalidOperationException("Database error"));

        RemoveService<IUnitOfWork>(services);
        services.AddSingleton(mockUnitOfWork.Object);

        services.ConfigureVehicleRepositoryMock();
    }

    /// <summary>
    /// Removes a service from the collection if it exists.
    /// </summary>
    /// <typeparam name="T">Type of service to remove.</typeparam>
    /// <param name="services">Service collection.</param>
    private static void RemoveService<T>(IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(T));
        if (descriptor != null)
        {
            services.Remove(descriptor);
        }
    }
}
