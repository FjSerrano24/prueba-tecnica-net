using System;
using System.Text.RegularExpressions;

namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Customer Email Value Object.
    /// </summary>
    public readonly struct CustomerEmail : IEquatable<CustomerEmail>
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

            var cleanedValue = value.Trim().ToLowerInvariant();

            if (!IsValidEmail(cleanedValue))
            {
                throw new DomainException($"Invalid email format: {value}");
            }

            _value = cleanedValue;
        }

        private static bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return emailRegex.IsMatch(email);
        }

        /// <inheritdoc/>
        public override string ToString() => _value;

        /// <inheritdoc/>
        public bool Equals(CustomerEmail other) => _value.Equals(other._value);

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is CustomerEmail other && Equals(other);

        /// <inheritdoc/>
        public override int GetHashCode() => _value.GetHashCode();

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(CustomerEmail left, CustomerEmail right) => left.Equals(right);

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(CustomerEmail left, CustomerEmail right) => !left.Equals(right);
    }
}

