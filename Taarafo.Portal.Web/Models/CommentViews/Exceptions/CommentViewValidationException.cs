// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Taarafo.Portal.Web.Models.CommentViews.Exceptions
{
    public class CommentViewValidationException : Xeption
    {
        public CommentViewValidationException(Xeption innerException)
            : base(message: "Comment validation error occured, please, try again.",
                      innerException: innerException)
        { }
    }
}
