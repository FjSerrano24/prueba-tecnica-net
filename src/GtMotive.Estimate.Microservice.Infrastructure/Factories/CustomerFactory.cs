using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Factories;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Infrastructure.Factories
{
    /// <summary>
    /// Customer Factory implementation.
    /// </summary>
    public sealed class CustomerFactory : ICustomerFactory
    {
        /// <inheritdoc/>
        public Customer CreateCustomer(CustomerName name, CustomerEmail email)
        {
            var customerId = CustomerId.New();
            return new Customer(customerId, name, email);
        }
    }
}

