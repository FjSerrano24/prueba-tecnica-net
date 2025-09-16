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
        private readonly ICreateVehicleOutputPort outputPort;
        private readonly VehicleRentalService vehicleRentalService;
        private readonly IUnitOfWork unitOfWork;

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
            this.outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
            this.vehicleRentalService = vehicleRentalService ?? throw new ArgumentNullException(nameof(vehicleRentalService));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
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
                var vehicle = await vehicleRentalService.CreateVehicleAsync(input.VehicleId, input.Model);

                // Save changes
                await this.unitOfWork.Save();

                // Build output
                var output = new CreateVehicleOutput(
                    vehicle.VehicleId,
                    vehicle.Model,
                    vehicle.Status.ToString(),
                    vehicle.CreationDate);

                this.outputPort.StandardHandle(output);
            }
            catch (DomainException ex)
            {
                this.outputPort.InvalidInput(ex.Message);
            }
            catch (Exception ex)
            {
                this.outputPort.NotFoundHandle(ex.Message);
            }
        }
    }
}
