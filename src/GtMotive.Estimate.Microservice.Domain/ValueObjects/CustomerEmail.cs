namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Customer Email Value Object.
    /// </summary>
    public readonly partial struct CustomerEmail
    {
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerEmail"/> struct.
        /// </summary>
        /// <param name="value">The customer email.</param>
        public CustomerEmail(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException("Customer email cannot be empty.");
            }

            var cleanedValue = value.Trim().ToUpperInvariant();

            if (!IsValidEmail(cleanedValue))
            {
                throw new DomainException($"Invalid email format: {value}");
            }

            _value = value;
        }

        /// <inheritdoc/>s
        public override string ToString()
        {
            return _value.ToString();
        }

        private static bool IsValidEmail(string email)
        {
            // Validate email...
            email.ToString();
            return true;
        }
    }
}
