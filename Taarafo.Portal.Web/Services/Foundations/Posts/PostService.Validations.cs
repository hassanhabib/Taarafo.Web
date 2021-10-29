// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Taarafo.Portal.Web.Models.Posts;
using Taarafo.Portal.Web.Models.Posts.Exceptions;

namespace Taarafo.Portal.Web.Services.Foundations.Posts
{
    public partial class PostService
    {
        private static void ValidatePost(Post post)
        {
            ValidatePostIsNotNull(post);
        }

        private static void ValidatePostIsNotNull(Post post)
        {
            if (post is null)
            {
                throw new NullPostException();
            }
        }
    }
}
