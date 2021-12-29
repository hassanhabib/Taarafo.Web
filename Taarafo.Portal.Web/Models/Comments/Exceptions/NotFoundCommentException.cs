// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Portal.Web.Models.Comments.Exceptions
{
    public class NotFoundCommentException : Xeption
    {
        public NotFoundCommentException(Exception innerException)
            : base(message: "Not found comment error occurred, please try again.",
                  innerException)
        { }
    }
}
