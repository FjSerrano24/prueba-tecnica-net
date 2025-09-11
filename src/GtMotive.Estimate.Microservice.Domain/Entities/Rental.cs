using System;
using GtMotive.Estimate.Microservice.Domain.Enums;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Rental Aggregate Root.
    /// </summary>
    public class Rental
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rental"/> class.
        /// </summary>
        /// <param name="rentalId">Rental identifier.</param>
        /// <param name="customerId">Customer identifier.</param>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <param name="startDate">Rental start date.</param>
        public Rental(
            RentalId rentalId,
            CustomerId customerId,
            VehicleId vehicleId,
            DateTime startDate)
        {
            Id = rentalId;
            CustomerId = customerId;
            VehicleId = vehicleId;
            StartDate = startDate;
            Status = RentalStatus.Active;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Private constructor for ORM.
        /// </summary>
        private Rental()
        {
        }

        /// <summary>
        /// Gets the rental identifier.
        /// </summary>
        public RentalId Id { get; private set; }

        /// <summary>
        /// Gets the customer identifier.
        /// </summary>
        public CustomerId CustomerId { get; private set; }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public VehicleId VehicleId { get; private set; }

        /// <summary>
        /// Gets the rental start date.
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        /// Gets the rental end date.
        /// </summary>
        public DateTime? EndDate { get; private set; }

        /// <summary>
        /// Gets the rental status.
        /// </summary>
        public RentalStatus Status { get; private set; }

        /// <summary>
        /// Gets the creation date.
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Gets the last update date.
        /// </summary>
        public DateTime? UpdatedAt { get; private set; }

        /// <summary>
        /// Checks if the rental is active.
        /// </summary>
        /// <returns>True if active, false otherwise.</returns>
        public bool IsActive() => Status == RentalStatus.Active;

        /// <summary>
        /// Completes the rental (vehicle return).
        /// </summary>
        /// <param name="endDate">Return date.</param>
        /// <exception cref="DomainException">Thrown when rental is not active or end date is invalid.</exception>
        public void Complete(DateTime endDate)
        {
            if (!IsActive())
            {
                throw new DomainException($"Rental {Id} is not active. Current status: {Status}");
            }

            if (endDate < StartDate)
            {
                throw new DomainException("End date cannot be earlier than start date.");
            }

            EndDate = endDate;
            Status = RentalStatus.Completed;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Cancels the rental.
        /// </summary>
        /// <exception cref="DomainException">Thrown when rental is not active.</exception>
        public void Cancel()
        {
            if (!IsActive())
            {
                throw new DomainException($"Rental {Id} is not active. Current status: {Status}");
            }

            Status = RentalStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets the duration of the rental.
        /// </summary>
        /// <returns>Duration in days if completed, otherwise null.</returns>
        public int? GetDurationInDays()
        {
            if (EndDate == null)
                return null;

            return (int)(EndDate.Value - StartDate).TotalDays;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is not Rental other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Id.Equals(other.Id);
        }

        /// <inheritdoc/>
        public override int GetHashCode() => Id.GetHashCode();
    }
}
