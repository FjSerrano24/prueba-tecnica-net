using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Models.Requests;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.RequestHandlers
{
    /// <summary>
    /// MediatR Request Handler for Rent Vehicle operation.
    /// Follows the pattern described in the README.md.
    /// </summary>
    public sealed class RentVehicleRequestHandler : IRequestHandler<RentVehicleRequest, IWebApiPresenter>
    {
        private readonly RentVehicleUseCase _useCase;
        private readonly IRentVehicleOutputPort _presenter;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleRequestHandler"/> class.
        /// </summary>
        /// <param name="useCase">Rent vehicle use case.</param>
        /// <param name="presenter">Rent vehicle presenter.</param>
        public RentVehicleRequestHandler(
            RentVehicleUseCase useCase, 
            IRentVehicleOutputPort presenter)
        {
            _useCase = useCase ?? throw new ArgumentNullException(nameof(useCase));
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        }

        /// <summary>
        /// Handles the Rent Vehicle request using MediatR pattern.
        /// Build the Input message then call the Use Case.
        /// The handler does not build the Response, instead this responsibility 
        /// is delegated to the presenter object (as per README.md guidance).
        /// </summary>
        /// <param name="request">Rent vehicle request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Web API presenter with the result.</returns>
        public async Task<IWebApiPresenter> Handle(RentVehicleRequest request, CancellationToken cancellationToken)
        {
            // Build the Input message (as recommended in README.md)
            var input = new RentVehicleInput(
                request.CustomerId,
                request.CustomerName,
                request.CustomerEmail,
                request.VehicleId,
                request.StartDate);

            // Call the Use Case (as recommended in README.md)
            await _useCase.Execute(input);

            // Return the presenter (as recommended in README.md)
            return (IWebApiPresenter)_presenter;
        }
    }
}

