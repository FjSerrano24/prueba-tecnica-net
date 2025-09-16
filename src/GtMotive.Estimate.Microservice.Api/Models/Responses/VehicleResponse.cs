using System;
using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Api.Models.Responses
{
    /// <summary>
    /// Vehicle response model.
    /// Simplified for Vehicle with Id, CreationDate, and Model only.
    /// </summary>
    public sealed class VehicleResponse(VehicleId VehicleId, string Model, string Status, DateTime CreationDate)
    {
        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        [Required]
        public Guid VehicleId { get; } = VehicleId.Value;

        /// <summary>
        /// Gets the vehicle model.
        /// </summary>
        [Required]
        public string Model { get; } = Model;

        /// <summary>
        /// Gets the vehicle status.
        /// </summary>
        [Required]
        public string Status { get; } = Status;

        /// <summary>
        /// Gets the creation date.
        /// </summary>
        [Required]
        public DateTime CreationDate { get; } = CreationDate;
    }
}
