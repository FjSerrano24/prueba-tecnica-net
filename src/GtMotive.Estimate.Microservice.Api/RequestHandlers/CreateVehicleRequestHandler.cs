using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Models.Requests;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.RequestHandlers
{
    /// <summary>
    /// MediatR Request Handler for Create Vehicle operation.
    /// Follows the pattern described in the README.md.
    /// Simplified for Vehicle with Id, CreationDate, and Model only.
    /// </summary>
    public sealed class CreateVehicleRequestHandler : IRequestHandler<CreateVehicleRequest, IWebApiPresenter>
    {
        private readonly CreateVehicleUseCase _useCase;
        private readonly ICreateVehicleOutputPort _presenter;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleRequestHandler"/> class.
        /// </summary>
        /// <param name="useCase">Create vehicle use case.</param>
        /// <param name="presenter">Create vehicle presenter.</param>
        public CreateVehicleRequestHandler(
            CreateVehicleUseCase useCase, 
            ICreateVehicleOutputPort presenter)
        {
            _useCase = useCase ?? throw new ArgumentNullException(nameof(useCase));
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        }

        /// <summary>
        /// Handles the Create Vehicle request using MediatR pattern.
        /// Build the Input message then call the Use Case.
        /// The handler does not build the Response, instead this responsibility 
        /// is delegated to the presenter object (as per README.md guidance).
        /// </summary>
        /// <param name="request">Create vehicle request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Web API presenter with the result.</returns>
        public async Task<IWebApiPresenter> Handle(CreateVehicleRequest request, CancellationToken cancellationToken)
        {
            // Build the Input message (as recommended in README.md)
            var input = new CreateVehicleInput(
                request.VehicleId,
                request.Model);

            // Call the Use Case (as recommended in README.md)
            await _useCase.Execute(input);

            // Return the presenter (as recommended in README.md)
            return (IWebApiPresenter)_presenter;
        }
    }
}