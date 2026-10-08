using Microsoft.EntityFrameworkCore;
using TvSite.Domain.Entities.Database;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Domain.Interfaces.Services;
using TvSite.Infrastructure.Data;

namespace TvSite.Application.Service;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _repository;

    public CommentService(ICommentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Comment>> GetCommentsByEpisodeMediaIdAsync(string episodeMediaId)
    {
        if (!string.IsNullOrWhiteSpace(episodeMediaId))
            return await _repository.GetCommentsByMediaIdAsync(episodeMediaId);

        else
            throw new ArgumentException("MediaId cannot be null or whitespace");
    }
    public async Task<Comment?> GetCommentByIdAsync(Guid commentId)
    {
        if (commentId != Guid.Empty)
            return await _repository.GetCommentByIdAsync(commentId);

        else
            throw new ArgumentException("CommentId cannot be empty");
    }

    public async Task CreateCommentAsync(string commentText, string episodeMediaId, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(commentText)
            || string.IsNullOrWhiteSpace(episodeMediaId)
            || userId == Guid.Empty)
            return;

        var comment = new Comment()
        {
            Id = Guid.NewGuid(),
            Text = commentText,
            ApplicationUserId = userId,
            EpisodeMediaId = episodeMediaId
        };

        await _repository.CreateCommentAsync(comment);
    }

    public async Task DeleteCommentAsync(Comment comment)
    {
        if (comment != null)
            await _repository.DeleteCommentAsync(comment);
    }
}
