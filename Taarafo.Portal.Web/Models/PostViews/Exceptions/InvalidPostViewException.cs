// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace Taarafo.Portal.Web.Models.PostViews.Exceptions
{
    public class InvalidPostViewException : Xeption
    {
        public InvalidPostViewException()
            : base(message: "Invalid post view. correct the errors and try again.")
        { }

        public InvalidPostViewException(Exception innerException, IDictionary data)
            : base(message: "Invalid post error occurred. please correct the errors and try again.",
                  innerException,
                  data)
        { }
    }
}
