using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Input for Return Vehicle use case.
    /// </summary>
    public sealed class ReturnVehicleInput : IUseCaseInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleInput"/> class.
        /// </summary>
        /// <param name="rentalId">Rental identifier.</param>
        /// <param name="endDate">Return date.</param>
        public ReturnVehicleInput(Guid rentalId, DateTime endDate)
        {
            RentalId = rentalId;
            EndDate = endDate;
        }

        /// <summary>
        /// Gets the rental identifier.
        /// </summary>
        public Guid RentalId { get; }

        /// <summary>
        /// Gets the return date.
        /// </summary>
        public DateTime EndDate { get; }
    }
}

