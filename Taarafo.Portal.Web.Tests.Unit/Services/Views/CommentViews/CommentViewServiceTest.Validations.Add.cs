// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Taarafo.Portal.Web.Models.Comments;
using Taarafo.Portal.Web.Models.CommentViews;
using Taarafo.Portal.Web.Models.CommentViews.Exceptions;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Views.CommentViews
{
    public partial class CommentViewServiceTest
    {
        [Fact]
        public async Task ShouldThrowExceptionOnAddIfCommentIsNullAndLogItAsync()
        {
            // given
            CommentView nullCommentView = null;

            var nullCommentViewException =
                new NullCommentViewException();

            var expectedCommentViewValidationException =
                new CommentViewValidationException(nullCommentViewException);

            // when
            ValueTask<CommentView> addCommentViewTask =
                this.commentViewService.AddCommentViewAsync(nullCommentView);

            // then
            await Assert.ThrowsAsync<CommentViewValidationException>(() =>
                    addCommentViewTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCommentViewValidationException))),
                        Times.Once);

            this.commentServiceMock.Verify(broker =>
                broker.AddCommentAsync(It.IsAny<Comment>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.commentServiceMock.VerifyNoOtherCalls(); 
        }
    }
}
