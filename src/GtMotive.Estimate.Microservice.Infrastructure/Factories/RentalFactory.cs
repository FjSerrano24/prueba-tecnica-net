using System;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Factories;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Infrastructure.Factories
{
    /// <summary>
    /// Rental Factory implementation.
    /// </summary>
    public sealed class RentalFactory : IRentalFactory
    {
        /// <inheritdoc/>
        public Rental CreateRental(CustomerId customerId, VehicleId vehicleId, DateTime startDate)
        {
            var rentalId = RentalId.New();
            return new Rental(rentalId, customerId, vehicleId, startDate);
        }
    }
}
