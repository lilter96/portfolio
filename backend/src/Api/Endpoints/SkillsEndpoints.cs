namespace Portfolio.Api.Endpoints
{
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.EntityFrameworkCore;
    using Portfolio.Application.Dtos;
    using Portfolio.Application.Mapping;
    using Portfolio.Infrastructure.Data;

    /// <summary>
    /// Read-only endpoints for skills.
    /// </summary>
    internal static class SkillsEndpoints
    {
        internal static void MapSkillsEndpoints(this WebApplication app)
        {
            var group = app
                .NewVersionedApi()
                .MapGroup("/api/v{version:apiVersion}/skills")
                .HasApiVersion(1, 0);

            group
                .MapGet("/", GetSkills)
                .WithName("GetSkills")
                .WithDescription("Returns all skills grouped by category, ordered by proficiency.")
                .Produces<List<SkillDto>>();

            group
                .MapGet("/{id:guid}", GetSkillById)
                .WithName("GetSkillById")
                .WithDescription("Returns a single skill by its unique identifier.")
                .Produces<SkillDto>()
                .Produces(StatusCodes.Status404NotFound);
        }

        private static async Task<Ok<List<SkillDto>>> GetSkills(
            AppDbContext db,
            CancellationToken cancellationToken)
        {
            var entities = await db.Skills
                .OrderBy(s => s.Category)
                .ThenBy(s => s.SortOrder)
                .ToListAsync(cancellationToken);

            return TypedResults.Ok(entities.Select(s => s.ToDto()).ToList());
        }

        private static async Task<Results<Ok<SkillDto>, NotFound>> GetSkillById(
            Guid id,
            AppDbContext db,
            CancellationToken cancellationToken)
        {
            var entity = await db.Skills
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync(cancellationToken);

            return entity is null
                ? TypedResults.NotFound()
                : TypedResults.Ok(entity.ToDto());
        }
    }
}
