using System;
using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Api.Models.Responses
{
    /// <summary>
    /// Vehicle response model.
    /// Simplified for Vehicle with Id, CreationDate, and Model only.
    /// </summary>
    public sealed class VehicleResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleResponse"/> class.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <param name="model">Vehicle model.</param>
        /// <param name="status">Vehicle status.</param>
        /// <param name="creationDate">Creation date.</param>
        public VehicleResponse(
            VehicleId vehicleId,
            string model,
            string status,
            DateTime creationDate)
        {
            VehicleId = vehicleId.ToGuid();
            Model = model;
            Status = status;
            CreationDate = creationDate;
        }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        [Required]
        public Guid VehicleId { get; }

        /// <summary>
        /// Gets the vehicle model.
        /// </summary>
        [Required]
        public string Model { get; }

        /// <summary>
        /// Gets the vehicle status.
        /// </summary>
        [Required]
        public string Status { get; }

        /// <summary>
        /// Gets the creation date.
        /// </summary>
        [Required]
        public DateTime CreationDate { get; }
    }
}