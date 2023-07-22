// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace Taarafo.Portal.Web.Models.PostImpressions.Exceptions
{
    public class InvalidPostImpressionException : Xeption
    {
        public InvalidPostImpressionException()
            : base(message: "Invalid post impression error ocurred, correct the errors and try again.")
        { }

        public InvalidPostImpressionException(Exception innerException, IDictionary data)
            : base(message: "Invalid post impression error occurred, please correct the errors and try again.",
                  innerException,
                  data)
                { }
    }
}
