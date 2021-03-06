// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Web.Models.Groups.Exceptions
{
    public class NotFoundGroupException : Xeption
    {
        public NotFoundGroupException(Guid groupId)
            : base(message: $"Could not find the group with id: {groupId}.")
        { }
    }
}
