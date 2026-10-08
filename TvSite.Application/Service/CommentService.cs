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
            throw new ArgumentException("EpisodeMediaId cannot be null or whitespace");
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
        // Domain entity will throw if comment text is null or whitespace
        if (string.IsNullOrWhiteSpace(commentText) || userId == Guid.Empty)
            return;

        if (string.IsNullOrWhiteSpace(episodeMediaId))
            throw new ArgumentException("EpisodeMediaId cannot be null or whitespace");

        var comment = new Comment()
        {
            Id = Guid.NewGuid(),
            Text = commentText,
            ApplicationUserId = userId,
            EpisodeMediaId = episodeMediaId
        };

        await _repository.CreateCommentAsync(comment);
    }

    public async Task DeleteCommentAsync(Guid commentId)
    {
        if (commentId == Guid.Empty)
            throw new ArgumentException("Comment id cannot be empty");

        var comment = await _repository.GetCommentByIdAsync(commentId);

        if (comment == null)
            throw new NullReferenceException("Comment does not exist");

        await _repository.DeleteCommentAsync(comment);
    }
}
