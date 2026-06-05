namespace Portfolio.Api.Endpoints
{
    using FluentValidation;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Portfolio.Api.Validation;
    using Portfolio.Domain.Interfaces;

    /// <summary>
    /// Contact form endpoint with validation, honeypot anti-spam, and per-IP rate limiting.
    /// </summary>
    internal static class ContactEndpoints
    {
        private const string RateLimitPolicy = "Contact";

        internal static void MapContactEndpoints(this WebApplication app)
        {
            var group = app
                .NewVersionedApi()
                .MapGroup("/api/v{version:apiVersion}/contact")
                .HasApiVersion(1, 0)
                .RequireRateLimiting(RateLimitPolicy);

            group
                .MapPost("/", SubmitContact)
                .WithName("SubmitContact")
                .WithDescription("Submits a contact form message. Includes anti-spam protections.")
                .WithValidation<ContactRequest>()
                .Produces<ContactResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status429TooManyRequests);
        }

        private static async Task<Ok<ContactResponse>> SubmitContact(
            ContactRequest request,
            IEmailSender emailSender,
            CancellationToken cancellationToken)
        {
            // Honeypot check — bots fill hidden fields
            if (!string.IsNullOrEmpty(request.Website))
            {
                return TypedResults.Ok(new ContactResponse(
                    Accepted: true,
                    Message: "Thank you for your message."));
            }

            await emailSender.SendAsync(
                recipient: "terentiy.gatsukov@gmail.com",
                subject: $"[Portfolio] Message from {request.Name}",
                body: $"From: {request.Name} ({request.Email})\n\n{request.Message}",
                cancellationToken);

            return TypedResults.Ok(new ContactResponse(
                Accepted: true,
                Message: "Thank you for your message. I'll get back to you soon."));
        }
    }

    /// <summary>
    /// Contact form submission DTO. The <c>Website</c> field is a honeypot —
    /// legitimate users leave it empty; bots fill it.
    /// </summary>
    internal sealed record ContactRequest(
        string Name,
        string Email,
        string Message,
        string? Website);

    /// <summary>
    /// Response returned after a successful contact submission.
    /// </summary>
    internal sealed record ContactResponse(bool Accepted, string Message);

    /// <summary>
    /// FluentValidation validator for <see cref="ContactRequest"/>.
    /// </summary>
    internal sealed class ContactRequestValidator : AbstractValidator<ContactRequest>
    {
        public ContactRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.")
                .MaximumLength(256).WithMessage("Email must not exceed 256 characters.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message is required.")
                .MinimumLength(10).WithMessage("Message must be at least 10 characters.")
                .MaximumLength(5000).WithMessage("Message must not exceed 5000 characters.");
        }
    }
}
