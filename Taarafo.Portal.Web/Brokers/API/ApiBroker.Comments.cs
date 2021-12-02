// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.Comments;

namespace Taarafo.Portal.Web.Brokers.API
{
    public partial class ApiBroker
    {
        private const string commentsRelativeUrl = "api/comments";

        public async ValueTask<Comment> PostCommentAsync(Comment comment) =>
                    await this.PostAsync(commentsRelativeUrl, comment);

        public async ValueTask<List<Comment>> GetAllCommentsAsync() =>
            await this.GetAsync<List<Comment>>(commentsRelativeUrl);

        public async ValueTask<Comment> GetCommentByIdAsync(Guid commentId) =>
            await this.GetAsync<Comment>($"{commentsRelativeUrl}/{commentId}");

        public async ValueTask<Comment> PutCommentAsync(Comment comment) =>
            await this.PutAsync(commentsRelativeUrl, comment);

        public async ValueTask<Comment> DeleteCommentByIdAsync(Guid commentId) =>
            await this.DeleteAsync<Comment>($"{commentsRelativeUrl}/{commentId}");
    }
}
