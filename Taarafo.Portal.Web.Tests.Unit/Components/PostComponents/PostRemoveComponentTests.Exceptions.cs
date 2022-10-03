// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
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
        public void ShouldRenderErrorIfExceptionOccurs()
        {
            // given
            PostRemoveComponentState expectedState =
                PostRemoveComponentState.Error;

            PostView somePostView = CreateRandomPostView();

            string randomMessage = GetRandomString();
            string exceptionMessage = randomMessage;
            string expectedErrorMessage = exceptionMessage;
            string expectedImageUrl = "imgs/error.png";

            var exception =
                new Exception(message: exceptionMessage);

            this.postViewServiceMock.Setup(service =>
                service.RemovePostViewByIdAsync(somePostView.Id))
                    .ThrowsAsync(exception);

            ComponentParameter inputComponentParameter =
                ComponentParameter.CreateParameter(
                    name: nameof(PostView),
                    value: somePostView);

            // when
            this.postRemoveRenderedComponent =
                RenderComponent<PostRemoveComponent>(inputComponentParameter);

            this.postRemoveRenderedComponent.Instance.Button.Click();

            this.postRemoveRenderedComponent.Render();

            // then
            this.postRemoveRenderedComponent.Instance.State
                .Should().Be(expectedState);

            this.postRemoveRenderedComponent.Instance.ErrorMessage
                .Should().Be(expectedErrorMessage);

            this.postRemoveRenderedComponent.Instance.ErrorImage.Url
                .Should().Be(expectedImageUrl);

            this.postViewServiceMock.Verify(service =>
                service.RemovePostViewByIdAsync(somePostView.Id),
                    Times.Once);

            this.postViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
