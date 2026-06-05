namespace Portfolio.Api.Tests.Integration
{
    /// <summary>
    /// Collection definition that ensures integration tests run sequentially
    /// (they share Testcontainer resources).
    /// </summary>
    [CollectionDefinition("Integration")]
    public sealed class IntegrationCollection : ICollectionFixture<PortfolioApiFactory>;
}
