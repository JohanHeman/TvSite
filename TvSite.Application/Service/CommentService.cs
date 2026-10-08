using Microsoft.EntityFrameworkCore;
using TvSite.Domain.Entities;
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
    public async Task<Comment?> GetCommentByIdAsync(Guid commentId)
    {
        return await _repository.GetCommentByIdAsync(commentId);
    }

    public async Task<IReadOnlyList<Comment>> GetCommentsByMediaIdAsync(string mediaId)
    {
        return await _repository.GetCommentsByMediaIdAsync(mediaId);
    }

    public async Task CreateCommentAsync(string commentText, string episodeMediaId, Guid userId)
    {
        await _repository.CreateCommentAsync(commentText, episodeMediaId, userId);
    }

    public async Task DeleteCommentAsync(Comment comment)
    {
        await _repository.DeleteCommentAsync(comment); 
    }
}
