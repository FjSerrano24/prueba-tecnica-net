using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.Models.Responses
{
    /// <summary>
    /// Return Vehicle Response model.
    /// </summary>
    public sealed class ReturnVehicleResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleResponse"/> class.
        /// </summary>
        /// <param name="rentalId">Rental identifier.</param>
        /// <param name="customerId">Customer identifier.</param>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <param name="vehicleLicensePlate">Vehicle license plate.</param>
        /// <param name="startDate">Rental start date.</param>
        /// <param name="endDate">Rental end date.</param>
        /// <param name="durationInDays">Rental duration in days.</param>
        /// <param name="status">Rental status.</param>
        /// <param name="completedAt">Completion date.</param>
        public ReturnVehicleResponse(
            Guid rentalId,
            Guid customerId,
            Guid vehicleId,
            DateTime startDate,
            DateTime endDate,
            string status,
            DateTime completedAt)
        {
            RentalId = rentalId;
            CustomerId = customerId;
            VehicleId = vehicleId;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            CompletedAt = completedAt;
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
        /// Gets the rental end date.
        /// </summary>
        [Required]
        public DateTime EndDate { get; }

        /// <summary>
        /// Gets the rental status.
        /// </summary>
        [Required]
        public string Status { get; }

        /// <summary>
        /// Gets the completion date.
        /// </summary>
        [Required]
        public DateTime CompletedAt { get; }
    }
}
