// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Portal.Web.Models.PostViews.Exceptions
{
    public class PostViewDependencyException : Xeption
    {
        public PostViewDependencyException(Exception innerException)
            : base(message: "Post view dependency error occurred, please contact support.", innerException)
        { }
    }
}
