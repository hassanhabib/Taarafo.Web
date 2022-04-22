// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
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
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfDependencyApiErrorOccursAndLogItAsync(
            Exception criticalDependencyException)
        {
            // given
            var failedGroupDependencyException =
                new FailedGroupDependencyException(criticalDependencyException);

            var expectedGroupDependencyException =
                new GroupDependencyException(
                    failedGroupDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllGroupsAsync())
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<List<Group>> retrieveAllGroupsTask =
                this.groupService.RetrieveAllGroupsAsync();

            // then
            await Assert.ThrowsAsync<GroupDependencyException>(() =>
               retrieveAllGroupsTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllGroupsAsync(),
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
