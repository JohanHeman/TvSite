using System;
using System.Collections.Generic;
using System.Text;
using TvSite.Domain.Entities.Database;

namespace TvSite.Domain.Interfaces.Services
{
    public interface ICommentService
    {
        public Task<Comment?> GetCommentByIdAsync(Guid commentId);
        public Task<IReadOnlyList<Comment>> GetCommentsByEpisodeMediaIdAsync(string episodeMediaId);

        public Task CreateCommentAsync(string commentText, string episodeMediaId, Guid userId);
        public Task DeleteCommentAsync(Guid commentId);
    }
}
