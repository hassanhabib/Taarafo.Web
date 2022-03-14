// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Bunit;
using FluentAssertions;
using Taarafo.Portal.Web.Models.Views.Components.PostDialogs;
using Taarafo.Portal.Web.Views.Components.PostDialogs;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Components.PostDialogs
{
    public partial class PostDialogComponentTests : TestContext
    {
        [Fact]
        public void ShouldInitializeComponent()
        {
            // given
            PostDialogComponentState expectedState =
                PostDialogComponentState.Loading;

            // when
            var initialPostDialog = new PostDialog();

            // then
            initialPostDialog.State.Should().Be(expectedState);
            initialPostDialog.PostViewService.Should().BeNull();
            initialPostDialog.Dialog.Should().BeNull();
            initialPostDialog.TextArea.Should().BeNull();
            initialPostDialog.IsVisible.Should().BeFalse();
        }

        [Fact]
        public void ShouldDisplayDialogIfOpenDialogIsClicked()
        {
            // given
            PostDialogComponentState expectedState =
                PostDialogComponentState.Content;

            string expectedInputHeight = "250px";

            // when
            this.postDialogRenderedComponent = RenderComponent<PostDialog>();
            this.postDialogRenderedComponent.Instance.OpenDialog();

            // then
            this.postDialogRenderedComponent.Instance.State.Should().Be(expectedState);
            this.postDialogRenderedComponent.Instance.PostViewService.Should().NotBeNull();
            this.postDialogRenderedComponent.Instance.Dialog.Should().NotBeNull();
            this.postDialogRenderedComponent.Instance.Dialog.IsVisible.Should().BeTrue();
            this.postDialogRenderedComponent.Instance.Dialog.ButtonTitle.Should().Be("POST");
            this.postDialogRenderedComponent.Instance.Dialog.Title.Should().Be("NEW POST");
            this.postDialogRenderedComponent.Instance.TextArea.Should().NotBeNull();
            this.postDialogRenderedComponent.Instance.TextArea.Height.Should().Be(expectedInputHeight);
            this.postDialogRenderedComponent.Instance.IsVisible.Should().BeTrue();
        }
    }
}
