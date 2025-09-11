using System;

namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Rental ID Value Object.
    /// </summary>
    public readonly struct RentalId : IEquatable<RentalId>
    {
        private readonly Guid _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalId"/> struct.
        /// </summary>
        /// <param name="value">The rental identifier.</param>
        public RentalId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new DomainException("Rental ID cannot be empty.");
            }

            _value = value;
        }

        /// <summary>
        /// Creates a new RentalId.
        /// </summary>
        /// <returns>A new RentalId.</returns>
        public static RentalId New() => new(Guid.NewGuid());

        /// <summary>
        /// Converts to Guid.
        /// </summary>
        /// <returns>The underlying Guid value.</returns>
        public Guid ToGuid() => _value;

        /// <inheritdoc/>
        public override string ToString() => _value.ToString();

        /// <inheritdoc/>
        public bool Equals(RentalId other) => _value.Equals(other._value);

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is RentalId other && Equals(other);

        /// <inheritdoc/>
        public override int GetHashCode() => _value.GetHashCode();

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(RentalId left, RentalId right) => left.Equals(right);

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(RentalId left, RentalId right) => !left.Equals(right);
    }
}

