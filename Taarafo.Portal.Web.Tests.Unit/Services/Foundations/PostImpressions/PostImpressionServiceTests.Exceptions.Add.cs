// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Taarafo.Portal.Web.Models.PostImpressions;
using Taarafo.Portal.Web.Models.PostImpressions.Exceptions;
using Taarafo.Portal.Web.Models.Posts.Exceptions;
using Xeptions;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Foundations.PostImpressions
{
    public partial class PostImpressionServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfCriticalErrorOccursAndLogItAsync(
            Exception criticalDependencyException)
        {
            // given
            var somePostImpression = CreateRandomPostImpression();

            var failedPostImpressionDependencyException =
                new FailedPostImpressionDependencyException(criticalDependencyException);

            var expectedPostImpressionDependencyException =
                new PostImpressionDependencyException(failedPostImpressionDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostPostImpressionAsync(It.IsAny<PostImpression>()))
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<PostImpression> addPostImpressionTask =
                this.postImpressionService.AddPostImpressionAsync(somePostImpression);

            // then
            await Assert.ThrowsAsync<PostImpressionDependencyException>(() =>
               addPostImpressionTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.PostPostImpressionAsync(It.IsAny<PostImpression>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedPostImpressionDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
