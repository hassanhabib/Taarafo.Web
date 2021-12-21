// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Taarafo.Portal.Web.Models.Comments.Exceptions
{
    public class CommentServiceException : Xeption
    {
        public CommentServiceException(Xeption innerException)
            : base(message: "Comment service error occurred, contact support.",
                  innerException)
        { }
    }
}
