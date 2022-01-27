// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Taarafo.Portal.Web.Models.CommentViews.Exceptions
{
    public class NullCommentViewException : Xeption
    {
        public NullCommentViewException() 
            : base(message: "Null comment error occured.") { }
    }
}
