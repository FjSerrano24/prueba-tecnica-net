using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Services;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Rent Vehicle Use Case.
    /// Simplified for the new Vehicle structure.
    /// </summary>
    public sealed class RentVehicleUseCase : IUseCase<RentVehicleInput>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly VehicleRentalService _vehicleRentalService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRentVehicleOutputPort _outputPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleUseCase"/> class.
        /// </summary>
        /// <param name="customerRepository">Customer repository.</param>
        /// <param name="vehicleRepository">Vehicle repository.</param>
        /// <param name="rentalRepository">Rental repository.</param>
        /// <param name="vehicleRentalService">Vehicle rental service.</param>
        /// <param name="unitOfWork">Unit of work.</param>
        /// <param name="outputPort">Output port.</param>
        public RentVehicleUseCase(
            ICustomerRepository customerRepository,
            IVehicleRepository vehicleRepository,
            IRentalRepository rentalRepository,
            VehicleRentalService vehicleRentalService,
            IUnitOfWork unitOfWork,
            IRentVehicleOutputPort outputPort)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
            _vehicleRentalService = vehicleRentalService ?? throw new ArgumentNullException(nameof(vehicleRentalService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
        }

        /// <summary>
        /// Executes the Rent Vehicle use case.
        /// Simplified for the new Vehicle structure.
        /// </summary>
        /// <param name="input">Input data.</param>
        /// <returns>Task representing the operation.</returns>
        public async Task Execute(RentVehicleInput input)
        {
            try
            {
                // Create value objects
                var customerId = new CustomerId(input.CustomerId);
                var customerName = new CustomerName(input.CustomerName);
                var customerEmail = new CustomerEmail(input.CustomerEmail);

                // Try to rent the vehicle through domain service
                var rental = await _vehicleRentalService.RentVehicleAsync(customerId, input.VehicleId, input.StartDate);

                // Check if customer exists, if not create it
                try
                {
                    await _customerRepository.GetByIdAsync(customerId);
                }
                catch (CustomerNotFoundException)
                {
                    // Customer doesn't exist, create it
                    var customer = _customerFactory.NewCustomer(customerId, customerName, customerEmail);
                    await _customerRepository.AddAsync(customer);
                }

                // Save changes
                await _unitOfWork.Save();

                // Build output
                var output = new RentVehicleOutput(
                    rental.Id.ToGuid(),
                    rental.CustomerId.ToGuid(),
                    rental.VehicleId,
                    rental.StartDate,
                    rental.Status.ToString(),
                    rental.CreatedAt);

                _outputPort.StandardHandle(output);
            }
            catch (CustomerNotFoundException ex)
            {
                _outputPort.NotFoundHandle(ex.Message);
            }
            catch (VehicleNotFoundException ex)
            {
                _outputPort.NotFoundHandle(ex.Message);
            }
            catch (CustomerAlreadyHasActiveRentalException ex)
            {
                _outputPort.ConflictHandle(ex.Message);
            }
            catch (DomainException ex)
            {
                _outputPort.ConflictHandle(ex.Message);
            }
            catch (Exception ex)
            {
                _outputPort.NotFoundHandle(ex.Message);
            }
        }
    }
}