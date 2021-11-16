// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Portal.Web.Models.Posts.Exceptions
{
    public class NotFoundPostException : Xeption
    {
        public NotFoundPostException(Exception innerException)
            : base(message: "Not found post error occurred, please try again.",
                  innerException)
        { }
    }
}
