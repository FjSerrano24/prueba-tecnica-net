using System;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Factories
{
    /// <summary>
    /// Factory interface for creating Rental instances.
    /// </summary>
    public interface IRentalFactory
    {
        /// <summary>
        /// Creates a new rental instance.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <param name="startDate">Rental start date.</param>
        /// <returns>A new rental instance.</returns>
        Rental CreateRental(CustomerId customerId, VehicleId vehicleId, DateTime startDate);
    }
}

