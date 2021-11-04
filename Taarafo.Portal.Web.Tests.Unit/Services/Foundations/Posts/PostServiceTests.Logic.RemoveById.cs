using System;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Taarafo.Portal.Web.Models.Posts;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Foundations.Posts
{
    public partial class PostServiceTest
    {
        [Fact]
        public async void ShouldRemovePostById()
        {
            // given
            Guid randomPostId = Guid.NewGuid();
            Guid inputPostId = randomPostId;
            Post randomPost = CreateRandomPost();
            Post deletedPost = randomPost;
            Post expectedPost = deletedPost.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.DeletePostByIdAsync(inputPostId))
                    .ReturnsAsync(deletedPost);

            // when
            Post actualPost =
                await this.postService.RemovePostByIdAsync(inputPostId);

            // then
            actualPost.Should().BeEquivalentTo(expectedPost);

            this.apiBrokerMock.Verify(broker =>
                broker.DeletePostByIdAsync(inputPostId),
                    Times.Once());

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
