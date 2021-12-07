// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using Taarafo.Portal.Web.Models.PostViews;
using Taarafo.Portal.Web.Models.PostViews.Exceptions;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.PostViews
{
    public partial class PostViewServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRemovePostViewByIdIfPostViewIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidPostViewId = Guid.Empty;

            var invalidPostViewException =
                new InvalidPostViewException();

            invalidPostViewException.AddData(
              key: nameof(PostView.Id),
              values: "Id is required");

            var expectedPostViewValidationException =
                new PostViewValidationException(invalidPostViewException);

            // when
            ValueTask<PostView> removePostViewByIdTask =
                this.postViewService.RemovePostViewByIdAsync(
                    invalidPostViewId);

            // then
            await Assert.ThrowsAsync<PostViewValidationException>(() =>
                removePostViewByIdTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPostViewValidationException))),
                        Times.Once);

            this.postServiceMock.Verify(service =>
                service.RemovePostByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.postServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
