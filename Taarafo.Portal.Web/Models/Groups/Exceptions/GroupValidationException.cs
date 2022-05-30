// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Portal.Web.Models.Groups.Exceptions
{
    public class GroupValidationException : Xeption
    {
        public GroupValidationException(Exception innerException)
            : base(message: "Group validation error occurred, please try again.",
                innerException)
        { }
    }
}