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
using Taarafo.Portal.Web.Models.PostViews.Exceptions;
using Taarafo.Portal.Web.Models.Views.Components.PostDialogs;
using Taarafo.Portal.Web.Views.Components.PostDialogs;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Components.PostDialogs
{
    public partial class PostDialogComponentTests : TestContext
    {
        [Fact]
        public async Task ShouldRenderValidationDetailsOnPostAsync()
        {
            // given
            string someContent = GetRandomContent();
            
            string[] randomErrorMessages =
                GetRandomErrorMessages();

            string[] returnedErrorMessages =
                randomErrorMessages;

            string[] expectedErrorMessages =
                returnedErrorMessages;

            var invalidPostViewException =
                new InvalidPostViewException();

            invalidPostViewException.AddData(
                key: nameof(PostView.Content),
                values: randomErrorMessages);

            var postViewValidationException =
                new PostViewValidationException(
                    invalidPostViewException);

            this.postViewServiceMock.Setup(service =>
                service.AddPostViewAsync(It.IsAny<PostView>()))
                    .ThrowsAsync(postViewValidationException);

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
            this.postDialogRenderedComponent.Instance.Dialog.IsVisible
                .Should().BeTrue();

            this.postDialogRenderedComponent.Instance.TextArea
                .IsDisabled.Should().BeFalse();

            this.postDialogRenderedComponent.Instance.Dialog.DialogButton
                .Disabled.Should().BeFalse();

            this.postDialogRenderedComponent.Instance.Spinner.IsVisible
                .Should().BeFalse();
            
            this.postDialogRenderedComponent.Instance.ContentValidationSummary
                .ValidationData.Should().BeEquivalentTo(invalidPostViewException.Data);

            this.postViewServiceMock.Verify(service =>
                service.AddPostViewAsync(
                    It.IsAny<PostView>()),
                        Times.Once);

            this.postViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
