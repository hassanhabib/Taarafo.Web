// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Taarafo.Portal.Web.Models.Comments.Exceptions
{
    public class NullCommentException : Xeption
    {
        public NullCommentException() : base(message: "The comment is null.") { }
    }
}
