namespace GtMotive.Estimate.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a customer is not found.
    /// </summary>
    public class CustomerNotFoundException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerNotFoundException"/> class.
        /// </summary>
        public CustomerNotFoundException()
            : base("Customer not found.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public CustomerNotFoundException(string message)
            : base(message)
        {
        }
    }
}

