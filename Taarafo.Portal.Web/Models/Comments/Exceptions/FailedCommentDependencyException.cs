// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Portal.Web.Models.Comments.Exceptions
{
    public class FailedCommentDependencyException : Xeption
    {
        public FailedCommentDependencyException(Exception innerException)
            : base(message: "Failed comment dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
