// <copyright file="TestWebApplicationFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GtMotive.Estimate.Microservice.Tests;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using GtMotive;
using GtMotive.Estimate;
using GtMotive.Estimate.Microservice;
using GtMotive.Estimate.Microservice.Tests;
using GtMotive.Estimate.Microservice.Tests.Infrastructure;
using GtMotive.Estimate.Microservice.Tests;

/// <summary>
/// Configurable factory for HTTP infrastructure tests.
/// Allows each test to configure its own mocks according to specific needs.
/// </summary>
/// <typeparam name="TProgram">The main program type.</typeparam>
internal class TestWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    private readonly Action<IServiceCollection>? configureServices;


    /// <summary>
    /// Initializes a new instance of the <see cref="TestWebApplicationFactory{TProgram}"/> class.
    /// </summary>
    /// <param name="configureServices">Configure services.</param>
    public TestWebApplicationFactory(Action<IServiceCollection>? configureServices = null)
    {
        this.configureServices = configureServices;
    }

    /// <inheritdoc/>
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        
        builder.ConfigureAppConfiguration((context, config) =>
        {
            // Clear existing sources to avoid issues with Azure Key Vault, etc.
            config.Sources.Clear();
            
            // Add minimal test configuration
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "MongoDb:ConnectionString", "mongodb://localhost:27017" },
                { "MongoDb:DatabaseName", "TestDB" },
                { "Logging:LogLevel:Default", "Warning" },
                { "Logging:LogLevel:Microsoft", "Warning" },
                { "Logging:LogLevel:Microsoft.AspNetCore", "Warning" },
                { "PathBase", "/" },
                { "AppSettings:JwtAuthority", "https://localhost:5001" },
                { "AppSettings:KeyVaultName", "" },
            });
        });

        builder.ConfigureServices(services =>
        {
            // Minimal logging for tests
            services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Warning);
            });

            // Allow each test to configure its specific services
            configureServices?.Invoke(services);
        });

        // Disable strict service provider validation for tests
        builder.UseDefaultServiceProvider(options =>
        {
            options.ValidateScopes = false;
            options.ValidateOnBuild = false;
        });
    }
}
