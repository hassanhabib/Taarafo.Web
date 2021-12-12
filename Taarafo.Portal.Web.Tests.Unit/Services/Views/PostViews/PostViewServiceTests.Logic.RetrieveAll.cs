// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Taarafo.Portal.Web.Models.Posts;
using Taarafo.Portal.Web.Models.PostViews;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Views.PostViews
{
    public partial class PostViewServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllPostViewsAsync()
        {
            // given
            List<dynamic> dynamicPostViewPropertiesCollection =
                CreateRandomPostViewCollections();

            List<Post> randomPosts =
                dynamicPostViewPropertiesCollection.Select(property =>
                    new Post
                    {
                        Id = property.Id,
                        Content = property.Content,
                        CreatedDate = property.CreatedDate,
                        UpdatedDate = property.UpdatedDate,
                        Author = property.Author
                    }).ToList();

            List<Post> retrievedPosts = randomPosts;

            List<PostView> randomPostViews =
                dynamicPostViewPropertiesCollection.Select(property =>
                    new PostView
                    {
                        Id = property.Id,
                        Content = property.Content,
                        CreatedDate = property.CreatedDate,
                        UpdatedDate = property.UpdatedDate,
                        Author = property.Author
                    }).ToList();

            List<PostView> expectedPostViews = randomPostViews;

            this.postServiceMock.Setup(service =>
                service.RetrieveAllPostsAsync())
                    .ReturnsAsync(retrievedPosts);

            // when
            List<PostView> retrievedPostViews =
                await this.postViewService.RetrieveAllPostViewsAsync();

            // then
            retrievedPostViews.Should().BeEquivalentTo(expectedPostViews);

            this.postServiceMock.Verify(service =>
                service.RetrieveAllPostsAsync(),
                    Times.Once());

            this.postServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
