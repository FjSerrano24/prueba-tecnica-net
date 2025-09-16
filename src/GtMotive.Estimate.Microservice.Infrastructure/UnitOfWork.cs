using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Infrastructure
{
    /// <summary>
    /// MongoDB Unit of Work implementation.
    /// Since MongoDB operations are atomic at the document level,
    /// this implementation provides a simple abstraction.
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Saves all changes to the database.
        /// In MongoDB, operations are typically atomic at the document level,
        /// so this implementation returns a success indicator.
        /// </summary>
        /// <returns>Number of affected operations (always 1 for MongoDB).</returns>
        public Task<int> Save()
        {
            // MongoDB operations are atomic at document level
            // This implementation assumes all repository operations
            // have already been committed
            return Task.FromResult(1);
        }
    }
}
