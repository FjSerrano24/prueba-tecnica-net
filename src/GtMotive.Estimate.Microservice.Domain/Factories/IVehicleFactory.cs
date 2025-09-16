using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Factories
{
    /// <summary>
    /// Vehicle Factory interface.
    /// Simplified for Vehicle with Id, CreationDate, Model, and Year.
    /// </summary>
    public interface IVehicleFactory
    {
        /// <summary>
        /// Creates a new vehicle instance.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <param name="model">Vehicle model.</param>
        /// <param name="year">Vehicle year.</param>
        /// <returns>New vehicle instance.</returns>
        Vehicle CreateVehicle(VehicleId vehicleId, string model, int year);
    }
}
