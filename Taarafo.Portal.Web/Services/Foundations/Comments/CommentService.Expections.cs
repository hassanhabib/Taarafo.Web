// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
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
            catch (HttpResponseNotFoundException httpResponseNotFoundException)
            {
                var notFoundCommentException =
                    new NotFoundCommentException(httpResponseNotFoundException);

                throw CreateAndLogDependencyValidationException(notFoundCommentException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidCommentException =
                    new InvalidCommentException(
                        httpResponseBadRequestException,
                        httpResponseBadRequestException.Data);

                throw CreateAndLogDependencyValidationException(invalidCommentException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var invalidCommentException =
                    new InvalidCommentException(
                        httpResponseConflictException,
                        httpResponseConflictException.Data);

                throw CreateAndLogDependencyValidationException(invalidCommentException);
            }
            catch (HttpResponseLockedException httpLockedException)
            {
                var lockedCommentException =
                    new LockedCommentException(httpLockedException);

                throw CreateAndLogDependencyValidationException(lockedCommentException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedCommentDependencyException =
                    new FailedCommentDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedCommentDependencyException);
            }
            catch (Exception exception)
            {
                var failedCommentServiceException =
                    new FailedCommentServiceException(exception);

                throw CreateAndLogCommentServiceException(failedCommentServiceException);
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
            catch (HttpResponseException httpResponseException)
            {
                var failedCommentDependencyException =
                    new FailedCommentDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedCommentDependencyException);
            }
            catch (Exception exception)
            {
                var failedCommentServiceException =
                    new FailedCommentServiceException(exception);

                throw CreateAndLogCommentServiceException(failedCommentServiceException);
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

        private CommentDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var commentDependencyValidationException =
                new CommentDependencyValidationException(exception);

            this.loggingBroker.LogError(commentDependencyValidationException);

            return commentDependencyValidationException;
        }

        private CommentDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var commentDependencyException =
                new CommentDependencyException(exception);

            this.loggingBroker.LogError(commentDependencyException);

            return commentDependencyException;
        }

        private CommentServiceException CreateAndLogCommentServiceException(Xeption exception)
        {
            var commentServiceException =
                new CommentServiceException(exception);

            this.loggingBroker.LogError(commentServiceException);

            return commentServiceException;
        }
    }
}
