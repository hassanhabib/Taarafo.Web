// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using RESTFulSense.Exceptions;
using Taarafo.Portal.Web.Models.Posts;
using Taarafo.Portal.Web.Models.Posts.Exceptions;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Foundations.Posts
{
    public partial class PostServiceTest
    {
        public static TheoryData CriticalDependencyExceptions()
        {
            string someMessage = GetRandomMessage();
            
            var httpResponseMessage = 
                new HttpResponseMessage();

            var httpRequestException =
                new HttpRequestException();

            var httpReponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException(
                    httpResponseMessage,
                    someMessage);

            var unauthorizedHttpResponseException =
                new HttpResponseUnauthorizedException(
                    httpResponseMessage,
                    someMessage);

            return new TheoryData<Exception>
            {
                httpRequestException,
                httpReponseUrlNotFoundException,
                unauthorizedHttpResponseException
            };
        }

        [Theory]
        [MemberData(nameof(CriticalDependencyExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRemoveifCriticalErrorOccursAndLogItAsync(
            Exception criticalDependencyException)
        {
            // given
            Guid somePostId = Guid.NewGuid();

            var failedPostDependencyException =
                new FailedPostDependencyException(criticalDependencyException);

            var expectedPostDependencyException =
                new PostDependencyException(failedPostDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.DeletePostByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<Post> removePostByIdTask =
                this.postService.RemovePostByIdAsync(somePostId);

            // then
            await Assert.ThrowsAsync<PostDependencyException>(() =>
               removePostByIdTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.DeletePostByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedPostDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
