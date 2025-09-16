namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Output port for Create Vehicle use case.
    /// </summary>
    public interface ICreateVehicleOutputPort : IOutputPortStandard<CreateVehicleOutput>
    {
        /// <summary>
        /// Handles invalid input scenarios.
        /// </summary>
        /// <param name="message">Error message.</param>
        void InvalidInput(string message);
    }
}
