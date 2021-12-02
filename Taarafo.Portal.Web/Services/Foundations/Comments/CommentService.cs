﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Taarafo.Portal.Web.Brokers.API;
using Taarafo.Portal.Web.Brokers.Loggings;
using Taarafo.Portal.Web.Models.Comments;

namespace Taarafo.Portal.Web.Services.Foundations.Comments
{
    public class CommentService : ICommentService
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

        public ValueTask<Comment> AddCommentAsync(Comment post)
            => throw new System.NotImplementedException();
    }
}
