using RESTFulSense.Exceptions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.Posts;
using Taarafo.Portal.Web.Models.Posts.Exceptions;
using Xeptions;

namespace Taarafo.Portal.Web.Services.Foundations.Posts
{
    public partial class PostService
    {
        private delegate ValueTask<Post> ReturningPostFunction();

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

                throw CreateAndLogCritialDependencyException(failedPostDependencyException);
            }
            catch(HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedPostDependencyException = 
                    new FailedPostDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCritialDependencyException(failedPostDependencyException);
            }
            catch(HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedPostDependencyException =
                    new FailedPostDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCritialDependencyException(failedPostDependencyException);
            }
        }

        private PostDependencyException CreateAndLogCritialDependencyException(Xeption exception)
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
    }
}
