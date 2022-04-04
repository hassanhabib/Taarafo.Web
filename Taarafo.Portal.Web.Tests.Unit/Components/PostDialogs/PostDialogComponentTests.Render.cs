// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Bunit;
using FluentAssertions;
using Moq;
using Taarafo.Portal.Web.Models.PostViews;
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
            initialPostDialog.PostView.Should().BeNull();
        }

        [Fact]
        public void ShouldDisplayDialogIfOpenDialogIsClicked()
        {
            // given
            PostDialogComponentState expectedState =
                PostDialogComponentState.Content;

            var expectedPostView = new PostView();

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
            this.postDialogRenderedComponent.Instance.PostView.Should().BeEquivalentTo(expectedPostView);
        }

        [Fact]
        public async Task ShouldSubmitPostViewAsync()
        {
            // given
            string randomContent = GetRandomContent();
            string inputContent = randomContent;
            string expectedContent = inputContent;

            var expectedPostView = new PostView
            {
                Content = inputContent
            };

            // when
            this.postDialogRenderedComponent =
                RenderComponent<PostDialog>();

            this.postDialogRenderedComponent.Instance
                .OpenDialog();

            await this.postDialogRenderedComponent.Instance.TextArea
                .SetValueAsync(inputContent);

            this.postDialogRenderedComponent.Instance.Dialog
                .Click();

            // then
            this.postDialogRenderedComponent.Instance.Dialog.IsVisible
                .Should().BeFalse();

            this.postDialogRenderedComponent.Instance.PostView
                .Should().BeEquivalentTo(expectedPostView);

            this.postViewServiceMock.Verify(service =>
                service.AddPostViewAsync(
                    this.postDialogRenderedComponent.Instance.PostView),
                        Times.Once);

            this.postViewServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldDisableControlAndDisplayLoadingOnSubmitAsync()
        {
            // given
            string someContent = GetRandomContent();

            var somePostView = new PostView
            {
                Content = someContent
            };

            this.postViewServiceMock.Setup(service =>
                service.AddPostViewAsync(It.IsAny<PostView>()))
                    .ReturnsAsync(
                        value: somePostView,
                        delay: TimeSpan.FromMilliseconds(500));

            // when
            this.postDialogRenderedComponent =
                RenderComponent<PostDialog>();

            this.postDialogRenderedComponent.Instance
                .OpenDialog();

            await this.postDialogRenderedComponent.Instance.TextArea
                .SetValueAsync(someContent);

            this.postDialogRenderedComponent.Instance.Dialog
                .Click();

            // then
            this.postDialogRenderedComponent.Instance.TextArea
                .IsDisabled.Should().BeTrue();

            this.postDialogRenderedComponent.Instance.Dialog.DialogButton
                .Disabled.Should().BeTrue();

            this.postViewServiceMock.Verify(service =>
                service.AddPostViewAsync(
                    It.IsAny<PostView>()),
                        Times.Once);

            this.postViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
