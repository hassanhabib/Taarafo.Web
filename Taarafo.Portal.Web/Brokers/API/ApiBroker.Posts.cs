// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.Posts;

namespace Taarafo.Portal.Web.Brokers.API
{
    public partial class ApiBroker
    {
        private const string relativeUrl = "api/posts";

        public async ValueTask<List<Post>> GetAllPosts() =>
            await this.GetAsync<List<Post>>(relativeUrl);
    }
}
