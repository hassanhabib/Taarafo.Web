// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Portal.Web.Models.Comments.Exceptions
{
    public class CommentValidationException : Xeption
    {
        public CommentValidationException(Exception innerException)
            : base(message: "Comment validation error ocurred, please try again.",
                  innerException)
        { }
    }
}
