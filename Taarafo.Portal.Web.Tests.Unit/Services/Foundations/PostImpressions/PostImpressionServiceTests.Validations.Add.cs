// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Moq;
using Taarafo.Portal.Web.Models.PostImpressions;
using Taarafo.Portal.Web.Models.PostImpressions.Exceptions;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Foundations.PostImpressions
{
    public partial class PostImpressionServiceTests
    {
        [Fact]
        public async void ShouldThrowValidationExceptionOnAddWhenPostImpressionIsNullAndLogItAsync()
        {
            // given
            PostImpression nullPostImpression = null;
            var nullPostImpressionException = new NullPostImpressionException();

            var expectedPostImpressionValidationException =
                new PostImpressionValidationException(nullPostImpressionException);

            // when
            ValueTask<PostImpression> createPostImpressionTask =
                this.postImpressionService.AddPostImpressionAsync(nullPostImpression);

            // then
            await Assert.ThrowsAsync<PostImpressionValidationException>(() =>
                createPostImpressionTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedPostImpressionValidationException))),
                    Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostPostImpressionAsync(It.IsAny<PostImpression>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
