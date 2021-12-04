﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Portal.Web.Models.Comments.Exceptions
{
    public class AlreadyExistsCommentException : Xeption
    {
        public AlreadyExistsCommentException(Exception innerException)
            : base(message: "Comment with the same id already exists.", innerException)
        { }
    }
}
