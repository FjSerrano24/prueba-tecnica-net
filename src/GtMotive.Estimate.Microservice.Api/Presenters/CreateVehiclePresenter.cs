using GtMotive.Estimate.Microservice.Api.Controllers;
using GtMotive.Estimate.Microservice.Api.Models.Responses;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Presenters
{
    /// <summary>
    /// Create Vehicle Presenter.
    /// Simplified for the new Vehicle structure.
    /// </summary>
    public sealed class CreateVehiclePresenter : IWebApiPresenter, ICreateVehicleOutputPort
    {
        /// <summary>
        /// Gets the Action Result.
        /// </summary>
        public IActionResult ActionResult { get; private set; } = new BadRequestResult();

        /// <summary>
        /// Handles successful Create Vehicle operation.
        /// Simplified for the new Vehicle structure.
        /// </summary>
        /// <param name="output">Create Vehicle output.</param>
        public void StandardHandle(CreateVehicleOutput output)
        {
            var response = new VehicleResponse(
                output.VehicleId,
                output.Model,
                output.Status,
                output.CreationDate);

            // Return Created with the vehicle response
            ActionResult = new CreatedResult(string.Empty, response);
        }

        /// <summary>
        /// Handles invalid input errors.
        /// </summary>
        /// <param name="message">Error message.</param>
        public void InvalidInput(string message)
        {
            ActionResult = new BadRequestObjectResult(message);
        }

        /// <summary>
        /// Handles conflict errors.
        /// </summary>
        /// <param name="message">Error message.</param>
        public void ConflictHandle(string message)
        {
            ActionResult = new ConflictObjectResult(message);
        }

        /// <summary>
        /// Handles not found errors.
        /// </summary>
        /// <param name="message">Error message.</param>
        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(message);
        }
    }
}
