// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Bunit;
using FluentAssertions;
using Moq;
using Taarafo.Portal.Web.Models.PostViews;
using Taarafo.Portal.Web.Models.Views.Components.PostComponents;
using Taarafo.Portal.Web.Views.Components.PostComponents;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Components.PostComponents
{
    public partial class PostRemoveComponentTests
    {
        [Fact]
        public void ShouldInitializeComponent()
        {
            // given
            PostRemoveComponentState expectedState =
                PostRemoveComponentState.Loading;

            // when
            var initialPostRemove = new PostRemoveComponent();

            // then
            initialPostRemove.State.Should().Be(expectedState);
            initialPostRemove.PostViewService.Should().BeNull();
            initialPostRemove.PostView.Should().BeNull();
            initialPostRemove.Button.Should().BeNull();
        }

        [Fact]
        public void ShouldRenderComponent()
        {
            // given
            PostRemoveComponentState expectedState =
                PostRemoveComponentState.Content;

            string expectedLabel = "🗑";

            PostView randomPostView = CreateRandomPostView();
            PostView inputPostView = randomPostView;
            PostView expectedPostView = inputPostView;

            ComponentParameter inputComponentParameter =
                ComponentParameter.CreateParameter(
                    name: nameof(PostView),
                    value: inputPostView);

            // when
            this.postRemoveRenderedComponent =
                RenderComponent<PostRemoveComponent>(inputComponentParameter);

            // then
            this.postRemoveRenderedComponent.Instance.State.Should().Be(expectedState);
            this.postRemoveRenderedComponent.Instance.PostViewService.Should().NotBeNull();
            this.postRemoveRenderedComponent.Instance.PostView.Should().BeEquivalentTo(expectedPostView);
            this.postRemoveRenderedComponent.Instance.Button.Label.Should().Be(expectedLabel);
            this.postRemoveRenderedComponent.Instance.Button.IsDisabled.Should().BeFalse();
            this.postRemoveRenderedComponent.Instance.Button.OnClick.Should().NotBeNull();
        }

        [Fact]
        public void ShouldRemovePostAsync()
        {
            // given
            PostView randomPostView = CreateRandomPostView();
            PostView inputPostView = randomPostView;
            PostView expectedPostView = inputPostView;

            ComponentParameter inputComponentParameter =
                ComponentParameter.CreateParameter(
                    name: nameof(PostView),
                    value: inputPostView);

            // when
            this.postRemoveRenderedComponent =
                RenderComponent<PostRemoveComponent>(inputComponentParameter);

            this.postRemoveRenderedComponent.Instance.Button
                .Click();

            // then
            this.postRemoveRenderedComponent.Instance.Button.IsDisabled
                .Should().BeTrue();

            this.postRemoveRenderedComponent.Instance.PostView
                .Should().BeEquivalentTo(expectedPostView);

            this.postViewServiceMock.Verify(service =>
                service.RemovePostViewByIdAsync(
                    this.postRemoveRenderedComponent.Instance.PostView.Id),
                        Times.Once);

            this.postViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
