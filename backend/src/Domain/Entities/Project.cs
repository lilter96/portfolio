namespace Portfolio.Domain.Entities
{
    /// <summary>
    /// Represents a portfolio project with a link, description, and technology tags.
    /// </summary>
    public sealed class Project
    {
        private Project() { } // EF Core constructor

        public Project(
            string title,
            string description,
            string? url,
            string? sourceUrl,
            List<string> technologies,
            int sortOrder)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Url = url;
            SourceUrl = sourceUrl;
            Technologies = technologies;
            SortOrder = sortOrder;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string? Url { get; private set; }
        public string? SourceUrl { get; private set; }
        public List<string> Technologies { get; private set; } = [];
        public int SortOrder { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
    }
}
