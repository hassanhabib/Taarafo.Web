﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
using Taarafo.Portal.Web.Models.PostImpressions;
using Taarafo.Portal.Web.Models.PostImpressions.Exceptions;
using Taarafo.Portal.Web.Models.Posts;
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

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfBadRequestExceptionOccursAndLogItAsync()
        {
            // given
            PostImpression somePostImpression = CreateRandomPostImpression();
            var randomDictionary = CreateRandomDictionary();
            var exceptionData = randomDictionary;
            string someMessage = GetRandomMessage();
            var someRepsonseMessage = new HttpResponseMessage();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException(
                    someRepsonseMessage,
                    someMessage);

            httpResponseBadRequestException.AddData(exceptionData);

            var invalidPostImpressionException =
                new InvalidPostImpressionException(
                    httpResponseBadRequestException,
                    exceptionData);

            var expectedPostImpressionDependencyValidationException =
                new PostImpressionDependencyValidationException(invalidPostImpressionException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostPostImpressionAsync(It.IsAny<PostImpression>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<PostImpression> addPostImpressionTask =
                this.postImpressionService.AddPostImpressionAsync(somePostImpression);

            // then
            await Assert.ThrowsAsync<PostImpressionDependencyValidationException>(() =>
                addPostImpressionTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.PostPostImpressionAsync(It.IsAny<PostImpression>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPostImpressionDependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}