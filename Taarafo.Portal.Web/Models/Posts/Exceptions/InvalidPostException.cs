// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace Taarafo.Portal.Web.Models.Posts.Exceptions
{
    public class InvalidPostException : Xeption
    {
        public InvalidPostException()
            : base(message: "Invalid post. correct the errors and try again.")
        { }

        public InvalidPostException(Exception innerException, IDictionary data)
            : base(message: "Invalid post error occurred. please correct the errors and try again.",
                  innerException,
                  data)
        { }
    }
}
