// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Portal.Web.Models.Groups.Exceptions
{
    public class FailedGroupDependencyException : Xeption
    {
        public FailedGroupDependencyException(Exception innerException)
            : base(message: "Failed group dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
