// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Taarafo.Portal.Web.Models.Posts;
using Taarafo.Portal.Web.Models.PostViews;
using Taarafo.Portal.Web.Models.PostViews.Exceptions;
using Xeptions;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Views.PostViews
{
    public partial class PostViewServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationExceptions))]
        public async Task ShouldThrowDependencyValidationOnAddIfDependencyValidationErrorOccurrsAndLogItAsync(
            Xeption dependencyValidationException)
        {
            // given
            PostView somePostView = CreateRandomPostView();

            var expectedPostViewDependencyValidationException =
                new PostViewDependencyValidationException(
                    dependencyValidationException.InnerException as Xeption);

            this.postServiceMock.Setup(service =>
                service.AddPostAsync(It.IsAny<Post>()))
                    .ThrowsAsync(dependencyValidationException);

            // when
            ValueTask<PostView> addPostViewTask =
                this.postViewService.AddPostViewAsync(somePostView);

            // then
            await Assert.ThrowsAsync<PostViewDependencyValidationException>(() =>
                addPostViewTask.AsTask());

            this.authorServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInAuthor(),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.postServiceMock.Verify(service =>
                service.AddPostAsync(It.IsAny<Post>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPostViewDependencyValidationException))),
                        Times.Once);

            this.postServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.authorServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
