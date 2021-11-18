// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Taarafo.Portal.Web.Models.Posts;
using Taarafo.Portal.Web.Models.PostViews;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.PostViews
{
    public partial class PostViewServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllPostViewsAsync()
        {
            // given
            DateTimeOffset randomDateTime = GetRandomDateTime();
            var randomAuthorId = Guid.NewGuid();

            dynamic dynamicPostProperties =
                CreateRandomPostProperties(
                    auditDates: randomDateTime,
                    auditIds: randomAuthorId);

            var post = new Post
            {
                Id = dynamicPostProperties.Id,
                Content = dynamicPostProperties.Content,
                CreatedDate = dynamicPostProperties.CreatedDate,
                UpdatedDate = dynamicPostProperties.UpdatedDate,
                Author = dynamicPostProperties.Author
            };

            var postView = new PostView
            {
                Content = dynamicPostProperties.Content,
            };

            var randomPosts = new List<Post> { post };
            var retrievedServicePosts = randomPosts;
            var expectedPostViews = new List<PostView> { postView };

            this.postServiceMock.Setup(service =>
                service.RetrieveAllPostsAsync())
                    .ReturnsAsync(retrievedServicePosts);

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
