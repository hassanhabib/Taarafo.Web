// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.Comments;
using Taarafo.Portal.Web.Models.Comments.Exceptions;
using Xeptions;

namespace Taarafo.Portal.Web.Services.Foundations.Comments
{
    public partial class CommentService
    {
        private delegate ValueTask<Comment> ReturningCommentFunction();

        private async ValueTask<Comment> TryCatch(ReturningCommentFunction returningCommentFunction)
        {
            try
            {
                return await returningCommentFunction();
            }
            catch (NullCommentException nullCommentException)
            {
                throw CreateAndLogValidationException(nullCommentException);
            }
            catch (InvalidCommentException invalidCommentException)
            {
                throw CreateAndLogValidationException(invalidCommentException);
            }
            catch (FailedCommentStorageException failedCommentStorageException)
            {
                throw CreateAndLogCriticalDependencyException(failedCommentStorageException);
            }
        }

        private CommentValidationException CreateAndLogValidationException(Xeption exception)
        {
            var commentValidationException = new CommentValidationException(exception);
            this.loggingBroker.LogError(commentValidationException);

            return commentValidationException;
        }

        private CommentDependencyException CreateAndLogCriticalDependencyException(
            Xeption exception)
        {
            var commentDependencyException = new CommentDependencyException(exception);
            this.loggingBroker.LogCritical(commentDependencyException);

            return commentDependencyException;
        }
    }
}
