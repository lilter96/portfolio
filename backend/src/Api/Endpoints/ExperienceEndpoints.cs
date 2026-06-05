namespace Portfolio.Api.Endpoints
{
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.EntityFrameworkCore;
    using Portfolio.Application.Dtos;
    using Portfolio.Application.Mapping;
    using Portfolio.Infrastructure.Data;

    /// <summary>
    /// Read-only endpoints for professional experience.
    /// </summary>
    internal static class ExperienceEndpoints
    {
        internal static void MapExperienceEndpoints(this WebApplication app)
        {
            var group = app
                .NewVersionedApi()
                .MapGroup("/api/v{version:apiVersion}/experience")
                .HasApiVersion(1, 0);

            group
                .MapGet("/", GetExperience)
                .WithName("GetExperience")
                .WithDescription("Returns all professional experience entries ordered by start date (most recent first).")
                .Produces<List<ExperienceDto>>();

            group
                .MapGet("/{id:guid}", GetExperienceById)
                .WithName("GetExperienceById")
                .WithDescription("Returns a single experience entry by its unique identifier.")
                .Produces<ExperienceDto>()
                .Produces(StatusCodes.Status404NotFound);
        }

        private static async Task<Ok<List<ExperienceDto>>> GetExperience(
            AppDbContext db,
            CancellationToken cancellationToken)
        {
            var entities = await db.Experiences
                .OrderByDescending(e => e.StartDate)
                .ToListAsync(cancellationToken);

            return TypedResults.Ok(entities.Select(e => e.ToDto()).ToList());
        }

        private static async Task<Results<Ok<ExperienceDto>, NotFound>> GetExperienceById(
            Guid id,
            AppDbContext db,
            CancellationToken cancellationToken)
        {
            var entity = await db.Experiences
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync(cancellationToken);

            return entity is null
                ? TypedResults.NotFound()
                : TypedResults.Ok(entity.ToDto());
        }
    }
}
