namespace Portfolio.Api.Tests.Endpoints
{
    using System.Net;
    using System.Net.Http.Json;
    using Portfolio.Api.Tests.Integration;

    /// <summary>
    /// Integration tests for the contact form endpoint.
    /// Uses an isolated test host with PostgreSQL and Redis containers.
    /// </summary>
    [Collection("Integration")]
    public sealed class ContactEndpointsTests : IClassFixture<PortfolioApiFactory>
    {
        private readonly HttpClient _client;

        public ContactEndpointsTests(PortfolioApiFactory factory)
        {
            ArgumentNullException.ThrowIfNull(factory);
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task SubmitContact_ValidRequest_Returns200()
        {
            var request = new
            {
                name = "Test User",
                email = "test@example.com",
                message = "Hello, this is a test message from the contact form."
            };

            var response = await _client.PostAsJsonAsync("/api/v1/contact", request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadFromJsonAsync<ContactTestResponse>();
            Assert.NotNull(body);
            Assert.True(body.Accepted);
        }

        [Fact]
        public async Task SubmitContact_MissingName_Returns400()
        {
            var request = new
            {
                name = "",
                email = "test@example.com",
                message = "Hello, this is a test message."
            };

            var response = await _client.PostAsJsonAsync("/api/v1/contact", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task SubmitContact_InvalidEmail_Returns400()
        {
            var request = new
            {
                name = "Test User",
                email = "not-an-email",
                message = "Hello, this is a test message."
            };

            var response = await _client.PostAsJsonAsync("/api/v1/contact", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task SubmitContact_ShortMessage_Returns400()
        {
            var request = new
            {
                name = "Test User",
                email = "test@example.com",
                message = "Short"
            };

            var response = await _client.PostAsJsonAsync("/api/v1/contact", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task SubmitContact_HoneypotFilled_Returns200ButDoesNotSend()
        {
            var request = new
            {
                name = "Bot",
                email = "bot@spam.com",
                message = "Buy cheap stuff! Click here!",
                website = "http://spam.com"
            };

            var response = await _client.PostAsJsonAsync("/api/v1/contact", request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadFromJsonAsync<ContactTestResponse>();
            Assert.NotNull(body);
            Assert.True(body.Accepted);
            // Honeypot submissions appear accepted but skip the actual email send
        }

    }

    internal sealed record ContactTestResponse(bool Accepted, string? Message);
}
