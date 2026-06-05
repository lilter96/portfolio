namespace Portfolio.Domain.Entities
{
    /// <summary>
    /// Represents a registered user in the system.
    /// </summary>
    public sealed class User
    {
        private User() { } // EF Core constructor

        public User(string email, string displayName)
        {
            Id = Guid.NewGuid();
            Email = email;
            DisplayName = displayName;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; private set; }
        public string Email { get; private set; } = string.Empty;
        public string DisplayName { get; private set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        public void UpdateDisplayName(string displayName)
        {
            DisplayName = displayName;
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}
