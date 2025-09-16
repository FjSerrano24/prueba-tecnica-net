namespace GtMotive.Estimate.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a rental is not found.
    /// </summary>
    public class RentalNotFoundException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentalNotFoundException"/> class.
        /// </summary>
        public RentalNotFoundException()
            : base("Rental not found.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public RentalNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public RentalNotFoundException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
