// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Taarafo.Portal.Web.Models.Posts.Exceptions
{
    public class PostValidationException : Xeption
    {
        public PostValidationException(Xeption innerException)
            : base(message: "Post validation exception occured, please try again.", innerException)
        { }
    }
}
