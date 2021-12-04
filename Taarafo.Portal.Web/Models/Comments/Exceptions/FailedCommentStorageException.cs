// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Portal.Web.Models.Comments.Exceptions
{
    public class FailedCommentStorageException : Xeption
    {
        public FailedCommentStorageException(Exception innerException)
            : base(message: "Failed comment storage error occurred, contact support.", innerException)
        { }
    }
}
