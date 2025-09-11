using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Services;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Create Vehicle use case implementation.
    /// </summary>
    public sealed class CreateVehicleUseCase : IUseCase<CreateVehicleInput>
    {
        private readonly ICreateVehicleOutputPort _outputPort;
        private readonly VehicleRentalService _vehicleRentalService;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleUseCase"/> class.
        /// </summary>
        /// <param name="outputPort">Output port.</param>
        /// <param name="vehicleRentalService">Vehicle rental service.</param>
        /// <param name="unitOfWork">Unit of work.</param>
        public CreateVehicleUseCase(
            ICreateVehicleOutputPort outputPort,
            VehicleRentalService vehicleRentalService,
            IUnitOfWork unitOfWork)
        {
            _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
            _vehicleRentalService = vehicleRentalService ?? throw new ArgumentNullException(nameof(vehicleRentalService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Executes the Create Vehicle use case.
        /// Simplified for the new Vehicle structure.
        /// </summary>
        /// <param name="input">Input data.</param>
        /// <returns>Task representing the operation.</returns>
        public async Task Execute(CreateVehicleInput input)
        {
            try
            {
                // Create vehicle through domain service
                var vehicle = await _vehicleRentalService.CreateVehicleAsync(
                    input.VehicleId, input.Model);

                // Save changes
                await _unitOfWork.Save();

                // Build output
                var output = new CreateVehicleOutput(
                    vehicle.VehicleId,
                    vehicle.Model,
                    vehicle.Status.ToString(),
                    vehicle.CreationDate);

                _outputPort.StandardHandle(output);
            }
            catch (DomainException ex)
            {
                _outputPort.InvalidInput(ex.Message);
            }
            catch (Exception ex)
            {
                _outputPort.NotFoundHandle(ex.Message);
            }
        }
    }
}
