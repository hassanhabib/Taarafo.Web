// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Taarafo.Portal.Web.Models.PostViews;
using Taarafo.Portal.Web.Models.PostViews.Exceptions;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.PostViews
{
    public partial class PostViewServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionIfDependecyErrorOccursAndLogItAsync(
            Exception dependencyException)
        {
            // given
            var expectedPostViewDependencyException =
                new PostViewDependencyException(dependencyException);

            this.postServiceMock.Setup(service =>
                service.RetrieveAllPostsAsync())
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<List<PostView>> retrieveAllPostViewsTask =
                this.postViewService.RetrieveAllPostViewsAsync();

            // then
            await Assert.ThrowsAsync<PostViewDependencyException>(() =>
                retrieveAllPostViewsTask.AsTask());

            this.postServiceMock.Verify(service =>
                service.RetrieveAllPostsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPostViewDependencyException))),
                        Times.Once);

            this.postServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionWhenServiceErrorOccursAndLogItAsync()
        {
            // given
            var serviceException = new Exception();

            var failedPostViewServiceException =
                new FailedPostViewServiceException(serviceException);

            var expectedPostViewServiceException =
                new PostViewServiceException(failedPostViewServiceException);

            this.postServiceMock.Setup(service =>
                service.RetrieveAllPostsAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<List<PostView>> retrieveAllPostViewsTask =
                this.postViewService.RetrieveAllPostViewsAsync();

            // then
            await Assert.ThrowsAsync<PostViewServiceException>(() =>
                retrieveAllPostViewsTask.AsTask());

            this.postServiceMock.Verify(service =>
                service.RetrieveAllPostsAsync(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPostViewServiceException))),
                        Times.Once());

            this.postServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
