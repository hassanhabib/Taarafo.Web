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
using Taarafo.Portal.Web.Models.Comments;
using Taarafo.Portal.Web.Models.Comments.Exceptions;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Foundations.Comments
{
    public partial class CommentServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfDependencyApiErrorOccursAndLogItAsync(
            Exception criticalDependencyException)
        {
            // given
            var failedCommentDependencyException =
                new FailedCommentDependencyException(criticalDependencyException);

            var expectedCommentDependencyException =
                new CommentDependencyException(
                    failedCommentDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllCommentsAsync())
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<List<Comment>> retrieveAllCommentsTask =
                this.commentService.RetrieveAllCommentsAsync();

            // then
            await Assert.ThrowsAsync<CommentDependencyException>(() =>
               retrieveAllCommentsTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllCommentsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedCommentDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfDependencyApiErrorOccursAndLogItAsync()
        {
            // given
            var randomExceptionMessage = GetRandomMessage();
            var responseMessage = new HttpResponseMessage();

            var httpResponseException =
                new HttpResponseException(
                    responseMessage,
                    randomExceptionMessage);

            var failedCommentDependencyException =
                new FailedCommentDependencyException(httpResponseException);

            var expectedDependencyException =
                new CommentDependencyException(
                    failedCommentDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllCommentsAsync())
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<List<Comment>> retrieveAllCommentsTask =
                commentService.RetrieveAllCommentsAsync();

            // then
            await Assert.ThrowsAsync<CommentDependencyException>(() =>
                retrieveAllCommentsTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllCommentsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var serviceException = new Exception();

            var failedCommentServiceException =
                new FailedCommentServiceException(serviceException);

            var expectedCommentServiceException =
                new CommentServiceException(failedCommentServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllCommentsAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<List<Comment>> retrieveAllCommentsTask =
                this.commentService.RetrieveAllCommentsAsync();

            // then
            await Assert.ThrowsAsync<CommentServiceException>(() =>
               retrieveAllCommentsTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllCommentsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCommentServiceException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
