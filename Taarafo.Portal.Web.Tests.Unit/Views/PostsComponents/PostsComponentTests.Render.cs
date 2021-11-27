// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Bunit;
using FluentAssertions;
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
    }
}
