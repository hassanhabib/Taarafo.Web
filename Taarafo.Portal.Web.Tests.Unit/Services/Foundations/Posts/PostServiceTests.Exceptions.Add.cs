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
using Taarafo.Portal.Web.Models.Posts.Exceptions;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Foundations.Posts
{
    public partial class PostServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddifCriticalErrorOccursAndLogItAsync(
            Exception criticalDependencyException)
        {
            // given
            Post somePost = CreateRandomPost();

            var failedPostDependencyException =
                new FailedPostDependencyException(criticalDependencyException);

            var expectedPostDependencyException =
                new PostDependencyException(failedPostDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostPostAsync(It.IsAny<Post>()))
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<Post> addPostTask =
                this.postService.AddPostAsync(somePost);

            // then
            await Assert.ThrowsAsync<PostDependencyException>(() =>
               addPostTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.PostPostAsync(It.IsAny<Post>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedPostDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
