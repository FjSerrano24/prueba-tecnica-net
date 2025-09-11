using System;
using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.Api.UseCases;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Models.Requests
{
    /// <summary>
    /// Return Vehicle Request model.
    /// Implements IRequest for MediatR pattern as recommended in README.md.
    /// </summary>
    public sealed class ReturnVehicleRequest : IRequest<IWebApiPresenter>
    {
        /// <summary>
        /// Gets or sets the rental identifier.
        /// This property will be populated from the route parameter.
        /// </summary>
        [Required]
        public Guid RentalId { get; set; }

        /// <summary>
        /// Gets or sets the return date.
        /// </summary>
        [Required]
        public DateTime EndDate { get; set; }
    }
}
