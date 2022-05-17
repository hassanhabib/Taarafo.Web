// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Taarafo.Portal.Web.Models.Groups;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Foundations.Groups
{
    public partial class GroupServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveGroupByIdAsync()
        {
            //given
            Group randomGroup = CreateRandomGroup();
            Group inputGroup = randomGroup;
            Group retrievedGroup = inputGroup;
            Group expectedGroup = retrievedGroup;

            this.apiBrokerMock.Setup(broker =>
                broker.GetGroupByIdAsync(inputGroup.Id))
                    .ReturnsAsync(retrievedGroup);

            //when
            Group actualGroup =
                await this.groupService.RetrieveGroupByIdAsync(inputGroup.Id);

            //then
            actualGroup.Should().BeEquivalentTo(expectedGroup);

            this.apiBrokerMock.Verify(broker =>
                broker.GetGroupByIdAsync(inputGroup.Id),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
