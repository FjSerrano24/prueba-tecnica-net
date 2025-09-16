using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Models.Requests
{
    /// <summary>
    /// Create Vehicle Request model.
    /// Simplified for Vehicle with Id, CreationDate, Model, and Year.
    /// Implements IRequest for MediatR pattern as recommended in README.md.
    /// </summary>
    public sealed class CreateVehicleRequest : IRequest<IWebApiPresenter>
    {
        /// <summary>
        /// Gets or sets the vehicle identifier.
        /// </summary>
        [Required]
        public VehicleId VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the vehicle model.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the vehicle year.
        /// </summary>
        [Required]
        [Range(1900, 2030, ErrorMessage = "Vehicle year must be between 1900 and 2030.")]
        public int Year { get; set; }
    }
}
