using System;
using System.Collections.Generic;
using System.Text;
using TvSite.Domain.Entities;

namespace TvSite.Tests.UnitTests.ClassData
{

    public class CommentInvalidTextClassData : TheoryData<Comment>
    {
        private string userId = Guid.NewGuid().ToString();

        public CommentInvalidTextClassData()
        {
            Add(new Comment()
            {
                Id = Guid.NewGuid().ToString(),
                Text = string.Empty,
                ApplicationUserId = userId,
                MediaId = "12345"
            });

            Add(new Comment()
            {
                Id = Guid.NewGuid().ToString(),
                Text = null,
                ApplicationUserId = userId,
                MediaId = "12345"
            });

            Add(new Comment()
            {
                Id = Guid.NewGuid().ToString(),
                Text = " ",
                ApplicationUserId = userId,
                MediaId = "12345"
            });

            Add(new Comment()
            {
                Id = Guid.NewGuid().ToString(),
                Text = "    ",
                ApplicationUserId = userId,
                MediaId = "12345"
            });
        }

    }
}
