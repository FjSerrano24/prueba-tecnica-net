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
    /// Vehicles Controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class VehiclesController : ControllerBase
    {
        /// <summary>
        /// Creates a new vehicle in the fleet.
        /// Follows MediatR pattern as recommended in README.md:
        /// "Controllers receive Requests and send commands to perform some operation"
        /// </summary>
        /// <param name="mediator">MediatR mediator.</param>
        /// <param name="request">Create vehicle request.</param>
        /// <returns>Created vehicle details.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateVehicle(
            [FromServices] IMediator mediator,
            [FromBody][Required] CreateVehicleRequest request)
        {
            // As per README.md: "var presenter = await _mediator.Send(request);"
            var presenter = await mediator.Send(request);
            
            // As per README.md: "return presenter.ActionResult;"
            return presenter.ActionResult;
        }

        /// <summary>
        /// Gets all available vehicles in the fleet.
        /// Follows MediatR pattern as recommended in README.md:
        /// "Controllers receive Requests and send commands to perform some operation"
        /// </summary>
        /// <param name="mediator">MediatR mediator.</param>
        /// <returns>List of available vehicles.</returns>
        [HttpGet("available")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAvailableVehicles(
            [FromServices] IMediator mediator)
        {
            // Create request (as recommended in README.md)
            var request = new ListAvailableVehiclesRequest();
            
            // As per README.md: "var presenter = await _mediator.Send(request);"
            var presenter = await mediator.Send(request);
            
            // As per README.md: "return presenter.ActionResult;"
            return presenter.ActionResult;
        }
    }
}
