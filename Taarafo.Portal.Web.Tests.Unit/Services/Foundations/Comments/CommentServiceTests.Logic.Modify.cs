// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Taarafo.Portal.Web.Models.Comments;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Foundations.Comments
{
    public partial class CommentServiceTests
    {
        [Fact]
        public async Task ShouldModifyCommentAsync()
        {
            // given
            Comment randomComment = CreateRandomComment();
            Comment inputComment = randomComment;
            Comment retrievedComment = inputComment;
            Comment expectedComment = retrievedComment;

            this.apiBrokerMock.Setup(broker =>
                broker.PutCommentAsync(inputComment))
                    .ReturnsAsync(retrievedComment);

            // when
            Comment actualComment =
                await this.commentService.ModifyCommentAsync(inputComment);

            // then
            actualComment.Should().BeEquivalentTo(expectedComment);

            this.apiBrokerMock.Verify(broker =>
                broker.PutCommentAsync(inputComment),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
