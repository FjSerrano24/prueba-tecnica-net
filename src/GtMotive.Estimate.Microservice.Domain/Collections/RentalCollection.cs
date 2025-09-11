using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Enums;

namespace GtMotive.Estimate.Microservice.Domain.Collections
{
    /// <summary>
    /// First-Class Collection for Rentals.
    /// Follows the First-Class Collections pattern described in README.md:
    /// "Each collection gets wrapped in its own class, so now behaviors related to the 
    /// collection have a home. You may find that filters become a part of this new class."
    /// </summary>
    public sealed class RentalCollection
    {
        private readonly IList<Rental> _rentals;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalCollection"/> class.
        /// </summary>
        public RentalCollection()
        {
            _rentals = new List<Rental>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalCollection"/> class.
        /// </summary>
        /// <param name="rentals">Initial rentals.</param>
        public RentalCollection(IEnumerable<Rental> rentals)
        {
            _rentals = rentals?.ToList() ?? new List<Rental>();
        }

        /// <summary>
        /// Adds a rental to the collection.
        /// </summary>
        /// <param name="rental">Rental to add.</param>
        public void Add(Rental rental) => _rentals.Add(rental);

        /// <summary>
        /// Adds multiple rentals to the collection.
        /// </summary>
        /// <param name="rentals">Rentals to add.</param>
        public void Add(IEnumerable<Rental> rentals)
        {
            foreach (var rental in rentals)
            {
                Add(rental);
            }
        }

        /// <summary>
        /// Gets all rentals as read-only collection.
        /// </summary>
        /// <returns>Read-only collection of rentals.</returns>
        public IReadOnlyCollection<Rental> GetRentals()
        {
            return new ReadOnlyCollection<Rental>(_rentals);
        }

        /// <summary>
        /// Gets active rentals only.
        /// Collection-specific behavior as recommended in README.md.
        /// </summary>
        /// <returns>Active rentals.</returns>
        public IReadOnlyCollection<Rental> GetActiveRentals()
        {
            var activeRentals = _rentals
                .Where(r => r.Status == RentalStatus.Active)
                .ToList();

            return new ReadOnlyCollection<Rental>(activeRentals);
        }

        /// <summary>
        /// Gets completed rentals only.
        /// </summary>
        /// <returns>Completed rentals.</returns>
        public IReadOnlyCollection<Rental> GetCompletedRentals()
        {
            var completedRentals = _rentals
                .Where(r => r.Status == RentalStatus.Completed)
                .ToList();

            return new ReadOnlyCollection<Rental>(completedRentals);
        }

        /// <summary>
        /// Gets rentals for a specific date range.
        /// Domain-specific behavior for the collection.
        /// </summary>
        /// <param name="startDate">Start date.</param>
        /// <param name="endDate">End date.</param>
        /// <returns>Rentals in the date range.</returns>
        public IReadOnlyCollection<Rental> GetRentalsInDateRange(DateTime startDate, DateTime endDate)
        {
            var rentalsInRange = _rentals
                .Where(r => r.StartDate >= startDate && r.StartDate <= endDate)
                .ToList();

            return new ReadOnlyCollection<Rental>(rentalsInRange);
        }

        /// <summary>
        /// Calculates total rental days for all completed rentals.
        /// Collection-specific behavior as recommended in README.md.
        /// </summary>
        /// <returns>Total rental days.</returns>
        public int GetTotalRentalDays()
        {
            return _rentals
                .Where(r => r.Status == RentalStatus.Completed)
                .Sum(r => r.GetDurationInDays() ?? 0);
        }

        /// <summary>
        /// Gets count of active rentals.
        /// </summary>
        /// <returns>Count of active rentals.</returns>
        public int GetActiveCount()
        {
            return _rentals.Count(r => r.Status == RentalStatus.Active);
        }

        /// <summary>
        /// Gets total count of rentals.
        /// </summary>
        /// <returns>Total count of rentals.</returns>
        public int Count => _rentals.Count;

        /// <summary>
        /// Checks if collection is empty.
        /// </summary>
        /// <returns>True if empty, false otherwise.</returns>
        public bool IsEmpty => _rentals.Count == 0;

        /// <summary>
        /// Checks if collection has any active rentals.
        /// Domain-specific query behavior.
        /// </summary>
        /// <returns>True if has active rentals, false otherwise.</returns>
        public bool HasActiveRentals()
        {
            return _rentals.Any(r => r.Status == RentalStatus.Active);
        }
    }
}

