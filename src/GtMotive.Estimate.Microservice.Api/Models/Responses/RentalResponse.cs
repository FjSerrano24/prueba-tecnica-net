using System;
using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Api.Models.Responses
{
    /// <summary>
    /// Rental Response model.
    /// </summary>
    public sealed class RentalResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentalResponse"/> class.
        /// </summary>
        /// <param name="rentalId">Rental identifier.</param>
        /// <param name="customerId">Customer identifier.</param>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <param name="startDate">Rental start date.</param>
        /// <param name="status">Rental status.</param>
        /// <param name="createdAt">Creation date.</param>
        public RentalResponse(
            Guid rentalId,
            Guid customerId,
            VehicleId vehicleId,
            DateTime startDate,
            string status,
            DateTime createdAt)
        {
            RentalId = rentalId;
            CustomerId = customerId;
            VehicleId = vehicleId.ToGuid();
            StartDate = startDate;
            Status = status;
            CreatedAt = createdAt;
        }

        /// <summary>
        /// Gets the rental identifier.
        /// </summary>
        [Required]
        public Guid RentalId { get; }

        /// <summary>
        /// Gets the customer identifier.
        /// </summary>
        [Required]
        public Guid CustomerId { get; }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        [Required]
        public Guid VehicleId { get; }

        /// <summary>
        /// Gets the rental start date.
        /// </summary>
        [Required]
        public DateTime StartDate { get; }

        /// <summary>
        /// Gets the rental status.
        /// </summary>
        [Required]
        public string Status { get; }

        /// <summary>
        /// Gets the creation date.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; }
    }
}
