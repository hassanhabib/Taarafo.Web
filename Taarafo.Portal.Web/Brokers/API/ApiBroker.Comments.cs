// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.Comments;

namespace Taarafo.Portal.Web.Brokers.API
{
    public partial class ApiBroker
    {
        private const string commentsRelativeUrl = "api/comments";

        public async ValueTask<Comment> PostCommentAsync(Comment comment) =>
            await this.PostAsync(commentsRelativeUrl, comment);
    }
}
