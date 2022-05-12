// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.Groups;

namespace Taarafo.Portal.Web.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string GroupsRelativeUrl = "api/groups";

        public async ValueTask<List<Group>> GetAllGroupsAsync() =>
            await this.GetAsync<List<Group>>(GroupsRelativeUrl);

        public async ValueTask<Group> GetGroupByIdAsync(Guid groupId) =>
            await this.GetAsync<Group>($"{GroupsRelativeUrl}/{groupId}");
    }
}
