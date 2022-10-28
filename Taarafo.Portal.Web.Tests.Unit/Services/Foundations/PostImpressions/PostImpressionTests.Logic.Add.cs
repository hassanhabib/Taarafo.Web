// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Taarafo.Portal.Web.Models.PostImpressions;
using Taarafo.Portal.Web.Models.Posts;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Foundations.PostImpressions
{
    public partial class PostImpressionTests
    {
        [Fact]
        public async Task ShouldAddPostImpressionAsync()
        {
            // given
            PostImpression randomPostImpression = CreateRandomPostImpression();
            PostImpression inputPostImpression = randomPostImpression;
            PostImpression retrievedPostImpression = inputPostImpression;
            PostImpression expectedPostImpression = retrievedPostImpression;

            this.apiBrokerMock.Setup(broker =>
                broker.PostPostImpressionAsync(inputPostImpression))
                    .ReturnsAsync(retrievedPostImpression);

            // when
            PostImpression actualPostImpression =
                await this.postImpressionService.AddPostImpressionAsync(inputPostImpression);

            // then
            actualPostImpression.Should().BeEquivalentTo(expectedPostImpression);

            this.apiBrokerMock.Verify(broker =>
                broker.PostPostImpressionAsync(inputPostImpression),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
