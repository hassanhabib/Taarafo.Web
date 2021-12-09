// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using Taarafo.Portal.Web.Models.Posts;
using Taarafo.Portal.Web.Models.Posts.Exceptions;
using Xeptions;

namespace Taarafo.Portal.Web.Services.Foundations.Posts
{
    public partial class PostService
    {
        private delegate ValueTask<Post> ReturningPostFunction();
        private delegate ValueTask<List<Post>> ReturningPostsFunction();

        private async ValueTask<Post> TryCatch(ReturningPostFunction returningPostFunction)
        {
            try
            {
                return await returningPostFunction();
            }
            catch (InvalidPostException invalidPostException)
            {
                throw CreateAndLogValidationException(invalidPostException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedPostDependencyException =
                    new FailedPostDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedPostDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedPostDependencyException =
                    new FailedPostDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedPostDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedPostDependencyException =
                    new FailedPostDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedPostDependencyException);
            }
            catch (HttpResponseNotFoundException httpResponseNotFoundException)
            {
                var notFoundPostException =
                    new NotFoundPostException(httpResponseNotFoundException);

                throw CreateAndLogDependencyValidationException(notFoundPostException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidPostException =
                    new InvalidPostException(
                        httpResponseBadRequestException,
                        httpResponseBadRequestException.Data);

                throw CreateAndLogDependencyValidationException(invalidPostException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var invalidPostException =
                    new InvalidPostException(
                        httpResponseConflictException,
                        httpResponseConflictException.Data);

                throw CreateAndLogDependencyValidationException(invalidPostException);
            }
            catch (HttpResponseLockedException httpLockedException)
            {
                var lockedPostException =
                    new LockedPostException(httpLockedException);

                throw CreateAndLogDependencyValidationException(lockedPostException);
            }
            //catch (HttpResponseException httpResponseException)
            //{
            //    var failedPostDependencyException =
            //        new FailedPostDependencyException(httpResponseException);

            //    throw CreateAndLogDependencyException(failedPostDependencyException);
            //}
            catch (Exception exception)
            {
                var failedPostServiceException =
                    new FailedPostServiceException(exception);

                throw CreateAndLogPostServiceException(failedPostServiceException);
            }
        }

        private async ValueTask<List<Post>> TryCatch(ReturningPostsFunction returningPostsFunction)
        {
            try
            {
                return await returningPostsFunction();
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedPostDependencyException =
                    new FailedPostDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedPostDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedPostDependencyException =
                    new FailedPostDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedPostDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedPostDependencyException =
                    new FailedPostDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedPostDependencyException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedPostDependencyException =
                    new FailedPostDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedPostDependencyException);
            }
            catch (Exception exception)
            {
                var failedPostServiceException =
                    new FailedPostServiceException(exception);

                throw CreateAndLogPostServiceException(failedPostServiceException);
            }
        }

        private PostDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var postDependencyValidationException =
                new PostDependencyValidationException(exception);

            this.loggingBroker.LogError(postDependencyValidationException);

            return postDependencyValidationException;
        }

        private PostDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var postDependencyException =
                new PostDependencyException(exception);

            this.loggingBroker.LogCritical(postDependencyException);

            return postDependencyException;
        }

        private PostValidationException CreateAndLogValidationException(
            Xeption exception)
        {
            var postValidationException =
                new PostValidationException(exception);

            this.loggingBroker.LogError(postValidationException);

            return postValidationException;
        }

        private PostDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var postDependencyException =
                new PostDependencyException(exception);

            this.loggingBroker.LogError(postDependencyException);

            return postDependencyException;
        }

        private PostServiceException CreateAndLogPostServiceException(Xeption exception)
        {
            var postServiceException =
                new PostServiceException(exception);

            this.loggingBroker.LogError(postServiceException);

            return postServiceException;
        }
    }
}
