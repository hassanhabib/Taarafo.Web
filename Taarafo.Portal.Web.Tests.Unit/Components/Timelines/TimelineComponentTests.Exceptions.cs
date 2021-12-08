// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bunit;
using FluentAssertions;
using Moq;
using Taarafo.Portal.Web.Models.PostViews;
using Taarafo.Portal.Web.Models.Views.Components.Timelines;
using Taarafo.Portal.Web.Views.Bases;
using Taarafo.Portal.Web.Views.Components.Timelines;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Components.Timelines
{
    public partial class TimelineComponentTests
    {
        [Fact]
        public void ShouldRenderErrorIfExceptionOccurs()
        {
            // given
            TimeLineComponentState expectedState =
                TimeLineComponentState.Error;

            string randomMessage = GetRandomString();
            string exceptionMessage = randomMessage;
            string expectedErrorMessage = exceptionMessage;
            
            var exception =
                new Exception(message: exceptionMessage);

            this.postViewServiceMock.Setup(service =>
                service.RetrieveAllPostViewsAsync())
                    .ThrowsAsync(exception);

            // when
            this.renderedTimelineComponent =
                RenderComponent<TimelineComponent>();

            // then
            this.renderedTimelineComponent.Instance.State
                .Should().Be(expectedState);

            this.renderedTimelineComponent.Instance.ErrorMessage
                .Should().Be(expectedErrorMessage);
            
            this.renderedTimelineComponent.Instance.Label.Value
                .Should().Be(expectedErrorMessage);

            IReadOnlyList<IRenderedComponent<CardBase>> postComponents =
                this.renderedTimelineComponent.FindComponents<CardBase>();

            postComponents.Should().HaveCount(0);

            this.renderedTimelineComponent.Instance.Spinner
                .Should().BeNull();

            this.postViewServiceMock.Verify(service =>
                service.RetrieveAllPostViewsAsync(),
                    Times.Once);

            this.postViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
