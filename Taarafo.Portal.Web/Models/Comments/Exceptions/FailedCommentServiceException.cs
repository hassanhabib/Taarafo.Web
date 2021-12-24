// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Portal.Web.Models.Comments.Exceptions
{
    public class FailedCommentServiceException : Xeption
    {
        public FailedCommentServiceException(Exception innerException)
            : base(message: "Failed comment service error occurred, contact support.",
                  innerException)
        { }
    }
}
