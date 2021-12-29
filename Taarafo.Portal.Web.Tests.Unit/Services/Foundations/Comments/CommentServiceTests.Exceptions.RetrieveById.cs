// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
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
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByIdIfCriticalErrorOccursAndLogItAsync(
            Exception criticalDependencyException)
        {
            // given
            Guid someCommentId = Guid.NewGuid();

            var failedCommentDependencyException =
                new FailedCommentDependencyException(criticalDependencyException);

            var expectedCommentDependencyException =
                new CommentDependencyException(failedCommentDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetCommentByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<Comment> retrieveCommentByIdTask =
                this.commentService.RetrieveCommentByIdAsync(someCommentId);

            // then
            await Assert.ThrowsAsync<CommentDependencyException>(() =>
               retrieveCommentByIdTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetCommentByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedCommentDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveByIdIfCommentIsNotFoundAndLogItAsync()
        {
            // given
            Guid someCommentId = Guid.NewGuid();
            string responseMessage = GetRandomMessage();
            var httpResponseMessage = new HttpResponseMessage();

            var httpResponseNotFoundException =
                new HttpResponseNotFoundException(
                    httpResponseMessage,
                    responseMessage);

            var notFoundCommentException =
                new NotFoundCommentException(httpResponseNotFoundException);

            var expectedCommentDependencyValidationException =
                new CommentDependencyValidationException(notFoundCommentException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetCommentByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(httpResponseNotFoundException);

            // when
            ValueTask<Comment> retrieveCommentByIdTask =
                this.commentService.RetrieveCommentByIdAsync(someCommentId);

            // then
            await Assert.ThrowsAsync<CommentDependencyValidationException>(() =>
                retrieveCommentByIdTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetCommentByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCommentDependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveByIdIfDependencyApiErrorOccursAndLogItAsync()
        {
            // given
            Guid someCommentId = Guid.NewGuid();
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
                broker.GetCommentByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<Comment> retrieveCommentByIdTask =
                this.commentService.RetrieveCommentByIdAsync(someCommentId);

            // then
            await Assert.ThrowsAsync<CommentDependencyException>(() =>
                retrieveCommentByIdTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetCommentByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveByIdIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Comment someComment = CreateRandomComment();
            var serviceException = new Exception();

            var failedCommentServiceException =
                new FailedCommentServiceException(serviceException);

            var expectedCommentServiceException =
                new CommentServiceException(failedCommentServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetCommentByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Comment> retrieveCommentTask =
                this.commentService.RetrieveCommentByIdAsync(someComment.Id);

            // then
            await Assert.ThrowsAsync<CommentServiceException>(() =>
                retrieveCommentTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetCommentByIdAsync(It.IsAny<Guid>()),
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
