namespace Portfolio.Application.Mapping
{
    using System;
    using Portfolio.Application.Dtos;
    using Portfolio.Domain.Entities;

    /// <summary>
    /// Extension methods for mapping domain entities to DTOs.
    /// </summary>
    public static class EntityMappingExtensions
    {
        public static ProjectDto ToDto(this Project project)
        {
            ArgumentNullException.ThrowIfNull(project);
            return new ProjectDto(
                project.Id,
                project.Title,
                project.Description,
                project.Url,
                project.SourceUrl,
                project.Technologies,
                project.SortOrder);
        }

        public static ExperienceDto ToDto(this Experience experience)
        {
            ArgumentNullException.ThrowIfNull(experience);
            return new ExperienceDto(
                experience.Id,
                experience.Company,
                experience.Role,
                experience.Description,
                experience.StartDate,
                experience.EndDate);
        }

        public static SkillDto ToDto(this Skill skill)
        {
            ArgumentNullException.ThrowIfNull(skill);
            return new SkillDto(
                skill.Id,
                skill.Name,
                skill.Category,
                skill.Proficiency,
                skill.SortOrder);
        }
    }
}
