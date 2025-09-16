using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Enums;

namespace GtMotive.Estimate.Microservice.Domain.Collections
{
    /// <summary>
    /// First-Class Collection for Vehicles.
    /// </summary>
    public sealed class VehicleCollection
    {
        private readonly List<Vehicle> _vehicles;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleCollection"/> class.
        /// </summary>
        public VehicleCollection()
        {
            _vehicles = new List<Vehicle>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleCollection"/> class.
        /// </summary>
        /// <param name="vehicles">Initial vehicles.</param>
        public VehicleCollection(IEnumerable<Vehicle> vehicles)
        {
            _vehicles = vehicles?.ToList() ?? new List<Vehicle>();
        }

        /// <summary>
        /// Gets total count of vehicles.
        /// </summary>
        /// <returns>Total count of vehicles.</returns>
        public int Count => _vehicles.Count;

        /// <summary>
        /// Gets a value indicating whether checks if collection is empty.
        /// </summary>
        /// <returns>True if empty, false otherwise.</returns>
        public bool IsEmpty => _vehicles.Count == 0;

        /// <summary>
        /// Adds a vehicle to the collection.
        /// </summary>
        /// <param name="vehicle">Vehicle to add.</param>
        public void Add(Vehicle vehicle) => _vehicles.Add(vehicle);

        /// <summary>
        /// Adds multiple vehicles to the collection.
        /// </summary>
        /// <param name="vehicles">Vehicles to add.</param>
        public void Add(IEnumerable<Vehicle> vehicles)
        {
            if (vehicles != null)
            {
                foreach (var vehicle in vehicles)
                {
                    Add(vehicle);
                }
            }
        }

        /// <summary>
        /// Gets all vehicles as read-only collection.
        /// </summary>
        /// <returns>Read-only collection of vehicles.</returns>
        public IReadOnlyCollection<Vehicle> Vehicles()
        {
            var transactions = new ReadOnlyCollection<Vehicle>(_vehicles);
            return transactions;
        }

        /// <summary>
        /// Gets available vehicles only.
        /// This behavior is specific to the vehicle collection domain.
        /// </summary>
        /// <returns>Available vehicles.</returns>
        public IReadOnlyCollection<Vehicle> GetAvailableVehicles()
        {
            var availableVehicles = _vehicles
                .Where(v => v.Status == VehicleStatus.Available)
                .ToList();

            return new ReadOnlyCollection<Vehicle>(availableVehicles);
        }

        /// <summary>
        /// Gets vehicles by model.
        /// Collection-specific behavior as recommended in README.md.
        /// </summary>
        /// <param name="model">Model to filter by.</param>
        /// <returns>Vehicles of the specified model.</returns>
        public IReadOnlyCollection<Vehicle> GetVehiclesByModel(string model)
        {
            var vehiclesByModel = _vehicles
                .Where(v => v.Model.Equals(model, System.StringComparison.OrdinalIgnoreCase))
                .ToList();

            return new ReadOnlyCollection<Vehicle>(vehiclesByModel);
        }

        /// <summary>
        /// Gets count of available vehicles.
        /// </summary>
        /// <returns>Count of available vehicles.</returns>
        public int GetAvailableCount()
        {
            return _vehicles.Count(v => v.Status == VehicleStatus.Available);
        }
    }
}
