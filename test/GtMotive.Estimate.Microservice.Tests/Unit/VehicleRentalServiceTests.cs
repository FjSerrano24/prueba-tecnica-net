using FluentAssertions;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Services;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using Moq;

namespace GtMotive.Estimate.Microservice.Tests.Unit
{
    /// <summary>
    /// Unit tests for VehicleRentalService.
    /// Validates methods in isolation without external dependencies using mocks.
    /// </summary>
    public class VehicleRentalServiceTests
    {
        private readonly Mock<IVehicleRepository> _mockVehicleRepository;
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly Mock<IRentalRepository> _mockRentalRepository;
        private readonly VehicleRentalService _service;

        /// <summary>
        /// Constructor that initializes mocks for each test.
        /// </summary>
        public VehicleRentalServiceTests()
        {
            _mockVehicleRepository = new Mock<IVehicleRepository>();
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockRentalRepository = new Mock<IRentalRepository>();
            
            _service = new VehicleRentalService(
                _mockVehicleRepository.Object,
                _mockCustomerRepository.Object,
                _mockRentalRepository.Object);
        }

        /// <summary>
        /// Tests that ValidateVehicleForFleetAsync succeeds when the data is valid.
        /// </summary>
        [Fact]
        public async Task ValidateVehicleForFleetAsync_WhenValidData_ShouldNotThrowException()
        {
            // Arrange - Prepare test data
            var vehicleId = VehicleId.New();
            var model = "Toyota Camry";

            // Configure mock to return false (vehicle does not exist)
            _mockVehicleRepository
                .Setup(x => x.ExistsByIdAsync(vehicleId))
                .ReturnsAsync(false);

            // Act - Execute the method under test
            var act = async () => await _service.ValidateVehicleForFleetAsync(vehicleId, model);

            // Assert - Verify that no exception is thrown
            await act.Should().NotThrowAsync();
            
            // Verify that repository was called exactly once
            _mockVehicleRepository.Verify(x => x.ExistsByIdAsync(vehicleId), Times.Once);
        }

        /// <summary>
        /// Tests that ValidateVehicleForFleetAsync throws DomainException when the ID already exists.
        /// </summary>
        [Fact]
        public async Task ValidateVehicleForFleetAsync_WhenVehicleIdAlreadyExists_ShouldThrowDomainException()
        {
            // Arrange - Prepare test data
            var vehicleId = VehicleId.New();
            var model = "Honda Civic";

            // Configure mock to return true (vehicle already exists)
            _mockVehicleRepository
                .Setup(x => x.ExistsByIdAsync(vehicleId))
                .ReturnsAsync(true);

            // Act - Execute the method under test
            var act = async () => await _service.ValidateVehicleForFleetAsync(vehicleId, model);

            // Assert - Verify that DomainException is thrown with correct message
            var exception = await act.Should().ThrowAsync<DomainException>();
            exception.WithMessage($"A vehicle with ID {vehicleId} already exists in the fleet.");
            
            // Verify that repository was called exactly once
            _mockVehicleRepository.Verify(x => x.ExistsByIdAsync(vehicleId), Times.Once);
        }

        /// <summary>
        /// Tests that ValidateVehicleForFleetAsync throws DomainException when the model is null.
        /// </summary>
        [Fact]
        public async Task ValidateVehicleForFleetAsync_WhenModelIsNull_ShouldThrowDomainException()
        {
            // Arrange - Prepare test data
            var vehicleId = VehicleId.New();
            string? model = null;

            // Configure mock to return false (vehicle does not exist)
            _mockVehicleRepository
                .Setup(x => x.ExistsByIdAsync(vehicleId))
                .ReturnsAsync(false);

            // Act - Execute the method under test
            var act = async () => await _service.ValidateVehicleForFleetAsync(vehicleId, model!);

            // Assert - Verify that DomainException is thrown with correct message
            var exception = await act.Should().ThrowAsync<DomainException>();
            exception.WithMessage("Vehicle model cannot be empty.");
            
            // Verify that repository was called exactly once (ID is validated before model)
            _mockVehicleRepository.Verify(x => x.ExistsByIdAsync(vehicleId), Times.Once);
        }

        /// <summary>
        /// Tests that ValidateVehicleForFleetAsync throws DomainException when the model is empty string.
        /// </summary>
        [Fact]
        public async Task ValidateVehicleForFleetAsync_WhenModelIsEmpty_ShouldThrowDomainException()
        {
            // Arrange - Prepare test data
            var vehicleId = VehicleId.New();
            var model = "";

            // Configure mock to return false (vehicle does not exist)
            _mockVehicleRepository
                .Setup(x => x.ExistsByIdAsync(vehicleId))
                .ReturnsAsync(false);

            // Act - Execute the method under test
            var act = async () => await _service.ValidateVehicleForFleetAsync(vehicleId, model);

            // Assert - Verify that DomainException is thrown with correct message
            var exception = await act.Should().ThrowAsync<DomainException>();
            exception.WithMessage("Vehicle model cannot be empty.");
            
            // Verify that repository was called exactly once (ID is validated before model)
            _mockVehicleRepository.Verify(x => x.ExistsByIdAsync(vehicleId), Times.Once);
        }

        /// <summary>
        /// Tests that ValidateVehicleForFleetAsync throws DomainException when the model is only whitespace.
        /// </summary>
        [Fact]
        public async Task ValidateVehicleForFleetAsync_WhenModelIsWhitespace_ShouldThrowDomainException()
        {
            // Arrange - Prepare test data
            var vehicleId = VehicleId.New();
            var model = "   ";

            // Configure mock to return false (vehicle does not exist)
            _mockVehicleRepository
                .Setup(x => x.ExistsByIdAsync(vehicleId))
                .ReturnsAsync(false);

            // Act - Execute the method under test
            var act = async () => await _service.ValidateVehicleForFleetAsync(vehicleId, model);

            // Assert - Verify that DomainException is thrown with correct message
            var exception = await act.Should().ThrowAsync<DomainException>();
            exception.WithMessage("Vehicle model cannot be empty.");
            
            // Verify that repository was called exactly once (ID is validated before model)
            _mockVehicleRepository.Verify(x => x.ExistsByIdAsync(vehicleId), Times.Once);
        }

        /// <summary>
        /// Tests that ValidateVehicleForFleetAsync throws DomainException when the model is too long.
        /// </summary>
        [Fact]
        public async Task ValidateVehicleForFleetAsync_WhenModelIsTooLong_ShouldThrowDomainException()
        {
            // Arrange - Prepare test data
            var vehicleId = VehicleId.New();
            var model = new string('A', 101); // 101 characters, exceeds limit of 100

            // Configure mock to return false (vehicle does not exist)
            _mockVehicleRepository
                .Setup(x => x.ExistsByIdAsync(vehicleId))
                .ReturnsAsync(false);

            // Act - Execute the method under test
            var act = async () => await _service.ValidateVehicleForFleetAsync(vehicleId, model);

            // Assert - Verify that DomainException is thrown with correct message
            var exception = await act.Should().ThrowAsync<DomainException>();
            exception.WithMessage("Vehicle model cannot exceed 100 characters.");
            
            // Verify that repository was called exactly once (ID is validated before model)
            _mockVehicleRepository.Verify(x => x.ExistsByIdAsync(vehicleId), Times.Once);
        }

        /// <summary>
        /// Tests that ValidateVehicleForFleetAsync succeeds when the model has exactly 100 characters (boundary case).
        /// </summary>
        [Fact]
        public async Task ValidateVehicleForFleetAsync_WhenModelIsExactly100Characters_ShouldNotThrowException()
        {
            // Arrange - Prepare test data
            var vehicleId = VehicleId.New();
            var model = new string('A', 100); // Exactly 100 characters

            // Configure mock to return false (vehicle does not exist)
            _mockVehicleRepository
                .Setup(x => x.ExistsByIdAsync(vehicleId))
                .ReturnsAsync(false);

            // Act - Execute the method under test
            var act = async () => await _service.ValidateVehicleForFleetAsync(vehicleId, model);

            // Assert - Verify that no exception is thrown
            await act.Should().NotThrowAsync();
            
            // Verify that repository was called exactly once
            _mockVehicleRepository.Verify(x => x.ExistsByIdAsync(vehicleId), Times.Once);
        }
    }
}