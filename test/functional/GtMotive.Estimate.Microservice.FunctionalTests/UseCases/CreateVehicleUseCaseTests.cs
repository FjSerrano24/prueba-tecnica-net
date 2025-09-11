using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Services;
using GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GtMotive.Estimate.Microservice.FunctionalTests.UseCases
{
    /// <summary>
    /// Functional tests for CreateVehicleUseCase.
    /// Integration tests that exclude the host but include all application layers.
    /// </summary>
    public sealed class CreateVehicleUseCaseTests : IClassFixture<CompositionRootCollectionFixture>
    {
        private readonly CompositionRootCollectionFixture _fixture;

        public CreateVehicleUseCaseTests(CompositionRootCollectionFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Execute_WithValidVehicleData_ShouldCreateVehicleSuccessfully()
        {
            // Arrange
            using var scope = _fixture.ServiceProvider.CreateScope();
            var useCase = scope.ServiceProvider.GetRequiredService<CreateVehicleUseCase>();
            var presenter = new TestCreateVehiclePresenter();
            
            // Replace the presenter in the scope for this test
            var outputPort = scope.ServiceProvider.GetRequiredService<ICreateVehicleOutputPort>();
            typeof(CreateVehicleUseCase)
                .GetField("_outputPort", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(useCase, presenter);

            var input = new CreateVehicleInput(
                licensePlate: "1234ABC",
                vin: "1HGBH41JXMN109186",
                brand: "Toyota",
                model: "Corolla",
                manufactureYear: DateTime.Now.Year);

            // Act
            await useCase.Execute(input);

            // Assert
            Assert.True(presenter.StandardHandleCalled);
            Assert.False(presenter.InvalidInputCalled);
            Assert.False(presenter.NotFoundHandleCalled);
            Assert.NotNull(presenter.Output);
            Assert.Equal("1234ABC", presenter.Output.LicensePlate);
            Assert.Equal("1HGBH41JXMN109186", presenter.Output.VIN);
            Assert.Equal("Toyota", presenter.Output.Brand);
            Assert.Equal("Corolla", presenter.Output.Model);
            Assert.Equal(DateTime.Now.Year, presenter.Output.ManufactureYear);
            Assert.Equal("Available", presenter.Output.Status);
        }

        [Fact]
        public async Task Execute_WithInvalidManufactureYear_ShouldCallInvalidInput()
        {
            // Arrange
            using var scope = _fixture.ServiceProvider.CreateScope();
            var useCase = scope.ServiceProvider.GetRequiredService<CreateVehicleUseCase>();
            var presenter = new TestCreateVehiclePresenter();
            
            // Replace the presenter in the scope for this test
            typeof(CreateVehicleUseCase)
                .GetField("_outputPort", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(useCase, presenter);

            var input = new CreateVehicleInput(
                licensePlate: "1234ABC",
                vin: "1HGBH41JXMN109186",
                brand: "Toyota",
                model: "Corolla",
                manufactureYear: DateTime.Now.Year - 6); // Too old (>5 years)

            // Act
            await useCase.Execute(input);

            // Assert
            Assert.False(presenter.StandardHandleCalled);
            Assert.True(presenter.InvalidInputCalled);
            Assert.False(presenter.NotFoundHandleCalled);
            Assert.Contains("exceeds maximum age of 5 years", presenter.ErrorMessage);
        }

        [Fact]
        public async Task Execute_WithDuplicateLicensePlate_ShouldCallInvalidInput()
        {
            // Arrange
            using var scope = _fixture.ServiceProvider.CreateScope();
            var useCase = scope.ServiceProvider.GetRequiredService<CreateVehicleUseCase>();
            var presenter1 = new TestCreateVehiclePresenter();
            var presenter2 = new TestCreateVehiclePresenter();

            var input1 = new CreateVehicleInput(
                licensePlate: "TEST123",
                vin: "1HGBH41JXMN109186",
                brand: "Toyota",
                model: "Corolla",
                manufactureYear: DateTime.Now.Year);

            var input2 = new CreateVehicleInput(
                licensePlate: "TEST123", // Same license plate
                vin: "2HGBH41JXMN109187", // Different VIN
                brand: "Honda",
                model: "Civic",
                manufactureYear: DateTime.Now.Year);

            // Act
            // First vehicle should be created successfully
            typeof(CreateVehicleUseCase)
                .GetField("_outputPort", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(useCase, presenter1);
            await useCase.Execute(input1);

            // Second vehicle with same license plate should fail
            typeof(CreateVehicleUseCase)
                .GetField("_outputPort", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(useCase, presenter2);
            await useCase.Execute(input2);

            // Assert
            Assert.True(presenter1.StandardHandleCalled); // First creation successful
            Assert.False(presenter2.StandardHandleCalled); // Second creation failed
            Assert.True(presenter2.InvalidInputCalled);
            Assert.Contains("already exists", presenter2.ErrorMessage);
        }

        /// <summary>
        /// Test presenter implementation for functional tests.
        /// </summary>
        private sealed class TestCreateVehiclePresenter : ICreateVehicleOutputPort
        {
            public bool StandardHandleCalled { get; private set; }
            public bool InvalidInputCalled { get; private set; }
            public bool NotFoundHandleCalled { get; private set; }
            public CreateVehicleOutput? Output { get; private set; }
            public string? ErrorMessage { get; private set; }

            public void StandardHandle(CreateVehicleOutput response)
            {
                StandardHandleCalled = true;
                Output = response;
            }

            public void InvalidInput(string message)
            {
                InvalidInputCalled = true;
                ErrorMessage = message;
            }

            public void NotFoundHandle(string message)
            {
                NotFoundHandleCalled = true;
                ErrorMessage = message;
            }
        }
    }
}

