// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Portal.Web.Models.Comments.Exceptions
{
    public class CommentServiceException : Xeption
    {
        public CommentServiceException(Exception innerException)
            : base(message: "Comment service error occurred, contact support.", innerException) { }
    }
}
