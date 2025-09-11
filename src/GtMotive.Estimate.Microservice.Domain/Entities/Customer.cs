using System;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Customer Entity.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        /// <param name="name">Customer name.</param>
        /// <param name="email">Customer email.</param>
        public Customer(
            CustomerId customerId,
            CustomerName name,
            CustomerEmail email)
        {
            Id = customerId;
            Name = name;
            Email = email;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Private constructor for ORM.
        /// </summary>
        private Customer()
        {
        }

        /// <summary>
        /// Gets the customer identifier.
        /// </summary>
        public CustomerId Id { get; private set; }

        /// <summary>
        /// Gets the customer name.
        /// </summary>
        public CustomerName Name { get; private set; }

        /// <summary>
        /// Gets the customer email.
        /// </summary>
        public CustomerEmail Email { get; private set; }

        /// <summary>
        /// Gets the creation date.
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Gets the last update date.
        /// </summary>
        public DateTime? UpdatedAt { get; private set; }

        /// <summary>
        /// Updates customer information.
        /// </summary>
        /// <param name="name">New customer name.</param>
        /// <param name="email">New customer email.</param>
        public void Update(CustomerName name, CustomerEmail email)
        {
            Name = name;
            Email = email;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is not Customer other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Id.Equals(other.Id);
        }

        /// <inheritdoc/>
        public override int GetHashCode() => Id.GetHashCode();
    }
}

