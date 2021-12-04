// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Portal.Web.Models.Comments.Exceptions
{
    public class CommentDependencyException : Xeption
    {
        public CommentDependencyException(Exception innerException) :
            base(message: "Comment dependency error occurred, contact support.", innerException)
        { }
    }
}
