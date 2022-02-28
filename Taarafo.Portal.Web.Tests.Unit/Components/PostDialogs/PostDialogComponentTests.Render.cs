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
            initialPostDialog.IsVisible.Should().BeFalse();
        }
    }
}
