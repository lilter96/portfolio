namespace Portfolio.Infrastructure.Email
{
    using Microsoft.Extensions.Logging;
    using Portfolio.Domain.Interfaces;

    /// <summary>
    /// Development-only implementation of <see cref="IEmailSender"/>.
    /// Logs emails at Information level instead of sending them.
    /// No SMTP credentials or external services are used — safe to commit.
    /// </summary>
    public sealed class NoOpEmailSender : IEmailSender
    {
        private readonly ILogger<NoOpEmailSender> _logger;

        public NoOpEmailSender(ILogger<NoOpEmailSender> logger) => _logger = logger;

        public Task SendAsync(
            string recipient,
            string subject,
            string body,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "DEV email — To: {Recipient}, Subject: {Subject}, Body: {Body}",
                recipient,
                subject,
                body);

            return Task.CompletedTask;
        }
    }
}
