using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Enums;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories
{
    /// <summary>
    /// MongoDB implementation of Rental Repository.
    /// Updated for VehicleId ValueObject.
    /// </summary>
    public sealed class RentalRepository : IRentalRepository
    {
        private readonly IMongoCollection<Rental> _rentals;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalRepository"/> class.
        /// </summary>
        /// <param name="mongoService">MongoDB service.</param>
        public RentalRepository(MongoService mongoService)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            _rentals = mongoService.MongoClient.GetDatabase("RentalDDBB").GetCollection<Rental>("Rentals");
        }

        /// <inheritdoc/>
        public async Task<Rental> GetByIdAsync(RentalId rentalId)
        {
            var filter = Builders<Rental>.Filter.Eq(r => r.Id, rentalId);
            var rental = await _rentals.Find(filter).FirstOrDefaultAsync();

            return rental ?? throw new RentalNotFoundException($"Rental with ID {rentalId} not found.");
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<Rental>> GetByCustomerIdAsync(CustomerId customerId)
        {
            var filter = Builders<Rental>.Filter.Eq(r => r.CustomerId, customerId);
            var rentals = await _rentals.Find(filter).ToListAsync();
            return rentals.AsReadOnly();
        }

        /// <summary>
        /// Gets all rentals for a vehicle.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <returns>List of vehicle rentals.</returns>
        public async Task<IReadOnlyList<Rental>> GetByVehicleIdAsync(VehicleId vehicleId)
        {
            var filter = Builders<Rental>.Filter.Eq(r => r.VehicleId, vehicleId);
            var rentals = await _rentals.Find(filter).ToListAsync();
            return rentals.AsReadOnly();
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<Rental>> GetByStatusAsync(RentalStatus status)
        {
            var filter = Builders<Rental>.Filter.Eq(r => r.Status, status);
            var rentals = await _rentals.Find(filter).ToListAsync();
            return rentals.AsReadOnly();
        }

        /// <inheritdoc/>
        public async Task<Rental?> GetActiveRentalByCustomerIdAsync(CustomerId customerId)
        {
            var filter = Builders<Rental>.Filter.And(
                Builders<Rental>.Filter.Eq(r => r.CustomerId, customerId),
                Builders<Rental>.Filter.Eq(r => r.Status, RentalStatus.Active));

            return await _rentals.Find(filter).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the active rental for a vehicle (if any).
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <returns>The active rental if found, null otherwise.</returns>
        public async Task<Rental?> GetActiveRentalByVehicleIdAsync(VehicleId vehicleId)
        {
            var filter = Builders<Rental>.Filter.And(
                Builders<Rental>.Filter.Eq(r => r.VehicleId, vehicleId),
                Builders<Rental>.Filter.Eq(r => r.Status, RentalStatus.Active));

            return await _rentals.Find(filter).FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task AddAsync(Rental rental)
        {
            ArgumentNullException.ThrowIfNull(rental);
            await _rentals.InsertOneAsync(rental);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(Rental rental)
        {
            ArgumentNullException.ThrowIfNull(rental);
            var filter = Builders<Rental>.Filter.Eq(r => r.Id, rental.Id);
            await _rentals.ReplaceOneAsync(filter, rental);
        }

        /// <inheritdoc/>
        public async Task<bool> CustomerHasActiveRentalAsync(CustomerId customerId)
        {
            var rental = await GetActiveRentalByCustomerIdAsync(customerId);
            return rental != null;
        }
    }
}
