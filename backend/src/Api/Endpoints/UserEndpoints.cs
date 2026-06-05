namespace Portfolio.Api.Endpoints
{
    using FluentValidation;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Portfolio.Api.Validation;

    /// <summary>
    /// Sample endpoints demonstrating FluentValidation with Minimal APIs.
    /// </summary>
    internal static class UserEndpoints
    {
        internal static void MapUserEndpoints(this WebApplication app)
        {
            var group = app
                .NewVersionedApi()
                .MapGroup("/api/v{version:apiVersion}/users")
                .HasApiVersion(1, 0);

            group
                .MapPost("/", CreateUser)
                .WithName("CreateUser")
                .WithDescription("Creates a new user. Validates the request body.")
                .WithValidation<CreateUserRequest>();
        }

        /// <summary>
        /// Creates a user — validation is handled by the WithValidation filter.
        /// </summary>
        private static Created<CreateUserResponse> CreateUser(CreateUserRequest request)
        {
            var response = new CreateUserResponse(
                Id: Guid.NewGuid(),
                Email: request.Email,
                DisplayName: request.DisplayName);

            return TypedResults.Created($"/api/v1/users/{response.Id}", response);
        }
    }

    /// <summary>
    /// Request DTO for user creation. Validated by <see cref="CreateUserRequestValidator"/>.
    /// </summary>
    internal sealed record CreateUserRequest(
        string Email,
        string DisplayName,
        int Age);

    /// <summary>
    /// FluentValidation validator for <see cref="CreateUserRequest"/>.
    /// </summary>
    internal sealed class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.")
                .MaximumLength(256).WithMessage("Email must not exceed 256 characters.");

            RuleFor(x => x.DisplayName)
                .NotEmpty().WithMessage("Display name is required.")
                .MinimumLength(2).WithMessage("Display name must be at least 2 characters.")
                .MaximumLength(100).WithMessage("Display name must not exceed 100 characters.");

            RuleFor(x => x.Age)
                .InclusiveBetween(13, 150).WithMessage("Age must be between 13 and 150.");
        }
    }

    /// <summary>
    /// Response DTO for successful user creation.
    /// </summary>
    internal sealed record CreateUserResponse(
        Guid Id,
        string Email,
        string DisplayName);
}
