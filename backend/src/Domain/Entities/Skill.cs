namespace Portfolio.Domain.Entities
{
    /// <summary>
    /// Represents a professional skill with a category and proficiency level.
    /// </summary>
    public sealed class Skill
    {
        private Skill() { } // EF Core constructor

        public Skill(
            string name,
            string category,
            int proficiency,
            int sortOrder,
            string? evidence = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Category = category;
            Proficiency = proficiency;
            SortOrder = sortOrder;
            Evidence = evidence;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Category { get; private set; } = string.Empty;
        public int Proficiency { get; private set; } // 0-100
        public int SortOrder { get; private set; }
        public string? Evidence { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
    }
}
