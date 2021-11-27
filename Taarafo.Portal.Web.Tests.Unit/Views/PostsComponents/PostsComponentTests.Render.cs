// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using Bunit;
using FluentAssertions;
using Moq;
using Taarafo.Portal.Web.Models.PostViews;
using Taarafo.Portal.Web.Models.Views.Components.PostsComponents;
using Taarafo.Portal.Web.Views.Components.PostsComponents;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Views.PostsComponents
{
    public partial class PostsComponentTests : TestContext
    {
        [Fact]
        public void ShouldInitComponent()
        {
            // given
            PostsComponentState expectedState =
                PostsComponentState.Loading;

            // when
            var initialPostsComponent =
                new PostsComponent();

            // then
            initialPostsComponent.PostViewService.Should().BeNull();
            initialPostsComponent.State.Should().Be(expectedState);
            initialPostsComponent.PostViews.Should().BeNull();
            initialPostsComponent.Grid.Should().BeNull();
            initialPostsComponent.ErrorMessage.Should().BeNull();
            initialPostsComponent.ErrorLabel.Should().BeNull();
        }

        [Fact]
        public void ShouldRenderPosts()
        {
            // given
            PostsComponentState expectedState =
                PostsComponentState.Content;

            List<PostView> randomPostViews =
                CreateRandomPostViews();

            List<PostView> retrievedPostViews =
                randomPostViews;

            List<PostView> expectedPostViews =
                retrievedPostViews;

            this.postViewServiceMock.Setup(service =>
                service.RetrieveAllPostViewsAsync())
                    .ReturnsAsync(retrievedPostViews);

            // when
            this.renderedPostsComponent =
                RenderComponent<PostsComponent>();

            // then
            this.renderedPostsComponent.Instance.State.Should()
                .Be(expectedState);

            this.renderedPostsComponent.Instance.PostViews.Should()
                .BeEquivalentTo(expectedPostViews);

            this.renderedPostsComponent.Instance.Grid.Should()
                .NotBeNull();

            this.renderedPostsComponent.Instance.Grid.DataSource.Should()
                .BeEquivalentTo(expectedPostViews);

            this.renderedPostsComponent.Instance.ErrorMessage.Should()
                .BeNull();

            this.renderedPostsComponent.Instance.ErrorLabel.Should()
                .BeNull();

            this.postViewServiceMock.Verify(service =>
                service.RetrieveAllPostViewsAsync(),
                    Times.Once);

            this.postViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
