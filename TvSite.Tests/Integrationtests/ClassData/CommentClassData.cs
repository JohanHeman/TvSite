using System.Collections;
using TvSite.Domain.Entities.Database;

namespace TvSite.Tests.Integrationtests.ClassData;

public class CommentClassData : IEnumerable<object[]>
{
    
    public IEnumerator<object[]> GetEnumerator() 
    {
        yield return new object[]
        {
            new Comment()
            {
                Id = Guid.NewGuid(),
                ApplicationUserId = Guid.NewGuid(),
                EpisodeMediaId = "4",
                Text = "This is a comment",
            }
        };
        yield return new object[]
        {
            new Comment()
            {
                Id = Guid.NewGuid(),
                ApplicationUserId = Guid.NewGuid(),
                EpisodeMediaId = "4",
                Text = "This is a comment",
            }
        };
        yield return new object[]
        {
            new Comment()
            {
                Id = Guid.NewGuid(),
                ApplicationUserId = Guid.NewGuid(),
                EpisodeMediaId = "4",
                Text = "This is a comment",
            }
        };
        yield return new object[]
        {
            new Comment()
            {
                Id = Guid.NewGuid(),
                ApplicationUserId = Guid.NewGuid(),
                EpisodeMediaId = "4",
                Text = "This is a comment",
            }
        };
        yield return new object[]
        {
            new Comment()
            {
                Id = Guid.NewGuid(),
                ApplicationUserId = Guid.NewGuid(),
                EpisodeMediaId = "8",
                Text = "This is a comment",
            }
        };
        yield return new object[]
        {
            new Comment()
            {
                Id = Guid.NewGuid(),
                ApplicationUserId = Guid.NewGuid(),
                EpisodeMediaId = "10",
                Text = "This is a comment",
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}