namespace GtMotive.Estimate.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a customer already has an active rental.
    /// Business rule: A customer cannot have more than one active rental at a time.
    /// </summary>
    public class CustomerAlreadyHasActiveRentalException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerAlreadyHasActiveRentalException"/> class.
        /// </summary>
        public CustomerAlreadyHasActiveRentalException()
            : base("Customer already has an active rental.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerAlreadyHasActiveRentalException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public CustomerAlreadyHasActiveRentalException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerAlreadyHasActiveRentalException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public CustomerAlreadyHasActiveRentalException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
