using System;
using FluentAssertions;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.Domain.ValueObjects
{
    /// <summary>
    /// Unit tests for the VehicleId Value Object.
    /// Tests the VehicleId implementation including validation and methods.
    /// </summary>
    public sealed class VehicleIdTests
    {
        /// <summary>
        /// Test: Creating a valid VehicleId should succeed.
        /// </summary>
        [Fact]
        public void CreateVehicleId_WithValidGuid_ShouldSucceed()
        {
            // Arrange
            var guid = Guid.NewGuid();

            // Act
            var vehicleId = new VehicleId(guid);

            // Assert
            vehicleId.Value.Should().Be(guid);
            vehicleId.ToGuid().Should().Be(guid);
            vehicleId.ToString().Should().Be(guid.ToString());
        }

        /// <summary>
        /// Test: Creating VehicleId with empty Guid should throw exception.
        /// </summary>
        [Fact]
        public void CreateVehicleId_WithEmptyGuid_ShouldThrowException()
        {
            // Arrange
            var emptyGuid = Guid.Empty;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => new VehicleId(emptyGuid));
            exception.Message.Should().Be("Vehicle ID cannot be empty.");
        }

        /// <summary>
        /// Test: VehicleId.New() should create a new non-empty VehicleId.
        /// </summary>
        [Fact]
        public void New_ShouldCreateNewVehicleId()
        {
            // Act
            var vehicleId = VehicleId.New();

            // Assert
            vehicleId.Value.Should().NotBe(Guid.Empty);
            vehicleId.ToGuid().Should().NotBe(Guid.Empty);
        }

        /// <summary>
        /// Test: VehicleId.FromGuid() should create VehicleId from Guid.
        /// </summary>
        [Fact]
        public void FromGuid_ShouldCreateVehicleIdFromGuid()
        {
            // Arrange
            var guid = Guid.NewGuid();

            // Act
            var vehicleId = VehicleId.FromGuid(guid);

            // Assert
            vehicleId.Value.Should().Be(guid);
            vehicleId.ToGuid().Should().Be(guid);
        }

        /// <summary>
        /// Test: VehicleIds with same Guid should be equal.
        /// </summary>
        [Fact]
        public void Equals_VehicleIdsWithSameGuid_ShouldBeEqual()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var vehicleId1 = new VehicleId(guid);
            var vehicleId2 = new VehicleId(guid);

            // Act & Assert
            vehicleId1.Equals(vehicleId2).Should().BeTrue();
            vehicleId1.Should().Be(vehicleId2);
            vehicleId1.GetHashCode().Should().Be(vehicleId2.GetHashCode());
        }

        /// <summary>
        /// Test: VehicleIds with different Guids should not be equal.
        /// </summary>
        [Fact]
        public void Equals_VehicleIdsWithDifferentGuids_ShouldNotBeEqual()
        {
            // Arrange
            var vehicleId1 = VehicleId.New();
            var vehicleId2 = VehicleId.New();

            // Act & Assert
            vehicleId1.Equals(vehicleId2).Should().BeFalse();
            vehicleId1.Should().NotBe(vehicleId2);
        }

        /// <summary>
        /// Test: Equality operators should work correctly.
        /// </summary>
        [Fact]
        public void EqualityOperators_ShouldWorkCorrectly()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var vehicleId1 = new VehicleId(guid);
            var vehicleId2 = new VehicleId(guid);
            var vehicleId3 = VehicleId.New();

            // Act & Assert
            (vehicleId1 == vehicleId2).Should().BeTrue();
            (vehicleId1 != vehicleId3).Should().BeTrue();
            (vehicleId2 != vehicleId3).Should().BeTrue();
        }

        /// <summary>
        /// Test: Equals with null should return false.
        /// </summary>
        [Fact]
        public void Equals_WithNull_ShouldReturnFalse()
        {
            // Arrange
            var vehicleId = VehicleId.New();

            // Act & Assert
            vehicleId.Equals(null).Should().BeFalse();
        }

        /// <summary>
        /// Test: Equals with different type should return false.
        /// </summary>
        [Fact]
        public void Equals_WithDifferentType_ShouldReturnFalse()
        {
            // Arrange
            var vehicleId = VehicleId.New();
            var guid = Guid.NewGuid();

            // Act & Assert
            vehicleId.Equals(guid).Should().BeFalse();
        }
    }
}

