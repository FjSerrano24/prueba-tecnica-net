// <copyright file="CreateVehicleFunctionalTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GtMotive.Estimate.Microservice.Tests.Functional;

using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using GtMotive.Estimate.Microservice.Api.Models.Requests;
using GtMotive.Estimate.Microservice.Api.Models.Responses;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using GtMotive.Estimate.Microservice.Tests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Moq;

/// <summary>
/// Functional tests for CreateVehicle use case.
/// Tests the complete business flow from API to Domain excluding external dependencies.
/// These tests execute the real business logic and verify end-to-end behavior.
/// </summary>
public class CreateVehicleFunctionalTests
{
    private const string BaseUrl = "/api/vehicles";

    /// <summary>
    /// Tests successful vehicle creation through the complete application pipeline.
    /// Verifies that a valid request flows through all layers and creates a vehicle correctly.
    /// </summary>
    [Fact]
    public async Task CreateVehicle_WithValidRequest_ShouldExecuteCompleteBusinessFlow()
    {
        // Arrange - Configure application with mocked infrastructure dependencies only
        var capturedVehicle = default(Vehicle);
        var mockVehicleRepository = new Mock<IVehicleRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        // Configure repository to capture the created vehicle
        mockVehicleRepository
            .Setup(x => x.ExistsByIdAsync(It.IsAny<VehicleId>()))
            .ReturnsAsync(false);

        mockVehicleRepository
            .Setup(x => x.AddAsync(It.IsAny<Vehicle>()))
            .Callback<Vehicle>(vehicle => capturedVehicle = vehicle)
            .Returns(Task.CompletedTask);

        mockUnitOfWork
            .Setup(x => x.Save())
            .ReturnsAsync(1);

        using var factory = new TestWebApplicationFactory<Program>(services =>
        {
            // Configure functional test scenario - full business logic with mocked infrastructure
            services.ConfigureForFunctionalTest(mockVehicleRepository.Object, mockUnitOfWork.Object);
        });

        using var client = factory.CreateClient();

        var vehicleId = VehicleId.New();
        var request = new CreateVehicleRequest
        {
            VehicleId = vehicleId,
            Model = "Toyota Camry 2024"
        };

        // Act - Execute the complete business flow
        var response = await client.PostAsJsonAsync(BaseUrl, request);

        // Assert - Verify HTTP response
        response.StatusCode.Should().Be(HttpStatusCode.Created, "valid vehicle should be created successfully");

        // Verify response content type and that content exists
        response.Content.Headers.ContentType?.MediaType.Should().Be("application/json");
        var responseContent = await response.Content.ReadAsStringAsync();
        responseContent.Should().NotBeEmpty("response should contain vehicle data");
        responseContent.Should().Contain("vehicleId", "response should contain vehicle ID field");
        responseContent.Should().Contain("Toyota Camry 2024", "response should contain the correct model");
        responseContent.Should().Contain("Available", "new vehicles should be available by default");

        // Assert - Verify domain effects (business logic execution)
        capturedVehicle.Should().NotBeNull("vehicle should have been created in domain");
        capturedVehicle!.Model.Should().Be("Toyota Camry 2024", "domain vehicle should have correct model");
        capturedVehicle.IsAvailable().Should().BeTrue("new vehicles should be available");

        // Assert - Verify infrastructure interactions
        mockVehicleRepository.Verify(x => x.ExistsByIdAsync(It.IsAny<VehicleId>()), Times.Once, "should check vehicle uniqueness");
        mockVehicleRepository.Verify(x => x.AddAsync(It.IsAny<Vehicle>()), Times.Once, "should add vehicle to repository");
        mockUnitOfWork.Verify(x => x.Save(), Times.Once, "should save changes to persistence");
    }
}

