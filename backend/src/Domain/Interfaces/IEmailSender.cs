namespace Portfolio.Domain.Interfaces
{
    /// <summary>
    /// Abstraction for sending email. A no-op implementation is used in development;
    /// a real SMTP implementation can be swapped in for production without changing
    /// any consuming code.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="recipient">Recipient address.</param>
        /// <param name="subject">Email subject line.</param>
        /// <param name="body">Plain-text or HTML body.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task SendAsync(
            string recipient,
            string subject,
            string body,
            CancellationToken cancellationToken = default);
    }
}
