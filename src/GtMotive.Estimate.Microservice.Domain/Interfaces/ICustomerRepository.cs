using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Customer Repository interface.
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Gets a customer by its identifier.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        /// <returns>The customer if found.</returns>
        /// <exception cref="Exceptions.CustomerNotFoundException">Thrown when customer is not found.</exception>
        Task<Customer> GetByIdAsync(CustomerId customerId);

        /// <summary>
        /// Gets a customer by its email.
        /// </summary>
        /// <param name="email">Customer email.</param>
        /// <returns>The customer if found, null otherwise.</returns>
        Task<Customer> GetByEmailAsync(CustomerEmail email);

        /// <summary>
        /// Adds a new customer to the repository.
        /// </summary>
        /// <param name="customer">Customer to add.</param>
        /// <returns>Task representing the operation.</returns>
        Task AddAsync(Customer customer);

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="customer">Customer to update.</param>
        /// <returns>Task representing the operation.</returns>
        Task UpdateAsync(Customer customer);

        /// <summary>
        /// Checks if an email already exists.
        /// </summary>
        /// <param name="email">Email to check.</param>
        /// <returns>True if exists, false otherwise.</returns>
        Task<bool> ExistsByEmailAsync(CustomerEmail email);
    }
}
