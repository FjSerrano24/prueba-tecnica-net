using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Factories
{
    /// <summary>
    /// Vehicle Factory interface.
    /// Simplified for Vehicle with Id, CreationDate, and Model only.
    /// </summary>
    public interface IVehicleFactory
    {
        /// <summary>
        /// Creates a new vehicle instance.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <param name="model">Vehicle model.</param>
        /// <returns>New vehicle instance.</returns>
        Vehicle CreateVehicle(VehicleId vehicleId, string model);
    }
}