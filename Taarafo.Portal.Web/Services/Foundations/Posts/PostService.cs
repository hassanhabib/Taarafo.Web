// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Brokers.API;
using Taarafo.Portal.Web.Brokers.Loggings;
using Taarafo.Portal.Web.Models.Posts;

namespace Taarafo.Portal.Web.Services.Foundations.Posts
{
    public partial class PostService : IPostService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public PostService(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<List<Post>> RetrieveAllPostsAsync()
        {
            throw new NotImplementedException();
        }

        public ValueTask<Post> RemovePostByIdAsync(Guid postId) =>
        TryCatch(async () =>
        {
            ValidatePostId(postId);

            return await this.apiBroker.DeletePostByIdAsync(postId);
        });
    }
}
