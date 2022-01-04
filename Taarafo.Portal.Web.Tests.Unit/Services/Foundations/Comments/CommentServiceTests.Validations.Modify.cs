// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

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
        public async void ShouldThrowValidationExceptionOnModifyWhenCommentIsNullAndLogItAsync()
        {
            // given
            Comment nullComment = null;
            var nullCommentException = new NullCommentException();

            var expectedCommentValidationException =
                new CommentValidationException(nullCommentException);

            // when
            ValueTask<Comment> modifyCommentTask =
                this.commentService.ModifyCommentAsync(nullComment);

            // then
            await Assert.ThrowsAsync<CommentValidationException>(() =>
                modifyCommentTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedCommentValidationException))),
                    Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PutCommentAsync(It.IsAny<Comment>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async void ShouldThrowValidationExceptionOnModifyIfCommentIsInvalidAndLogItAsync(
            string invalidText)
        {
            // given
            var invalidComment = new Comment
            {
                Content = invalidText
            };

            var invalidCommentException = new InvalidCommentException();

            invalidCommentException.AddData(
                key: nameof(Comment.Id),
                values: "Id is required");

            invalidCommentException.AddData(
                key: nameof(Comment.Content),
                values: "Text is required");

            invalidCommentException.AddData(
                key: nameof(Comment.CreatedDate),
                values: "Date is required");

            invalidCommentException.AddData(
                key: nameof(Comment.UpdatedDate),
                values: "Date is required");

            invalidCommentException.AddData(
                key: nameof(Comment.PostId),
                values: "Id is required");

            var expectedCommentValidationException =
                new CommentValidationException(invalidCommentException);

            // when
            ValueTask<Comment> modifyCommentTask =
                this.commentService.ModifyCommentAsync(invalidComment);

            // then
            await Assert.ThrowsAsync<CommentValidationException>(() =>
                modifyCommentTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCommentValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PutCommentAsync(It.IsAny<Comment>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
