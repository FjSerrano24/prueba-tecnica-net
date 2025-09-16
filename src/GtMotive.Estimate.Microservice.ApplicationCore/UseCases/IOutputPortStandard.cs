namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases
{
    /// <summary>
    /// Interface to define the Standard Output Port.
    /// </summary>
    /// <typeparam name="TUseCaseOutput">Tyoe of the use case response dto.</typeparam>
    public interface IOutputPortStandard<in TUseCaseOutput>
        where TUseCaseOutput : IUseCaseOutput
    {
        /// <summary>
        /// Writes to the Standard Output.
        /// </summary>
        /// <param name="response">The Output Port Message.</param>
        void StandardHandle(TUseCaseOutput response);

        /// <summary>
        /// Writes to the not found output.
        /// </summary>
        /// <param name="message">The not found response message.</param>
        void NotFoundHandle(string message);

        /// <summary>
        /// Writes to the conflict output.
        /// </summary>
        /// <param name="message">The conflict response message.</param>
        void ConflictHandle(string message);
    }
}
