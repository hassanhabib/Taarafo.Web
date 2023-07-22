// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Portal.Web.Models.PostImpressions.Exceptions
{
    public class FailedPostImpressionServiceException : Xeption
    {
        public FailedPostImpressionServiceException(Exception innerException)
            : base(message: "Post impression service error occurred, contact support.",
                  innerException)
        { }
    }
}
