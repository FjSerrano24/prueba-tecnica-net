using System;
using FluentAssertions;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Enums;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.Domain.Entities
{
    /// <summary>
    /// Unit tests for the Vehicle entity with VehicleId ValueObject.
    /// Tests the entity with VehicleId, CreationDate, and Model.
    /// </summary>
    public sealed class VehicleTests
    {
        /// <summary>
        /// Test: Creating a valid vehicle should succeed.
        /// </summary>
        [Fact]
        public void CreateVehicle_WithValidParameters_ShouldSucceed()
        {
            // Arrange
            var vehicleId = VehicleId.New();
            var model = "Toyota Corolla";

            // Act
            var vehicle = new Vehicle(vehicleId, model);

            // Assert
            vehicle.VehicleId.Should().Be(vehicleId);
            vehicle.Model.Should().Be(model);
            vehicle.Status.Should().Be(VehicleStatus.Available);
            vehicle.CreationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        /// <summary>
        /// Test: Creating a vehicle with empty model should throw exception.
        /// </summary>
        [Fact]
        public void CreateVehicle_WithEmptyModel_ShouldThrowException()
        {
            // Arrange
            var vehicleId = VehicleId.New();
            var model = "";

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => new Vehicle(vehicleId, model));
            exception.Message.Should().Be("Vehicle model cannot be empty.");
        }

        /// <summary>
        /// Test: Creating a vehicle with model longer than 100 characters should throw exception.
        /// </summary>
        [Fact]
        public void CreateVehicle_WithLongModel_ShouldThrowException()
        {
            // Arrange
            var vehicleId = VehicleId.New();
            var model = new string('A', 101); // 101 characters

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => new Vehicle(vehicleId, model));
            exception.Message.Should().Be("Vehicle model cannot exceed 100 characters.");
        }

        /// <summary>
        /// Test: MarkAsRented should change status to Rented.
        /// </summary>
        [Fact]
        public void MarkAsRented_FromAvailableStatus_ShouldChangeToRented()
        {
            // Arrange
            var vehicle = new Vehicle(VehicleId.New(), "Toyota Corolla");
            
            // Act
            vehicle.MarkAsRented();

            // Assert
            vehicle.Status.Should().Be(VehicleStatus.Rented);
        }

        /// <summary>
        /// Test: MarkAsRented on already rented vehicle should throw exception.
        /// </summary>
        [Fact]
        public void MarkAsRented_FromRentedStatus_ShouldThrowException()
        {
            // Arrange
            var vehicle = new Vehicle(VehicleId.New(), "Toyota Corolla");
            vehicle.MarkAsRented();

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => vehicle.MarkAsRented());
            exception.Message.Should().Contain("is not available for rental");
        }

        /// <summary>
        /// Test: MarkAsAvailable should change status from Rented to Available.
        /// </summary>
        [Fact]
        public void MarkAsAvailable_FromRentedStatus_ShouldChangeToAvailable()
        {
            // Arrange
            var vehicle = new Vehicle(VehicleId.New(), "Toyota Corolla");
            vehicle.MarkAsRented();

            // Act
            vehicle.MarkAsAvailable();

            // Assert
            vehicle.Status.Should().Be(VehicleStatus.Available);
        }

        /// <summary>
        /// Test: IsAvailable should return true for available vehicles.
        /// </summary>
        [Fact]
        public void IsAvailable_ForAvailableVehicle_ShouldReturnTrue()
        {
            // Arrange
            var vehicle = new Vehicle(VehicleId.New(), "Toyota Corolla");

            // Act
            var isAvailable = vehicle.IsAvailable();

            // Assert
            isAvailable.Should().BeTrue();
        }

        /// <summary>
        /// Test: IsAvailable should return false for rented vehicles.
        /// </summary>
        [Fact]
        public void IsAvailable_ForRentedVehicle_ShouldReturnFalse()
        {
            // Arrange
            var vehicle = new Vehicle(VehicleId.New(), "Toyota Corolla");
            vehicle.MarkAsRented();

            // Act
            var isAvailable = vehicle.IsAvailable();

            // Assert
            isAvailable.Should().BeFalse();
        }

        /// <summary>
        /// Test: Vehicle equality should be based on VehicleId.
        /// </summary>
        [Fact]
        public void Equals_VehiclesWithSameVehicleId_ShouldBeEqual()
        {
            // Arrange
            var vehicleId = VehicleId.New();
            var vehicle1 = new Vehicle(vehicleId, "Toyota Corolla");
            var vehicle2 = new Vehicle(vehicleId, "Honda Civic");

            // Act & Assert
            vehicle1.Equals(vehicle2).Should().BeTrue();
            vehicle1.GetHashCode().Should().Be(vehicle2.GetHashCode());
        }

        /// <summary>
        /// Test: Vehicles with different VehicleIds should not be equal.
        /// </summary>
        [Fact]
        public void Equals_VehiclesWithDifferentVehicleIds_ShouldNotBeEqual()
        {
            // Arrange
            var vehicle1 = new Vehicle(VehicleId.New(), "Toyota Corolla");
            var vehicle2 = new Vehicle(VehicleId.New(), "Toyota Corolla");

            // Act & Assert
            vehicle1.Equals(vehicle2).Should().BeFalse();
        }
    }
}