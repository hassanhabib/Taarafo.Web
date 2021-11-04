// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.Posts;

namespace Taarafo.Portal.Web.Services.Foundations.Posts
{
    public interface IPostService
    {
        ValueTask<Post> RemovePostByIdAsync(Guid postId);
    }
}
