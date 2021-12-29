// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Brokers.Apis;
using Taarafo.Portal.Web.Brokers.Loggings;
using Taarafo.Portal.Web.Models.Comments;

namespace Taarafo.Portal.Web.Services.Foundations.Comments
{
    public partial class CommentService : ICommentService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public CommentService(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Comment> AddCommentAsync(Comment comment) =>
        TryCatch(async () =>
        {
            ValidateCommentOnAdd(comment);

            return await this.apiBroker.PostCommentAsync(comment);
        });

        public ValueTask<List<Comment>> RetrieveAllCommentsAsync() =>
        TryCatch(async () => await this.apiBroker.GetAllCommentsAsync());

        public ValueTask<Comment> RetrieveCommentByIdAsync(Guid commentId) =>
        TryCatch(async () =>
        {
            ValidateCommentId(commentId);

            return await this.apiBroker.GetCommentByIdAsync(commentId);
        });

        public ValueTask<Comment> ModifyCommentAsync(Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}
