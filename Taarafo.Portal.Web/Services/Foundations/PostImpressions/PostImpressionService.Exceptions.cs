// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.PostImpressions;
using Taarafo.Portal.Web.Models.PostImpressions.Exceptions;
using Taarafo.Portal.Web.Models.Posts;
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
        }

        private PostImpressionValidationException CreateAndLogValidationException(
            Xeption exception)
        {
            var postImpressionValidationException =
                new PostImpressionValidationException(exception);

            this.loggingBroker.LogError(postImpressionValidationException);

            return postImpressionValidationException;
        }
    }
}
