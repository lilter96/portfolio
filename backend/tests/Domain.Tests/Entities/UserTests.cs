namespace Portfolio.Domain.Tests.Entities
{
    using Portfolio.Domain.Entities;

    public sealed class UserTests
    {
        [Fact]
        public void Create_AssignsIdAndTimestamps()
        {
            var user = new User("test@example.com", "Test User");

            Assert.NotEqual(Guid.Empty, user.Id);
            Assert.Equal("test@example.com", user.Email);
            Assert.Equal("Test User", user.DisplayName);
            Assert.NotEqual(default, user.CreatedAt);
            Assert.Null(user.UpdatedAt);
        }

        [Fact]
        public void UpdateDisplayName_SetsNameAndTimestamp()
        {
            var user = new User("test@example.com", "Original");
            var before = user.UpdatedAt;

            user.UpdateDisplayName("Updated");

            Assert.Equal("Updated", user.DisplayName);
            Assert.NotNull(user.UpdatedAt);
            Assert.NotEqual(before, user.UpdatedAt);
        }
    }
}
