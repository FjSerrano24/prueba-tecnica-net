using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Enums;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories
{
    /// <summary>
    /// MongoDB Vehicle Repository implementation.
    /// Simplified for Vehicle with Id, CreationDate, and Model only.
    /// </summary>
    public sealed class VehicleRepository : IVehicleRepository
    {
        private readonly IMongoCollection<Vehicle> _vehicles;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleRepository"/> class.
        /// </summary>
        /// <param name="mongoService">MongoDB service.</param>
        public VehicleRepository(MongoService mongoService)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            _vehicles = mongoService.GetCollection<Vehicle>("vehicles");
        }

        /// <summary>
        /// Gets a vehicle by its identifier.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <returns>The vehicle if found.</returns>
        /// <exception cref="VehicleNotFoundException">Thrown when vehicle is not found.</exception>
        public async Task<Vehicle> GetByIdAsync(VehicleId vehicleId)
        {
            var filter = Builders<Vehicle>.Filter.Eq(v => v.VehicleId, vehicleId);
            var vehicle = await _vehicles.Find(filter).FirstOrDefaultAsync();

            if (vehicle == null)
            {
                throw new VehicleNotFoundException($"Vehicle with ID {vehicleId} was not found.");
            }

            return vehicle;
        }

        /// <summary>
        /// Gets all vehicles with the specified status.
        /// </summary>
        /// <param name="status">Vehicle status.</param>
        /// <returns>List of vehicles.</returns>
        public async Task<IReadOnlyList<Vehicle>> GetByStatusAsync(VehicleStatus status)
        {
            var filter = Builders<Vehicle>.Filter.Eq(v => v.Status, status);
            var vehicles = await _vehicles.Find(filter).ToListAsync();
            return vehicles.AsReadOnly();
        }

        /// <summary>
        /// Gets all available vehicles.
        /// </summary>
        /// <returns>List of available vehicles.</returns>
        public async Task<IReadOnlyList<Vehicle>> GetAvailableVehiclesAsync()
        {
            return await GetByStatusAsync(VehicleStatus.Available);
        }

        /// <summary>
        /// Gets vehicles by model.
        /// </summary>
        /// <param name="model">Vehicle model to search for.</param>
        /// <returns>List of vehicles with matching model.</returns>
        public async Task<IReadOnlyList<Vehicle>> GetByModelAsync(string model)
        {
            var filter = Builders<Vehicle>.Filter.Regex(v => v.Model, new BsonRegularExpression(model, "i"));
            var vehicles = await _vehicles.Find(filter).ToListAsync();
            return vehicles.AsReadOnly();
        }

        /// <summary>
        /// Adds a new vehicle to the repository.
        /// </summary>
        /// <param name="vehicle">Vehicle to add.</param>
        /// <returns>Task representing the operation.</returns>
        public async Task AddAsync(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);
            await _vehicles.InsertOneAsync(vehicle);
        }

        /// <summary>
        /// Updates an existing vehicle.
        /// </summary>
        /// <param name="vehicle">Vehicle to update.</param>
        /// <returns>Task representing the operation.</returns>
        public async Task UpdateAsync(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);
            var filter = Builders<Vehicle>.Filter.Eq(v => v.VehicleId, vehicle.VehicleId);
            await _vehicles.ReplaceOneAsync(filter, vehicle);
        }

        /// <summary>
        /// Checks if a vehicle with the specified ID already exists.
        /// </summary>
        /// <param name="vehicleId">Vehicle ID to check.</param>
        /// <returns>True if exists, false otherwise.</returns>
        public async Task<bool> ExistsByIdAsync(VehicleId vehicleId)
        {
            var filter = Builders<Vehicle>.Filter.Eq(v => v.VehicleId, vehicleId);
            var count = await _vehicles.CountDocumentsAsync(filter);
            return count > 0;
        }
    }
}