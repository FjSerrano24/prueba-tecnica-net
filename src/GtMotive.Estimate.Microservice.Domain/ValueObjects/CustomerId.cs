using System;

namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Customer ID Value Object.
    /// </summary>
    public readonly struct CustomerId : IEquatable<CustomerId>
    {
        private readonly Guid _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerId"/> struct.
        /// </summary>
        /// <param name="value">The customer identifier.</param>
        public CustomerId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new DomainException("Customer ID cannot be empty.");
            }

            _value = value;
        }

        /// <summary>
        /// Creates a new CustomerId.
        /// </summary>
        /// <returns>A new CustomerId.</returns>
        public static CustomerId New() => new(Guid.NewGuid());

        /// <summary>
        /// Converts to Guid.
        /// </summary>
        /// <returns>The underlying Guid value.</returns>
        public Guid ToGuid() => _value;

        /// <inheritdoc/>
        public override string ToString() => _value.ToString();

        /// <inheritdoc/>
        public bool Equals(CustomerId other) => _value.Equals(other._value);

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is CustomerId other && Equals(other);

        /// <inheritdoc/>
        public override int GetHashCode() => _value.GetHashCode();

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(CustomerId left, CustomerId right) => left.Equals(right);

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(CustomerId left, CustomerId right) => !left.Equals(right);
    }
}

