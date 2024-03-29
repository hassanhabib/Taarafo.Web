﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Portal.Web.Models.Posts.Exceptions
{
    public class PostValidationException : Xeption
    {
        public PostValidationException(Exception innerException)
            : base(message: "Post validation error ocurred, please try again.",
                  innerException)
        { }
    }
}
