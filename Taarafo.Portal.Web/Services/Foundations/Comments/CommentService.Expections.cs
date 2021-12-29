// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using Taarafo.Portal.Web.Models.Comments;
using Taarafo.Portal.Web.Models.Comments.Exceptions;
using Xeptions;

namespace Taarafo.Portal.Web.Services.Foundations.Comments
{
    public partial class CommentService
    {
        private delegate ValueTask<Comment> ReturningCommentFunction();
        private delegate ValueTask<List<Comment>> ReturningCommentsFunction();

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
            catch (HttpRequestException httpRequestException)
            {
                var failedCommentDependencyException =
                    new FailedCommentDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedCommentDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedCommentDependencyException =
                    new FailedCommentDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedCommentDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedCommentDependencyException =
                    new FailedCommentDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedCommentDependencyException);
            }
        }

        private async ValueTask<List<Comment>> TryCatch(ReturningCommentsFunction returningCommentsFunction)
        {
            try
            {
                return await returningCommentsFunction();
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedCommentDependencyException =
                    new FailedCommentDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedCommentDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedCommentDependencyException =
                    new FailedCommentDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedCommentDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedCommentDependencyException =
                    new FailedCommentDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedCommentDependencyException);
            }
        }


        private CommentValidationException CreateAndLogValidationException(
            Xeption exception)
        {
            var commentValidationException =
                new CommentValidationException(exception);

            this.loggingBroker.LogError(commentValidationException);

            return commentValidationException;
        }

        private CommentDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var commentDependencyException =
                new CommentDependencyException(exception);

            this.loggingBroker.LogCritical(commentDependencyException);

            return commentDependencyException;
        }
    }
}
