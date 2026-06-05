namespace Portfolio.Api.Endpoints
{
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.EntityFrameworkCore;
    using Portfolio.Application.Dtos;
    using Portfolio.Application.Mapping;
    using Portfolio.Infrastructure.Data;

    /// <summary>
    /// Read-only endpoints for portfolio projects.
    /// </summary>
    internal static class ProjectsEndpoints
    {
        internal static void MapProjectsEndpoints(this WebApplication app)
        {
            var group = app
                .NewVersionedApi()
                .MapGroup("/api/v{version:apiVersion}/projects")
                .HasApiVersion(1, 0);

            group
                .MapGet("/", GetProjects)
                .WithName("GetProjects")
                .WithDescription("Returns all portfolio projects ordered by display order.")
                .Produces<List<ProjectDto>>();

            group
                .MapGet("/{id:guid}", GetProjectById)
                .WithName("GetProjectById")
                .WithDescription("Returns a single project by its unique identifier.")
                .Produces<ProjectDto>()
                .Produces(StatusCodes.Status404NotFound);
        }

        private static async Task<Ok<List<ProjectDto>>> GetProjects(
            AppDbContext db,
            CancellationToken cancellationToken)
        {
            var entities = await db.Projects
                .OrderBy(p => p.SortOrder)
                .ToListAsync(cancellationToken);

            return TypedResults.Ok(entities.Select(p => p.ToDto()).ToList());
        }

        private static async Task<Results<Ok<ProjectDto>, NotFound>> GetProjectById(
            Guid id,
            AppDbContext db,
            CancellationToken cancellationToken)
        {
            var entity = await db.Projects
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync(cancellationToken);

            return entity is null
                ? TypedResults.NotFound()
                : TypedResults.Ok(entity.ToDto());
        }
    }
}
