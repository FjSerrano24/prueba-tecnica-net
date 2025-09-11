using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    /// <summary>
    /// Rentals Controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class RentalsController : ControllerBase
    {
        /// <summary>
        /// Rents a vehicle to a customer.
        /// Follows MediatR pattern as recommended in README.md:
        /// "Controllers receive Requests and send commands to perform some operation"
        /// </summary>
        /// <param name="mediator">MediatR mediator.</param>
        /// <param name="request">Rent vehicle request.</param>
        /// <returns>Rental details.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> RentVehicle(
            [FromServices] IMediator mediator,
            [FromBody][Required] RentVehicleRequest request)
        {
            // As per README.md: "var presenter = await _mediator.Send(request);"
            var presenter = await mediator.Send(request);
            
            // As per README.md: "return presenter.ActionResult;"
            return presenter.ActionResult;
        }

        /// <summary>
        /// Returns a rented vehicle.
        /// Follows MediatR pattern as recommended in README.md:
        /// "Controllers receive Requests and send commands to perform some operation"
        /// </summary>
        /// <param name="mediator">MediatR mediator.</param>
        /// <param name="rentalId">Rental identifier.</param>
        /// <param name="request">Return vehicle request.</param>
        /// <returns>Return details.</returns>
        [HttpPut("{rentalId}/return")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReturnVehicle(
            [FromServices] IMediator mediator,
            [FromRoute] Guid rentalId,
            [FromBody][Required] ReturnVehicleRequest request)
        {
            // Set the RentalId from route parameter to the request
            request.RentalId = rentalId;
            
            // As per README.md: "var presenter = await _mediator.Send(request);"
            var presenter = await mediator.Send(request);
            
            // As per README.md: "return presenter.ActionResult;"
            return presenter.ActionResult;
        }
    }
}
