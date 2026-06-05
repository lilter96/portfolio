namespace Portfolio.Domain.Interfaces
{
    /// <summary>
    /// Abstraction over distributed cache operations.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Gets a cached value by key, or null if not present.
        /// </summary>
        Task<string?> GetAsync(string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets a cached value with an optional expiration.
        /// </summary>
        Task SetAsync(
            string key,
            string value,
            TimeSpan? expiration = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes a cached value by key.
        /// </summary>
        Task RemoveAsync(string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks whether the cache is reachable.
        /// </summary>
        Task<bool> IsHealthyAsync(CancellationToken cancellationToken = default);
    }
}
