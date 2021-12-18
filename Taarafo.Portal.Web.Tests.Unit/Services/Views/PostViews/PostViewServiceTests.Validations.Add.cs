// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Moq;
using Taarafo.Portal.Web.Models.Posts;
using Taarafo.Portal.Web.Models.PostViews;
using Taarafo.Portal.Web.Models.PostViews.Exceptions;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Views.PostViews
{
    public partial class PostViewServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfPostViewIsNullAndLogItAsync()
        {
            // given
            PostView nullPostView = null;
            var nullPostViewException = new NullPostViewException();

            var expectedPostViewValidationException =
                new PostViewValidationException(nullPostViewException);

            // when
            ValueTask<PostView> addPostViewTask =
                this.postViewService.AddPostViewAsync(nullPostView);

            // then
            await Assert.ThrowsAsync<PostViewValidationException>(() =>
               addPostViewTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPostViewValidationException))),
                        Times.Once);

            this.authorServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInAuthor(),
                    Times.Never);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Never);

            this.postServiceMock.Verify(service =>
                service.AddPostAsync(It.IsAny<Post>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.authorServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.postServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task ShouldThrowValidationExceptionOnAddIfPostViewIsInvalidAndLogItAsync(
            string invalidText)
        {
            // given
            PostView invalidPostView = new PostView
            {
                Content = invalidText,
            };

            var invalidPostViewException =
                new InvalidPostViewException();

            invalidPostViewException.AddData(
                key: nameof(Post.Content),
                values: "Text is required");

            var expectedPostViewValidationException =
                new PostViewValidationException(invalidPostViewException);

            // when
            ValueTask<PostView> addPostViewTask =
                this.postViewService.AddPostViewAsync(
                    invalidPostView);

            // then
            await Assert.ThrowsAsync<PostViewValidationException>(() =>
                addPostViewTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPostViewValidationException))),
                        Times.Once);

            this.authorServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInAuthor(),
                    Times.Never);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Never);

            this.postServiceMock.Verify(service =>
                service.AddPostAsync(It.IsAny<Post>()),
                    Times.Never);

            this.postServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.authorServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
