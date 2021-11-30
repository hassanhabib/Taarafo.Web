// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
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
        public async Task ShouldRemovePostViewByIdAsync()
        {
            // given
            Guid randomPostViewId = Guid.NewGuid();
            Guid inputPostViewId = randomPostViewId;

            dynamic postViewProperties =
                CreateRandomPostViewProperties();

            var randomPost = new Post
            {
                Id = postViewProperties.Id,
                Author = postViewProperties.Author,
                Content = postViewProperties.Content,
                CreatedDate = postViewProperties.CreatedDate,
                UpdatedDate = postViewProperties.UpdatedDate
            };

            Post removedPost = randomPost;

            var randomPostView = new PostView
            {
                Id = postViewProperties.Id,
                Author = postViewProperties.Author,
                Content = postViewProperties.Content,
                CreatedDate = postViewProperties.CreatedDate,
                UpdatedDate = postViewProperties.UpdatedDate
            };

            PostView expectedPostView = randomPostView;

            this.postServiceMock.Setup(service =>
                service.RemovePostByIdAsync(inputPostViewId))
                    .ReturnsAsync(removedPost);

            // when
            PostView actualPostView =
                await this.postViewService.RemovePostViewByIdAsync(
                    inputPostViewId);

            // then
            actualPostView.Should().BeEquivalentTo(expectedPostView);

            this.postServiceMock.Verify(service =>
                service.RemovePostByIdAsync(inputPostViewId),
                    Times.Once());

            this.postServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
