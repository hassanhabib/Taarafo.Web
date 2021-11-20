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
    public partial interface IApiBroker
    {
        ValueTask<Post> PostPostAsync(Post post);
        ValueTask<List<Post>> GetAllPostsAsync();
        ValueTask<Post> DeletePostByIdAsync(Guid postId);
    }
}
