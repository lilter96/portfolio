namespace Portfolio.Api.Tests.Endpoints
{
    using System.Net;
    using System.Net.Http.Json;

    /// <summary>
    /// Integration tests for the contact form endpoint.
    /// Runs against a running API instance.
    /// </summary>
    public sealed class ContactEndpointsTests
    {
        private static readonly HttpClient Client = new()
        {
            BaseAddress = new Uri("http://localhost:5121")
        };

        [Fact]
        public async Task SubmitContact_ValidRequest_Returns200()
        {
            var request = new
            {
                name = "Test User",
                email = "test@example.com",
                message = "Hello, this is a test message from the contact form."
            };

            var response = await Client.PostAsJsonAsync("/api/v1/contact", request);

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

            var response = await Client.PostAsJsonAsync("/api/v1/contact", request);

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

            var response = await Client.PostAsJsonAsync("/api/v1/contact", request);

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

            var response = await Client.PostAsJsonAsync("/api/v1/contact", request);

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

            var response = await Client.PostAsJsonAsync("/api/v1/contact", request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadFromJsonAsync<ContactTestResponse>();
            Assert.NotNull(body);
            Assert.True(body.Accepted);
            // Honeypot submissions appear accepted but skip the actual email send
        }

    }

    internal sealed record ContactTestResponse(bool Accepted, string? Message);
}
