// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Taarafo.Portal.Web.Models.Comments.Exceptions
{
    public class CommentDependencyValidationException : Xeption
    {
        public CommentDependencyValidationException(Xeption innerException)
            : base(message: "Comment dependency validation occurred, please try again.", innerException)
        { }
    }
}
