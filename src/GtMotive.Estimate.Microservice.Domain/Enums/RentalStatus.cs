namespace GtMotive.Estimate.Microservice.Domain.Enums
{
    /// <summary>
    /// Rental Status enumeration.
    /// </summary>
    public enum RentalStatus
    {
        /// <summary>
        /// Rental is active.
        /// </summary>
        Active = 1,

        /// <summary>
        /// Rental has been completed/returned.
        /// </summary>
        Completed = 2,

        /// <summary>
        /// Rental has been cancelled.
        /// </summary>
        Cancelled = 3
    }
}
