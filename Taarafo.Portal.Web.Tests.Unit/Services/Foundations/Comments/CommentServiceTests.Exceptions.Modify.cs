// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections;
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
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfCriticalErrorOccursAndLogItAsync(
            Exception criticalDependencyException)
        {
            // given
            Comment someComment = CreateRandomComment();

            var failedCommentDependencyException =
                new FailedCommentDependencyException(criticalDependencyException);

            var expectedCommentDependencyException =
                new CommentDependencyException(failedCommentDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.PutCommentAsync(It.IsAny<Comment>()))
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<Comment> modifyCommentTask =
                this.commentService.ModifyCommentAsync(someComment);

            // then
            await Assert.ThrowsAsync<CommentDependencyException>(() =>
               modifyCommentTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.PutCommentAsync(It.IsAny<Comment>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedCommentDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnModifyIfBadRequestExceptionOccursAndLogItAsync()
        {
            // given
            Comment someComment = CreateRandomComment();
            IDictionary randomDictionary = CreateRandomDictionary();
            IDictionary exceptionData = randomDictionary;
            string someMessage = GetRandomMessage();
            var someRepsonseMessage = new HttpResponseMessage();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException(
                    someRepsonseMessage,
                    someMessage);

            httpResponseBadRequestException.AddData(exceptionData);

            var invalidCommentException =
                new InvalidCommentException(
                    httpResponseBadRequestException,
                    exceptionData);

            var expectedCommentDependencyValidationException =
                new CommentDependencyValidationException(invalidCommentException);

            this.apiBrokerMock.Setup(broker =>
                broker.PutCommentAsync(It.IsAny<Comment>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<Comment> modifyCommentTask =
                this.commentService.ModifyCommentAsync(someComment);

            // then
            await Assert.ThrowsAsync<CommentDependencyValidationException>(() =>
                modifyCommentTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.PutCommentAsync(It.IsAny<Comment>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCommentDependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnModifyIfConflictExceptionOccursAndLogItAsync()
        {
            // given
            Comment someComment = CreateRandomComment();
            IDictionary randomDictionary = CreateRandomDictionary();
            IDictionary exceptionData = randomDictionary;
            string someMessage = GetRandomMessage();
            var someRepsonseMessage = new HttpResponseMessage();

            var httpResponseConflictException =
                new HttpResponseConflictException(
                    someRepsonseMessage,
                    someMessage);

            httpResponseConflictException.AddData(exceptionData);

            var invalidCommentException =
                new InvalidCommentException(
                    httpResponseConflictException,
                    exceptionData);

            var expectedCommentDependencyValidationException =
                new CommentDependencyValidationException(invalidCommentException);

            this.apiBrokerMock.Setup(broker =>
                broker.PutCommentAsync(It.IsAny<Comment>()))
                    .ThrowsAsync(httpResponseConflictException);

            // when
            ValueTask<Comment> modifyCommentTask =
                this.commentService.ModifyCommentAsync(someComment);

            // then
            await Assert.ThrowsAsync<CommentDependencyValidationException>(() =>
                modifyCommentTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.PutCommentAsync(It.IsAny<Comment>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCommentDependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
