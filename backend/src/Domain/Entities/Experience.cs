namespace Portfolio.Domain.Entities
{
    /// <summary>
    /// Represents professional experience — a job, role, or engagement.
    /// </summary>
    public sealed class Experience
    {
        private Experience() { } // EF Core constructor

        public Experience(
            string company,
            string role,
            string description,
            DateOnly startDate,
            DateOnly? endDate)
        {
            Id = Guid.NewGuid();
            Company = company;
            Role = role;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; private set; }
        public string Company { get; private set; } = string.Empty;
        public string Role { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public DateOnly StartDate { get; private set; }
        public DateOnly? EndDate { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
    }
}
