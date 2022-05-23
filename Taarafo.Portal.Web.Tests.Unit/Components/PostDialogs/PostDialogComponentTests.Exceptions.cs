// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Bunit;
using FluentAssertions;
using Moq;
using Taarafo.Portal.Web.Models.PostViews;
using Taarafo.Portal.Web.Models.PostViews.Exceptions;
using Taarafo.Portal.Web.Views.Components.PostDialogs;
using Xeptions;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Components.PostDialogs
{
    public partial class PostDialogComponentTests : TestContext
    {
        [Theory]
        [MemberData(nameof(DependencyValidationExceptions))]
        public async Task ShouldRenderValidationDetailsOnPostAsync(Xeption postViewValidationException)
        {
            // given
            string someContent = GetRandomContent();

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
                .ValidationData.Should().BeEquivalentTo(
                    postViewValidationException.InnerException.Data);

            this.postDialogRenderedComponent.Instance.ContentValidationSummary
                .Color.Should().Be("Red");

            this.postViewServiceMock.Verify(service =>
                service.AddPostViewAsync(
                    It.IsAny<PostView>()),
                        Times.Once);

            this.postViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
