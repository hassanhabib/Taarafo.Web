// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Taarafo.Portal.Web.Models.Comments;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Foundations.Comments
{
    public partial class CommentServiceTests
    {
        [Fact]
        public async void ShouldRemoveCommentByIdAsync()
        {
            // given
            Guid randomCommentId = Guid.NewGuid();
            Guid inputCommentId = randomCommentId;
            Comment randomComment = CreateRandomComment();
            Comment deletedComment = randomComment;
            Comment expectedComment = deletedComment.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.DeleteCommentByIdAsync(inputCommentId))
                    .ReturnsAsync(deletedComment);

            // when
            Comment actualComment =
                await this.commentService.RemoveCommentByIdAsync(inputCommentId);

            // then
            actualComment.Should().BeEquivalentTo(expectedComment);

            this.apiBrokerMock.Verify(broker =>
                broker.DeleteCommentByIdAsync(inputCommentId),
                    Times.Once());

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
