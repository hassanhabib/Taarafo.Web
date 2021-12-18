// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using Taarafo.Portal.Web.Models.Posts;
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
        public async Task ShouldThrowDependencyValidationOnAddIfDependencyValidationErrorOccurrsAndLogItAsync(
            Xeption dependencyValidationException)
        {
            // given
            PostView somePostView = CreateRandomPostView();

            var expectedPostViewDependencyValidationException =
                new PostViewDependencyValidationException(
                    dependencyValidationException.InnerException as Xeption);

            this.postServiceMock.Setup(service =>
                service.AddPostAsync(It.IsAny<Post>()))
                    .ThrowsAsync(dependencyValidationException);

            // when
            ValueTask<PostView> addPostViewTask =
                this.postViewService.AddPostViewAsync(somePostView);

            // then
            await Assert.ThrowsAsync<PostViewDependencyValidationException>(() =>
                addPostViewTask.AsTask());

            this.authorServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInAuthor(),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.postServiceMock.Verify(service =>
                service.AddPostAsync(It.IsAny<Post>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPostViewDependencyValidationException))),
                        Times.Once);

            this.postServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.authorServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnAddIfDependencyErroOccursAndLogItAsync(
            Exception dependencyException)
        {
            // given
            PostView somePostView = CreateRandomPostView();

            var expectedPostViewDependencyException =
                new PostViewDependencyException(dependencyException);

            this.postServiceMock.Setup(service =>
                service.AddPostAsync(It.IsAny<Post>()))
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<PostView> addPostViewTask =
                this.postViewService.AddPostViewAsync(somePostView);

            // then
            await Assert.ThrowsAsync<PostViewDependencyException>(() =>
                addPostViewTask.AsTask());

            this.authorServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInAuthor(),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.postServiceMock.Verify(service =>
                service.AddPostAsync(It.IsAny<Post>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPostViewDependencyException))),
                        Times.Once);

            this.postServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.authorServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            PostView somePostView = CreateRandomPostView();
            var serviceException = new Exception();

            var failedPostViewServiceException =
                new FailedPostViewServiceException(serviceException);

            var expectedPostViewServiceException =
                new PostViewServiceException(failedPostViewServiceException);

            this.postServiceMock.Setup(service =>
                service.AddPostAsync(It.IsAny<Post>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<PostView> addPostViewTask =
                this.postViewService.AddPostViewAsync(somePostView);

            // then
            await Assert.ThrowsAsync<PostViewServiceException>(() =>
                addPostViewTask.AsTask());

            this.authorServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInAuthor(),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.postServiceMock.Verify(service =>
                service.AddPostAsync(It.IsAny<Post>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPostViewServiceException))),
                        Times.Once);

            this.postServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.authorServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
