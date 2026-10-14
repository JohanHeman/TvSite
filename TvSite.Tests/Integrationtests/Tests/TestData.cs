using TvSite.Domain.Entities.Database;

namespace TvSite.Tests.Integrationtests.Tests
{
    internal class TestData
    {
        public List<Comment> GetTestCommentsAsync()
        {
            var comments = new List<Comment>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Text = "Great episode!",
                DateTime = DateTime.Now.AddMinutes(-20),
                EpisodeMediaId = "3"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Text = "The ending was amazing.",
                DateTime = DateTime.Now.AddMinutes(-15),
                EpisodeMediaId = "3"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Text = "I cannot wait for the next episode.",
                DateTime = DateTime.Now.AddMinutes(-10),
                EpisodeMediaId = "3"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Text = "The actors did a great job.",
                DateTime = DateTime.Now.AddMinutes(-5),
                EpisodeMediaId = "8"
            }
        };
            return comments;
        }
        public List<ApplicationUser> GetTestUsers()
        {
            var userIds = new[]
            {
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid()
        };

            var users = userIds.Select(id => new ApplicationUser
            {
                Id = id,
                UserName = $"user-{id}",
                NormalizedUserName = $"USER-{id}".ToUpperInvariant(),
                Email = $"{id}@example.com",
                NormalizedEmail = $"{id}@example.com".ToUpperInvariant(),
                DisplayName = "Test User"
            });
            return users.ToList();
        }
    }
}