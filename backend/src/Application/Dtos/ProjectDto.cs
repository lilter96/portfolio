namespace Portfolio.Application.Dtos
{
    /// <summary>
    /// Read-model DTO for <see cref="Portfolio.Domain.Entities.Project"/>.
    /// </summary>
    public sealed record ProjectDto(
        Guid Id,
        string Title,
        string Description,
        string? Url,
        string? SourceUrl,
        List<string> Technologies,
        int SortOrder);
}
