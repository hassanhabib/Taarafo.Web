﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Taarafo.Portal.Web.Models.Comments.Exceptions
{
    public class CommentValidationException : Xeption
    {
        public CommentValidationException(Xeption innerException)
            : base(message: "Comment validation errors occurred, please try again.",
                  innerException)
        { }
    }
}
