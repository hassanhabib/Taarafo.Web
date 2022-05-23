// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using Taarafo.Portal.Web.Models.Groups;
using Taarafo.Portal.Web.Models.Groups.Exceptions;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Foundations.Groups
{
    public partial class GroupServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyExceptions))]

        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByIdIfDependencyApiErrorOccursAndLogItAsync(
            Exception criticalDependencyException)
        {
            //given 
            Guid someGroupId = Guid.NewGuid();

            var failedGroupDependencyException =
                new FailedGroupDependencyException(criticalDependencyException);

            var expectedGroupDependencyException =
                new GroupDependencyException(
                    failedGroupDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetGroupByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(criticalDependencyException);

            //when
            ValueTask<Group> retrieveGroupByIdTask =
                this.groupService.RetrieveGroupByIdAsync(someGroupId);

            //then
            await Assert.ThrowsAsync<GroupDependencyException>(() =>
                retrieveGroupByIdTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetGroupByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedGroupDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
