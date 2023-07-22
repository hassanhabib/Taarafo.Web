// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using Taarafo.Portal.Web.Models.PostImpressions;
using Taarafo.Portal.Web.Models.PostImpressions.Exceptions;
using Xeptions;

namespace Taarafo.Portal.Web.Services.Foundations.PostImpressions
{
    public partial class PostImpressionService
    {
        private delegate ValueTask<PostImpression> ReturningPostImpressionFunction();

        private async ValueTask<PostImpression> TryCatch(ReturningPostImpressionFunction returningPostImpressionFunction)
        {
            try
            {
                return await returningPostImpressionFunction();
            }
            catch (NullPostImpressionException nullPostImpressionException)
            {
                throw CreateAndLogValidationException(nullPostImpressionException);
            }
            catch (InvalidPostImpressionException invalidPostImpressionException)
            {
                throw CreateAndLogValidationException(invalidPostImpressionException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedPostImpressionDependencyException =
                    new FailedPostImpressionDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedPostImpressionDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedPostImpressionDependencyException =
                    new FailedPostImpressionDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedPostImpressionDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedPostImpressionDependencyException =
                    new FailedPostImpressionDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedPostImpressionDependencyException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidPostImpressionException =
                    new InvalidPostImpressionException(
                        httpResponseBadRequestException,
                        httpResponseBadRequestException.Data);

                throw CreateAndLogDependencyValidationException(invalidPostImpressionException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var invalidPostImpressionException =
                    new InvalidPostImpressionException(
                        httpResponseConflictException,
                        httpResponseConflictException.Data);

                throw CreateAndLogDependencyValidationException(invalidPostImpressionException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedPostImpressionDependencyException =
                    new FailedPostImpressionDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedPostImpressionDependencyException);
            }
            catch (Exception exception)
            {
                var failedPostImpressionServiceException =
                    new FailedPostImpressionServiceException(exception);

                throw CreateAndLogPostServiceException(failedPostImpressionServiceException);
            }
        }

        private PostImpressionValidationException CreateAndLogValidationException(
            Xeption exception)
        {
            var postImpressionValidationException =
                new PostImpressionValidationException(exception);

            this.loggingBroker.LogError(postImpressionValidationException);

            return postImpressionValidationException;
        }

        private PostImpressionDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var postImpressionDependencyException =
                new PostImpressionDependencyException(exception);

            this.loggingBroker.LogCritical(postImpressionDependencyException);

            return postImpressionDependencyException;
        }

        private PostImpressionDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var postImpressionDependencyValidationException =
                new PostImpressionDependencyValidationException(exception);

            this.loggingBroker.LogError(postImpressionDependencyValidationException);

            return postImpressionDependencyValidationException;
        }

        private PostImpressionDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var postImpressionDependencyException =
                new PostImpressionDependencyException(exception);

            this.loggingBroker.LogError(postImpressionDependencyException);

            return postImpressionDependencyException;
        }

        private PostImpressionServiceException CreateAndLogPostServiceException(Xeption exception)
        {
            var postImpressionServiceException =
                new PostImpressionServiceException(exception);

            this.loggingBroker.LogError(postImpressionServiceException);

            return postImpressionServiceException;
        }
    }
}
