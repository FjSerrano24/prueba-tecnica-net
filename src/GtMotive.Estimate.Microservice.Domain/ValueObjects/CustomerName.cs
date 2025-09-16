namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Customer Name Value Object.
    /// </summary>
    public readonly struct CustomerName
    {
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerName"/> struct.
        /// </summary>
        /// <param name="value">The customer name.</param>
        public CustomerName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException("Customer name cannot be empty.");
            }

            if (value.Trim().Length < 2)
            {
                throw new DomainException("Customer name must have at least 2 characters.");
            }

            if (value.Trim().Length > 100)
            {
                throw new DomainException("Customer name cannot exceed 100 characters.");
            }

            _value = value.Trim();
        }

        /// <inheritdoc/>
        public override string ToString() => _value;
    }
}
