// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Taarafo.Portal.Web.Models.PostViews.Exceptions
{
    public class PostViewValidationException : Xeption
    {
        public PostViewValidationException(Xeption innerException)
            : base(message: "Post view validation error ocurred, please try again.",
                  innerException)
        { }
    }
}
