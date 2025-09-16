using GtMotive.Estimate.Microservice.Api.Models.Responses;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Presenters
{
    /// <summary>
    /// Return Vehicle Presenter.
    /// </summary>
    public sealed class ReturnVehiclePresenter : IReturnVehicleOutputPort, IWebApiPresenter
    {
        public IActionResult ActionResult { get; private set; } = new NoContentResult();

        /// <inheritdoc/>
        public void StandardHandle(ReturnVehicleOutput output)
        {
            var response = new ReturnVehicleResponse(
                output.RentalId,
                output.CustomerId,
                output.VehicleId,
                output.StartDate,
                output.EndDate,
                output.Status,
                output.CompletedAt);

            ActionResult = new OkObjectResult(response);
        }

        /// <inheritdoc/>
        public void InvalidInput(string message)
        {
            ActionResult = new BadRequestObjectResult(message);
        }

        /// <inheritdoc/>
        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(message);
        }

        public void ConflictHandle(string message)
        {
            ActionResult = new ConflictObjectResult(message);
        }
    }
}
