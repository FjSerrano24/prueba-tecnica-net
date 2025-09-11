using GtMotive.Estimate.Microservice.Api.Models.Responses;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Presenters
{
    /// <summary>
    /// Return Vehicle Presenter.
    /// </summary>
    public sealed class ReturnVehiclePresenter : IReturnVehicleOutputPort, IWebApiPresenter
    {
        /// <inheritdoc/>
        public IActionResult ActionResult { get; private set; } = new NoContentResult();

        /// <inheritdoc/>
        public void StandardHandle(ReturnVehicleOutput output)
        {
            var response = new ReturnVehicleResponse(
                output.RentalId,
                output.CustomerId,
                output.VehicleId,
                output.VehicleLicensePlate,
                output.StartDate,
                output.EndDate,
                output.DurationInDays,
                output.Status,
                output.CompletedAt);

            ActionResult = new OkObjectResult(response);
        }

        /// <inheritdoc/>
        public void InvalidInput(string message)
        {
            ActionResult = new BadRequestObjectResult(new { Error = message });
        }

        /// <inheritdoc/>
        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(new { Error = message });
        }
    }
}

