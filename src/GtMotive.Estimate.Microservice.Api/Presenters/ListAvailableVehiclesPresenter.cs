using System.Linq;
using GtMotive.Estimate.Microservice.Api.Models.Responses;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Presenters
{
    /// <summary>
    /// List Available Vehicles Presenter.
    /// Simplified for the new Vehicle structure.
    /// </summary>
    public sealed class ListAvailableVehiclesPresenter : IWebApiPresenter, IListAvailableVehiclesOutputPort
    {
        /// <summary>
        /// Gets the Action Result.
        /// </summary>
        public IActionResult ActionResult { get; private set; } = new BadRequestResult();

        /// <summary>
        /// Handles successful List Available Vehicles operation.
        /// Simplified for the new Vehicle structure.
        /// </summary>
        /// <param name="output">List Available Vehicles output.</param>
        public void StandardHandle(ListAvailableVehiclesOutput output)
        {
            var response = output.Vehicles.Select(v => new AvailableVehicleResponse(
                v.VehicleId,
                v.Model,
                v.Status,
                v.CreationDate)).ToList();

            ActionResult = new OkObjectResult(response);
        }

        /// <summary>
        /// Handles not found errors.
        /// </summary>
        /// <param name="message">Error message.</param>
        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(new { Error = message });
        }
    }
}