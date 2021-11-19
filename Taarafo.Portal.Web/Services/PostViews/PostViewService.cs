// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Brokers.Loggings;
using Taarafo.Portal.Web.Models.Posts;
using Taarafo.Portal.Web.Models.PostViews;
using Taarafo.Portal.Web.Services.Foundations.Posts;

namespace Taarafo.Portal.Web.Services.PostViews
{
    public partial class PostViewService : IPostViewService
    {
        private readonly IPostService postService;
        private readonly ILoggingBroker loggingBroker;

        public PostViewService(
            IPostService postService,
            ILoggingBroker loggingBroker)
        {
            this.postService = postService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<List<PostView>> RetrieveAllPostViewsAsync() =>
        TryCatch(async () =>
        {
            List<Post> posts =
                await this.postService.RetrieveAllPostsAsync();

            return posts.Select(AsPostView).ToList();
        });

        private static Func<Post, PostView> AsPostView =>
            post => new PostView
            {
                Content = post.Content
            };
    }
}
