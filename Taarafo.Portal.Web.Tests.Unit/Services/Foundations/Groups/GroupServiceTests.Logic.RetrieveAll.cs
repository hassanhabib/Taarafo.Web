// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Taarafo.Portal.Web.Models.Groups;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Foundations.Groups
{
    public partial class GroupServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllGroupsAsync()
        {
            //given
            List<Group> randomGroups = CreateRandomGroups();
            List<Group> apiGroups = randomGroups;
            List<Group> expectedGroups = apiGroups.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllGroupsAsync())
                    .ReturnsAsync(apiGroups);

            //when
            List<Group> retrievedGroups =
                await groupService.RetrieveAllGroupsAsync();

            //then
            retrievedGroups.Should().BeEquivalentTo(expectedGroups);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllGroupsAsync(),
                    Times.Once());

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
