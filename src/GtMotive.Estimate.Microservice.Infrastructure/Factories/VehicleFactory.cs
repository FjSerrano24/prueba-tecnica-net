using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Factories;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Infrastructure.Factories
{
    /// <summary>
    /// Vehicle Factory implementation.
    /// Simplified for Vehicle with Id, CreationDate, and Model only.
    /// </summary>
    public sealed class VehicleFactory : IVehicleFactory
    {
        /// <summary>
        /// Creates a new vehicle instance.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <param name="model">Vehicle model.</param>
        /// <returns>New vehicle instance.</returns>
        public Vehicle CreateVehicle(VehicleId vehicleId, string model)
        {
            return new Vehicle(vehicleId, model);
        }
    }
}
