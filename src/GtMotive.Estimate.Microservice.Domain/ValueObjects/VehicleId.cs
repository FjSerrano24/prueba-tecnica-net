using System;

namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Vehicle identifier Value Object.
    /// Encapsulates the unique identifier for a vehicle.
    /// </summary>
    public readonly struct VehicleId
    {
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

            Value = value;
        }

        /// <summary>
        /// Gets the GUID value.
        /// </summary>
        public Guid Value { get; }

        /// <summary>
        /// Creates a new VehicleId with a new GUID.
        /// </summary>
        /// <returns>A new VehicleId.</returns>
        public static VehicleId New() => new(Guid.NewGuid());

        /// <summary>
        /// Creates a VehicleId from a GUID.
        /// </summary>
        /// <param name="value">The GUID value.</param>
        /// <returns>A new VehicleId.</returns>
        public static VehicleId FromGuid(Guid value) => new(value);

        /// <summary>
        /// Converts the VehicleId to a GUID.
        /// </summary>
        /// <returns>The GUID value.</returns>
        public Guid ToGuid() => Value;

        /// <inheritdoc/>
        public override string ToString() => Value.ToString();
    }
}
