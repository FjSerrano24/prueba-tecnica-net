namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Output port for Rent Vehicle use case.
    /// </summary>
    public interface IRentVehicleOutputPort : IOutputPortStandard<RentVehicleOutput>
    {
        /// <summary>
        /// Handles invalid input scenarios.
        /// </summary>
        /// <param name="message">Error message.</param>
        void InvalidInput(string message);

        /// <summary>
        /// Handles business rule violation scenarios.
        /// </summary>
        /// <param name="message">Error message.</param>
        void BusinessRuleViolation(string message);
    }
}
