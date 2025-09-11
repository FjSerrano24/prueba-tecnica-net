namespace GtMotive.Estimate.Microservice.Domain.Enums
{
    /// <summary>
    /// Vehicle Status enumeration.
    /// </summary>
    public enum VehicleStatus
    {
        /// <summary>
        /// Vehicle is available for rental.
        /// </summary>
        Available = 1,

        /// <summary>
        /// Vehicle is currently rented.
        /// </summary>
        Rented = 2,

        /// <summary>
        /// Vehicle is under maintenance.
        /// </summary>
        Maintenance = 3
    }
}

