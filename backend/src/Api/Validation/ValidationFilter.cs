namespace Portfolio.Api.Validation
{
    using FluentValidation;

    /// <summary>
    /// Extension methods for adding FluentValidation to Minimal API endpoints.
    /// Uses AddEndpointFilterFactory for explicit DI resolution.
    /// </summary>
    internal static class ValidationFilterExtensions
    {
        /// <summary>
        /// Applies a validation filter that validates <typeparamref name="TRequest"/> from the request body.
        /// Returns a 400 ProblemDetails response with validation errors on failure.
        /// </summary>
        public static RouteHandlerBuilder WithValidation<TRequest>(
            this RouteHandlerBuilder builder)
            where TRequest : class
        {
            builder.AddEndpointFilterFactory((filterContext, next) =>
            {
                var validator = filterContext.ApplicationServices
                    .GetService<IValidator<TRequest>>();

                return async (context) =>
                {
                    var argument = context.Arguments
                        .OfType<TRequest>()
                        .FirstOrDefault();

                    if (argument is null)
                    {
                        return TypedResults.Problem(
                            detail: $"Expected a request body of type {typeof(TRequest).Name}.",
                            statusCode: StatusCodes.Status400BadRequest);
                    }

                    if (validator is not null)
                    {
                        var result = await validator.ValidateAsync(argument);

                        if (!result.IsValid)
                        {
                            var errors = result.Errors
                                .GroupBy(e => e.PropertyName)
                                .ToDictionary(
                                    g => g.Key,
                                    g => g.Select(e => e.ErrorMessage).ToArray());

                            return TypedResults.ValidationProblem(
                                errors: errors!,
                                detail: "One or more validation errors occurred.",
                                instance: $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}",
                                title: "Validation Failed");
                        }
                    }

                    return await next(context);
                };
            });

            return builder;
        }
    }
}
