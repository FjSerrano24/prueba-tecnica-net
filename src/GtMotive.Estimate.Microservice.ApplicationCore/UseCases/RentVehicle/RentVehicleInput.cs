using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Input for Rent Vehicle use case.
    /// </summary>
    public sealed class RentVehicleInput : IUseCaseInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleInput"/> class.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        /// <param name="customerName">Customer name.</param>
        /// <param name="customerEmail">Customer email.</param>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <param name="startDate">Rental start date.</param>
        public RentVehicleInput(
            Guid customerId,
            string customerName,
            string customerEmail,
            VehicleId vehicleId,
            DateTime startDate)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            VehicleId = vehicleId;
            StartDate = startDate;
        }

        /// <summary>
        /// Gets the customer identifier.
        /// </summary>
        public Guid CustomerId { get; }

        /// <summary>
        /// Gets the customer name.
        /// </summary>
        public string CustomerName { get; }

        /// <summary>
        /// Gets the customer email.
        /// </summary>
        public string CustomerEmail { get; }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public VehicleId VehicleId { get; }

        /// <summary>
        /// Gets the rental start date.
        /// </summary>
        public DateTime StartDate { get; }
    }
}
