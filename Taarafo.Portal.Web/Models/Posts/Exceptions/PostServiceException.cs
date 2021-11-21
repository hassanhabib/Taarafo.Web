// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Portal.Web.Models.Posts.Exceptions
{
    public class PostServiceException : Xeption
    {
        public PostServiceException(Xeption innerException)
            : base(message: "Post service error occurred, contact support.",
                  innerException)
        { }
    }
}
