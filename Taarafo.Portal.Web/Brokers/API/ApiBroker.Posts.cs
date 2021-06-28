// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.Posts;

namespace Taarafo.Portal.Web.Brokers.API
{
    public partial class ApiBroker
    {
        private const string relativeUrl = "api/posts";

        public ValueTask<Post> GetAllPosts() =>
            this.GetAsync<Post>(relativeUrl);
    }
}
