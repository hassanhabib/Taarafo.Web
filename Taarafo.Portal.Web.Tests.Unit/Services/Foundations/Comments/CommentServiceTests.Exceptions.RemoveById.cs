﻿// ---------------------------------------------------------------
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
        public async Task ShouldThrowCriticalDependencyExceptionOnRemoveifCriticalErrorOccursAndLogItAsync(
            Exception criticalDependencyException)
        {
            // given
            Guid someCommentId = Guid.NewGuid();

            var failedCommentDependencyException =
                new FailedCommentDependencyException(criticalDependencyException);

            var expectedCommentDependencyException =
                new CommentDependencyException(failedCommentDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.DeleteCommentByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<Comment> removeCommentByIdTask =
                this.commentService.RemoveCommentByIdAsync(someCommentId);

            // then
            await Assert.ThrowsAsync<CommentDependencyException>(() =>
               removeCommentByIdTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.DeleteCommentByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedCommentDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveIfCommentIsNotFoundAndLogItAsync()
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
                broker.DeleteCommentByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(httpResponseNotFoundException);

            // when
            ValueTask<Comment> removeCommentByIdTask =
                this.commentService.RemoveCommentByIdAsync(someCommentId);

            // then
            await Assert.ThrowsAsync<CommentDependencyValidationException>(() =>
                removeCommentByIdTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.DeleteCommentByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCommentDependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveIfValidationErrorOccursAndLogItAsync()
        {
            // given
            Guid someCommentId = Guid.NewGuid();
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
                broker.DeleteCommentByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<Comment> removeCommentByIdTask =
                this.commentService.RemoveCommentByIdAsync(someCommentId);

            // then
            await Assert.ThrowsAsync<CommentDependencyValidationException>(() =>
                removeCommentByIdTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.DeleteCommentByIdAsync(It.IsAny<Guid>()),
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
