// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Taarafo.Portal.Web.Models.CommentViews;
using Taarafo.Portal.Web.Models.CommentViews.Exceptions;

namespace Taarafo.Portal.Web.Services.Views.CommentViews
{
    public partial class CommentViewService 
    {
        private static void ValidateCommentView(CommentView commentView)
        {
            ValidateCommentViewIsNotNull(commentView);
        }

        private static void ValidateCommentViewIsNotNull(CommentView commentView)
        {
            if (commentView is null)
            {
                throw new NullCommentViewException();
            }
        }
    }
}
