// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using RESTFulSense.Exceptions;
using Taarafo.Portal.Web.Models.Posts;
using Taarafo.Portal.Web.Models.Posts.Exceptions;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Foundations.Posts
{
    public partial class PostServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfCriticalDependencyExceptionOccursAndLogItAsync(
            Exception criticalDependencyException)
        {
            var failedPostDependencyException =
                new FailedPostDependencyException(criticalDependencyException);

            var expectedPostDependencyException =
                new PostDependencyException(
                    failedPostDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllPostsAsync())
                    .ThrowsAsync(criticalDependencyException);

            ValueTask<List<Post>> retrieveAllPostsTask =
                this.postService.RetrieveAllPostsAsync();

            await Assert.ThrowsAsync<PostDependencyException>(() =>
               retrieveAllPostsTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllPostsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedPostDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowPostDependencyExceptionOnRetrieveAllIfDependencyApiErrorOccursAndLogItAsync()
        {
            var randomExceptionMessage = GetRandomMessage();
            var responseMessage = new HttpResponseMessage();

            var httpResponseException =
                new HttpResponseException(
                    httpResponseMessage: responseMessage,
                    message: randomExceptionMessage);

            var failedPostDependencyException =
                new FailedPostDependencyException(httpResponseException);

            var expectedDependencyException =
                new PostDependencyException(
                    failedPostDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllPostsAsync())
                    .ThrowsAsync(httpResponseException);

            ValueTask<List<Post>> retrieveAllPostsTask =
                postService.RetrieveAllPostsAsync();

            await Assert.ThrowsAsync<PostDependencyException>(() =>
                retrieveAllPostsTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllPostsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameValidationExceptionAs(
                        expectedDependencyException))),
                            Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
