// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Taarafo.Portal.Web.Models.Comments;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Foundations.Comments
{
    public partial class CommentServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllCommentsAsync()
        {
            // given
            List<Comment> randomComments = CreateRandomComments();
            List<Comment> apiComments = randomComments;
            List<Comment> expectedComments = apiComments.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllCommentsAsync())
                    .ReturnsAsync(apiComments);

            // when
            List<Comment> retrievedComments =
                await commentService.RetrieveAllCommentsAsync();

            // then
            retrievedComments.Should().BeEquivalentTo(expectedComments);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllCommentsAsync(),
                    Times.Once());

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
