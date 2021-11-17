// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
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
        public async Task ShouldRetrieveAllPostsAsync()
        {
            // given
            List<Post> randomPosts = CreateRandomPosts();
            List<Post> apiPosts = randomPosts;
            List<Post> expectedPosts = apiPosts.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllPostsAsync())
                    .ReturnsAsync(apiPosts);

            // when
            List<Post> retrievedPosts =
                await postService.RetrieveAllPostsAsync();

            // then
            retrievedPosts.Should().BeEquivalentTo(expectedPosts);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllPostsAsync(),
                    Times.Once());

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
