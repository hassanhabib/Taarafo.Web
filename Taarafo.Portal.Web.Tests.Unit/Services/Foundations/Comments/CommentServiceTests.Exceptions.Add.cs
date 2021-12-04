// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using Taarafo.Portal.Web.Models.Comments;
using Taarafo.Portal.Web.Models.Comments.Exceptions;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Foundations.Comments
{
    public partial class CommentServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Comment someComment = CreateRandomComment();
            Exception someException = new Exception(GetRandomMessage());

            var failedCommentStorageException =
                new FailedCommentStorageException(someException);

            var expectedCommentDependencyException =
                new CommentDependencyException(failedCommentStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(failedCommentStorageException);

            // when
            ValueTask<Comment> addCommentTask =
                this.commentService.AddCommentAsync(someComment);

            // then
            var actualException = await Assert.ThrowsAsync<CommentDependencyException>(() =>
               addCommentTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostCommentAsync(It.IsAny<Comment>()),
                    Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedCommentDependencyException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfCommentAlreadyExsitsAndLogItAsync()
        {
            // given
            Comment randomComment = CreateRandomComment();
            Comment alreadyExistsComment = randomComment;
            string randomMessage = GetRandomMessage();

            var someException =
                new Exception(GetRandomMessage());

            var alreadyExistsCommentException =
                new AlreadyExistsCommentException(someException);

            var expectedCommentDependencyValidationException =
                new CommentDependencyValidationException(alreadyExistsCommentException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(alreadyExistsCommentException);

            // when
            ValueTask<Comment> addCommentTask =
                this.commentService.AddCommentAsync(alreadyExistsComment);

            // then
            await Assert.ThrowsAsync<CommentDependencyValidationException>(() =>
                addCommentTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostCommentAsync(It.IsAny<Comment>()),
                    Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCommentDependencyValidationException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void ShouldThrowValidationExceptionOnAddIfReferenceErrorOccursAndLogItAsync()
        {
            // given
            Comment someComment = CreateRandomComment();
            string randomMessage = GetRandomMessage();
            string exceptionMessage = randomMessage;

            var someException =
                 new Exception(GetRandomMessage());

            var invalidCommentReferenceException =
                new InvalidCommentReferenceException(someException);

            var expectedCommentValidationException =
                new CommentDependencyValidationException(invalidCommentReferenceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(invalidCommentReferenceException);

            // when
            ValueTask<Comment> addCommentTask =
                this.commentService.AddCommentAsync(someComment);

            // then
            await Assert.ThrowsAsync<CommentDependencyValidationException>(() =>
                addCommentTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCommentValidationException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
