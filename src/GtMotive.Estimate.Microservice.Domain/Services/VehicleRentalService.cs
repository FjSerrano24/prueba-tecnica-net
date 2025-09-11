using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Services
{
    /// <summary>
    /// Domain service for vehicle rental operations.
    /// Handles complex business rules and coordination between aggregates.
    /// </summary>
    public class VehicleRentalService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRentalRepository _rentalRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleRentalService"/> class.
        /// </summary>
        /// <param name="vehicleRepository">Vehicle repository.</param>
        /// <param name="customerRepository">Customer repository.</param>
        /// <param name="rentalRepository">Rental repository.</param>
        public VehicleRentalService(
            IVehicleRepository vehicleRepository,
            ICustomerRepository customerRepository,
            IRentalRepository rentalRepository)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
        }

        /// <summary>
        /// Rents a vehicle to a customer.
        /// Enforces business rules: customer cannot have more than one active rental.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <param name="startDate">Rental start date.</param>
        /// <returns>The created rental.</returns>
        /// <exception cref="CustomerNotFoundException">Thrown when customer is not found.</exception>
        /// <exception cref="VehicleNotFoundException">Thrown when vehicle is not found.</exception>
        /// <exception cref="CustomerAlreadyHasActiveRentalException">Thrown when customer already has an active rental.</exception>
        /// <exception cref="DomainException">Thrown when vehicle is not available or other business rules are violated.</exception>
        public async Task<Rental> RentVehicleAsync(CustomerId customerId, VehicleId vehicleId, DateTime startDate)
        {
            // Validate customer exists
            var customer = await _customerRepository.GetByIdAsync(customerId);

            // Validate vehicle exists and is available
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
            
            if (!vehicle.IsAvailable())
            {
                throw new DomainException($"Vehicle {vehicleId} is not available for rental. Current status: {vehicle.Status}");
            }

            // Business Rule: Customer cannot have more than one active rental
            var hasActiveRental = await _rentalRepository.CustomerHasActiveRentalAsync(customerId);
            if (hasActiveRental)
            {
                throw new CustomerAlreadyHasActiveRentalException($"Customer {customerId} already has an active rental.");
            }

            // Validate start date
            if (startDate.Date < DateTime.UtcNow.Date)
            {
                throw new DomainException("Rental start date cannot be in the past.");
            }

            // Create rental
            var rentalId = RentalId.New();
            var rental = new Rental(rentalId, customerId, vehicleId, startDate);

            // Mark vehicle as rented
            vehicle.MarkAsRented();

            // Persist changes
            await _rentalRepository.AddAsync(rental);
            await _vehicleRepository.UpdateAsync(vehicle);

            return rental;
        }

        /// <summary>
        /// Returns a rented vehicle.
        /// </summary>
        /// <param name="rentalId">Rental identifier.</param>
        /// <param name="endDate">Return date.</param>
        /// <returns>The completed rental.</returns>
        /// <exception cref="RentalNotFoundException">Thrown when rental is not found.</exception>
        /// <exception cref="VehicleNotFoundException">Thrown when vehicle is not found.</exception>
        /// <exception cref="DomainException">Thrown when rental is not active or other business rules are violated.</exception>
        public async Task<Rental> ReturnVehicleAsync(RentalId rentalId, DateTime endDate)
        {
            // Get rental
            var rental = await _rentalRepository.GetByIdAsync(rentalId);

            // Get vehicle
            var vehicle = await _vehicleRepository.GetByIdAsync(rental.VehicleId);

            // Complete rental
            rental.Complete(endDate);

            // Mark vehicle as available
            vehicle.MarkAsAvailable();

            // Persist changes
            await _rentalRepository.UpdateAsync(rental);
            await _vehicleRepository.UpdateAsync(vehicle);

            return rental;
        }

        /// <summary>
        /// Validates if a vehicle can be added to the fleet.
        /// Simplified validation for the new Vehicle structure.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <param name="model">Vehicle model.</param>
        /// <returns>Task representing the validation operation.</returns>
        /// <exception cref="DomainException">Thrown when validation fails.</exception>
        public async Task ValidateVehicleForFleetAsync(VehicleId vehicleId, string model)
        {
            // Validate vehicle ID uniqueness
            var existsById = await _vehicleRepository.ExistsByIdAsync(vehicleId);
            if (existsById)
            {
                throw new DomainException($"A vehicle with ID {vehicleId} already exists in the fleet.");
            }

            // Validate model
            if (string.IsNullOrWhiteSpace(model))
            {
                throw new DomainException("Vehicle model cannot be empty.");
            }

            if (model.Length > 100)
            {
                throw new DomainException("Vehicle model cannot exceed 100 characters.");
            }
        }

        /// <summary>
        /// Creates a new vehicle and adds it to the fleet.
        /// Simplified for the new Vehicle structure.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <param name="model">Vehicle model.</param>
        /// <returns>The created vehicle.</returns>
        /// <exception cref="DomainException">Thrown when validation fails.</exception>
        public async Task<Vehicle> CreateVehicleAsync(VehicleId vehicleId, string model)
        {
            // Validate vehicle for fleet
            await ValidateVehicleForFleetAsync(vehicleId, model);

            // Create vehicle
            var vehicle = new Vehicle(vehicleId, model);

            // Add to repository
            await _vehicleRepository.AddAsync(vehicle);

            return vehicle;
        }
    }
}
