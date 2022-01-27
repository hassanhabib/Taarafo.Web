// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.CommentViews;
using Taarafo.Portal.Web.Models.CommentViews.Exceptions;
using Xeptions;

namespace Taarafo.Portal.Web.Services.Views.CommentViews
{
    public partial class CommentViewService
    {
        private delegate ValueTask<CommentView> ReturningCommentViewFunction();

        private async ValueTask<CommentView> TryCatch(ReturningCommentViewFunction returningCommentViewFunction)
        {
            try
            {
                return await returningCommentViewFunction();
            }
            catch (NullCommentViewException nullCommentViewException)
            {
                throw CreateAndLogValidationException(nullCommentViewException);
            }
        }

        private Xeption CreateAndLogValidationException(NullCommentViewException exception)
        {

            var commentViewValidationException = new CommentViewValidationException(exception);
            this.loggingBroker.LogError(commentViewValidationException);

            return commentViewValidationException;
        }
    }
}
