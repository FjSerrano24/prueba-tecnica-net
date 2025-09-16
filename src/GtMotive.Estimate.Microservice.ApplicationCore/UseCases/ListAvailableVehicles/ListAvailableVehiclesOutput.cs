using System.Collections.Generic;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles
{
    /// <summary>
    /// Output for List Available Vehicles use case.
    /// </summary>
    public sealed class ListAvailableVehiclesOutput : IUseCaseOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListAvailableVehiclesOutput"/> class.
        /// </summary>
        /// <param name="vehicles">List of available vehicles.</param>
        public ListAvailableVehiclesOutput(IReadOnlyList<VehicleModel> vehicles)
        {
            Vehicles = vehicles ?? new List<VehicleModel>();
        }

        /// <summary>
        /// Gets the list of available vehicles.
        /// </summary>
        public IReadOnlyList<VehicleModel> Vehicles { get; }
    }
}
