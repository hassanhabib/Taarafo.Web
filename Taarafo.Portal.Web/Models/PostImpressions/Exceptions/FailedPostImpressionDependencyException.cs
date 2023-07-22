// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Portal.Web.Models.PostImpressions.Exceptions
{
    public class FailedPostImpressionDependencyException : Xeption
    {
        public FailedPostImpressionDependencyException(Exception innerException)
            : base(message: "Failed post impression dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
