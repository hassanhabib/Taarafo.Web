// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Taarafo.Portal.Web.Models.PostImpressions.Exceptions
{
    public class PostImpressionServiceException : Xeption
    {
        public PostImpressionServiceException(Xeption innerException)
            : base(message: "Post impression service error occurred, contact support.",
                  innerException)
        { }
    }
}
