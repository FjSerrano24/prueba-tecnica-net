using System;
using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.Domain.Enums;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Vehicle Aggregate Root.
    /// Simplified version with Id, CreationDate, and Model only.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <param name="model">Vehicle model.</param>
        public Vehicle(VehicleId vehicleId, string model)
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                throw new DomainException("Vehicle model cannot be empty.");
            }

            if (model.Length > 100)
            {
                throw new DomainException("Vehicle model cannot exceed 100 characters.");
            }

            VehicleId = vehicleId;
            Model = model.Trim();
            CreationDate = DateTime.UtcNow;
            Status = VehicleStatus.Available;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// Private constructor for ORM.
        /// </summary>
        private Vehicle()
        {
        }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public VehicleId VehicleId { get; private set; }

        /// <summary>
        /// Gets the creation date.
        /// </summary>
        public DateTime CreationDate { get; private set; }

        /// <summary>
        /// Gets the vehicle model.
        /// </summary>
        [MaxLength(100)]
        public string Model { get; private set; } = string.Empty;

        /// <summary>
        /// Gets the current status of the vehicle.
        /// </summary>
        public VehicleStatus Status { get; private set; }

        /// <summary>
        /// Checks if the vehicle is available for rental.
        /// </summary>
        /// <returns>True if available, false otherwise.</returns>
        public bool IsAvailable() => Status == VehicleStatus.Available;

        /// <summary>
        /// Marks the vehicle as rented.
        /// </summary>
        /// <exception cref="DomainException">Thrown when vehicle is not available.</exception>
        public void MarkAsRented()
        {
            if (!IsAvailable())
            {
                throw new DomainException($"Vehicle {VehicleId} is not available for rental. Current status: {Status}");
            }

            Status = VehicleStatus.Rented;
        }

        /// <summary>
        /// Marks the vehicle as available (returns it).
        /// </summary>
        /// <exception cref="DomainException">Thrown when vehicle is not currently rented.</exception>
        public void MarkAsAvailable()
        {
            if (Status != VehicleStatus.Rented)
            {
                throw new DomainException($"Vehicle {VehicleId} is not currently rented. Current status: {Status}");
            }

            Status = VehicleStatus.Available;
        }

        /// <summary>
        /// Marks the vehicle as under maintenance.
        /// </summary>
        public void MarkAsUnderMaintenance()
        {
            if (Status == VehicleStatus.Rented)
            {
                throw new DomainException($"Cannot put rented vehicle {VehicleId} under maintenance.");
            }

            Status = VehicleStatus.Maintenance;
        }
    }
}
