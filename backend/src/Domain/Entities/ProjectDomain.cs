namespace Portfolio.Domain.Entities
{
    /// <summary>
    /// Domain category for portfolio projects.
    /// Used for high-level filtering: iGaming, Crypto, Real-Time, Fullstack.
    /// </summary>
    public enum ProjectDomain
    {
        /// <summary>Slot engines, casino backends, provably-fair systems.</summary>
        IGaming = 0,

        /// <summary>Crypto trading, blockchain, DeFi tooling.</summary>
        Crypto = 1,

        /// <summary>WebSocket, SignalR, streaming, live data pipelines.</summary>
        RealTime = 2,

        /// <summary>End-to-end apps spanning backend + frontend.</summary>
        Fullstack = 3
    }
}
