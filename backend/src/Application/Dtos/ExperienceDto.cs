namespace Portfolio.Application.Dtos
{
    /// <summary>
    /// Read-model DTO for <see cref="Portfolio.Domain.Entities.Experience"/>.
    /// </summary>
    public sealed record ExperienceDto(
        Guid Id,
        string Company,
        string Role,
        string Description,
        DateOnly StartDate,
        DateOnly? EndDate);
}
