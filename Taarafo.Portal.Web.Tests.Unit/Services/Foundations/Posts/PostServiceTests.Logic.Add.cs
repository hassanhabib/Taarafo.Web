// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Taarafo.Portal.Web.Models.Posts;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Foundations.Posts
{
    public partial class PostServiceTests
    {
        [Fact]
        public async Task ShouldAddPostAsync()
        {
            // given
            Post randomPost = CreateRandomPost();
            Post inputPost = randomPost;
            Post postedPost = inputPost;
            Post expectedPost = postedPost.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.PostPostAsync(inputPost))
                    .ReturnsAsync(postedPost);

            // when
            Post actualPost =
                await this.postService.AddPostAsync(inputPost);

            // then
            actualPost.Should().BeEquivalentTo(expectedPost);

            this.apiBrokerMock.Verify(broker =>
                broker.PostPostAsync(inputPost),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
