// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Taarafo.Portal.Web.Models.PostViews.Exceptions
{
    public class PostViewDependencyValidationException : Xeption
    {
        public PostViewDependencyValidationException(Xeption innerException)
            : base(message: "Post view dependency validation error occurred, please trya gain",
                  innerException)
        { }
    }
}
