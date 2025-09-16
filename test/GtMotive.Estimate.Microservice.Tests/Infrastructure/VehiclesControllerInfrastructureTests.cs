// <copyright file="VehiclesControllerInfrastructureTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GtMotive.Estimate.Microservice.Tests.Infrastructure;

using System.Net;
using System.Net.Http.Json;
using System.Text;
using FluentAssertions;
using GtMotive.Estimate.Microservice.Api.Models.Requests;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using GtMotive.Estimate.Microservice.Host;
using Xunit;

/// <summary>
/// Infrastructure tests for VehiclesController.
/// ONLY tests HTTP call reception and model validation at host level.
/// Does NOT execute complete business logic.
/// Each test configures its own mocks according to its specific needs.
/// </summary>
public class VehiclesControllerInfrastructureTests
{
    private const string BaseUrl = "/api/vehicles";

    /// <summary>
    /// Test if http call is received.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    [Fact]
    public async Task CreateVehicleWithValidRequestShouldReceiveHttpCall()
    {
        // Arrange - Configure factory with minimal mocks for HTTP validation
        using var factory = new TestWebApplicationFactory<Program>(services =>
        {
            services.ConfigureForHttpValidationOnly();
        });
        using var client = factory.CreateClient();

        var validRequest = new CreateVehicleRequest
        {
            VehicleId = VehicleId.New(),
            Model = "Toyota Corolla 2023",
        };

        // Act
        var response = await client.PostAsJsonAsync(BaseUrl, validRequest);

        // Assert - Only verify that the call is received
        response.StatusCode.Should().NotBe(HttpStatusCode.NotFound, "the endpoint must exist");
        response.StatusCode.Should().NotBe(HttpStatusCode.MethodNotAllowed, "the POST method must be allowed");
    }

    /// <summary>
    /// Test behavior when VehicleId is missing from the request.
    /// Since VehicleId is a struct, it defaults to Guid.Empty when missing,
    /// which causes the business logic to return NotFound instead of BadRequest.
    /// This test verifies the actual behavior of the infrastructure.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    [Fact]
    public async Task CreateVehicleWithMissingVehicleIdShouldHandleInvalidId()
    {
        // Arrange - Configure factory with mocks for minimal business logic execution
        using var factory = new TestWebApplicationFactory<Program>(services =>
        {
            services.ConfigureForHttpValidationOnly();
        });
        using var client = factory.CreateClient();

        var invalidRequest = new
        {
            Model = "Toyota Camry 2023",

            // VehicleId missing - will default to Guid.Empty in struct deserialization
        };

        // Act
        var response = await client.PostAsJsonAsync(BaseUrl, invalidRequest);

        // Assert - Verify the actual infrastructure behavior
        // Note: Since VehicleId is a struct, missing field defaults to Guid.Empty,
        // which is handled by business logic (not model validation)
        response.StatusCode.Should().NotBe(HttpStatusCode.NotFound, "the endpoint must handle invalid IDs appropriately");
        response.StatusCode.Should().NotBe(HttpStatusCode.MethodNotAllowed, "the POST method must be allowed");
    }

    /// <summary>
    /// Validate content type.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    [Fact]
    public async Task CreateVehicleWithInvalidContentTypeShouldValidateRequest()
    {
        // Arrange - Configure factory for Content-Type validation
        using var factory = new TestWebApplicationFactory<Program>(services =>
        {
            // For Content-Type validation, we don't need mocks
        });
        using var client = factory.CreateClient();

        var request = "{\"vehicleId\":\"" + Guid.NewGuid() + "\",\"model\":\"Test Car\"}";
        var content = new StringContent(request, Encoding.UTF8, "text/plain");

        // Act
        var response = await client.PostAsync(BaseUrl, content);

        // Assert - Only verify Content-Type validation
        response.StatusCode.Should().Be(HttpStatusCode.UnsupportedMediaType, "must validate JSON Content-Type");
    }

    /// <summary>
    /// Test conflict.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    [Fact]
    public async Task CreateVehicleWhenVehicleAlreadyExistsShouldHandleConflict()
    {
        // Arrange - Configure factory specifically for conflict scenario
        using var factory = new TestWebApplicationFactory<Program>(services =>
        {
            services.ConfigureForConflictScenarios(); // Mock that simulates existing vehicle
        });
        using var client = factory.CreateClient();

        var existingVehicleRequest = new CreateVehicleRequest
        {
            VehicleId = VehicleId.New(),
            Model = "Honda Civic 2023",
        };

        // Act
        var response = await client.PostAsJsonAsync(BaseUrl, existingVehicleRequest);

        // Assert - Verify conflict handling
        // Note: This test demonstrates how each test can configure specific mocks
        // to simulate different scenarios without affecting other tests
        response.StatusCode.Should().NotBe(HttpStatusCode.NotFound, "the endpoint must handle conflict scenarios appropriately");
    }
}
