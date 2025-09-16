using GtMotive.Estimate.Microservice.Api.UseCases;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Models.Requests
{
    /// <summary>
    /// List Available Vehicles Request model.
    /// Implements IRequest for MediatR pattern as recommended in README.md.
    /// </summary>
    public sealed class ListAvailableVehiclesRequest : IRequest<IWebApiPresenter>
    {
        // This request doesn't require any parameters since we're listing all available vehicles
    }
}
