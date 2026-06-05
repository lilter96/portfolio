namespace Portfolio.Application.Dtos
{
    /// <summary>
    /// Read-model DTO for <see cref="Portfolio.Domain.Entities.Skill"/>.
    /// </summary>
    public sealed record SkillDto(
        Guid Id,
        string Name,
        string Category,
        int Proficiency,
        int SortOrder);
}
