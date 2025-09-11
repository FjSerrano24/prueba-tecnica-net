using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Enums;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Vehicle Repository interface.
    /// Simplified for Vehicle with Id, CreationDate, and Model only.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Gets a vehicle by its identifier.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <returns>The vehicle if found.</returns>
        /// <exception cref="Exceptions.VehicleNotFoundException">Thrown when vehicle is not found.</exception>
        Task<Vehicle> GetByIdAsync(VehicleId vehicleId);

        /// <summary>
        /// Gets all vehicles with the specified status.
        /// </summary>
        /// <param name="status">Vehicle status.</param>
        /// <returns>List of vehicles.</returns>
        Task<IReadOnlyList<Vehicle>> GetByStatusAsync(VehicleStatus status);

        /// <summary>
        /// Gets all available vehicles.
        /// </summary>
        /// <returns>List of available vehicles.</returns>
        Task<IReadOnlyList<Vehicle>> GetAvailableVehiclesAsync();

        /// <summary>
        /// Gets vehicles by model.
        /// </summary>
        /// <param name="model">Vehicle model to search for.</param>
        /// <returns>List of vehicles with matching model.</returns>
        Task<IReadOnlyList<Vehicle>> GetByModelAsync(string model);

        /// <summary>
        /// Adds a new vehicle to the repository.
        /// </summary>
        /// <param name="vehicle">Vehicle to add.</param>
        /// <returns>Task representing the operation.</returns>
        Task AddAsync(Vehicle vehicle);

        /// <summary>
        /// Updates an existing vehicle.
        /// </summary>
        /// <param name="vehicle">Vehicle to update.</param>
        /// <returns>Task representing the operation.</returns>
        Task UpdateAsync(Vehicle vehicle);

        /// <summary>
        /// Checks if a vehicle with the specified ID already exists.
        /// </summary>
        /// <param name="vehicleId">Vehicle ID to check.</param>
        /// <returns>True if exists, false otherwise.</returns>
        Task<bool> ExistsByIdAsync(VehicleId vehicleId);
    }
}
