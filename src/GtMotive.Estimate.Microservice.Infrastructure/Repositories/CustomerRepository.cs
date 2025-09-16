using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories
{
    /// <summary>
    /// MongoDB implementation of Customer Repository.
    /// </summary>
    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _customers;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRepository"/> class.
        /// </summary>
        /// <param name="mongoService">MongoDB service.</param>
        public CustomerRepository(MongoService mongoService)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            _customers = mongoService.MongoClient.GetDatabase("RentalDDBB").GetCollection<Customer>("customers");
        }

        /// <inheritdoc/>
        public async Task<Customer> GetByIdAsync(CustomerId customerId)
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.Id, customerId);
            var customer = await _customers.Find(filter).FirstOrDefaultAsync();

            return customer ?? throw new CustomerNotFoundException($"Customer with ID {customerId} not found.");
        }

        /// <inheritdoc/>
        public async Task<Customer> GetByEmailAsync(CustomerEmail email)
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.Email, email);
            return await _customers.Find(filter).FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task AddAsync(Customer customer)
        {
            ArgumentNullException.ThrowIfNull(customer);
            await _customers.InsertOneAsync(customer);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(Customer customer)
        {
            ArgumentNullException.ThrowIfNull(customer);
            var filter = Builders<Customer>.Filter.Eq(c => c.Id, customer.Id);
            await _customers.ReplaceOneAsync(filter, customer);
        }

        /// <inheritdoc/>
        public async Task<bool> ExistsByEmailAsync(CustomerEmail email)
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.Email, email);
            var count = await _customers.CountDocumentsAsync(filter);
            return count > 0;
        }
    }
}
