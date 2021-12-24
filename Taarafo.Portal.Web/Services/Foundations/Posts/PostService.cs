// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Brokers.Apis;
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

        public ValueTask<Post> AddPostAsync(Post post) =>
        TryCatch(async () =>
        {
            ValidatePostOnAdd(post);

            return await this.apiBroker.PostPostAsync(post);
        });

        public ValueTask<List<Post>> RetrieveAllPostsAsync() =>
        TryCatch(async () => await apiBroker.GetAllPostsAsync());

        public ValueTask<Post> RemovePostByIdAsync(Guid postId) =>
        TryCatch(async () =>
        {
            ValidatePostId(postId);

            return await this.apiBroker.DeletePostByIdAsync(postId);
        });
    }
}
