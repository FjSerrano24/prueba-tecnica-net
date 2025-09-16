using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Output for Create Vehicle use case.
    /// Simplified for Vehicle with Id, CreationDate, Model, and Year.
    /// </summary>
    public sealed class CreateVehicleOutput : IUseCaseOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleOutput"/> class.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <param name="model">Vehicle model.</param>
        /// <param name="year">Vehicle year.</param>
        /// <param name="status">Vehicle status.</param>
        /// <param name="creationDate">Creation date.</param>
        public CreateVehicleOutput(
            VehicleId vehicleId,
            string model,
            int year,
            string status,
            DateTime creationDate)
        {
            VehicleId = vehicleId;
            Model = model;
            Year = year;
            Status = status;
            CreationDate = creationDate;
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

        /// <summary>
        /// Gets the status.
        /// </summary>
        public string Status { get; }

        /// <summary>
        /// Gets the creation date.
        /// </summary>
        public DateTime CreationDate { get; }
    }
}
