// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Taarafo.Portal.Web.Models.PostImpressions.Exceptions
{
    public class InvalidPostImpressionException : Xeption
    {
        public InvalidPostImpressionException()
            : base(message: "Invalid post impression error ocurred, correct the errors and try again.")
        { }
    }
}
