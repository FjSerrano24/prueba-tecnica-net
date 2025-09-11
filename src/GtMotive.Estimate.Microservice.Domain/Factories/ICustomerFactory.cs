using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Factories
{
    /// <summary>
    /// Factory interface for creating Customer instances.
    /// </summary>
    public interface ICustomerFactory
    {
        /// <summary>
        /// Creates a new customer instance.
        /// </summary>
        /// <param name="name">Customer name.</param>
        /// <param name="email">Customer email.</param>
        /// <returns>A new customer instance.</returns>
        Customer CreateCustomer(CustomerName name, CustomerEmail email);
    }
}

