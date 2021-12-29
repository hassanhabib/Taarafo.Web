// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Portal.Web.Models.Comments.Exceptions
{
    public class LockedCommentException : Xeption
    {
        public LockedCommentException(Exception innerException)
            : base(message: "Locked comment error occurred, please try again later.",
                  innerException)
        { }
    }
}
