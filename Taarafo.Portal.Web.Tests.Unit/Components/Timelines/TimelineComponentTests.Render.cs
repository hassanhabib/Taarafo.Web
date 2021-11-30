// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Bunit;
using FluentAssertions;
using Taarafo.Portal.Web.Models.Views.Components.Timelines;
using Taarafo.Portal.Web.Views.Components.Timelines;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Components.Timelines
{
    public partial class TimelineComponentTests : TestContext
    {
        [Fact]
        public void ShouldInitComponent()
        {
            // given
            TimeLineComponentState expectedState =
                TimeLineComponentState.Loading;

            // when
            var initialTimelineComponent =
                new TimelineComponent();

            // then
            initialTimelineComponent.State.Should().Be(expectedState);
            initialTimelineComponent.PostViewService.Should().BeNull();
            initialTimelineComponent.PostViews.Should().BeNull();
            initialTimelineComponent.Label.Should().BeNull();
            initialTimelineComponent.ErrorMessage.Should().BeNull();
        }
    }
}
