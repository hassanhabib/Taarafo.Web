// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Portal.Web.Models.PostImpressions.Exceptions
{
    public class PostImpressionValidationException : Xeption
    {
        public PostImpressionValidationException(Exception innerException)
            : base(message: "Post impression validation error ocurred, please try again.",
                  innerException)
        { }
    }
}
