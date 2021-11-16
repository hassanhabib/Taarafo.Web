// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Portal.Web.Models.Posts.Exceptions
{
    public class FailedPostDependencyException : Xeption
    {
        public FailedPostDependencyException(Exception innerException)
            : base(message: "Failed post dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
