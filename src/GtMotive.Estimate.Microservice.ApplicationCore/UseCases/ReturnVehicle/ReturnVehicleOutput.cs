using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Output for Return Vehicle use case.
    /// </summary>
    public sealed class ReturnVehicleOutput : IUseCaseOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleOutput"/> class.
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
        public ReturnVehicleOutput(
            Guid rentalId,
            Guid customerId,
            Guid vehicleId,
            string vehicleLicensePlate,
            DateTime startDate,
            DateTime endDate,
            int durationInDays,
            string status,
            DateTime completedAt)
        {
            RentalId = rentalId;
            CustomerId = customerId;
            VehicleId = vehicleId;
            VehicleLicensePlate = vehicleLicensePlate;
            StartDate = startDate;
            EndDate = endDate;
            DurationInDays = durationInDays;
            Status = status;
            CompletedAt = completedAt;
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
        public Guid VehicleId { get; }

        /// <summary>
        /// Gets the vehicle license plate.
        /// </summary>
        public string VehicleLicensePlate { get; }

        /// <summary>
        /// Gets the rental start date.
        /// </summary>
        public DateTime StartDate { get; }

        /// <summary>
        /// Gets the rental end date.
        /// </summary>
        public DateTime EndDate { get; }

        /// <summary>
        /// Gets the rental duration in days.
        /// </summary>
        public int DurationInDays { get; }

        /// <summary>
        /// Gets the rental status.
        /// </summary>
        public string Status { get; }

        /// <summary>
        /// Gets the completion date.
        /// </summary>
        public DateTime CompletedAt { get; }
    }
}

