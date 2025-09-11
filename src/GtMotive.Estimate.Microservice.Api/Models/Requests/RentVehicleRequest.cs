using System;
using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Models.Requests
{
    /// <summary>
    /// Rent Vehicle Request model.
    /// Implements IRequest for MediatR pattern as recommended in README.md.
    /// </summary>
    public sealed class RentVehicleRequest : IRequest<IWebApiPresenter>
    {
        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        [Required]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the customer email.
        /// </summary>
        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the vehicle identifier.
        /// </summary>
        [Required]
        public VehicleId VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the rental start date.
        /// </summary>
        [Required]
        public DateTime StartDate { get; set; }
    }
}
