// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.Posts;

namespace Taarafo.Portal.Web.Brokers.API
{
    public partial class ApiBroker
    {
        private const string postsRelativeUrl = "api/posts";

        public async ValueTask<Post> PostPostAsync(Post post) =>
            await this.PostAsync(postsRelativeUrl, post);

        public async ValueTask<List<Post>> GetAllPostsAsync() =>
            await this.GetAsync<List<Post>>(postsRelativeUrl);

        public async ValueTask<Post> DeletePostByIdAsync(Guid postId) =>
            await this.DeleteAsync<Post>($"{postsRelativeUrl}/{postId}");
    }
}
