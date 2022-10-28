// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Taarafo.Portal.Web.Models.PostImpressions.Exceptions
{
    public class PostImpressionDependencyException : Xeption
    {
        public PostImpressionDependencyException(Xeption innerException)
            : base(message: "Post impression dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
