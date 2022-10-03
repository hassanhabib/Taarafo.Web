// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using FluentAssertions;
using Taarafo.Portal.Web.Models.Views.Components.PostComponents;
using Taarafo.Portal.Web.Models.Views.Components.PostDialogs;
using Taarafo.Portal.Web.Views.Components.PostComponents;
using Taarafo.Portal.Web.Views.Components.PostDialogs;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Components.PostComponents
{
    internal partial class PostRemoveComponentTests
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
    }
}
