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
