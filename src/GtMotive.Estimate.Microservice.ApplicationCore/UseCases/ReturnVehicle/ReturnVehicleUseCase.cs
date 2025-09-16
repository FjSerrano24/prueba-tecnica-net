using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Services;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Return Vehicle use case implementation.
    /// </summary>
    public sealed class ReturnVehicleUseCase : IUseCase<ReturnVehicleInput>
    {
        private readonly IReturnVehicleOutputPort _outputPort;
        private readonly VehicleRentalService _vehicleRentalService;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleUseCase"/> class.
        /// </summary>
        /// <param name="outputPort">Output port.</param>
        /// <param name="vehicleRentalService">Vehicle rental service.</param>
        /// <param name="vehicleRepository">Vehicle repository.</param>
        /// <param name="unitOfWork">Unit of work.</param>
        public ReturnVehicleUseCase(
            IReturnVehicleOutputPort outputPort,
            VehicleRentalService vehicleRentalService,
            IVehicleRepository vehicleRepository,
            IUnitOfWork unitOfWork)
        {
            _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
            _vehicleRentalService = vehicleRentalService ?? throw new ArgumentNullException(nameof(vehicleRentalService));
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Executes the Return Vehicle use case.
        /// </summary>
        /// <param name="input">Input data.</param>
        /// <returns>Task representing the operation.</returns>
        public async Task Execute(ReturnVehicleInput input)
        {
            try
            {
                // Create value objects
                var rentalId = new RentalId(input.RentalId);

                // Return vehicle through domain service
                var rental = await _vehicleRentalService.ReturnVehicleAsync(rentalId, input.EndDate);

                // Get vehicle details for output
                var vehicle = await _vehicleRepository.GetByIdAsync(rental.VehicleId);

                // Save changes
                await _unitOfWork.Save();

                // Build output
                var output = new ReturnVehicleOutput(
                    rental.Id.ToGuid(),
                    rental.CustomerId.ToGuid(),
                    vehicle.VehicleId.Value,
                    rental.StartDate,
                    rental.EndDate!.Value,
                    rental.Status.ToString(),
                    rental.UpdatedAt!.Value);

                _outputPort.StandardHandle(output);
            }
            catch (RentalNotFoundException ex)
            {
                _outputPort.NotFoundHandle(ex.Message);
            }
            catch (VehicleNotFoundException ex)
            {
                _outputPort.NotFoundHandle(ex.Message);
            }
            catch (DomainException ex)
            {
                _outputPort.InvalidInput(ex.Message);
            }
            catch (Exception ex)
            {
                _outputPort.ConflictHandle(ex.Message);
            }
        }
    }
}
