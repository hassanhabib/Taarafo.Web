// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Brokers.Apis;
using Taarafo.Portal.Web.Brokers.Loggings;
using Taarafo.Portal.Web.Models.Groups;

namespace Taarafo.Portal.Web.Services.Foundations.Groups
{
    public partial class GroupService : IGroupService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public GroupService(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<List<Group>> RetrieveAllGroupsAsync() =>
        TryCatch(async () => await this.apiBroker.GetAllGroupsAsync());

        public ValueTask<Group> RetrieveGroupByIdAsync(Guid groupId) =>
        TryCatch(async () =>
        {
            ValidateGroupId(groupId);

            Group maybeGroup = await this.apiBroker.GetGroupByIdAsync(groupId);

            ValidateApiGroup(maybeGroup, groupId);

            return maybeGroup;
        });
    }
}
