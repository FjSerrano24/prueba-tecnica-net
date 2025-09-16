using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Input for Create Vehicle use case.
    /// Simplified for Vehicle with Id, CreationDate, Model, and Year.
    /// </summary>
    public sealed class CreateVehicleInput : IUseCaseInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleInput"/> class.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <param name="model">Vehicle model.</param>
        /// <param name="year">Vehicle year.</param>
        public CreateVehicleInput(VehicleId vehicleId, string model, int year)
        {
            VehicleId = vehicleId;
            Model = model;
            Year = year;
        }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public VehicleId VehicleId { get; }

        /// <summary>
        /// Gets the vehicle model.
        /// </summary>
        public string Model { get; }

        /// <summary>
        /// Gets the vehicle year.
        /// </summary>
        public int Year { get; }
    }
}
