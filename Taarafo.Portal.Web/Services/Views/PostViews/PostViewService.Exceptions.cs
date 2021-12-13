// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.Posts.Exceptions;
using Taarafo.Portal.Web.Models.PostViews;
using Taarafo.Portal.Web.Models.PostViews.Exceptions;
using Xeptions;

namespace Taarafo.Portal.Web.Services.Views.PostViews
{
    public partial class PostViewService
    {
        private delegate ValueTask<List<PostView>> ReturningPostViewsFunction();
        private delegate ValueTask<PostView> ReturningPostViewFunction();

        private async ValueTask<List<PostView>> TryCatch(ReturningPostViewsFunction returningPostViewsFunction)
        {
            try
            {
                return await returningPostViewsFunction();
            }
            catch (PostDependencyException postDependencyException)
            {
                throw CreateAndLogDependencyException(postDependencyException);
            }
            catch (PostServiceException postServiceException)
            {
                throw CreateAndLogDependencyException(postServiceException);
            }
            catch (Exception serviceException)
            {
                var failedPostViewServiceException =
                    new FailedPostViewServiceException(serviceException);

                throw CreateAndLogServiceException(failedPostViewServiceException);
            }
        }

        private async ValueTask<PostView> TryCatch(ReturningPostViewFunction returningPostViewFunction)
        {
            try
            {
                return await returningPostViewFunction();
            }
            catch (NullPostViewException nullPostViewException)
            {
                throw CreateAndLogValidationException(nullPostViewException);
            }
            catch (InvalidPostViewException invalidPostViewException)
            {
                throw CreateAndLogValidationException(invalidPostViewException);
            }
            catch (PostValidationException postValidationException)
            {
                throw CreateAndLogDependencyValidationException(postValidationException);
            }
            catch (PostDependencyValidationException postDependencyValidationException)
            {
                throw CreateAndLogDependencyValidationException(postDependencyValidationException);
            }
            catch(PostDependencyException postDependencyException)
            {
                throw CreateAndLogDependencyException(postDependencyException);
            }
            catch (PostServiceException postServiceException)
            {
                throw CreateAndLogDependencyException(postServiceException);
            }
            catch(Exception exception)
            {
                var failedPostViewServiceException =
                    new FailedPostViewServiceException(exception);

                throw CreateAndLogServiceException(failedPostViewServiceException);
            }
        }

        private PostViewValidationException CreateAndLogValidationException(Xeption innerException)
        {
            var postViewValidationException = new PostViewValidationException(innerException);
            this.loggingBroker.LogError(postViewValidationException);

            return postViewValidationException;
        }

        private PostViewDependencyException CreateAndLogDependencyException(Xeption innerException)
        {
            var postViewDependencyException = new PostViewDependencyException(innerException);
            this.loggingBroker.LogError(postViewDependencyException);

            return postViewDependencyException;
        }

        private PostViewDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var postViewDependencyValidationException =
                new PostViewDependencyValidationException(exception.InnerException as Xeption);

            this.loggingBroker.LogError(postViewDependencyValidationException);

            return postViewDependencyValidationException;
        }

        private Exception CreateAndLogServiceException(Xeption innerException)
        {
            var postViewServiceException = new PostViewServiceException(innerException);
            this.loggingBroker.LogError(postViewServiceException);

            return postViewServiceException;
        }
    }
}
