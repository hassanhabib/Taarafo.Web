// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Portal.Web.Models.PostViews.Exceptions
{
    public class FailedPostViewServiceException : Xeption
    {
        public FailedPostViewServiceException(Exception innerException)
            : base(message: "Failed post view service error occurred, please contact support.", innerException)
        { }
    }
}
