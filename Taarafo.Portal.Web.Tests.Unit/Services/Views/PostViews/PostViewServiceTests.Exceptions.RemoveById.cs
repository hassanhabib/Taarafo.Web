// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using Taarafo.Portal.Web.Models.PostViews;
using Taarafo.Portal.Web.Models.PostViews.Exceptions;
using Xeptions;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Views.PostViews
{
    public partial class PostViewServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationExceptions))]
        public async Task ShouldThrowDependencyValidationOnRemoveIfDependencyValidationErrorOccurrsAndLogItAsync(
            Xeption dependencyValidationException)
        {
            // given
            Guid somePostViewId = Guid.NewGuid();

            var expectedPostViewDependencyValidationException =
                new PostViewDependencyValidationException(
                    dependencyValidationException.InnerException as Xeption);

            this.postServiceMock.Setup(service =>
                service.RemovePostByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(dependencyValidationException);

            // when
            ValueTask<PostView> removePostViewByIdTask =
                this.postViewService.RemovePostViewByIdAsync(somePostViewId);

            // then
            await Assert.ThrowsAsync<PostViewDependencyValidationException>(() =>
                removePostViewByIdTask.AsTask());

            this.postServiceMock.Verify(service =>
                service.RemovePostByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPostViewDependencyValidationException))),
                        Times.Once);

            this.postServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRemoveByIdIfDependencyErroOccursAndLogItAsync(
            Exception dependencyException)
        {
            // given
            Guid somePostViewId = Guid.NewGuid();

            var expectedPostViewDependencyException =
                new PostViewDependencyException(dependencyException);

            this.postServiceMock.Setup(service =>
                service.RemovePostByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<PostView> removePostViewByIdTask =
                this.postViewService.RemovePostViewByIdAsync(somePostViewId);

            // then
            await Assert.ThrowsAsync<PostViewDependencyException>(() =>
                removePostViewByIdTask.AsTask());

            this.postServiceMock.Verify(service =>
                service.RemovePostByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPostViewDependencyException))),
                        Times.Once);

            this.postServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemovePostViewByIdIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Guid somePostViewId = Guid.NewGuid();
            var serviceException = new Exception();

            var failedPostViewServiceException =
                new FailedPostViewServiceException(serviceException);

            var expectedPostViewServiceException =
                new PostViewServiceException(failedPostViewServiceException);

            this.postServiceMock.Setup(service =>
                service.RemovePostByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<PostView> removePostViewByIdTask =
                this.postViewService.RemovePostViewByIdAsync(somePostViewId);

            // then
            await Assert.ThrowsAsync<PostViewServiceException>(() =>
                removePostViewByIdTask.AsTask());

            this.postServiceMock.Verify(service =>
                service.RemovePostByIdAsync(It.IsAny<Guid>()),
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
