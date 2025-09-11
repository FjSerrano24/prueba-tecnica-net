using System;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles
{
    /// <summary>
    /// Vehicle model for listing.
    /// Simplified for Vehicle with Id, CreationDate, and Model only.
    /// </summary>
    public sealed class VehicleModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleModel"/> class.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <param name="model">Vehicle model.</param>
        /// <param name="status">Vehicle status.</param>
        /// <param name="creationDate">Creation date.</param>
        public VehicleModel(
            VehicleId vehicleId,
            string model,
            string status,
            DateTime creationDate)
        {
            VehicleId = vehicleId;
            Model = model;
            Status = status;
            CreationDate = creationDate;
        }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public VehicleId VehicleId { get; }

        /// <summary>
        /// Gets the vehicle model.
        /// </summary>
        public string Model { get; }

        /// <summary>
        /// Gets the vehicle status.
        /// </summary>
        public string Status { get; }

        /// <summary>
        /// Gets the creation date.
        /// </summary>
        public DateTime CreationDate { get; }
    }
}