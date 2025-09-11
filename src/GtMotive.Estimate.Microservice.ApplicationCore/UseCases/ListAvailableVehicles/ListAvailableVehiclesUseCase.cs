using System;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles
{
    /// <summary>
    /// List Available Vehicles Use Case.
    /// Simplified for the new Vehicle structure.
    /// </summary>
    public sealed class ListAvailableVehiclesUseCase : IUseCase<ListAvailableVehiclesInput>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IListAvailableVehiclesOutputPort _outputPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListAvailableVehiclesUseCase"/> class.
        /// </summary>
        /// <param name="vehicleRepository">Vehicle repository.</param>
        /// <param name="outputPort">Output port.</param>
        public ListAvailableVehiclesUseCase(
            IVehicleRepository vehicleRepository,
            IListAvailableVehiclesOutputPort outputPort)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
        }

        /// <summary>
        /// Executes the List Available Vehicles use case.
        /// Simplified for the new Vehicle structure.
        /// </summary>
        /// <param name="input">Input data.</param>
        /// <returns>Task representing the operation.</returns>
        public async Task Execute(ListAvailableVehiclesInput input)
        {
            try
            {
                var vehicles = await _vehicleRepository.GetAvailableVehiclesAsync();

                var vehicleModels = vehicles.Select(v => new VehicleModel(
                    v.VehicleId,
                    v.Model,
                    v.Status.ToString(),
                    v.CreationDate)).ToList();

                var output = new ListAvailableVehiclesOutput(vehicleModels);

                _outputPort.StandardHandle(output);
            }
            catch (Exception ex)
            {
                _outputPort.NotFoundHandle(ex.Message);
            }
        }
    }
}