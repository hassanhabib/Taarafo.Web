// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Taarafo.Portal.Web.Models.Groups.Exceptions
{
    public class InvalidGroupException : Xeption
    {
        public InvalidGroupException()
            : base(message: "Invalid group. Please correct the error and try again.")
        { }
    }
}
