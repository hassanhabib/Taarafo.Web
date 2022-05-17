// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using Taarafo.Portal.Web.Models.Groups;
using Taarafo.Portal.Web.Models.Groups.Exceptions;
using Xeptions;

namespace Taarafo.Portal.Web.Services.Foundations.Groups
{
    public partial class GroupService
    {
        private delegate ValueTask<List<Group>> ReturningGroupsFunction();
        private delegate ValueTask<Group> ReturningGroupFunction();

        private async ValueTask<List<Group>> TryCatch(ReturningGroupsFunction returningGroupsFunction)
        {
            try
            {
                return await returningGroupsFunction();
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedGroupDependencyException =
                    new FailedGroupDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedGroupDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedGroupDependencyException =
                    new FailedGroupDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedGroupDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedGroupDependencyException =
                    new FailedGroupDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedGroupDependencyException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedGroupDependencyException =
                    new FailedGroupDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedGroupDependencyException);
            }
            catch (Exception exception)
            {
                var failedGroupServiceException =
                    new FailedGroupServiceException(exception);

                throw CreateAndLogServiceException(failedGroupServiceException);
            }
        }

        private async ValueTask<Group> TryCatch(ReturningGroupFunction returningGroupFunction)
        {
            try
            {
                return await returningGroupFunction();
            }
            catch (InvalidGroupException invalidGroupException)
            {
                throw CreateAndLogValidationException(invalidGroupException);
            }
        }

        private GroupValidationException CreateAndLogValidationException(Xeption exception)
        {
            var groupValidationException =
                new GroupValidationException(exception);

            this.loggingBroker.LogError(groupValidationException);

            return groupValidationException;
        }

        private GroupDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var groupDependencyException =
                new GroupDependencyException(exception);

            this.loggingBroker.LogCritical(groupDependencyException);

            return groupDependencyException;
        }

        private GroupDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var groupDependencyException =
                new GroupDependencyException(exception);

            this.loggingBroker.LogError(groupDependencyException);

            return groupDependencyException;
        }

        private GroupServiceException CreateAndLogServiceException(Xeption exception)
        {
            var groupServiceException =
                new GroupServiceException(exception);

            this.loggingBroker.LogError(groupServiceException);

            return groupServiceException;
        }
    }
}
