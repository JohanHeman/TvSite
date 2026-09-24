using System;
using System.Collections.Generic;
using System.Text;
using TvSite.Domain.Entities;

namespace TvSite.Domain.Interfaces.Services
{
    public interface ICommentService
    {
        public Task<Comment?> GetCommentByIdAsync(string commentId);
        public Task<IReadOnlyList<Comment>> GetCommentsByMediaIdAsync(string mediaId);

        public Task CreateCommentAsync(Comment comment);
        public Task DeleteCommentAsync(Comment comment);
    }
}
