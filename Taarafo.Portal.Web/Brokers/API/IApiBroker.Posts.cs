// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.Posts;

namespace Taarafo.Portal.Web.Brokers.API
{
    public partial interface IApiBroker
    {
        ValueTask<List<Post>> GetAllPosts();
        ValueTask<Post> PostPostAsync(Post post);
    }
}
