// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Taarafo.Portal.Web.Models.Comments.Exceptions
{
    public class InvalidCommentException : Xeption
    {
        public InvalidCommentException()
            : base(message: "Invalid comment. Please correct the errors and try again.")
        { }
    }
}
