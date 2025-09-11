using System;

namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Vehicle identifier Value Object.
    /// Encapsulates the unique identifier for a vehicle.
    /// </summary>
    public readonly struct VehicleId : IEquatable<VehicleId>
    {
        private readonly Guid _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleId"/> struct.
        /// </summary>
        /// <param name="value">The GUID value.</param>
        public VehicleId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new DomainException("Vehicle ID cannot be empty.");
            }

            _value = value;
        }

        /// <summary>
        /// Gets the GUID value.
        /// </summary>
        public Guid Value => _value;

        /// <summary>
        /// Creates a new VehicleId with a new GUID.
        /// </summary>
        /// <returns>A new VehicleId.</returns>
        public static VehicleId New() => new VehicleId(Guid.NewGuid());

        /// <summary>
        /// Converts the VehicleId to a GUID.
        /// </summary>
        /// <returns>The GUID value.</returns>
        public Guid ToGuid() => _value;

        /// <summary>
        /// Creates a VehicleId from a GUID.
        /// </summary>
        /// <param name="value">The GUID value.</param>
        /// <returns>A new VehicleId.</returns>
        public static VehicleId FromGuid(Guid value) => new VehicleId(value);

        /// <inheritdoc/>
        public bool Equals(VehicleId other) => _value.Equals(other._value);

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is VehicleId other && Equals(other);

        /// <inheritdoc/>
        public override int GetHashCode() => _value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => _value.ToString();

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>True if equal.</returns>
        public static bool operator ==(VehicleId left, VehicleId right) => left.Equals(right);

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>True if not equal.</returns>
        public static bool operator !=(VehicleId left, VehicleId right) => !left.Equals(right);
    }
}