using GtMotive.Estimate.Microservice.Api.Controllers;
using GtMotive.Estimate.Microservice.Api.Models.Responses;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Presenters
{
    /// <summary>
    /// Rent Vehicle Presenter.
    /// Updated for VehicleId ValueObject.
    /// </summary>
    public sealed class RentVehiclePresenter : IWebApiPresenter, IRentVehicleOutputPort
    {
        /// <summary>
        /// Gets the Action Result.
        /// </summary>
        public IActionResult ActionResult { get; private set; } = new BadRequestResult();

        /// <summary>
        /// Handles successful Rent Vehicle operation.
        /// Updated for VehicleId ValueObject.
        /// </summary>
        /// <param name="output">Rent Vehicle output.</param>
        public void StandardHandle(RentVehicleOutput output)
        {
            var response = new RentalResponse(
                output.RentalId,
                output.CustomerId,
                output.VehicleId,
                output.StartDate,
                output.Status,
                output.CreatedAt);

            ActionResult = new CreatedAtActionResult(
                nameof(RentalsController.RentVehicle),
                "RentalsController",
                new { rentalId = output.RentalId },
                response);
        }

        /// <summary>
        /// Handles invalid input errors.
        /// </summary>
        /// <param name="message">Error message.</param>
        public void InvalidInput(string message)
        {
            ActionResult = new BadRequestObjectResult(new { Error = message });
        }

        /// <summary>
        /// Handles conflict errors.
        /// </summary>
        /// <param name="message">Error message.</param>
        public void ConflictHandle(string message)
        {
            ActionResult = new ConflictObjectResult(new { Error = message });
        }

        /// <summary>
        /// Handles not found errors.
        /// </summary>
        /// <param name="message">Error message.</param>
        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(new { Error = message });
        }

        /// <summary>
        /// Handles business rule violation errors.
        /// </summary>
        /// <param name="message">Error message.</param>
        public void BusinessRuleViolation(string message)
        {
            ActionResult = new ConflictObjectResult(new { Error = message });
        }
    }
}