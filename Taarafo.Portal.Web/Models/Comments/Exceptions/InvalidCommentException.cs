// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace Taarafo.Portal.Web.Models.Comments.Exceptions
{
    public class InvalidCommentException : Xeption
    {
        public InvalidCommentException()
            : base(message: "Invalid comment. Correct the errors and try again.")
        { }

        public InvalidCommentException(Exception innerException, IDictionary data)
            : base(message: "Invalid comment error occurred. Please correct the errors and try again.",
                  innerException,
                  data)
        { }
    }
}
