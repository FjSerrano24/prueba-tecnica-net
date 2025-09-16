using System;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Output for Rent Vehicle use case.
    /// </summary>
    public sealed class RentVehicleOutput : IUseCaseOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleOutput"/> class.
        /// </summary>
        /// <param name="rentalId">Rental identifier.</param>
        /// <param name="customerId">Customer identifier.</param>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <param name="startDate">Rental start date.</param>
        /// <param name="status">Rental status.</param>
        /// <param name="createdAt">Creation date.</param>
        public RentVehicleOutput(
            Guid rentalId,
            Guid customerId,
            VehicleId vehicleId,
            DateTime startDate,
            string status,
            DateTime createdAt)
        {
            RentalId = rentalId;
            CustomerId = customerId;
            VehicleId = vehicleId;
            StartDate = startDate;
            Status = status;
            CreatedAt = createdAt;
        }

        /// <summary>
        /// Gets the rental identifier.
        /// </summary>
        public Guid RentalId { get; }

        /// <summary>
        /// Gets the customer identifier.
        /// </summary>
        public Guid CustomerId { get; }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public VehicleId VehicleId { get; }

        /// <summary>
        /// Gets the rental start date.
        /// </summary>
        public DateTime StartDate { get; }

        /// <summary>
        /// Gets the rental status.
        /// </summary>
        public string Status { get; }

        /// <summary>
        /// Gets the creation date.
        /// </summary>
        public DateTime CreatedAt { get; }
    }
}
