using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Models.Requests;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.RequestHandlers
{
    /// <summary>
    /// MediatR Request Handler for List Available Vehicles operation.
    /// Follows the pattern described in the README.md.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ListAvailableVehiclesRequestHandler"/> class.
    /// </remarks>
    /// <param name="useCase">List available vehicles use case.</param>
    /// <param name="presenter">List available vehicles presenter.</param>
    public class ListAvailableVehiclesRequestHandler(ListAvailableVehiclesUseCase useCase,
        IListAvailableVehiclesOutputPort presenter) : IRequestHandler<ListAvailableVehiclesRequest, IWebApiPresenter>
    {
        private readonly ListAvailableVehiclesUseCase useCase = useCase ?? throw new ArgumentNullException(nameof(useCase));
        private readonly IListAvailableVehiclesOutputPort presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));

        /// <summary>
        /// Handles the List Available Vehicles request using MediatR pattern.
        /// Build the Input message then call the Use Case.
        /// The handler does not build the Response, instead this responsibility
        /// is delegated to the presenter object (as per README.md guidance).
        /// </summary>
        /// <param name="request">List available vehicles request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Web API presenter with the result.</returns>
        public async Task<IWebApiPresenter> Handle(ListAvailableVehiclesRequest request, CancellationToken cancellationToken)
        {
            // Build the Input message (as recommended in README.md)
            var input = new ListAvailableVehiclesInput();

            // Call the Use Case (as recommended in README.md)
            await this.useCase.Execute(input).ConfigureAwait(true);

            // Return the presenter (as recommended in README.md)
            return (IWebApiPresenter)this.presenter;
        }
    }
}
