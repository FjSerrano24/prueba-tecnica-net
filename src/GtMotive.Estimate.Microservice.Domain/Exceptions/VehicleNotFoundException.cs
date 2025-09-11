namespace GtMotive.Estimate.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a vehicle is not found.
    /// </summary>
    public class VehicleNotFoundException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotFoundException"/> class.
        /// </summary>
        public VehicleNotFoundException()
            : base("Vehicle not found.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public VehicleNotFoundException(string message)
            : base(message)
        {
        }
    }
}

