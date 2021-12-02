// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
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
        public async Task ShouldAddCommentAsync()
        {
            // given
            Comment randomComment = CreateRandomComment();
            Comment inputComment = randomComment;
            Comment postedComment = inputComment;
            Comment expectedComment = postedComment.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.PostCommentAsync(inputComment))
                    .ReturnsAsync(postedComment);

            // when
            Comment actualComment = await this.commentService
                .AddCommentAsync(inputComment);

            // then
            actualComment.Should().BeEquivalentTo(expectedComment);

            this.apiBrokerMock.Verify(broker =>
                broker.PostCommentAsync(inputComment),
                    Times.Once());

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
