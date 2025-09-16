using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Enums;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Rental Repository interface.
    /// </summary>
    public interface IRentalRepository
    {
        /// <summary>
        /// Gets a rental by its identifier.
        /// </summary>
        /// <param name="rentalId">Rental identifier.</param>
        /// <returns>The rental if found.</returns>
        /// <exception cref="Exceptions.RentalNotFoundException">Thrown when rental is not found.</exception>
        Task<Rental> GetByIdAsync(RentalId rentalId);

        /// <summary>
        /// Gets all rentals for a customer.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        /// <returns>List of customer rentals.</returns>
        Task<IReadOnlyList<Rental>> GetByCustomerIdAsync(CustomerId customerId);

        /// <summary>
        /// Gets all rentals for a vehicle.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <returns>List of vehicle rentals.</returns>
        Task<IReadOnlyList<Rental>> GetByVehicleIdAsync(VehicleId vehicleId);

        /// <summary>
        /// Gets all rentals with the specified status.
        /// </summary>
        /// <param name="status">Rental status.</param>
        /// <returns>List of rentals.</returns>
        Task<IReadOnlyList<Rental>> GetByStatusAsync(RentalStatus status);

        /// <summary>
        /// Gets the active rental for a customer (if any).
        /// Business rule: A customer can only have one active rental at a time.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        /// <returns>The active rental if found, null otherwise.</returns>
        Task<Rental> GetActiveRentalByCustomerIdAsync(CustomerId customerId);

        /// <summary>
        /// Gets the active rental for a vehicle (if any).
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <returns>The active rental if found, null otherwise.</returns>
        Task<Rental> GetActiveRentalByVehicleIdAsync(VehicleId vehicleId);

        /// <summary>
        /// Adds a new rental to the repository.
        /// </summary>
        /// <param name="rental">Rental to add.</param>
        /// <returns>Task representing the operation.</returns>
        Task AddAsync(Rental rental);

        /// <summary>
        /// Updates an existing rental.
        /// </summary>
        /// <param name="rental">Rental to update.</param>
        /// <returns>Task representing the operation.</returns>
        Task UpdateAsync(Rental rental);

        /// <summary>
        /// Checks if a customer has an active rental.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        /// <returns>True if customer has active rental, false otherwise.</returns>
        Task<bool> CustomerHasActiveRentalAsync(CustomerId customerId);
    }
}
