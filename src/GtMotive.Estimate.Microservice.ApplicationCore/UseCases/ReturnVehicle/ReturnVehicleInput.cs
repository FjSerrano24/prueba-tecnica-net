using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Input for Return Vehicle use case.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ReturnVehicleInput"/> class.
    /// </remarks>
    /// <param name="rentalId">Rental identifier.</param>
    /// <param name="endDate">Return date.</param>
    public sealed class ReturnVehicleInput(Guid rentalId, DateTime endDate) : IUseCaseInput
    {
        /// <summary>
        /// Gets the rental identifier.
        /// </summary>
        public Guid RentalId { get; } = rentalId;

        /// <summary>
        /// Gets the return date.
        /// </summary>
        public DateTime EndDate { get; } = endDate;
    }
}
